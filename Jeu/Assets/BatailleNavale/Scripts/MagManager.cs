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
    private ShipManager SM;
    private VisualManager VM;
    private Camera cam;

    // Start is called before the first frame update
    void start()
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
    }

    public MagManager(string nom, Vector3 posvm, ShipManager SMx, Camera camx)
    {
        MagHolder = new GameObject(nom);
        pos = new Vector3(posvm.x, posvm.y, posvm.z);
        this.SM = SMx;
        this.cam = camx;
        Magasinpos = 1;//ouvre le magasin au début de la scène
        Cvs = new CanvasGenerator("CanvasMagasin", new Vector3(pos.x, pos.y, pos.z), new Vector2(1, 1), RenderMode.WorldSpace, cam, 10, "PanelLayer", MagHolder);
        Cvs.addPanel("PanelBateaux", new Vector3(pos.x + 12f, pos.y + 5f, pos.z + 0), new Vector2(7.25f, 12), new Color32(255, 255, 255, 160));
        Cvs.addPanel("PanelBouton" + pos.x, new Vector3(pos.x + 8.875f, pos.y + 5, pos.z + 0), new Vector2(1, 12), new Color32(22, 25, 77, 175));
        Cvs.addText(Cvs.getPanel(1), "MagasinText", Cvs.getPanel(1).transform.position, new Vector2(12, 2), 1, "Magasin (Fermer)", Color.black, TextAnchor.MiddleCenter);
        Cvs.rotateText(0);
        Cvs.addPanel("Bouttonplacement", new Vector3(pos.x - 3, pos.y + 10.1f, pos.z), new Vector2(4, 1), new Color32(192, 72, 73, 255));
        Cvs.addText(Cvs.getPanel(2), "Bouttontext", Cvs.getPanel(2).transform.position, new Vector2(5, 2), 1, "Confirmer", Color.black, TextAnchor.MiddleCenter);
    }

    public void MoveMagasin()//fonction qui répond à l'action du bouton du panel2
    {
        Debug.Log("MAGASIN");
        if (Magasinpos == 0)//si les panneaux sont rangés, déplace les deux panneaux pour l'ouverture
        {
            this.setOuvrir();
            SM.moveShip(-4.5f, 0, 0);
        }
        else
        {
            if (Magasinpos == 1)//si les panneaux sont ouverts, déplace les deux panneaux pour la fermeture
            {
                this.setFermer();
                SM.moveShip(4.5f, 0, 0);
            }
        }
    }

    public void setFermer()
    {
        Cvs.MovePanel(0, 6, 0, 0);
        Cvs.MovePanel(1, 6, 0, 0);
        Cvs.setText(0, "Magasin (Ouvrir)");
        Magasinpos = 0;
        SM.moveShip(4.5f, 0, 0);
    }

    public void setOuvrir()
    {
        Cvs.MovePanel(0, -6, 0, 0);
        Cvs.MovePanel(1, -6, 0, 0);
        Cvs.setText(0, "Magasin (Fermer)");
        Magasinpos = 1;
        SM.moveShip(-4.5f, 0, 0);
    }

    public int getMagasinpos()
    {
        return Magasinpos;
    }

    public void FinPlacement()
    {
        if (cam.enabled == false)
        {
            return;
        }

        int count = 0;
        for (int i = 0; i < 5; i++)
        {
            if (SM.getShip(i).GetComponent<Draggable>().getMag() == false)
            {
                count++;
            }
        }
        if (count == 5)
        {
            for (int i = 0; i < 5; i++)
            {
                SM.getShip(i).GetComponent<BoxCollider>().enabled = false;
            }
            VM.switchCam();
            Cvs.disable();
        }
    }

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A)) && (cam.enabled == true))
        {
            {
                FinPlacement();
            }
        }
    }
}