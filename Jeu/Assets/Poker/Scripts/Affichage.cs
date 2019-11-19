using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Affichage : MonoBehaviour
{
    public GameObject[] disposition = new GameObject[4];
    // Start is called before the first frame update
    void Start()
    {
        affichageCarteJoueur();
    }
    // Update is called once per frame
    void Update()
    {
        updateAffichage();
    }
    public void updateAffichage()//Regroupe les différentes fonctions d'update d'affichage
    {
        updateAffichageMain();
        updateAffichageBourse();
        updateAffichageMise();
        //updateAffichageCombinaison();
        updateAffichageBouton();
        updateAffichageNomJoueur();
    }
    public void updateAffichageMain()//Permet l'affichage des cartes du joueur si c'est à son tour
    {
        GameObject c1 = GameObject.Find("Carte_1");
        GameObject c2 = GameObject.Find("Carte_2");
        GameObject c = GameObject.Find("Carte");
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        poker.joueursManche[poker.getTour()].GetComponent<Joueur>().main[0].transform.position = c1.transform.position;
        poker.joueursManche[poker.getTour()].GetComponent<Joueur>().main[1].transform.position = c2.transform.position;
        for(int i = 0; i < Poker.nbJoueurs; i++)
        {
            if(poker.joueursManche[poker.getTour()] != poker.joueurs[i])
            {
                poker.joueurs[i].GetComponent<Joueur>().main[0].transform.position = c.transform.position;
                poker.joueurs[i].GetComponent<Joueur>().main[1].transform.position = c.transform.position;
            }
        }
    }
    public void updateAffichageBourse()//Permet l'affichage de le bourse du joueur
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        GameObject.Find("Bourse").GetComponent<Text>().text = p.joueursManche[p.getTour()].GetComponent<Joueur>().getBourse().ToString();
        switch (Poker.nbJoueurs)
        {
            case 2:
                GameObject.Find("Bourse_2_2").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                break;
            case 3:
                GameObject.Find("Bourse_2_3").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                GameObject.Find("Bourse_3_3").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                break;
            case 4:
                GameObject.Find("Bourse_2_4").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                GameObject.Find("Bourse_3_4").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                GameObject.Find("Bourse_4_4").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 3) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                break;
            case 5:
                GameObject.Find("Bourse_2_5").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                GameObject.Find("Bourse_3_5").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                GameObject.Find("Bourse_4_5").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 3) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                GameObject.Find("Bourse_5_5").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 4) % p.joueursManche.Count].GetComponent<Joueur>().getBourse().ToString();
                break;
            default:
                break;
        }
    }
    public void updateAffichageMise()//Permet l'affichage de le mise du joueur
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        GameObject.Find("Mise").GetComponent<Text>().text = "Mise : " + p.joueursManche[p.getTour()].GetComponent<Joueur>().mise;
        GameObject.Find("MiseGlobale").GetComponent<Text>().text = Poker.miseManche.ToString();
        switch (Poker.nbJoueurs)
        {
            case 2:
                GameObject.Find("Mise_2_2").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                break;
            case 3:
                GameObject.Find("Mise_2_3").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                GameObject.Find("Mise_3_3").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                break;
            case 4:
                GameObject.Find("Mise_2_4").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                GameObject.Find("Mise_3_4").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                GameObject.Find("Mise_4_4").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 3) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                break;
            case 5:
                GameObject.Find("Mise_2_5").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                GameObject.Find("Mise_3_5").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                GameObject.Find("Mise_4_5").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 3) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                GameObject.Find("Mise_5_5").GetComponent<Text>().text = "Mise : " + p.joueursManche[(p.getTour() + 4) % p.joueursManche.Count].GetComponent<Joueur>().mise.ToString();
                break;
            default:
                break;
        }
    }
    /*public void updateAffichageCombinaison()//Permet l'affichage de le combinaison du joueur du joueur
    {
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        poker.joueursManche[poker.getTour()].GetComponent<Joueur>().determinaisonCombinaison();
        GameObject.Find("Combinaison").GetComponent<Text>().text = poker.joueursManche[poker.getTour()].GetComponent<Joueur>().combinaison.ToString() + " de " + Joueur.max(poker.joueursManche[poker.getTour()].GetComponent<Joueur>().l).valeur.ToString();
    }*/
    public void updateAffichageBouton()//Permet d'afficher les bonnes valeurs pour les boutons
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        Joueur j = p.joueursManche[p.getTour()].GetComponent<Joueur>();
        int x = Poker.miseManche - j.mise;
        if (x == j.getBourse())
        {
            GameObject.Find("Suivre").transform.GetChild(0).GetComponent<Text>().text = "Tapis";
            GameObject.Find("Relancer").GetComponent<Button>().interactable = false;
            GameObject.Find("Se coucher").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("Suivre").transform.GetChild(0).GetComponent<Text>().text = "Suivre : " + (Poker.miseManche - j.mise);
            GameObject.Find("Relancer").GetComponent<Button>().interactable = true;
            GameObject.Find("Se coucher").GetComponent<Button>().interactable = true;
        }
        if(s.value != 0 && s.value == s.maxValue)
        {
            GameObject.Find("Relancer").transform.GetChild(0).GetComponent<Text>().text = "Tapis";
        }
        else
        {
            GameObject.Find("Relancer").transform.GetChild(0).GetComponent<Text>().text = "Relancer : " + (int)s.value;
        }
    }
    public void updateAffichageNomJoueur()//Permet l'affichage du bon nom du joueur
    {
        Poker p = FindObjectOfType<Poker>();
        GameObject.Find("Nom").GetComponent<Text>().text = p.joueursManche[p.getTour()].GetComponent<Joueur>().nom;
        switch (Poker.nbJoueurs)
        {
            case 2:
                GameObject.Find("Nom_2_2").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                break;
            case 3:
                GameObject.Find("Nom_2_3").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                GameObject.Find("Nom_3_3").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                break;
            case 4:
                GameObject.Find("Nom_2_4").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                GameObject.Find("Nom_3_4").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                GameObject.Find("Nom_4_4").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 3) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                break;
            case 5:
                GameObject.Find("Nom_2_5").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 1) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                GameObject.Find("Nom_3_5").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 2) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                GameObject.Find("Nom_4_5").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 3) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                GameObject.Find("Nom_5_5").GetComponent<Text>().text = p.joueursManche[(p.getTour() + 4) % p.joueursManche.Count].GetComponent<Joueur>().nom;
                break;
            default:
                break;
        }
    }
    public void affichageCarteJoueur()//Permet le bon affichage des composants des joueurs en fonction du nb de joueurs
    {
        Poker p = FindObjectOfType<Poker>();
        for (int i = 0; i < 4; i++)
        {
            if (i == Poker.nbJoueurs-2)
            {
                this.disposition[i].SetActive(true);
            }
            else
            {
                this.disposition[i].SetActive(false);
            }

        }
    }
}
