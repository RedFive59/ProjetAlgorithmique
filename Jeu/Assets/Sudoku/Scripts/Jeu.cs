using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeu : MonoBehaviour
{
    private UIManager UIManager;
    private Transform parent;
    private GrilleSudoku grille = null;
    int numGrille = 1;
    string difficulte = "Easy";

    void Start()
    {
        grille = new GrilleSudoku(9, 9);
        grille.initVal(0);
        grille.chargementGrille(numGrille, difficulte);
        UIManager = GameObject.Find("Jeu").GetComponent<UIManager>();
        UIManager.Init();
        parent = GameObject.Find("GridManager").transform;
        UIManager.GenerateGrid(0f, 0f, parent);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("remplirGrille()");
            grille.remplirGrille(numGrille, difficulte);
            UIManager.UpdateGrid();
            UIManager.finishGame();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Grille correct ? : " + grille.verifGrille());
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log("UpdateGrid()");
            UIManager.UpdateGrid();
        }
    }

    internal GrilleSudoku getGrille()
    {
        return grille;
    }
}
