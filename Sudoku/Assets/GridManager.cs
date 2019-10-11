using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Classe à ajouter sur chacun de vos sous-projets pour afficher quelque chose à l'écran
class GridManager
{
    private GameObject tileReference;
    private int ligne, colonne;
    private float espacement = 1.1f;
    private Grille<int> grille;

    public GridManager(Grille<int> grille)
    {
        tileReference = GameObject.Find("TilePrefab");
        this.grille = grille;
        this.colonne = grille.getCols();
        this.ligne = grille.getRows();
    }

    public void GenerateGrid(float posX, float posY, Transform parent)
    {
        for (int i = 0; i < this.ligne; i++)
        {
            for (int j = 0; j < this.colonne; j++)
            {
                Vector2 pos = new Vector2(posX + (j * espacement - (this.colonne - 1) * espacement / 2), posY + (i * -espacement - (this.ligne - 1) * -espacement / 2));
                GameObject tile = UnityEngine.Object.Instantiate(tileReference, pos, tileReference.transform.rotation, parent);
                tile.name = "Case" + i + "_" + j;
                afficher(i, j, tile);
            }
        }
    }

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

    // Fonction à modifier selon vos besoins
    private void afficher(int i, int j, GameObject tile)
    {
        if(this.grille.getVal(i, j) != -1) 
            tile.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = this.grille.getVal(i, j).ToString();
        else
            tile.GetComponent<SpriteRenderer>().color = Color.black;
    }
}