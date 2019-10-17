using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeu : MonoBehaviour
{
    GridManager g;
    Transform parent;
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
            remplirSudokuVide();
            g.UpdateGrid();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i<10; i++)
            {
                int a = (int)Random.Range(1,10);
                Debug.Log(a);
            }
        }
    }

    public void remplirSudokuVide()
    {
        int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> list = new List<int>();
        int val = Random.Range(1, 10);
        if (grille == null)
        {
            grille = new GrilleSudoku(9, 9);
            grille.initVal(0);
        }
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                //Problème de loop à check
                do {
                    if (list.Count == 0) list.AddRange(array);
                    int rand = Random.Range(0, list.Count);
                    val = list[rand];
                    list.RemoveAt(rand);
                    Debug.Log("grille[" + i + "," + j + "] = " + val);
                } while (!grille.getVal(i, j).verifValeur(val, i, j));
                grille.getVal(i, j).setValeur(val);
            }
        }
    }
}
