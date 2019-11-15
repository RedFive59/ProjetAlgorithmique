using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_FlipCard : MonoBehaviour
{
    FlipCard flipper;
    Carte modele;

    public GameObject carte;

    void Awake()
    {
        modele = carte.GetComponent<Carte>();
        flipper = carte.GetComponent<FlipCard>();
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(10,10,100,28), "Flip it!"))
        {
            flipper.flipCard(modele.cardFace, modele.cardFace);
        }
    }
}
