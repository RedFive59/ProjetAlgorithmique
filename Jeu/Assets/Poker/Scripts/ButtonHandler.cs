using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void joueurSuivant()//Permet de passer au joueur suivant
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        if(p.tourGlobal < 5 && p.getTour() == 4)
        {
            p.flopper();
            p.tourGlobal++;
        }
        p.setTour((p.getTour() + 1) % 5);
        GameObject.Find("testI").GetComponent<Text>().text = "Joueur " + (p.getTour()+1);
    }
}
