using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagManager
{
    private GameObject MagHolder;
    private CanvasGenerator Cvs;
    private int Magasinpos;//var qui dit si le magasin est ouvert ou fermé
    private Vector3 pos;
    private ShipManager Ships;

    // Start is called before the first frame update
    public MagManager(string nom,Vector3 posvm,ShipManager SM,Camera cam)
    {
        MagHolder = new GameObject(nom);
        pos = new Vector3(posvm.x,posvm.y,posvm.z);
        Ships = SM;
        Magasinpos = 1;//ouvre le magasin au début de la scène
        Cvs = new CanvasGenerator("CanvasMagasin", new Vector3(pos.x, pos.y, pos.z), new Vector2(1, 1), RenderMode.WorldSpace,cam, 10, "PanelLayer", MagHolder);
        Cvs.addPanel("PanelBateaux", new Vector3(pos.x + 12f, pos.y + 5f, 0), new Vector2(7.25f, 12), new Color32(255, 255, 255, 160));
        Cvs.addPanel("PanelBouton", new Vector3(pos.x + 8.875f, pos.y + 5, 0), new Vector2(1, 12), new Color32(22, 25, 77, 175));
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
