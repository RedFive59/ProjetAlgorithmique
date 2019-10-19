using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeu : MonoBehaviour
{
    private GridManager g;
    private Transform parent;
    private GrilleSudoku grille = null;

    void Start()
    {
        grille = new GrilleSudoku(9, 9);
        grille.initVal(0);
        g = new GridManager(grille);
        parent = GameObject.Find("GridManager").transform;
        g.GenerateGrid(0f, 0f, parent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            remplirSudoku();
            g.UpdateGrid();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            grille.remplirGrilleAvecTrou();
            g.UpdateGrid();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Grille correct ? : " + grille.verifGrille());
        }
    }

    public void remplirSudoku()
    {
        int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> list = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (list.Count == 0) list.AddRange(array);
                int rand = UnityEngine.Random.Range(0, list.Count);
                int val = list[rand];
                list.RemoveAt(rand);
                grille.getVal(i, j).setValeur(val);
            }
        }
    }
}
