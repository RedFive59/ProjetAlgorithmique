using System;

class Case
{
    private Boolean affichable;
    private int valeur;
    private Grille<Case> grille;

    public Case(int valeur, Grille<Case> grille)
    {
        affichable = false;
        this.valeur = valeur;
        this.grille = grille;
    }

    public Boolean getAffichable()
    {
        return affichable;
    }

    public int getValeur()
    {
        return valeur;
    }

    public void setAffichable(Boolean cond)
    {
        affichable = cond;
    }

    public Boolean verifValeur(int val, int i, int j)
    {
        if (verifLigne(val, i) && verifColonne(val, j) && verifCarre(val, i, j))
        {
            return true;
        }
        return false;
    }

    public void setValeur(int val)
    {
        if (val > 0 && val < 10) valeur = val;
    }

    public void setGrille(Grille<Case> g)
    {
        grille = g;
    }

    private Boolean verifLigne(int var, int nbLigne)
    {
        for(int n = 0; n<9; n++)
        {
            if (grille.getVal(nbLigne, n).getValeur() == var) return false;
        }
        return true;
    }

    private Boolean verifColonne(int var, int nbColonne)
    {
        for (int n = 0; n < 9; n++)
        {
            if (grille.getVal(n, nbColonne).getValeur() == var) return false;
        }
        return true;
    }

    private int numCarre(int i, int j)
    {
        if (i < 3)
        {
            if (j < 3)
            {
                return 1;
            }
            else
            {
                if (j < 6)
                {
                    return 4;
                }
                else
                {
                    return 7;
                }
            }
        }
        else
        {
            if (i < 6)
            {
                if (j < 3)
                {
                    return 2;
                }
                else
                {
                    if (j < 6)
                    {
                        return 5;
                    }
                    else
                    {
                        return 8;
                    }
                }
            }
            else
            {
                if (j < 3)
                {
                    return 3;
                }
                else
                {
                    if (j < 6)
                    {
                        return 6;
                    }
                    else
                    {
                        if (j < 9) return 9;
                        else return -1;
                    }
                }
            }
        }
    }

    private Boolean verifCarre(int var, int i, int j)
    {
        var numCarre = this.numCarre(i, j);
        if(numCarre != -1) return verifCarre(var, numCarre);
        return false;
    }

    private Boolean verifCarre(int var, int nCarre)
    {
        switch (nCarre)
        {
            case 1:
                for(int i = 0; i<3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            case 2:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            case 3:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            case 4:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            case 5:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            case 6:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            case 7:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            case 8:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            case 9:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        if (grille.getVal(i, j).getValeur() == var) return false;
                    }
                }
                break;
            default:
                return false;
        }
        return true;
    }

    public override string ToString()
    {
        return "" + this.getValeur();
    }
}