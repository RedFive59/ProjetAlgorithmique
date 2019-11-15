using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Affichage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

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
        updateAffichageCombinaison();
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
        for(int i = 0; i < poker.joueursManche.Count; i++)
        {
            if(poker.getTour() != i)
            {
                poker.joueursManche[i].GetComponent<Joueur>().main[0].transform.position = c.transform.position;
                poker.joueursManche[i].GetComponent<Joueur>().main[1].transform.position = c.transform.position;
            }
        }
    }
    public void updateAffichageBourse()//Permet l'affichage de le bourse du joueur
    {
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        GameObject.Find("Bourse").GetComponent<Text>().text = poker.joueursManche[poker.getTour()].GetComponent<Joueur>().getBourse().ToString();
    }
    public void updateAffichageMise()//Permet l'affichage de le mise du joueur
    {
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        GameObject.Find("Mise").GetComponent<Text>().text = "Mise : " + poker.joueursManche[poker.getTour()].GetComponent<Joueur>().mise;
        GameObject.Find("MiseGlobale").GetComponent<Text>().text = "Mise Globale : " + Poker.miseManche;
    }
    public void updateAffichageCombinaison()//Permet l'affichage de le combinaison du joueur du joueur
    {
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        poker.joueursManche[poker.getTour()].GetComponent<Joueur>().determinaisonCombinaison();
        GameObject.Find("Combinaison").GetComponent<Text>().text = poker.joueursManche[poker.getTour()].GetComponent<Joueur>().combinaison.ToString() + " de " + Joueur.max(poker.joueursManche[poker.getTour()].GetComponent<Joueur>().l).valeur.ToString();
    }
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
    }
}
