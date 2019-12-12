using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship //Classe représentant un bateau
{
    private GameObject Sp;//Ship Manager Maitre
    private string namex;//Nom du bateau (avec un l'int de sa position à la fin)
    private string[] namey;//Nom du bateau sans l'entier
    private int length;//Longueur du bateau
    private Vector3 pos;//position du centre du GO du bateau
    private int HP;//points de vie
    private Grille<Vector3> G;//Grille de coordonnées
    private bool rot;//rotation du bateau hor/vert
    private VisualManager VM;//VisualManager du jeu

    public Ship(GameObject SM, string namex, int length, Vector3 scale, string texture, string slayer) //Constructeur du bateau
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        Sp = new GameObject(namex);
        G = new Grille<Vector3>(length);
        this.namex = namex;
        this.length = length;
        HP = length;
        Sp.transform.localScale = scale;
        Sp.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/" + texture);
        Sp.transform.SetParent(SM.transform, false);
        Sp.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        Sp.AddComponent<Draggable>();
        Sp.GetComponent<SpriteRenderer>().sortingLayerName = slayer;
        rot = false;
        namey = namex.Split(' ');//Permet de couper le nom du bateau, pour l'affichage
    }

    public int getLength()//Retourne la longueur d'un bateau
    {
        return length;
    }
    public Grille<Vector3> getVecteur()//Retourne la valeur (ici des positions) stocké dans un tableau
    {
        return G;
    }

    public void updateG()
    {
        pos = Sp.transform.position;
        Vector3 V;
        if ((rot == false) && (length % 2 == 0))
        {
            for (int i = 0; i < (length / 2); i++)
            {
                V = new Vector3(pos.x - i - 0.5f, pos.y, pos.z);
                G.setVal(i, V);
                V = new Vector3(pos.x + i + 0.5f, pos.y, pos.z);
                G.setVal(i + (length / 2), V);
            }
        }

        if ((rot == true) && (length % 2 == 0))
        {
            for (int i = 0; i < (length / 2); i++)
            {
                V = new Vector3(pos.x, pos.y - i - 0.5f, pos.z);
                G.setVal(i, V);
                V = new Vector3(pos.x, pos.y + i + 0.5f, pos.z);
                G.setVal(i + (length / 2), V);
            }
        }

        if ((rot == false) && (length % 2 != 0))
        {
            G.setVal(length / 2, new Vector3(pos.x, pos.y, pos.z));
            for (int i = 0; i < (length / 2) + 1; i++)
            {
                V = new Vector3(pos.x - i, pos.y, pos.z);
                G.setVal(i, V);
                V = new Vector3(pos.x + i, pos.y, pos.z);
                G.setVal(i + (length / 2), V);
            }
        }

        if ((rot == true) && (length % 2 != 0))
        {
            G.setVal(length / 2, new Vector3(pos.x, pos.y, pos.z));
            for (int i = 0; i < (length / 2) + 1; i++)
            {
                V = new Vector3(pos.x, pos.y - i, pos.z);
                G.setVal(i, V);
                V = new Vector3(pos.x, pos.y + i, pos.z);
                G.setVal(i + (length / 2), V);
            }
        }

    }
    public void changeRotShip()
    {
        if (rot == false)
        {
            rot = true;
        }
        else
        {
            rot = false;
        }
    }
    
    public void hit()
    {
        CanvasGenerator CvsGN = GameObject.FindObjectOfType<GameNavale>().getCvsGN();
        Debug.Log("CurrentHp :" + HP);
        HP--;
                if (HP == 0)
                {
                    Debug.Log("Coulé");
                    GameObject.Find("TextSlider1").GetComponent<Text>().text = namey[0] + " coulé";
                    CvsGN.setText(4, "");
                    GameObject.Find("TextSlider2").GetComponent<Text>().text = namex[0] + " coulé";
                    CvsGN.setText(5, "");
                    CvsGN.getPanel(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Sinking");
                    CvsGN.getPanel(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Sinking");

            if ((VM.getCameraVM(1).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(3).GetComponent<Camera>().enabled == true))
            {
                for (int i = 0; i < length; i++)
                {
                    GameObject skull = new GameObject("Skull " + namex + i);
                    skull.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Skull");
                    skull.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer";
                    skull.transform.position = new Vector3(G.getVal(i).x-30, G.getVal(i).y+30, G.getVal(i).z);
                }
            }

            if ((VM.getCameraVM(2).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(4).GetComponent<Camera>().enabled == true))
            {
                for (int i = 0; i < length; i++)
                {
                    GameObject skull = new GameObject("Skull " + namex + i);
                    skull.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/Skull");
                    skull.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer";
                    skull.transform.position = new Vector3(G.getVal(i).x+30, G.getVal(i).y+30, G.getVal(i).z);
                }
            }
        }
                else
                {
                    CvsGN.setText(4, "");
                    GameObject.Find("TextSlider1").GetComponent<Text>().text = "Touché";
                    CvsGN.getPanel(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Hit");
                    CvsGN.setText(5, "");
                    GameObject.Find("TextSlider2").GetComponent<Text>().text = "Touché";
                    CvsGN.getPanel(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Hit");
        }
        if ((VM.getCameraVM(1).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(3).GetComponent<Camera>().enabled == true))
        {
            if (VM.getShipM(2).getHPtotal() == 0)
            {
                CvsGN.setText(2, "Le Joueur 1\n l'emporte!");
                CvsGN.setText(3, "Le Joueur 1\n l'emporte!");
                GameObject.Find("Victoire").transform.position = new Vector3(VM.getCameraVM(2).transform.position.x, VM.getCameraVM(2).transform.position.y, 0);
                GameObject.Find("Victoire2").transform.position = new Vector3(VM.getCameraVM(4).transform.position.x, VM.getCameraVM(4).transform.position.y, 0);
            }
        }
        if ((VM.getCameraVM(2).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(4).GetComponent<Camera>().enabled == true))
        {
            if (VM.getShipM(1).getHPtotal() == 0)
            {
                CvsGN.setText(2, "Le Joueur 2\n l'emporte!");
                CvsGN.setText(3, "Le Joueur 2\n l'emporte!");
                GameObject.Find("Victoire").transform.position = new Vector3(VM.getCameraVM(1).transform.position.x, VM.getCameraVM(1).transform.position.y, 0);
                GameObject.Find("Victoire2").transform.position = new Vector3(VM.getCameraVM(3).transform.position.x, VM.getCameraVM(3).transform.position.y, 0);
            }

        }
    }

    public int getHP()
    {
        return HP;
    }
}
