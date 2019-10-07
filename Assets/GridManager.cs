using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    private GameObject tileReference;
    private int ligne = 9, colonne = 2;
    private float espacement = 1.1f;
    public Sprite sprite;
    private Grille<int> grille2;

    void Start()
    {
        tileReference = GameObject.Find("TilePrefab");
        grille2 = new Grille<int>(ligne, colonne);
        grille2.setVal(10);
        GenerateGrid();
        grille2.ajoutMat(1, 1, 1);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            UpdateGrid();
        }
    }

    private void GenerateGrid()
    {
        Transform parent = GameObject.Find("GridManager").transform;
        for (int i = 0; i < this.ligne; i++)
        {
            for (int j = 0; j < this.colonne; j++)
            {
                Vector2 pos = new Vector2(i * espacement - this.ligne / 2, j * espacement - this.colonne / 2);
                GameObject tile = Instantiate(tileReference, pos, tileReference.transform.rotation, parent);
                tile.name = "Case" + i + "_" + j;
                tile.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = grille2.getVal(i, j).ToString();
                //Debug.Log("grille.tab[" + i + "][" + j + "] : " + grille2.getVal(i, j));
            }
        }
        //Debug.Log("Grid created");
    }

    private void UpdateGrid()
    {
        for (int i = 0; i < this.ligne; i++)
        {
            for (int j = 0; j < this.colonne; j++)
            {
                GameObject tile = GameObject.Find("Case" + i + "_" + j);
                tile.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = grille2.getVal(i, j).ToString();
            }
        }
    }
}