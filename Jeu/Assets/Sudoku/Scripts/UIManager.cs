using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameObject tileReference;
    private int ligne, colonne;
    private float espacement = 1.1f;
    private GrilleSudoku grille;
    private int i, j;

    public void Init()
    {
        tileReference = GameObject.Find("TilePrefab");
        grille = GameObject.Find("Jeu").GetComponent<Jeu>().getGrille();
        colonne = grille.getCols();
        ligne = grille.getRows();
        generateNumberSelection();
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
        tileReference.SetActive(false);
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

    private void afficher(int i, int j, GameObject tile)
    {
        Case selectedCase = grille.getVal(i, j);
        Color blue = new Color(40, 100, 180, 255);
        Color white = new Color(205, 205, 205, 255);
        Color selectedColor = new Color(120, 150, 170);
        Color defaultColor = new Color(35, 35, 35, 255);
        if (selectedCase.changeable == false)
        {
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.grille.getVal(i, j).ToString();
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = white;
            tile.GetComponent<Button>().interactable = false;
        }
        else
        {
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = blue;
        }
        if (selectedCase.valeur == 0)
        {
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
        }
        if (selectedCase.selected)
        {
            tile.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            tile.GetComponent<Image>().color = Color.white;
        }
    }

    public void setTileSelected()
    {
        string nom = EventSystem.current.currentSelectedGameObject.name;
        string ind = Regex.Replace(nom, "[^0-9]", "");
        i = ind[0] - 48;
        j = ind[1] - 48;
        Case selectedCase = grille.getVal(i, j);
        deselectOther(i, j);
        selectedCase.selected = true;
    }

    public void generateNumberSelection()
    {
        GameObject buttonReference = GameObject.Find("ButtonPrefab");
        for (int i = 1; i < 10; i++)
        {
            GameObject tile = UnityEngine.Object.Instantiate(buttonReference, buttonReference.transform.position, buttonReference.transform.rotation, GameObject.Find("ButtonManager").transform);
            tile.name = "Bouton_"+ i;
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.ToString();
            tile.transform.position = new Vector3((float)(tile.transform.position.x + 1.1*(i-1)), tile.transform.position.y);
        }
        buttonReference.SetActive(false);
    }

    public void deselectOther(int i, int j)
    {
        for(int k = 0; k<9; k++)
        {
            for(int l=0; l<9; l++)
            {
                if (k != i || l != j) if (grille.getVal(k, l).selected) grille.getVal(k, l).selected = false;
            }
        }
    }
}
