using System;
using System.Collections;

public class Mise
{
    //Attributs
    private ArrayList jetons;
    private int idJoueur;
    //Constructeurs
    public Mise(ArrayList mise, int id)
    {
        this.idJoueur = id;
        this.jetons = mise;
    }
    public Mise()
    {
        this.idJoueur = -1;
        this.jetons = new ArrayList();
    }
    public Mise(int id)
    {
        this.idJoueur = id;
        this.jetons = new ArrayList();
    }
    //Méthodes
    public void augmenterMise(Jeton montant)//Augmente la mise du montant "montant";
    {
        this.jetons.Add(montant);
    }
    public void augmenterMise(Mise montant)//Additione les deux mises;
    {
        foreach(Jeton j in montant.getJetons())
        {
            this.augmenterMise(j);
        }
    }
    public void augmenterMise(ArrayList montant)//Ajoute la liste de jeton pour augmenter la mise;
    {
        foreach(Jeton j in montant)
        {
            this.augmenterMise(j);
        }
    }
    public bool diminuerMise(Jeton montant)//Retire le jeton de la mise s'il y est et retourne true sinon retourne false;
    {
        foreach(Jeton j in this.jetons)
        {
            if (j.memeValeur(montant))
            {
                jetons.Remove(montant);
                return true;
            }
        }
        return false;
    }
    public bool transferer(Mise m, Jeton j)//Transfert le jeton j de la mise actuel vers la mise m;
    {
        if (diminuerMise(j))
        {
            augmenterMise(j);
            return true;
        }
        return false;
    }
    public void fusionner(Mise m)//Transfert les jetons de la mise m vers la mise actuelle
    {
        foreach(Jeton j in m.getJetons())
        {
            if (!(this.jetons.Contains(j)))
            {
                this.jetons.Add(j);
                m.getJetons().Remove(j);
            }
        }
    }
    public void setMise(ArrayList mise, int idJoueur)//Change le montant de la mise et l'ID Joueur;
    {
        this.jetons = mise;
        this.idJoueur = idJoueur;
    }
    public void setMise(ArrayList mise)//Change le montant de la mise pour "mise";
    {
        this.jetons = mise;
    }
    public void setJoueur(int id)//Change l'ID Joueur de la mise pour l'ID indiquer;
    {
        this.idJoueur = id;
    }
    public ArrayList getJetons()//Retourne le montant de la mise;
    {
        return this.jetons;
    }
    public int getJoueur()//Retourne l'ID du joueur à qui la mise appartient;
    {
        return this.idJoueur;
    }
    public bool equals(Mise m)//Retourne true si les deux mises ont le même montant et ID joueur;
    {
        if((this.jetons == m.getJetons())&&(this.idJoueur == m.getJoueur()))
            return true;
        return false;
    }
    public int getMontant()//Retourne un entier correspondant à la somme totale des jetons de la mise;
    {
        int somme = 0;
        foreach(Jeton j in this.jetons)
        {
            somme += (int)j.getValeur();
        }
        return somme;
    }
    public bool memeMontant(Mise m)//Retourne true si les deux mises ont le même montant
    {
        if (this.getMontant() == m.getMontant())
           return true;
        return false;
    }
    public bool memeJoueur(Mise m)//Retourne true si les deux mises appartiennent au même joueur
    {
        if (this.idJoueur == m.getJoueur())
            return true;
        return false;
    }
}
