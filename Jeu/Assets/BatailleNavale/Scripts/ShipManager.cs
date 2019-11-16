using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager
{
    private GameObject SM;

    // Start is called before the first frame update
    public ShipManager(string nom, Vector3 pos)
    {
        SM = new GameObject("nom");

        GameObject Torpilleur = new GameObject("Torpilleur");
        Torpilleur.transform.localScale = new Vector3(2, 1, 1);//set la taille en unité du bateau
        Torpilleur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtTp128"); ;//lie la texture correspondante au bateau
        Torpilleur.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);//place un boxcollider 2d sur le bateau avec auto size
        Torpilleur.transform.parent = SM.transform;//attache le bateau au GO du script (GenShips)
        Torpilleur.AddComponent<Draggable>();

        GameObject ContreTorpilleur = new GameObject("ContreTorpilleur");
        ContreTorpilleur.transform.localScale = new Vector3(3, 1, 1);
        ContreTorpilleur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtCtp256");
        ContreTorpilleur.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        ContreTorpilleur.transform.parent = SM.transform;
        ContreTorpilleur.AddComponent<Draggable>();

        GameObject SousMarin = new GameObject("SousMarin");
        SousMarin.transform.localScale = new Vector3(3, 1, 1);
        SousMarin.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtSm512");
        SousMarin.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        SousMarin.transform.parent = SM.transform;
        SousMarin.AddComponent<Draggable>();

        GameObject Croiseur = new GameObject("Croiseur");
        Croiseur.transform.localScale = new Vector3(4, 1, 1);
        Croiseur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtCs512");
        Croiseur.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        Croiseur.transform.parent = SM.transform;
        Croiseur.AddComponent<Draggable>();

        GameObject PorteAvion = new GameObject("PorteAvion");
        PorteAvion.transform.localScale = new Vector3(5, 1, 1);
        PorteAvion.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtPa512");
        PorteAvion.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        PorteAvion.transform.parent = SM.transform;
        PorteAvion.AddComponent<Draggable>();

        for (int i = 0; i < 5; i++)
        {
            SM.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer";//donne le bon shortlayer (ordre d'affichage) au rendu de chaque bateau
            SM.transform.GetChild(i).transform.position = new Vector3(pos.x + 12.5f, pos.y + 1 + i * 2, pos.z + 0);
        }
    }

    public void moveShip(int i, float x, float y, float z)
    {
        Vector3 V;
        if (SM.transform.GetChild(i).GetComponent<Draggable>().getMag())
        {
            V = SM.transform.GetChild(i).position;
            SM.transform.GetChild(i).position = new Vector3(V.x + x, V.y + y, V.z + z);
        }
    }
    public void moveShip(float x, float y, float z)
    {
        Vector3 V;
        for (int i = 0; i < 5; i++)
        {
            if (SM.transform.GetChild(i).GetComponent<Draggable>().getMag())
            {
                V = SM.transform.GetChild(i).position;
                SM.transform.GetChild(i).position = new Vector3(V.x + x, V.y + y, V.z + z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
