using System;
using System.Collections.Generic;

class Case
{
    private List<int> listIndice;
    public int valeur;
    public Boolean changeable;
    public Boolean selected = false;

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

    public override string ToString()
    {
        return "" + this.valeur;
    }
}