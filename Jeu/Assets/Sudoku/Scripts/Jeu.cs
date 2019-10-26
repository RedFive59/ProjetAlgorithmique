using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeu : MonoBehaviour
{
    private UIManager UIManager;
    private Transform parent;
    private GrilleSudoku grille = null;
    private int i;
    private int j;

    void Start()
    {
        grille = new GrilleSudoku(9, 9);
        grille.initVal(0);
        grille.remplirGrilleAvecTrou();
        UIManager = GameObject.Find("Jeu").GetComponent<UIManager>();
        UIManager.Init();
        parent = GameObject.Find("GridManager").transform;
        UIManager.GenerateGrid(0f, 0f, parent);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("remplirMauvaixSudoku()");
            grille.remplirMauvaixSudoku();
            UIManager.UpdateGrid();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("remplirGrilleAvecTrou()");
            grille.remplirGrilleAvecTrou();
            UIManager.UpdateGrid();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Grille correct ? : " + grille.verifGrille());
        }
    }

    internal GrilleSudoku getGrille()
    {
        return grille;
    }
}
