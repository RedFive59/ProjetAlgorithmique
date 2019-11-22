using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    private GameObject Sp;
    private string namex;
    private int length;
    private Vector3 pos;
    private int HP;
    private Grille<Vector3> G;
    private bool rot;
    private VisualManager VM;

    public Ship(GameObject SM, string namex, int length, Vector3 scale, string texture, string slayer)
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        Sp = new GameObject(namex);
        G = new Grille<Vector3>(length);
        this.namex = namex;
        this.length = length;
        this.HP = length;
        Sp.transform.localScale = scale;
        Sp.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/" + texture);
        Sp.transform.SetParent(SM.transform, false);
        Sp.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        Sp.AddComponent<Draggable>();
        Sp.GetComponent<SpriteRenderer>().sortingLayerName = slayer;
        rot = false;
    }

    public int getLength()
    {
        return length;
    }
    public Grille<Vector3> getVecteur()
    {
        return G;
    }

    public string getName()
    {
        return namex;
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
        HP--;
                if (HP == 0)
                {
                    Debug.Log("Coulé");
                    CvsGN.setText(3, namex + "coulé");
                }
                else
                {
                    Debug.Log("Touché");
                    CvsGN.setText(3, "touché");
        }
    }

    public int getHP()
    {
        return HP;
    }
}
