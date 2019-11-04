using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Jeu : MonoBehaviour
{
    private UIManager UIManager;
    private Transform parent;
    private GrilleSudoku grille = null;
    string numGrille;
    string difficulte;

    void Start()
    {
        string[] level = SelectionNiveauAleatoire();
        difficulte = level[0];
        numGrille = level[1];
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

    private string[] SelectionNiveauAleatoire()
    {
        string[] res = new string[2];
        int difficulty = UnityEngine.Random.Range(1, 4);
        switch (difficulty)
        {
            case 1:
                res[0] = "Easy";
                break;
            case 2:
                res[0] = "Medium";
                break;
            case 3:
                res[0] = "Hard";
                break;
        }
        int cpt = 0;
        string directoryPath = Path.Combine(Application.dataPath, ("Sudoku/Levels/" + res[0] + "/"));
        var info = new DirectoryInfo(directoryPath);
        var fileInfo = info.GetFiles();
        foreach (FileInfo f in fileInfo) if(f.Extension == ".json") cpt++;
        int level = UnityEngine.Random.Range(1, cpt+1);
        res[1] = level.ToString();
        return res;
    }

    internal GrilleSudoku getGrille()
    {
        return grille;
    }
}
