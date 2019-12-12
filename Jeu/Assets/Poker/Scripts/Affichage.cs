using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Affichage : MonoBehaviour
{
    public GameObject[] disposition = new GameObject[4];//Tableau contenant toutes les dispositions différentes en fonction du nombre de joueurs
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
    public void updateAffichage()//Gère l'affichage du jeu en regroupant les différentes fonctions d'affichage
    {
        updateAffichageMain();
        updateAffichageBourse();
        updateAffichageMise();
        updateAffichageBouton();
        updateAffichageNomJoueur();
        changementCouleurCarte();
        affichageBlinde();
    }
    public void updateAffichageMain()//Permet l'affichage des cartes du joueur si c'est à son tour
    {
        GameObject c1 = GameObject.Find("Carte_1");
        GameObject c2 = GameObject.Find("Carte_2");
        GameObject c = GameObject.Find("Carte");
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        poker.joueurs[poker.getTour()].GetComponent<Joueur>().main[0].transform.position = c1.transform.position;
        poker.joueurs[poker.getTour()].GetComponent<Joueur>().main[1].transform.position = c2.transform.position;
        for(int i = 0; i < Poker.nbJoueurs; i++)
        {
            if(poker.joueurs[poker.getTour()] != poker.joueurs[i])
            {
                poker.joueurs[i].GetComponent<Joueur>().main[0].transform.position = c.transform.position;
                poker.joueurs[i].GetComponent<Joueur>().main[1].transform.position = c.transform.position;
            }
        }
    }
    public void changementCouleurCarte()//Affiche les cartes des joueurs qui se sont couchés en gris
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        for(int i = 1; i < Poker.nbJoueurs; i++)
        {
            if (p.joueurs[(p.getTour() + i) % Poker.nbJoueurs].GetComponent<Joueur>().aPasse)
            {
                for (int j = 1; j <= 2; j++)
                {
                    GameObject carte = GameObject.Find("Carte_" + j + "_"+ (i+1) + "_" + Poker.nbJoueurs);
                    carte.GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }
            else
            {
                for (int j = 1; j <= 2; j++)
                {
                    GameObject carte = GameObject.Find("Carte_" + j + "_" + (i + 1) + "_" + Poker.nbJoueurs);
                    carte.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }
    public void updateAffichageBourse()//Permet l'affichage des bourses des joueurs
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        GameObject.Find("Bourse").GetComponent<TextMeshProUGUI>().text = p.joueurs[p.getTour()].GetComponent<Joueur>().getBourse().ToString();
        for (int i = 1; i < Poker.nbJoueurs; i++)
        {
            GameObject.Find("Bourse_" + (i+1) + "_" + Poker.nbJoueurs).GetComponent<TextMeshProUGUI>().text = p.joueurs[(p.getTour() + i) % p.joueurs.Count].GetComponent<Joueur>().getBourse().ToString();
        }
    }
    public void updateAffichageMise()//Permet l'affichage de la mise des joueurs
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        GameObject.Find("Mise").GetComponent<TextMeshProUGUI>().text = "Mise : " + p.joueurs[p.getTour()].GetComponent<Joueur>().mise;
        GameObject.Find("MiseGlobale").GetComponent<TextMeshProUGUI>().text = Poker.miseManche.ToString();
        for (int i = 1; i < Poker.nbJoueurs; i++)
        {
            GameObject.Find("Mise_" + (i+1) + "_" + Poker.nbJoueurs).GetComponent<TextMeshProUGUI>().text = "Mise : " + p.joueurs[(p.getTour() + i) % p.joueurs.Count].GetComponent<Joueur>().mise.ToString();
        }
    }
    public void updateAffichageBouton()//Modifie l'affichage des boutons de manière à ce qu'ils intéragissent mieux avec l'utilisateur
    {
        Poker p = GameObject.Find("Poker").GetComponent<Poker>();
        Slider s = GameObject.Find("Slider").GetComponent<Slider>();
        Joueur j = p.joueurs[p.getTour()].GetComponent<Joueur>();
        int x = Poker.miseManche - j.mise;
        if (x >= j.getBourse())
        {
            GameObject.Find("Suivre").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Tapis";
            GameObject.Find("Relancer").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("Suivre").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Suivre : " + (Poker.miseManche - j.mise);
            GameObject.Find("Relancer").GetComponent<Button>().interactable = true;
            GameObject.Find("Se coucher").GetComponent<Button>().interactable = true;
        }
        if(s.value != 0 && s.value == s.maxValue)
        {
            GameObject.Find("Relancer").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Tapis";
        }
        else
        {
            GameObject.Find("Relancer").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Relancer : " + (int)s.value;
        }
    }
    public void updateAffichageNomJoueur()//Permet l'affichage du nom des joueurs
    {
        Poker p = FindObjectOfType<Poker>();
        GameObject.Find("Nom").GetComponent<TextMeshProUGUI>().text = p.joueurs[p.getTour()].GetComponent<Joueur>().nom;
        for (int i = 1; i < Poker.nbJoueurs; i++)
        {
            GameObject.Find("Nom_" + (i+1) + "_" + Poker.nbJoueurs).GetComponent<TextMeshProUGUI>().text = p.joueurs[(p.getTour() + i) % p.joueurs.Count].GetComponent<Joueur>().nom;
        }
    }
    public void affichageCarteJoueur()//Choisie la bonne disposition de carte en fonction du nombre de joueurs
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
    public void affichageBlinde()//Permet d'afficher les jetons de blinde
    {
        Poker p = FindObjectOfType<Poker>();
        for (int i = 0; i < Poker.nbJoueurs; i++)
        {
            if (p.joueurs[(p.getTour() + i) % Poker.nbJoueurs].GetComponent<Joueur>().isSmallBlind)
            {
                if (i == 0)
                {
                    GameObject.Find("SmallBlind").GetComponent<Transform>().position = GameObject.Find("Blinde_0").GetComponent<Transform>().position;
                }
                else
                {
                    GameObject.Find("SmallBlind").GetComponent<Transform>().position = GameObject.Find("Blinde_" + (i+1) + Poker.nbJoueurs).GetComponent<Transform>().position;
                }
            }
            else
            {
                if (p.joueurs[(p.getTour() + i) % Poker.nbJoueurs].GetComponent<Joueur>().isBigBlind)
                {
                    if (i == 0)
                    {
                        GameObject.Find("BigBlind").GetComponent<Transform>().position = GameObject.Find("Blinde_0").GetComponent<Transform>().position;
                    }
                    else
                    {
                        GameObject.Find("BigBlind").GetComponent<Transform>().position = GameObject.Find("Blinde_" + (i+1) + Poker.nbJoueurs).GetComponent<Transform>().position;
                    }
                }
            }
        }
    }
}
