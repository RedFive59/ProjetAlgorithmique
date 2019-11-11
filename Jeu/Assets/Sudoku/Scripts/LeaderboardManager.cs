using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LeaderboardManager : MonoBehaviour
{
    private GameObject leaderboard; // Référence à l'objet Leaderboard sur la scène
    private GameObject boardRef; // Référence à la ligne type du leaderboard
    private string filePath; // Chemin du fichier d'historique des parties

    // Méthode appelé au lancement de la scène SudokuMenu
    void Start()
    {
        filePath = Path.Combine(Application.dataPath, "Saves/Sudoku.json"); // Définition du chemin du fichier
        leaderboard = GameObject.Find("Leaderboard");
        boardRef = GameObject.Find("BoardReference");
        if (File.Exists(filePath)) updateLeaderboard();
        else Debug.LogError("Chargement de " + filePath + " impossible");
    }
    
    // Méthode qui permet l'ajout dans le tableau des scores de toutes les parties précédement jouées
    private void updateLeaderboard()
    {
        GameObject parent = GameObject.Find("Content");
        if (parent == null || boardRef == null || leaderboard == null) // Vérification de présence des objets
        {
            Debug.LogError("Problème objet");
            return;
        }
        var loadedData = JSON.Parse(File.ReadAllText(filePath)); // Répartition des données dans loadedData
        if(!loadedData["history"] && loadedData["history"].Count != 0)
        {
            for (int i = 0; i < loadedData["history"].Count; i++)
            {
                Vector3 pos = boardRef.transform.position + new Vector3(0, -30 * i);
                GameObject boardTab = Instantiate(boardRef, pos, boardRef.transform.rotation, parent.transform);
                boardTab.name = "board" + (i + 1);
                boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][0];
                boardTab.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][1];
                boardTab.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][2];
            }
            if (loadedData["history"].Count < 16) // Vérification qui permet la disparition de la barre de scroll si le tableau n'est pas assez grand
            {
                GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                GameObject.Find("Scrollbar Vertical").SetActive(false);
            }
        }
        Destroy(boardRef);
    }

    // Méthode de tri du tableau des scores
    public void triLeaderboard(string type)
    {
        Transform g;
        var loadedData = JSON.Parse(File.ReadAllText(filePath));
        if (loadedData["history"].Count != 0)
        {
            //Debug.Log("Tri par " + type);
            string[,] history = new string[loadedData["history"].Count, 3];
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
                    comparer = new Array2DSort(history, 0);
                    break;
                case "timer":
                    g = GameObject.Find("time").transform.GetChild(0).transform;
                    GameObject.Find("diff").transform.GetChild(0).transform.rotation = new Quaternion(0, 0, 0, 0);
                    comparer = new Array2DSort(history, 2);
                    break;
                default:
                    g = null;
                    break;
            }
            g.rotation = Quaternion.Euler(0, 0, 180);
            string[,] sortedData = comparer.ToSortedArray();
            string res = "[\n";
            for (int i = 0; i < loadedData["history"].Count; i++)
            {
                res += "\t[";
                res += sortedData[i, 0] + ", ";
                res += sortedData[i, 1] + ", ";
                if(i != loadedData["history"].Count-1) res += sortedData[i, 2] + "],\n";
                else res += sortedData[i, 2] + "]\n";
            }
            res += "]";
            Debug.Log(res);
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

        protected int CompareStrings(int x, int y, int col)
        {
            return _sortArray[x, col].CompareTo(_sortArray[y, col]);
        }
    }
}
