using System;
using System.Collections;

public class Main
{
    //Attributs
    private ArrayList cartes;
    //Constructeurs
	public Main()
	{
        this.cartes = new ArrayList();
	}
    public Main(ArrayList cartes)
    {
        this.cartes = cartes;
    }
    //Méthodes
    public void setCartes(ArrayList cartes)//Définie la liste passée en paramètre comme la nouvelle liste de cartes de la main;
    {
        this.cartes = cartes;
    }
    public ArrayList getCartes()//Renvoie la liste de cartes de la main;
    {
        return this.cartes;
    }
    public void ajouterCarte(Carte c)//Ajoute la carte à la liste de cartes;
    {
        this.cartes.Add(cartes);
    }
    public Carte jouerCarte(Carte c)//Supprime la carte que l'on vient de jouer et la retourne;
    {
        this.cartes.Remove(c);
        return c;
    }
    public bool equals(Main m)
    {
        if (this.cartes == m.getCartes()) return true;
        return false;
    }
}
