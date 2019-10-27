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
    public void initGrille(List<int> ordreLigne)
    {
        int[] t = new int[nbCaseFullLigne];
        List<int> caseLibre = new List<int>();

        for (int i = 0; i < this.cols; i++) caseLibre.Add(i);

        foreach (int i in ordreLigne)
        {
            indiceCasesVide(t, caseLibre, 0);
            for(int j = 0; j < t.Length; j++)
            {
                this.setVal(i, t[j], -1);
            }
        }
    }

    //donne un tableau contenant des cases vide
    private void indiceCasesVide(int[] t, List<int> caseLibre, int min)
    {
        int ind;
        if(caseLibre.Count > 1)
        {
            for (int i = 0; i < t.Length; i++)
            {
                ind = Random.Range(min, caseLibre.Count);
                t[i] = caseLibre[ind];
                caseLibre.RemoveAt(ind);
            }
        }
        else
        {
            List<int> caseLibre2 = new List<int>();
            for (int i = 0; i < this.cols; i++) caseLibre2.Add(i);

            t[0] = caseLibre[0];

            for (int i = 1; i < 3; i++)
            {
                ind = Random.Range(min, caseLibre2.Count);
                t[i] = caseLibre2[ind];
                caseLibre2.RemoveAt(ind);
            }
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
