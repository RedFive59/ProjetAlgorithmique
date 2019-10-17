using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeu : MonoBehaviour
{
    private Grille<Case> grille;

    void Start()
    {
        remplirSudokuVide();
        GridManager g = new GridManager(grille);
        Transform parent = GameObject.Find("GridManager").transform;
        g.GenerateGrid(0f, 0f, parent);
    }



    void remplirSudokuVide()
    {
        if (grille == null)
        {
            grille = new Grille<Case>(9, 9);
            Case defaut = new Case(0, grille);
            grille.setVal(defaut);

            int val;
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < 9; i++)
                {
                    val = (int)Random.Range(1, 9);
                    Case c = grille.getVal(i, j);
                    while (!c.verifValeur(val, i, j))
                    {
                        val = (int)Random.Range(1, 9);
                    }
                    Debug.Log("grille[" + i + "," + j + "] = " + val);
                    c.setValeur(val);
                }
            }
        }
    }
}
