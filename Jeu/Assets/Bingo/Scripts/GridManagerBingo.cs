using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridManagerBingo
{
    private GameObject tileReference;
    private int ligne, colonne;
    private float espacement = 1.05f;
    public Sprite sprite;
    private Grille<int> grille;
    private int ind;

    public GridManagerBingo(Grille<int> grille, int ind)
    {
        tileReference = GameObject.Find("TilePrefab");
        this.grille = grille;
        this.colonne = grille.getCols();
        this.ligne = grille.getRows();
        this.ind = ind;
    }

    public GridManagerBingo()
    {
        tileReference = GameObject.Find("TilePrefab");
    }

    //fonction qui affiche la grille dans unity
    public void GenerateGrid(float posX, float posY, Transform parent)
    {
        for (int i = 0; i < this.ligne; i++)
        {
            for (int j = 0; j < this.colonne; j++)
            {
                Vector3 pos = new Vector3(posX + (j * espacement - (this.colonne - 1) * espacement / 2), posY + (i * -espacement - (this.ligne - 1) * -espacement / 2), 0);
                GameObject tile = UnityEngine.Object.Instantiate(tileReference, pos, tileReference.transform.rotation, parent);
                tile.transform.GetComponent<RectTransform>().position.z = 0f;
                tile.name = "Case " + ind + ": " + i + "_" + j;
                afficher(i, j, tile);
            }
        }
    }

    //fonction qui affiche une valeur dans la case "CaseTirage"
    public void GenerateVal(float posX, float posY, Transform parent)
    {
        Vector3 pos = new Vector3(posX, posY, 0);
        GameObject tile = UnityEngine.Object.Instantiate(tileReference, pos, tileReference.transform.rotation, parent);
        tile.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        tile.name = "CaseTirage";
        tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 28.0f;
        afficher(tile, "");
    }

    //fonction qui met à jour les valeurs de la grille dans unity
    public void UpdateGrid()
    {
        for (int i = 0; i < this.ligne; i++)
        {
            for (int j = 0; j < this.colonne; j++)
            {
                GameObject tile = GameObject.Find("Case" + i + "_" + j);
                afficher(i, j, tile);
            }
        }
    }

    //fonction qui met a jour la valeur dans la case "CaseTirage"
    public void UpdateVal(int val)
    {
        GameObject tile = GameObject.Find("CaseTirage");
        afficher(tile, val.ToString());
    }

    //fonctionne qui affecte une valeur au text contenu dans la case
    //ou affiche celle-ci en noir si elle est vide
    private void afficher(int i, int j, GameObject tile)
    {
        if (this.grille.getVal(i, j) != -1)
            afficher(tile, this.grille.getVal(i, j).ToString());
        else
        {
            tile.GetComponent<SpriteRenderer>().color = Color.gray;
            afficher(tile, "");
        }
    }

    //fonction qui affiche la valeur val dans la case
    private void afficher(GameObject tile, string val)
    {
        tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = val;
    }
}