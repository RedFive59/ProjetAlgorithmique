using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void max()
    {
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        s.value = s.maxValue;
    }
    public void min()
    {
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        s.value = s.minValue;
    }
    public void seCoucher()
    {
        Poker p = FindObjectOfType<Poker>();
        int tour = p.getTour();
        p.joueursManche.Remove(p.joueursManche[tour]);
        if(tour == p.joueursManche.Count)
        {
            p.setTour(0);
        }
    }
    public void suivre()
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        Joueur j = p.joueursManche[p.getTour()].GetComponent<Joueur>(); 
        int x = j.getBourse() - (Poker.miseManche - j.mise);
        if(x >= 0)
        {
            j.diminuerBourse(Poker.miseManche - j.mise);
            j.mise = Poker.miseManche;
        }
        else
        {
            j.mise += j.getBourse();
            j.diminuerBourse(j.getBourse());
        }
        ts();
    }
    public void relancer()
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        Joueur j = p.joueursManche[p.getTour()].GetComponent<Joueur>();
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        int valeur = (int)s.value;
        suivre();
        j.diminuerBourse(valeur);
        Poker.miseManche += valeur;
        j.mise = Poker.miseManche;
    }
    public void ts()
    {
        Poker p = FindObjectOfType<Poker>();
        int tour = p.getTour();
        if(p.tourGlobal < 5 && tour == p.joueursManche.Count - 1)
        {
            p.tourGlobal++;
            p.flopper();
        }
        p.setTour((tour + 1) % p.joueursManche.Count);
    }
}
