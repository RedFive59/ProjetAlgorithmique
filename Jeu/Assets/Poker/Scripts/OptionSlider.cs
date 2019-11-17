using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        updateValeur();
    }
    public void updateValeur()//Permet à la valeur du Slider de se mettre à jour en fonction de la mise actuelle
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        Joueur j = p.joueursManche[p.getTour()].GetComponent<Joueur>();
        int maxvalue = j.getBourse() - (Poker.miseManche - j.mise);
        if (maxvalue >= 0)
        {
            s.maxValue = maxvalue;
        }
        else
        {
            s.maxValue = 0;
        }
        s.value = s.maxValue / 2;
    }
}
