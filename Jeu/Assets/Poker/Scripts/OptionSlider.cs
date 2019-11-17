using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static void updateValeur()//Permet à la valeur du Slider de se mettre à jour en fonction de la mise actuelle
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        GameObject.Find("Slider").GetComponent<Slider>().value = (Poker.miseManche*2 - p.joueursManche[p.getTour()].GetComponent<Joueur>().mise);
        GameObject.Find("Slider").GetComponent<Slider>().maxValue = p.joueursManche[p.getTour()].GetComponent<Joueur>().getBourse();
        GameObject.Find("Slider").GetComponent<Slider>().minValue = Poker.miseManche*2 - p.joueursManche[p.getTour()].GetComponent<Joueur>().mise;
    }
}
