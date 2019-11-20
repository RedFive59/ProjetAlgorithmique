using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship
{
    private GameObject Sp;
    private string name;
    private int length;
    private Vector3 pos;
    private int HP;
    private Grille<Vector3> G;
    private bool rot;

    public Ship(GameObject SM, string name, int length, Vector3 scale, string texture, string slayer)
    {
        Sp = new GameObject(name);
        G = new Grille<Vector3>(length);
        this.name = name;
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

    public Ship()
    
    /*
    void Update()
    {
        if (length % 2 != 0)
        {
            for (int i = 0; i < length / 2; i++)
            {
                G.setVal(i, Sp.transform.position);
            }
            {
                G.setVal(i, Sp.transform.position);
            }
        }
    }
    */
}