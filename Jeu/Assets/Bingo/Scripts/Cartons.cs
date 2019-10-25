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

    //initialise la grille
    public void initGrille()
    {
        int[] t = new int[nbCaseFullLigne];

        for (int i = 0; i < this.rows; i++)
        {
            indiceCasesVide(t, this.cols, 0);
            for(int j = 0; j < t.Length; j++)
            {
                this.setVal(i, t[j], -1);
            }
        }
    }

    //donne un tableau contenant les indices des cases vide
    private void indiceCasesVide(int[] t, int max, int min)
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

    //verifie si la valeur x est dans le tableau t
    public bool estdans(int x, int[] t)
    {
        foreach(int i in t)
        {
            if (i == -1) continue;
            if (x == i) return true;
        }
        return false;
    }

    //copie les valeurs de grille
    public void copie(Cartons grille)
    {
        for (int i = 0; i < this.rows; i++)
        {
            for (int j = 0; j < this.cols; j++)
            {
                this.setVal(i, j, grille.getVal(i, j));
            }
        }
    }

    //afficha console de la matrice
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
