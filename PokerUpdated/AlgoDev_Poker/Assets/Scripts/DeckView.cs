using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pioche))]

public class DeckView : MonoBehaviour
{
    Pioche pioche;

    public Vector3 start;
    public float cardOffset; //distance séparant chaque carte à l'affichage
    public GameObject prefabCarte;

    void Start()
    {    
        pioche = GetComponent<Pioche>();
        showCards();
    }

    void showCards()
    {
        int cardCount = 0;

        foreach(int i in pioche.GetCartes())
        {
            float co = cardOffset * cardCount;

            GameObject cardCopy = (GameObject)Instantiate(prefabCarte);
            Vector3 temp = start + new Vector3(co, 0f);
            cardCopy.transform.position = temp;

            CardModel cardModel = cardCopy.GetComponent<CardModel>();
            cardModel.pointeurCartes = i;
            cardModel.changerFace(true);

            cardCount++;
        }
    }
}
