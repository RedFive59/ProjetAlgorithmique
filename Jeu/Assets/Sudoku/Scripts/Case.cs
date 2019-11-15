using System;
using System.Collections.Generic;

class Case
{
    private List<int> listIndice; // Liste dans laquelle apparaît les indices présents dans la case
    public int valeur; // Valeur de la case
    public Boolean changeable; // Booléen afin de savoir si la case est changeable ou pas
    public Boolean selected = false; // Booléen pour savoir si la case est sélectionné dans le jeu
    public Boolean highlighted = false; // Booléen pour savoir si la case est sélectionné dans le jeu

    // Constructeurs
    public Case()
    {
        changeable = true;
        listIndice = new List<int>();
    }

    public Case(int valeur)
    {
        changeable = true;
        this.valeur = valeur;
        listIndice = new List<int>();
    }

    // Getters & Setters
    public void setValeur(int val)
    {
        if (val > 0 && val < 10)
        {
            valeur = val;
        }
    }

    public void ajoutIndice(int i)
    {
        if (listIndice.Contains(i)) return;
        listIndice.Add(i);
    }

    public void retraitIndice(int i)
    {
        if (listIndice.Contains(i)) listIndice.Remove(i);
    }

    public void retraitIndices()
    {
        listIndice.Clear();
    }

    public bool indiceIn(int i)
    {
        if (listIndice.Contains(i)) return true;
        return false;
    }

    // Chaine de caractères utilisées pour afficher les indices sur le jeu
    public string indicesToString()
    {
        string res = "";
        for(int i = 1; i<10; i++)
        {
            if (indiceIn(i)) res += (i + " ");
            else res += "   ";
            if (i % 3 == 0 && i != 9) res += "\n";
        }
        return res;
    }

    // Override du ToString pour ne récupérer que la valeur de la case pour l'affichage InGame
    public override string ToString()
    {
        if(valeur != 0) return "" + this.valeur;
        return "";
    }
}