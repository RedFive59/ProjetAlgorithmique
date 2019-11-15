using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magasin : MonoBehaviour
{
    private GenVisualManager GVM;
    private CanvasGenerator Cvs;
    private int Magasinpos;//var qui dit si le magasin est ouvert ou fermé
    private int pos;
    private GenShips Ships;

    // Start is called before the first frame update
    void Start()
    {
        GVM = GameObject.FindObjectOfType<GenVisualManager>();
        pos = GVM.getposGVM();
        Ships = GameObject.FindObjectOfType<GenShips>();
        Magasinpos = 1;//ouvre le magasin au début de la scène
        Cvs = new CanvasGenerator("CanvasMagasin", new Vector3(pos, pos, 0), new Vector2(1, 1), RenderMode.WorldSpace,(Camera)GameObject.FindObjectOfType<Camera>(), 10, "PanelLayer", gameObject);
        Cvs.addPanel("PanelBateaux", new Vector3(pos + 12f, pos + 5f, 0), new Vector2(7.25f, 12), new Color32(255, 255, 255, 160));
        Cvs.addPanel("PanelBouton", new Vector3(pos + 8.875f, pos + 5, 0), new Vector2(1, 12), new Color32(22, 25, 77, 175));
        Cvs.addText(Cvs.getPanel(1), "MagasinText", Cvs.getPanel(1).transform.position, new Vector2(12, 2), 1, "Magasin (Fermer)", Color.black, TextAnchor.MiddleCenter);
        Cvs.rotateText(0);
        Cvs.getPanel(1).AddComponent<Button>().onClick.AddListener(MoveMagasin);
    }

       public void MoveMagasin()//fonction qui répond à l'action du bouton du panel2
        {
            if (Magasinpos == 0)//si les panneaux sont rangés, déplace les deux panneaux pour l'ouverture
            {
                this.setOuvrir();
                Ships.moveShip(-4.5f, 0, 0);
            }
            else
            {
                if (Magasinpos == 1)//si les panneaux sont ouverts, déplace les deux panneaux pour la fermeture
                {
                this.setFermer();
                    Ships.moveShip(4.5f, 0, 0);
                }
            }
        }

    public void setFermer()
    {
        Cvs.MovePanel(0, 6, 0, 0);
        Cvs.MovePanel(1, 6, 0, 0);
        Cvs.setText("Magasin (Ouvrir)");
        Magasinpos = 0;
        Ships.moveShip(4.5f, 0, 0);
    }

    public void setOuvrir()
    {
        Cvs.MovePanel(0, -6, 0, 0);
        Cvs.MovePanel(1, -6, 0, 0);
        Cvs.setText("Magasin (Fermer)");
        Magasinpos = 1;
        Ships.moveShip(-4.5f, 0, 0);
    }

public int getMagasinpos()
    {
        return Magasinpos;
    }
}
