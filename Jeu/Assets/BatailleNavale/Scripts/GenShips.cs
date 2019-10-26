using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenShips : MonoBehaviour
{
    public Sprite txtTp;
    public Sprite txtCtp;
    public Sprite txtSm;
    public Sprite txtCs;
    public Sprite txtPa;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Torpilleur = new GameObject("Torpilleur");
        Torpilleur.transform.localScale=new Vector3(2,1,1);
        Torpilleur.AddComponent<SpriteRenderer>().sprite = txtTp; ;
        Torpilleur.AddComponent<BoxCollider2D>().autoTiling = true;
        Torpilleur.GetComponent<BoxCollider2D>().size = new Vector2(2, 1);
        Torpilleur.transform.parent = this.transform;

        GameObject ContreTorpilleur = new GameObject("ContreTorpilleur");
        ContreTorpilleur.transform.localScale = new Vector3(3, 1, 1);
        ContreTorpilleur.AddComponent<SpriteRenderer>().sprite = txtCtp;
        ContreTorpilleur.AddComponent<BoxCollider2D>().autoTiling = true;
        ContreTorpilleur.GetComponent<BoxCollider2D>().size = new Vector2(3, 1);
        ContreTorpilleur.transform.parent = this.transform;

        GameObject SousMarin = new GameObject("SousMarin");
        SousMarin.transform.localScale = new Vector3(3, 1, 1);
        SousMarin.AddComponent<SpriteRenderer>().sprite = txtSm;
        SousMarin.AddComponent<BoxCollider2D>().autoTiling = true;
        SousMarin.GetComponent<BoxCollider2D>().size = new Vector2(3, 1);
        SousMarin.transform.parent = this.transform;

        GameObject Croiseur = new GameObject("Croiseur");
        Croiseur.transform.localScale = new Vector3(4, 1, 1);
        Croiseur.AddComponent<SpriteRenderer>().sprite = txtCs;
        Croiseur.AddComponent<BoxCollider2D>().autoTiling = true;
        Croiseur.GetComponent<BoxCollider2D>().size = new Vector2(4, 1);
        Croiseur.transform.parent = this.transform;

        GameObject PorteAvion = new GameObject("PorteAvion");
        PorteAvion.transform.localScale = new Vector3(5, 1, 1);
        PorteAvion.AddComponent<SpriteRenderer>().sprite = txtPa;
        PorteAvion.AddComponent<BoxCollider2D>().autoTiling = true;
        PorteAvion.GetComponent<BoxCollider2D>().size = new Vector2(5, 1);
        PorteAvion.transform.parent = this.transform;
    
        for(int i = 0; i < 5; i++)
        {
            Debug.Log(this.transform.GetChild(i).GetComponent<SpriteRenderer>());
            this.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
