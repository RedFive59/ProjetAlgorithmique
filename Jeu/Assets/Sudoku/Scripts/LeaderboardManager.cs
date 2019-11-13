using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class LeaderboardManager : MonoBehaviour
{
    private GameObject leaderboard; // Référence à l'objet Leaderboard sur la scène
    private GameObject boardRef; // Référence à la ligne type du leaderboard
    private GameObject parent;
    private string filePath; // Chemin du fichier d'historique des parties
    private string[,] history; // Grille de strings dans lesquelles les données du leaderboard sont placés
    private int nombreColonnes;

    // Méthode appelé au lancement de la scène SudokuMenu
    void Start()
    {
        nombreColonnes = 3; // Nombre de colonnes de votre leaderboard
        filePath = defineSudoku.cheminLeaderboard; // Définition du chemin du fichier
        leaderboard = GameObject.Find("Leaderboard");
        boardRef = GameObject.Find("BoardReference");
        parent = GameObject.Find("Content");
        if (File.Exists(filePath))
        {
            var loadedData = JSON.Parse(File.ReadAllText(filePath)); // Répartition des données dans loadedData
            history = new string[loadedData["history"].Count, nombreColonnes];
            updateLeaderboard();
        }
        else Debug.LogError("Chargement de " + filePath + " impossible");
    }
    
    // Méthode qui permet l'ajout dans le tableau des scores de toutes les parties précédement jouées
    private void updateLeaderboard()
    {
        if (boardRef)
        {
            if (parent == null || leaderboard == null) // Vérification de présence des objets
            {
                Debug.LogError("Problème objet");
                return;
            }
            var loadedData = JSON.Parse(File.ReadAllText(filePath)); // Répartition des données dans loadedData
            if (!loadedData["history"] && loadedData["history"].Count != 0)
            {
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    history[i, 0] = loadedData["history"][i][0];
                    history[i, 1] = loadedData["history"][i][1];
                    history[i, 2] = loadedData["history"][i][2];
                }
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    Vector3 pos = boardRef.transform.position + new Vector3(0, -30 * i);
                    GameObject boardTab = Instantiate(boardRef, pos, boardRef.transform.rotation, parent.transform);
                    boardTab.name = "board" + (i + 1);
                    boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + history[i, 0];
                    boardTab.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = " " + history[i, 1];
                    boardTab.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = " " + history[i, 2];
                }
                if (loadedData["history"].Count < 16) // Vérification qui permet la disparition de la barre de scroll si le tableau n'est pas assez grand
                {
                    GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                    GameObject.Find("Scrollbar Vertical").SetActive(false);
                }
            } else
            {
                GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                GameObject.Find("Scrollbar Vertical").SetActive(false);
            }
            Destroy(boardRef);
        } else
        {
            var loadedData = JSON.Parse(File.ReadAllText(filePath)); // Répartition des données dans loadedData
            if (!loadedData["history"] && loadedData["history"].Count != 0)
            {
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    GameObject boardTab = GameObject.Find("board"+(i+1));
                    boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + history[i, 0];
                    boardTab.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = " " + history[i, 1];
                    boardTab.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = " " + history[i, 2];
                }
            }
        }
    }

    // Méthode de tri du tableau des scores
    public void triLeaderboard(string type)
    {
        Transform g;
        var loadedData = JSON.Parse(File.ReadAllText(filePath));
        if (loadedData["history"].Count != 0)
        {
            //Debug.Log("Tri par " + type);
            for (int i = 0; i < loadedData["history"].Count; i++)
            {
                history[i, 0] = loadedData["history"][i][0];
                history[i, 1] = loadedData["history"][i][1];
                history[i, 2] = loadedData["history"][i][2];
            }
            Array2DSort comparer = null;
            switch (type)
            {
                case "difficulty":
                    g = GameObject.Find("diff").transform.GetChild(0).transform;
                    GameObject.Find("time").transform.GetChild(0).transform.rotation = new Quaternion(0, 0, 0, 0);
                    comparer = new Array2DSort(history, 1);
                    comparer = new Array2DSort(history, 0);
                    break;
                case "timer":
                    g = GameObject.Find("time").transform.GetChild(0).transform;
                    GameObject.Find("diff").transform.GetChild(0).transform.rotation = new Quaternion(0, 0, 0, 0);
                    comparer = new Array2DSort(history, 1);
                    comparer = new Array2DSort(history, 2);
                    break;
                default:
                    g = null;
                    break;
            }
            g.rotation = Quaternion.Euler(0, 0, 180);
            string[,] sortedData = comparer.ToSortedArray(); //Tri
            for (int i = 0; i < loadedData["history"].Count; i++)
            {
                history[i, 0] = sortedData[i, 0];
                history[i, 1] = sortedData[i, 1];
                history[i, 2] = sortedData[i, 2];
            }
            updateLeaderboard();
        }
    }

    // Objet utilisé pour trier le leaderboard
    class Array2DSort : IComparer<int>
    {
        string[,] _sortArray;
        int[] _tagArray;
        int _sortIndex;

        protected string[,] SortArray { get { return _sortArray; } }
        
        public Array2DSort(string[,] theArray, int sortIndex)
        {
            _sortArray = theArray;
            _tagArray = new int[_sortArray.GetLength(0)];
            for (int i = 0; i < _sortArray.GetLength(0); ++i) _tagArray[i] = i;
            _sortIndex = sortIndex;
        }

        public string[,] ToSortedArray()
        {
            Array.Sort(_tagArray, this);
            string[,] result = new string[
                _sortArray.GetLength(0), _sortArray.GetLength(1)];
            for (int i = 0; i < _sortArray.GetLength(0); i++)
            {
                for (int j = 0; j < _sortArray.GetLength(1); j++)
                {
                    result[i, j] = _sortArray[_tagArray[i], j];
                }
            }
            return result;
        }
        
        public virtual int Compare(int x, int y)
        {
            if (_sortIndex < 0) return 0;
            return CompareStrings(x, y, _sortIndex);
        }

        // Méthode qui permet de mettre les différentes règles de tri
        protected int CompareStrings(int x, int y, int col)
        {
            
            int res = 0;
            res = _sortArray[x, col].CompareTo(_sortArray[y, col]);
            if (col == 0) // Cas où on check la difficulté
            {
                if(_sortArray[x, col] == _sortArray[y, col])
                {
                    if (_sortArray[x, col + 1] == _sortArray[y, col + 1]) res = _sortArray[x, col + 2].CompareTo(_sortArray[y, col + 2]);
                    else res = _sortArray[x, col + 1].CompareTo(_sortArray[y, col + 1]);
                } else
                {
                    if (_sortArray[x, col] == "Medium" && _sortArray[y, col] == "Hard") res = -1;
                    if (_sortArray[x, col] == "Hard" && _sortArray[y, col] == "Medium") res = 1;
                }
            }
            if (col == 2) // Cas où on check le timer
            {
                string[] temp = Regex.Split(_sortArray[x, col], ":");
                string[] temp2 = Regex.Split(_sortArray[y, col], ":");
                res = int.Parse(temp[0]).CompareTo(int.Parse(temp2[0]));
                if(res == 0) res = int.Parse(temp[1]).CompareTo(int.Parse(temp2[1]));
            }
            return res;
        }
    }
}
