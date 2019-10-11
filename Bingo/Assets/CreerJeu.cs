using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreerJeu : MonoBehaviour
{
    private int ligne = 3, colonne = 9, nbgrilles = 4;
    private Cartons[] grilles;
    private GridManager[] grid;

    void Start()
    {
        nbgrilles = 4;
        GameObject tile = GameObject.Find(0 + ":Case" + 0 + "_" + 0);
        grilles = new Cartons[nbgrilles];
        grid = new GridManager[nbgrilles];

        if (!tile)
        {
            creerBingo();
        }
    }

    private void creerBingo()
    {
        grilles = new Cartons[nbgrilles];
        do
        {
            for (int i = 0; i < nbgrilles; i++)
            {
                grilles[i] = new Cartons(ligne, colonne);
                grilles[i].initVal(0);
                grilles[i].initVal();
                grid[i] = new GridManager(grilles[i]);
            }
        }
        while (!grilles[0].valCorrect(grilles));

        ajoutVal();

        Transform parent;
        for (int i = 0; i < nbgrilles; i++)
        {
            parent = GameObject.Find("GridManager" + i).transform;
            grid[i].GenerateGrid(parent.position.x, parent.position.y, parent);
        }
    }

    private void ajoutVal()
    {
        int[] vals = new int[this.ligne * this.nbgrilles];

        for (int i = 0; i < this.colonne; i++)
        {
            valsDansGrilles(vals, i);
            genRand(vals, 9 + i * 10, i * 10);
            trieVal(vals);
            setValCol(vals, i);
        }
    }

    private void valsDansGrilles(int[] vals, int col)
    {
        int i = 0;
        while (i < vals.Length)
        {
            for (int j = 0; j < this.grilles.Length; j++)
            {
                for (int k = 0; k < this.ligne; k++)
                {
                    vals[i] = this.grilles[j].getVal(k, col);
                    i++;
                }
            }
        }
    }

    private bool estdans(int x, int[] t)
    {
        foreach (int i in t)
        {
            if (i == -1) continue;
            if (x == i) return true;
        }
        return false;
    }

    private void genRand(int[] t, int max, int min)
    {
        int ind;
        for (int i = 0; i < t.Length; i++)
        {
            if (t[i] != -1)
            {
                do
                {
                    ind = UnityEngine.Random.Range(min, max);
                } while (estdans(ind, t));
                t[i] = ind;
            }
        }
    }

    private void setValCol(int[] vals, int col)
    {
        int i = 0;
        while (i < vals.Length)
        {
            for (int j = 0; j < this.grilles.Length; j++)
            {
                for (int k = 0; k < this.ligne; k++)
                {
                    this.grilles[j].setVal(k, col, vals[i]);
                    i++;
                }
            }
        }
    }

    private void trieVal(int[] val)
    {
        int min, temp;
        for (int i = 0; i < val.Length - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < val.Length; j++)
            {
                if (val[j] != -1)
                {
                    if (val[j] < val[min])
                    {
                        temp = val[j];
                        val[j] = val[min];
                        val[min] = temp;
                    }
                }
            }
        }
    }

    public void afficher(int[] val)
    {
        foreach (int i in val) Debug.Log("valeur: " + i);
    }
}
