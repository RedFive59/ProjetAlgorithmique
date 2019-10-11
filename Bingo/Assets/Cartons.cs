using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartons : Grille<int>
{
    private int nbCaseFullLigne = 4;

    public Cartons(int n) : base(n)
    {
    }

    public Cartons(int n, int m) : base(n, m)
    {
    }

    public void initVal()
    {
        int[] t = new int[nbCaseFullLigne];

        for (int i = 0; i < this.rows; i++)
        {
            genCacheeRand(t, this.cols, 0);
            for(int j = 0; j < t.Length; j++)
            {
                this.setVal(i, t[j], -1);
            }
        }
    }

    private void genCacheeRand(int[] t, int max, int min)
    {
        int ind;
        for (int i = 0; i < t.Length; i++)
        {
            do
            {
                ind = Random.Range(min, max);
            } while (estdans(ind, t) || indColle(ind, t));
            t[i] = ind;
        }
    }

    public bool indColle(int x, int[] t)
    {
        for(int i = 0; i < t.Length; i++)
        {
            if ((x - 1 == t[i])) return true;
        }
        return false;
    }

    public bool estdans(int x, int[] t)
    {
        foreach(int i in t)
        {
            if (i == -1) continue;
            if (x == i) return true;
        }
        return false;
    }

    /*public bool sontdans(int[] val, Bingo[] b, int ind)
    {
        int[] colonne = new int[this.rows];
        foreach(Bingo grille in b)
        {
            colonne = grille.getCol(ind);
            for (int j = 0; j < val.Length; j++)
            {
                if (colonne[j] != -1)
                {
                    if (estdans(val[j], colonne))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    
    public bool valDansGrilles(int[] val, Bingo[] b, int col)
    {
        for(int i = 0; i < b.Length; i++)
        {
            for(int j = 0; j < val.Length; j++)
            {
                if (b[i].getVal(j, col) != -1)
                {
                    if (estdans(val[j], b[i].getCol(col)))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void ajoutVal(Bingo[] b)
    {
        Debug.Log("test");
        int[] val = new int[this.rows];

        for (int i = 0; i < this.cols; i++)
        {
            do
            {
                genRand(val, this.cols + i * 10, i * 10);
            } while (valDansGrilles(val, b, i));

            for(int j = 0; j < this.rows; j++)
            {
                if(this.getVal(j, i) != -1)
                    this.setVal(j, i, val[j]);
            }
        }
    }
 
    private void genRand(int[] t, int max, int min)
    {
        int ind;
        for (int i = 0; i < t.Length; i++)
        {
            do
            {
                ind = Random.Range(min, max);
            } while (estdans(ind, t));
            t[i] = ind;
        }
    }

    */

    public bool valCorrect(Cartons[] b)
    {
        int cpt;
        for(int i = 0; i < this.cols; i++)
        {
            cpt = 0;
            for(int j = 0; j < b.Length; j++)
            {
                for(int k = 0; k < this.rows; k++)
                {
                    if(b[j].getVal(k, i) == -1)
                    {
                        cpt++;
                    }
                }
            }
            if ((cpt < 2) || (cpt > 6)) return false;
        }

        return true;
    }

    public void afficherMat()
    {
        for(int j = 0; j < this.cols; j++)
        {
            for(int i = 0; i < this.rows; i++)
            {
                Debug.Log("ligne:"+i+" colonne:"+j+" valeur:"+this.getVal(i,j));
            }
        }
    }
}
