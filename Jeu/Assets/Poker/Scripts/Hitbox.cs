using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    bool dessus = false;//Booléen indiquant si la souris est sur l'objet

    FlipCard flipper;
    Carte modele;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        modele = this.GetComponent<Carte>();
        flipper = this.GetComponent<FlipCard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<Poker>().flop.Contains(gameObject))
        {
            select();
        } 
    }

    void OnMouseEnter()
    {
        GestionSons.Instance.SonFlip();
    }

    private void select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //GameObject tile = GameObject.Find(hit.transform.gameObject.name);
            if (this.name == hit.transform.gameObject.name)
            {
                if (!dessus) //!dessus
                {
                    flipper.flipCard(modele.cardBack, modele.cardFace);
                    //print("Flip " + hit.transform.gameObject.name);
                    dessus = true;
                }
            }
        }
        //else
        //{
            if (dessus) //dessus //Input.GetKeyDown(KeyCode.DownArrow)
            {
                flipper.flipCard(modele.cardFace, modele.cardBack);
                //print("Flip inverse " + this.name);
                dessus = false;
            }
        //}
    }
}
