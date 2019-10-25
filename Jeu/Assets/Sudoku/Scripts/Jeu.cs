using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeu : MonoBehaviour
{
    private GridManager g;
    private Transform parent;
    private GrilleSudoku grille = null;
    private int i;
    private int j;

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
            Debug.Log("remplirMauvaixSudoku()");
            grille.remplirMauvaixSudoku();
            g.UpdateGrid();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("remplirGrilleAvecTrou()");
            grille.remplirGrilleAvecTrou();
            g.UpdateGrid();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Grille correct ? : " + grille.verifGrille());
        }
    }
}
