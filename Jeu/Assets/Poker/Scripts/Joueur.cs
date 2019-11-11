using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Joueur : MonoBehaviour
{
    //Attributs
    public string nom;//Nom du joueur
    public List<GameObject> main;//Main du joueur
    public int mise = 0;//Mise d'argent que le joueur a misé cette manche
    private int bourse = Poker.BOURSEDEPART;//Bourse totale du joueur
    public Combinaison combinaison = Combinaison.Hauteur;//Combinaison la plus haute que possède le joueur
    public List<Carte> l = new List<Carte>();//Liste des cartes composant la combinaison 

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        determinaisonCombinaison();
    }
    public int getBourse()//Retourne la bourse du joueur
    {
        return this.bourse;
    }
    public void diminuerBourse(int valeur)//Diminue la bourse du joueur de la valeur passée en paramètre
    {
        if (this.bourse - valeur >= 0) this.bourse -= valeur;
    }
    public void suivre()//Permet au joueur de suivre la mise
    {
        if(this.bourse + this.mise > Poker.miseManche)
        {
            diminuerBourse(Poker.miseManche - mise);
            mise = Poker.miseManche;
        }
        else
        {
            diminuerBourse(this.bourse);
            mise = this.bourse;
        }
    }
    public void determinaisonCombinaison()
    {
        List<Carte> liste = new List<Carte>();
        foreach (GameObject g in this.main)
        {
            liste.Add(g.GetComponent<Carte>());
        }
        foreach (GameObject g in FindObjectOfType<Poker>().flop)
        {
            liste.Add(g.GetComponent<Carte>());
        }
        if (quinteFlushRoyale(liste)) this.combinaison = Combinaison.QuinteFlushRoyal;
        else
        {
            if (quinteFlush(liste)) this.combinaison = Combinaison.QuinteFlush;
            else
            {
                if (carre(liste)) this.combinaison = Combinaison.Carré;
                else
                {
                    if (full(liste)) this.combinaison = Combinaison.Full;
                    else
                    {
                        if (couleur(liste)) this.combinaison = Combinaison.Couleur;
                        else
                        {
                            if (quinte(liste)) this.combinaison = Combinaison.Quinte;
                            else
                            {
                                if (brelan(liste)) this.combinaison = Combinaison.Brelan;
                                else
                                {
                                    if (doublePaire(liste)) this.combinaison = Combinaison.DoublePaire;
                                    else
                                    {
                                        if (paire(liste)) this.combinaison = Combinaison.Paire;
                                        else
                                        {
                                            l = liste;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public bool paire(List<Carte> liste)//Retourne true si il y a une paire dans la liste
    {
        if (liste.Count < 2) return false;
        l = new List<Carte>();
        foreach (Carte c in liste)
        {
            foreach(Carte c2 in liste)
            {
                if (!c.Equals(c2))
                {
                    if (c.getValeur() == c2.getValeur())
                    {
                        l.Add(c);
                        l.Add(c2);
                        return true;
                    }
                }
            }
        }
        return false;
    }
    public bool doublePaire(List<Carte> liste)//Retourne true s'il y a une double paire dans la liste
    {
        if (liste.Count < 4) return false;
        l = new List<Carte>();
        int nbPaire = 0;//Nombre de paire
        for (int i = 0; i< liste.Count-1; i++)
        {
            for(int j = i+1; j <liste.Count; j++)
            {
                if (liste[i].getValeur() == liste[j].getValeur())//Si les deux cartes sont identiques
                {
                    nbPaire++;
                    l.Add(liste[i]);
                    l.Add(liste[j]);
                    if (nbPaire == 2) return true;
                }
            }
        }
        return false;
    }
    public bool brelan(List<Carte> liste)//Retourne true s'il y a un brelan dans la liste
    {
        if (liste.Count < 3) return false;
        int occurence = 1;
        l = new List<Carte>();
        for(int i = 0; i < liste.Count-1; i++)
        {
            l.Add(liste[i]);
            for (int j = i+1; j < liste.Count; j++)
            {
                if(liste[i].valeur == liste[j].valeur)
                {
                    occurence++;
                    l.Add(liste[j]);
                    if (occurence == 3) return true;
                }
            }
            occurence = 1;
            l.Clear();
        }
        return false;
    }
    public bool quinte(List<Carte> liste)//Retourne true s'il y a une suite dans liste
    {
        if (liste.Count < 5) return false;
        l = new List<Carte>();
        bool a, b, c, d;
        a = b = c = d = false;
        foreach(Carte c1 in liste)
        {
            foreach(Carte c2 in liste)
            {
                if (!c1.Equals(c2))
                {
                    if(c1.valeur+4 == c2.valeur)
                    {
                        a = true;
                        l.Add(c2);
                    }
                    if (c1.valeur + 3 == c2.valeur)
                    {
                        b = true;
                        l.Add(c2);
                    }
                    if (c1.valeur + 2 == c2.valeur)
                    {
                        c = true;
                        l.Add(c2);
                    }
                    if (c1.valeur + 1 == c2.valeur)
                    {
                        d = true;
                        l.Add(c2);
                    }
                    if (a && b && c && d) return true;
                }
                a = b = c = d = false;
                l.Clear();
            }
        }
        return false;
    }
    public bool couleur(List<Carte> liste)//Retourne true s'il y a une couleur dans la liste
    {
        if (liste.Count < 5) return false;
        int couleur = 1;
        l = new List<Carte>();
        for (int i = 0; i < liste.Count - 4; i++)
        {
            l.Add(liste[i]);
            for (int j = i + 1; j < liste.Count; j++)
            {
                if(liste[i].couleur == liste[j].couleur)
                {
                    l.Add(liste[j]);
                    couleur++;
                    if (couleur == 5) return true;
                }
            }
            couleur = 1;
            l.Clear();
        }
        return false;
    }
    public bool full(List<Carte> liste)//Retourne true s'il y a un full dans liste
    {
        if (liste.Count < 5) return false;
        List<Carte> copie = new List<Carte>();
        List<Carte> tempo = new List<Carte>();
        l = new List<Carte>();
        //int occurence = 0;
        foreach (Carte c in liste)
        {
            copie.Add(c);
        }
        if (paire(liste))
        {
            foreach(Carte c in l)
            {
                tempo.Add(c);
                copie.Remove(c);
            }
            if (brelan(copie))
            {
                foreach(Carte c in tempo)
                {
                    l.Add(c);
                }
                return true;
            }
        }
        return false;
        /*foreach (Carte c1 in copie)
        {
            foreach (Carte c2 in copie)
            {
                if (!c1.Equals(c2))
                {
                    if (c1.getValeur() == c2.getValeur())
                    {
                        occurence++;
                        if (occurence == 3)
                        {
                            Valeur v = c1.getValeur();
                            foreach (Carte c3 in liste)
                            {
                                if (c3.getValeur() != v)
                                {
                                    foreach (Carte c4 in liste)
                                    {
                                        if (!c3.Equals(c4))
                                        {
                                            if (c3.getValeur() == c4.getValeur())
                                                return true;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            occurence = 1;
        }
        return false;*/
    }
    public bool carre(List<Carte> liste)//Retourne true s'il y a un carré dans la liste
    {
        if (liste.Count < 4) return false;
        int occurence = 1;
        for (int i = 0; i < liste.Count - 4; i++)
        {
            l.Add(liste[i]);
            for (int j = i + 1; j < liste.Count; j++)
            {
                if (liste[i].valeur == liste[j].valeur)
                {
                    occurence++;
                    l.Add(liste[j]);
                    if (occurence == 4) return true;
                }
            }
            l.Clear();
            occurence = 1;
        }
        return false;
    }
    public bool quinteFlush(List<Carte> liste)//Retourne true s'il y a une quinte flush dans liste
    {
        if (liste.Count < 5) return false;
        l = new List<Carte>();
        List<Carte> tempo = new List<Carte>();
        if (quinte(liste))
        {
            foreach(Carte c in l)
            {
                tempo.Add(c);
            }
            if (couleur(tempo)) return true;
        }
        return false;
    }
    public bool quinteFlushRoyale(List<Carte> liste)//Retourne true s'il y a une quinte flush royale dans liste
    {
        if (liste.Count < 5) return false;
        bool a, k, q, j, t;
        a = k = q = j = t = true;
        List<Carte> l2 = new List<Carte>();
        foreach(Carte c1 in liste)
        {
            if(c1.getValeur() == Valeur.As && a)
            {
                l2.Add(c1);
            }
            if (c1.getValeur() == Valeur.Roi && k)
            {
                l2.Add(c1);
            }
            if (c1.getValeur() == Valeur.Dame && q)
            {
                l2.Add(c1);
            }
            if (c1.getValeur() == Valeur.Vallee && j)
            {
                l2.Add(c1);
            }
            if (c1.getValeur() == Valeur.Dix && t)
            {
                l2.Add(c1);
            }
        }
        if (couleur(l2))
        {
            l = l2;
            return true;
        }
        return false;
    }
    public static Carte max(List<Carte> liste)
    {
        Carte max = liste[0];
        for(int i = 1; i < liste.Count; i++)
        {
            if (liste[i].superieurA(max)) max = liste[i];
        }
        return max;
    }
}
