using System;
using System.Collections.Generic;

class Case
{
    private int valeur;
    private Boolean changeable;
    private List<int> listIndice;
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

    public Boolean getChangeable()
    {
        return changeable;
    }

    public void setChangeable(Boolean cond)
    {
        changeable = cond;
    }

    public int getValeur()
    {
        return valeur;
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
        return "" + this.getValeur();
    }
}