using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    bool dessus = false;//Booléen indiquant si la souris est sur l'objet
    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<Poker>().flop.Contains(gameObject))
        {
            select();
        } 
    }
    private void select()//Permet de retourner les cartes si le curseur se pose sur les cartes de la main.
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(this.name == hit.transform.gameObject.name){
                if (!dessus)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<Carte>().cardFace;
                    dessus = true;
                }
            }   
        }
        else
        {
            if (dessus)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<Carte>().cardBack;
                dessus = false;
            }
        }
    }
}
