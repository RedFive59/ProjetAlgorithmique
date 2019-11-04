using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        GameObject.Find("Infos").GetComponent<TextMeshProUGUI>().text = "Difficulty : " + difficulte + "           Level : " + numGrille;
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
            if(grille.verifGrille()) UIManager.finishGame();
        }
    }

    internal GrilleSudoku getGrille()
    {
        return grille;
    }
}
