using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void max()//Mets le slider à sa valeur maximale
    {
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        s.value = s.maxValue;
    }
    public void min()//Mets le slider à sa valeur minimale
    {
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        s.value = s.minValue;
    }
    public void seCoucher()//Permet au joueur de se coucher
    {
        GestionSons.Instance.SonFlip();
        Poker p = FindObjectOfType<Poker>();
        p.joueurs[p.getTour()].GetComponent<Joueur>().aPasse = true;
        ts();
    }
    public void suivre()//Permet au joueur de suivre la mise
    {
        GestionSons.Instance.SonJetons();
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        Joueur j = p.joueurs[p.getTour()].GetComponent<Joueur>(); 
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
        j.aJoue = true;
        ts();
    }
    public void relancer()//Permet au joueur de relancer d'une mise égale au montant du slider
    {
        GestionSons.Instance.SonJetons();
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        Joueur j = p.joueurs[p.getTour()].GetComponent<Joueur>();
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        int valeur = (int)s.value;
        p.setJoueursAJoue(false);
        suivre();
        j.diminuerBourse(valeur);
        Poker.miseManche += valeur;
        j.mise = Poker.miseManche;
    }
    public void ts()//Permet de passer au joueur suivant
    {
        GestionSons.Instance.SonJetons();
        Poker p = FindObjectOfType<Poker>();
        List<GameObject> liste = p.joueurs;
        if (p.tourGlobal >= 3 || p.nombreDeJoueurPasse() == liste.Count - 1)
        {
            p.nouvelleManche();
        }
        else
        {
            do
            {
                if (p.toutLeMondeAJoue())
                {
                    if (p.tourGlobal == 0)
                    {
                        p.flopper(3);
                        p.setJoueursAJoue(false);
                    }
                    else
                    {
                        if (p.tourGlobal < 3)
                        {
                            p.flopper(1);
                            p.setJoueursAJoue(false);
                        }
                        else
                        {
                            p.nouvelleManche();
                            break;
                        }
                    }
                    p.tourGlobal++;
                }
                p.setTour((p.getTour() + 1) % liste.Count);
            } while (liste[p.getTour()].GetComponent<Joueur>().aPasse);
        }
        liste[p.getTour()].GetComponent<Joueur>().determinaisonCombinaison();
    }
}
