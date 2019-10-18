using System;

public class Mise
{
    //Attributs
    private int mise;
    private int idJoueur;
    //Constructeurs
    public Mise(int mise, int id)
    {
        this.idJoueur = id;
        this.mise = mise;
    }
    public Mise()
    {
        this.idJoueur = -1;
        this.mise = 0;
    }
    public Mise(int id)
    {
        this.idJoueur = id;
        this.mise = 0;
    }
    //Méthodes
    public void augmenterMise(int montant)//Augmente la mise du montant "montant";
    {
        this.mise += montant;
    }
    public void setMise(int mise, int idJoueur)//Change le montant de la mise et l'ID Joueur;
    {
        this.mise = mise;
        this.idJoueur = idJoueur;
    }
    public void setMise(int mise)//Change le montant de la mise pour "mise";
    {
        this.mise = mise;
    }
    public void setJoueur(int id)//Change l'ID Joueur de la mise pour l'ID indiquer;
    {
        this.idJoueur = id;
    }
    public int getMise()//Retourne le montant de la mise;
    {
        return this.mise;
    }
    public int getJoueur()//Retourne l'ID du joueur à qui la mise appartient;
    {
        return this.idJoueur;
    }
    public bool equals(Mise m)//Retourne true si les deux mises ont le même montant et ID joueur;
    {
        if((this.mise == m.getMise)&&(this.mise == m.getJoueur))
            return true;
        return false;
    }
    public bool memeMontant(Mise m)//Retourne true si les deux mises ont le même montant
    {
        if (this.mise == m.getMise)
           return true;
        return false;
    }
    public bool memeJoueur(Mise m)//Retourne true si les deux mises appartiennent au même joueur
    {
        if (this.idJoueur == m.getJoueur)
            return true;
        return false;
    }
}
