using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenShips : MonoBehaviour
{
private GenVisualManager GVM;
    // Start is called before the first frame update
    void Start()
    {
        GVM = GameObject.FindObjectOfType<GenVisualManager>();
        GameObject Torpilleur = new GameObject("Torpilleur");
        Torpilleur.transform.localScale=new Vector3(2,1,1);//set la taille en unité du bateau
        Torpilleur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtTp128"); ;//lie la texture correspondante au bateau
        Torpilleur.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);//place un boxcollider 2d sur le bateau avec auto size
        Torpilleur.transform.parent = this.transform;//attache le bateau au GO du script (GenShips)
        Torpilleur.AddComponent<Draggable>();

        GameObject ContreTorpilleur = new GameObject("ContreTorpilleur");
        ContreTorpilleur.transform.localScale = new Vector3(3, 1, 1);
        ContreTorpilleur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtCtp256");
        ContreTorpilleur.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        ContreTorpilleur.transform.parent = this.transform;
        ContreTorpilleur.AddComponent<Draggable>();

        GameObject SousMarin = new GameObject("SousMarin");
        SousMarin.transform.localScale = new Vector3(3, 1, 1);
        SousMarin.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtSm512");
        SousMarin.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        SousMarin.transform.parent = this.transform;
        SousMarin.AddComponent<Draggable>();

        GameObject Croiseur = new GameObject("Croiseur");
        Croiseur.transform.localScale = new Vector3(4, 1, 1);
        Croiseur.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtCs512");
        Croiseur.AddComponent<BoxCollider>().size = new Vector3(1, 1, 1);
        Croiseur.transform.parent = this.transform;
        Croiseur.AddComponent<Draggable>();

        GameObject PorteAvion = new GameObject("PorteAvion");
        PorteAvion.transform.localScale = new Vector3(5, 1, 1);
        PorteAvion.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/txtPa512");
        PorteAvion.AddComponent<BoxCollider>().size=new Vector3(1,1,1);
        PorteAvion.transform.parent = this.transform;
        PorteAvion.AddComponent<Draggable>();

        for (int i = 0; i < 5; i++)
        {
            this.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer";//donne le bon shortlayer (ordre d'affichage) au rendu de chaque bateau
            this.transform.GetChild(i).transform.position = new Vector3(GVM.getposGVM()+12.5f,GVM.getposGVM()+1+i*2, 0);
        }
    }

    public void moveShip(int i, float x, float y, float z)
    {
        Vector3 V;
            if (this.transform.GetChild(i).GetComponent<Draggable>().getMag())
            {
                V = this.transform.GetChild(i).position;
                this.transform.GetChild(i).position = new Vector3(V.x + x, V.y + y, V.z + z);
            }
    }
    public void moveShip(float x,float y,float z)
    {
        Vector3 V;
        for(int i = 0; i< 5; i++)
        {
            if (this.transform.GetChild(i).GetComponent<Draggable>().getMag())
            {
                V = this.transform.GetChild(i).position;
                this.transform.GetChild(i).position = new Vector3(V.x + x, V.y + y, V.z + z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    }
