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
    private int i = -1, j = -1;
    public GameObject finishCanvas;

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
        Color blue = new Color32(0, 110, 255, 255);
        Color white = new Color32(205, 205, 205, 255);
        Color goodValue = new Color32(80, 170, 255, 255);
        Color badValue = new Color32(170, 20, 20, 255);
        Color unchangeableCase = new Color32(0, 0, 0, 255);
        if (selectedCase.changeable == false)
        {
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.grille.getVal(i, j).ToString();
            tile.GetComponent<Button>().interactable = false;
            tile.GetComponent<Image>().color = unchangeableCase;
        } else {
            if (selectedCase.valeur == 0)
            {
                // Valeur de la case à 0
                tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            }
            else
            {
                Color32 textColor = goodValue;
                //Debug.Log("grille.verifLigne(" + i + ") = " + grille.verifLigne(i));
                //Debug.Log("grille.verifColonne(" + j + ") = " + grille.verifColonne(j));
                //Debug.Log("grille.verifCarre(grille.numCarre(" + i + ", " + j + ")) = " + grille.verifCarre(grille.numCarre(i, j)));
                if (!grille.verifLigne(i) || !grille.verifColonne(j) || !grille.verifCarre(grille.numCarre(i, j))) textColor = badValue;
                // Valeur de la case entre 1 et 9
                tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.grille.getVal(i, j).ToString();
                tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = textColor;
            }
            if (selectedCase.selected)
            {
                // Case sélectionnée
                tile.GetComponent<Image>().color = blue;
            }
            else
            {
                // Case non sélectionnée
                tile.GetComponent<Image>().color = white;
            }
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
        for (int n = 1; n < 10; n++)
        {
            GameObject tile = UnityEngine.Object.Instantiate(buttonReference, buttonReference.transform.position, buttonReference.transform.rotation, GameObject.Find("ButtonManager").transform);
            tile.name = "Bouton_"+ n;
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = n.ToString();
            tile.transform.position = new Vector3((float)(tile.transform.position.x + 1.1*(n-1)), tile.transform.position.y);
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

    public void buttonClick()
    {
        string nom = EventSystem.current.currentSelectedGameObject.name;
        string ind = Regex.Replace(nom, "[^0-9]", "");
        int buttonVal = ind[0] - 48;
        if (i != -1 && j != -1)
        {
            //Debug.Log("Avant appui du bouton " + buttonVal + ", la case " + i + " " + j + " vaut " + grille.getVal(i, j).valeur);
            grille.getVal(i, j).setValeur(buttonVal);
            //Debug.Log("Après appui du bouton " + buttonVal + ", la case " + i + " " + j + " vaut " + grille.getVal(i, j).valeur);
            UpdateGrid();
            if (grille.verifGrille()) finishGame();
        }
    }

    public void eraseValue()
    {
        if (i != -1 && j != -1)
        {
            grille.getVal(i, j).valeur = 0;
            UpdateGrid();
        }
    }

    public void finishGame()
    {
        for(int n = 1; n < 10; n++)
        {
            GameObject.Find("Bouton_" + n).GetComponent<Button>().interactable = false;
        }
        for (int k = 0; k < 9; k++)
            for (int l = 0; l < 9; l++)
                GameObject.Find("Case" + k + "_" + l).GetComponent<Button>().interactable = false;
        GameObject.Find("Eraser").GetComponent<Button>().interactable = false;
        finishCanvas.SetActive(true);
    }
}
