using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameObject tileReference; // GameObject qui définit les cases
    private int ligne, colonne; // Nombre de lignes et de colonnes
    private float espacement = 1.1f; // Entier qui définit l'espacement entre 2 cases
    private GrilleSudoku grille; // Référence à la grille
    private int i = -1, j = -1; // Place de la case [i,j]
    public GameObject finishCanvas; // GameObject a afficher en fin de partie
    private bool notesActivated = false; // Défini l'activation des notes
    public string tempsFin; // Chaine de caractères pour la fin du timer
    private int cpt1 = 0, cpt2 = 0, cpt3 = 0, cpt4 = 0, cpt5 = 0, cpt6 = 0, cpt7 = 0, cpt8 = 0, cpt9 = 0;

    // Méthode permettant la détection d'entrées numériques sur le jeu
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.KeypadPeriod))
            eraseValue();
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            notesSwitch();
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            buttonClick(1);
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            buttonClick(2);
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            buttonClick(3);
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            buttonClick(4);
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            buttonClick(5);
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            buttonClick(6);
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            buttonClick(7);
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            buttonClick(8);
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            buttonClick(9);
    }

    // Méthode qui est appelé au start du script Jeu afin d'initialiser les objets
    public void Init()
    {
        tileReference = GameObject.Find("TilePrefab");
        grille = GameObject.Find("Jeu").GetComponent<Jeu>().getGrille();
        colonne = grille.getCols();
        ligne = grille.getRows();
        generateNumberSelection();
    }

    // Méthode qui permet la génération de toutes les cases de la grille centrale
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

    // Méthode qui permet de mettre à jour toutes la grille sur le jeu et de vérifier la possibilité de toucher aux boutons en bas ou pas
    public void UpdateGrid()
    {
        grille.sauvegardeGrille();
        cpt1 = 0; cpt2 = 0; cpt3 = 0; cpt4 = 0; cpt5 = 0; cpt6 = 0; cpt7 = 0; cpt8 = 0; cpt9 = 0;
        for (int i = 0; i < this.ligne; i++)
        {
            for (int j = 0; j < this.colonne; j++)
            {
                if (grille.getVal(i, j).valeur != 0)
                {
                    switch (grille.getVal(i, j).valeur)
                    {
                        case 1:
                            cpt1++;
                            break;
                        case 2:
                            cpt2++;
                            break;
                        case 3:
                            cpt3++;
                            break;
                        case 4:
                            cpt4++;
                            break;
                        case 5:
                            cpt5++;
                            break;
                        case 6:
                            cpt6++;
                            break;
                        case 7:
                            cpt7++;
                            break;
                        case 8:
                            cpt8++;
                            break;
                        case 9:
                            cpt9++;
                            break;
                        default:
                            break;
                    }
                    if (cpt1 == 9) switchBouton(1, false);
                    else switchBouton(1, true);
                    if (cpt2 == 9) switchBouton(2, false);
                    else switchBouton(2, true);
                    if (cpt3 == 9) switchBouton(3, false);
                    else switchBouton(3, true);
                    if (cpt4 == 9) switchBouton(4, false);
                    else switchBouton(4, true);
                    if (cpt5 == 9) switchBouton(5, false);
                    else switchBouton(5, true);
                    if (cpt6 == 9) switchBouton(6, false);
                    else switchBouton(6, true);
                    if (cpt7 == 9) switchBouton(7, false);
                    else switchBouton(7, true);
                    if (cpt8 == 9) switchBouton(8, false);
                    else switchBouton(8, true);
                    if (cpt9 == 9) switchBouton(9, false);
                    else switchBouton(9, true);
                    //Debug.Log(cpt1 + " " + cpt2 + " " + cpt3 + " " + cpt4 + " " + cpt5 + " " + cpt6 + " " + cpt7 + " " + cpt8 + " " + cpt9);
                }
                GameObject tile = GameObject.Find("Case" + i + "_" + j);
                afficher(i, j, tile);
            }
        }
    }

    // Méthode qui permet au jeu de définir différentes règles sur l'affichage afin d'être plus parlant pour l'utilisateur
    private void afficher(int i, int j, GameObject tile)
    {
        Case selectedCase = grille.getVal(i, j);
        Color blue = new Color32(0, 110, 255, 255);
        Color white = new Color32(205, 205, 205, 255);
        Color goodValue = new Color32(80, 170, 255, 255);
        Color badValue = new Color32(170, 20, 20, 255);
        Color unchangeableCase = new Color32(0, 0, 0, 255);
        if (selectedCase.changeable == false) // Case prédéfinie, préremplie
        {
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.grille.getVal(i, j).ToString();
            tile.GetComponent<Button>().interactable = false;
            tile.GetComponent<Image>().color = unchangeableCase;
        } else // Case qui peut être remplie
        {
            tile.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = grille.getVal(i, j).ToString();
            tile.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = grille.getVal(i, j).indicesToString();
            if (selectedCase.valeur != 0)
            {
                // Valeur de la case entre 1 et 9
                Color32 textColor = goodValue;
                if (!grille.verifLigne(i) || !grille.verifColonne(j) || !grille.verifCarre(grille.numCarre(i, j))) textColor = badValue; // Changement de couleur si les cases sont mal remplies
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

    // Permet de changer l'état d'activité des boutons en bas de l'écran 
    private void switchBouton(int num, bool activated)
    {
        if(!activated) GameObject.Find("Bouton_" + num).GetComponent<Button>().interactable = false;
        else GameObject.Find("Bouton_" + num).GetComponent<Button>().interactable = true;
    }

    // Méthode pour les cases du jeu, elle permet la sélection de ses cases
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

    // Permet de générer les boutons de 1 à 9 pour remplir la grille
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

    // Méthode de déselection des cases
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

    // Méthode qui définit quel valeur doit être ajouter ou modifier dans la case, sert pour les cliques
    public void buttonClick()
    {
        string nom = EventSystem.current.currentSelectedGameObject.name;
        string ind = Regex.Replace(nom, "[^0-9]", "");
        int buttonVal = ind[0] - 48;
        if (i != -1 && j != -1)
        {
            if (!notesActivated)
            {
                grille.getVal(i, j).setValeur(buttonVal);
                grille.getVal(i, j).retraitIndices();
                UpdateGrid();
                if (grille.verifGrille()) finishGame();
            } else
            {
                if(grille.getVal(i, j).valeur == 0)
                {
                    appuiIndice(buttonVal);
                    UpdateGrid();
                }
            }
        }
    }

    // Méthode qui définit quel valeur doit être ajouter ou modifier dans la case, sert pour les touches du clavier
    public void buttonClick(int num)
    {
        if (i != -1 && j != -1 && GameObject.Find("Bouton_" + num).GetComponent<Button>().IsInteractable())
        {
            if (!notesActivated)
            {
                grille.getVal(i, j).setValeur(num);
                grille.getVal(i, j).retraitIndices();
                UpdateGrid();
                if (grille.verifGrille()) finishGame();
            } else
            {
                if (grille.getVal(i, j).valeur == 0)
                {
                    appuiIndice(num);
                    UpdateGrid();
                }
            }
        }
    }

    // Méthode pour bouton afin de savoir quel bouton a été pressé
    public void appuiIndice(int num)
    {
        Case c = grille.getVal(i, j);
        if (!c.indiceIn(num)) c.ajoutIndice(num);
        else c.retraitIndice(num);
    }

    // Permet de remplacer la valeur d'une case par 0 et de supprimer les indices / Clean de la case
    public void eraseValue()
    {
        if (i != -1 && j != -1)
        {
            grille.getVal(i, j).valeur = 0;
            grille.getVal(i, j).retraitIndices();
            UpdateGrid();
        }
    }

    // Méthode qui sert à activer ou désactiver le système de notes dans le jeu
    public void notesSwitch()
    {
        GameObject notesButton = GameObject.Find("Notes");
        Color unactive = new Color32(255, 255, 255, 255);
        Color active = new Color32(80, 150, 255, 255);
        if (notesButton)
        {
            if (notesActivated)
            {
                //Debug.Log("Mode indice désactivé");
                notesActivated = false;
                notesButton.GetComponent<Image>().color = unactive;
            }
            else
            {
                //Debug.Log("Mode indice activé");
                notesActivated = true;
                notesButton.GetComponent<Image>().color = active;
            }
        }
    }

    // Méthode qui permet de finaliser la partie
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
        GameObject.Find("Notes").GetComponent<Button>().interactable = false;
        GameObject.Find("Infos").SetActive(false);
        finishCanvas.SetActive(true);
        finishCanvas.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text += " in " + tempsFin;
    }
}
