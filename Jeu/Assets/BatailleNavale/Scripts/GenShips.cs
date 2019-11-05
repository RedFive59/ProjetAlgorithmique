﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenShips : MonoBehaviour
{
    //!!! OUT
    //TEXTURES BATEAUX
    //public Sprite txtTp;
    //public Sprite txtCtp;
    //public Sprite txtSm;
    //public Sprite txtCs;
    //public Sprite txtPa;
    // !!! OUT

    // Start is called before the first frame update
    void Start()
    {
        GameObject Torpilleur = new GameObject("Torpilleur");
        Torpilleur.transform.localScale=new Vector3(2,1,1);//set la taille en unité du bateau
        Torpilleur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtTp128"); ;//lie la texture correspondante au bateau
        Torpilleur.AddComponent<BoxCollider2D>().autoTiling = true;//place un boxcollider 2d sur le bateau avec auto size
        Torpilleur.transform.parent = this.transform;//attache le bateau au GO du script (GenShips)
        Torpilleur.AddComponent<Draggable>();

        GameObject ContreTorpilleur = new GameObject("ContreTorpilleur");
        ContreTorpilleur.transform.localScale = new Vector3(3, 1, 1);
        ContreTorpilleur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtCtp256");
        ContreTorpilleur.AddComponent<BoxCollider2D>().autoTiling = true;
        ContreTorpilleur.transform.parent = this.transform;
        ContreTorpilleur.AddComponent<Draggable>();

        GameObject SousMarin = new GameObject("SousMarin");
        SousMarin.transform.localScale = new Vector3(3, 1, 1);
        SousMarin.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtSm512");
        SousMarin.AddComponent<BoxCollider2D>().autoTiling = true;
        SousMarin.transform.parent = this.transform;
        SousMarin.AddComponent<Draggable>();

        GameObject Croiseur = new GameObject("Croiseur");
        Croiseur.transform.localScale = new Vector3(4, 1, 1);
        Croiseur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtCs512");
        Croiseur.AddComponent<BoxCollider2D>().autoTiling = true;
        Croiseur.transform.parent = this.transform;
        Croiseur.AddComponent<Draggable>();

        GameObject PorteAvion = new GameObject("PorteAvion");
        PorteAvion.transform.localScale = new Vector3(5, 1, 1);
        PorteAvion.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtPa512");
        PorteAvion.AddComponent<BoxCollider2D>().autoTiling = true;
        PorteAvion.transform.parent = this.transform;
        PorteAvion.AddComponent<Draggable>();

        for (int i = 0; i < 5; i++)
        {
            Debug.Log(this.transform.GetChild(i).GetComponent<SpriteRenderer>());
            this.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer";//donne le bon shortlayer (ordre d'affichage) au rendu de chaque bateau
            this.transform.GetChild(i).transform.position = new Vector3(12.5f, -10 + i*2, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
