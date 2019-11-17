using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    //Attributs
    public GameObject Text;//Texte liée au bouton

    void Update()
    {
        updateTexte();
    }
    public void joueurSuivant()//Permet de passer au joueur suivant
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        if (p.tourGlobal < 5 && p.getTour() == (p.joueursManche.Count - 1))
        {
            p.flopper();
            p.tourGlobal++;
        }
        p.setTour((p.getTour() + 1) % p.joueursManche.Count);
        GameObject.Find("joueur").GetComponent<Text>().text = p.joueursManche[p.getTour()].name;
        OptionSlider.updateValeur();
        p.joueursManche[p.getTour()].GetComponent<Joueur>().determinaisonCombinaison();
        
    }
    public void suivre()//Est déclenchée quand le joueur appuie sur Suivre
    {
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        poker.joueursManche[poker.getTour()].GetComponent<Joueur>().suivre();
        joueurSuivant();
    }
    public void seCoucher()//Est déclenchée quand le joueur appuie sur Se coucher
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        p.joueursManche.Remove(p.joueursManche[p.getTour()]);
        if (p.tourGlobal < 5 && p.getTour() == (p.joueursManche.Count - 1))
        {
            p.flopper();
            p.tourGlobal++;
        }
        GameObject.Find("joueur").GetComponent<Text>().text = p.joueursManche[p.getTour()].name;
        OptionSlider.updateValeur();
    }
    public void relancer()//Est déclenchée quand le joueur appuie sur Relancer
    {
        int valeur = (int)GameObject.Find("Slider").GetComponent<Slider>().value;
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        poker.joueursManche[poker.getTour()].GetComponent<Joueur>().diminuerBourse(valeur);
        joueurSuivant();
        Poker.miseManche += valeur;
        OptionSlider.updateValeur();
    }
    public void updateTexte()//Permet d'afficher les bonnes valeurs pour les boutons
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        switch (this.name)
        {
            default:
                break;
            case "Suivre":
                int val = (Poker.miseManche - p.joueursManche[p.getTour()].GetComponent<Joueur>().mise);
                if(val > p.joueursManche[p.getTour()].GetComponent<Joueur>().getBourse()) this.Text.GetComponent<Text>().text = "Suivre : " + p.joueursManche[p.getTour()].GetComponent<Joueur>().getBourse();
                else this.Text.GetComponent<Text>().text = "Suivre : " + (Poker.miseManche - p.joueursManche[p.getTour()].GetComponent<Joueur>().mise);
                break;
            case "Relancer":
                this.Text.GetComponent<Text>().text = "Relancer : " + (int)GameObject.Find("Slider").GetComponent<Slider>().value;
                break;
        }
    }
}
