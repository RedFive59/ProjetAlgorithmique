using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private GameObject leaderboard; // Référence à l'objet Leaderboard sur la scène
    private GameObject boardRef; // Référence à la ligne type du leaderboard
    private GameObject parent;
    private string filePathSudoku; // Chemin du fichier d'historique des parties du Sudoku
    private string filePathBingo; // Chemin du fichier d'historique des parties du Sudoku
    private string filePathPoker; // Chemin du fichier d'historique des parties du Sudoku
    private string[,] history; // Grille de strings dans lesquelles les données du leaderboard sont placés
    private int taille; // Taille de history
    private int numBoard; // Numéro de board

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            filePathSudoku = defineSudoku.cheminLeaderboard; // Définition du chemin du fichier
            filePathBingo = Path.Combine(Application.dataPath, "StreamingAssets/Leaderboard/leaderboardBingo.json"); // Définition du chemin du fichier
            filePathPoker = Path.Combine(Application.dataPath, "StreamingAssets/Leaderboard/leaderboardPoker.json"); // Définition du chemin du fichier
            leaderboard = GameObject.Find("Leaderboard");
            boardRef = GameObject.Find("BoardReference");
            parent = GameObject.Find("Content");
            if (File.Exists(filePathSudoku))
            {
                var loadedData = JSON.Parse(File.ReadAllText(filePathSudoku)); // Répartition des données dans loadedData
                updateLeaderboardSudoku();
            } else Debug.LogError("Chargement de " + filePathSudoku + " impossible");
            if (File.Exists(filePathBingo))
            {
                var loadedData = JSON.Parse(File.ReadAllText(filePathBingo)); // Répartition des données dans loadedData
                updateLeaderboardBingo();
            } else Debug.LogError("Chargement de " + filePathSudoku + " impossible");
            if (File.Exists(filePathPoker))
            {
                var loadedData = JSON.Parse(File.ReadAllText(filePathPoker)); // Répartition des données dans loadedData
                updateLeaderboardPoker();
            } else Debug.LogError("Chargement de " + filePathPoker + " impossible");
            var loadedDataS = JSON.Parse(File.ReadAllText(filePathSudoku));
            var loadedDataB = JSON.Parse(File.ReadAllText(filePathBingo));
            var loadedDataP = JSON.Parse(File.ReadAllText(filePathPoker));
            taille = loadedDataS["history"].Count + loadedDataB["history"].Count + loadedDataP["history"].Count;
            history = new string[taille, 2];
            numBoard = 0;
        }
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (ConfigMenu.Seen)
            {
                GameObject parent = GameObject.Find("Canvas");

                Transform[] go = parent.GetComponentsInChildren<RectTransform>(true);
                go[2].gameObject.SetActive(false);
                go[8].gameObject.SetActive(true);
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string sceneName)
    {
        ConfigMenu.Seen = true;
        SceneManager.LoadScene(sceneName);
    }

    public void returnToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void switchLeaderboard()
    {
        float posDepart = 9.2f, posFin = -5.6f;
        GameObject Leaderboard = GameObject.Find("AffichageLeaderboard");
        GameObject arrow = GameObject.Find("Fleche");
        if (Leaderboard.transform.localPosition.x == posDepart)
        {
            arrow.GetComponent<RectTransform>().transform.localRotation = new Quaternion(0f, 0f, 1f, 1f);
            Leaderboard.transform.localPosition = new Vector3(posFin, 0, 0);
        }
        else
        if (Leaderboard.transform.localPosition.x == posFin)
        {
            arrow.GetComponent<RectTransform>().transform.localRotation = new Quaternion(0f, 0f, 0.7f, -0.7f);
            Leaderboard.transform.localPosition = new Vector3(posDepart, 0, 0);
        }
    }

    private void updateLeaderboardSudoku()
    {
        if (boardRef)
        {
            if (parent == null || leaderboard == null) // Vérification de présence des objets
            {
                Debug.LogError("Problème objet");
                return;
            }
            var loadedData = JSON.Parse(File.ReadAllText(filePathSudoku)); // Répartition des données dans loadedData
            if (!loadedData["history"] && loadedData["history"].Count != 0)
            {
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    Vector3 pos = boardRef.transform.position + new Vector3(0, -20 * i);
                    GameObject boardTab = Instantiate(boardRef, pos, boardRef.transform.rotation, parent.transform);
                    boardTab.name = "board" + (numBoard + 1);
                    numBoard++;
                    boardTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = " AnonymeSudoku";
                    boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][3];
                }
                if (loadedData["history"].Count < 5) // Vérification qui permet la disparition de la barre de scroll si le tableau n'est pas assez grand
                {
                    GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                    GameObject.Find("Scrollbar Vertical").SetActive(false);
                }
            }
            else
            {
                GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                GameObject.Find("Scrollbar Vertical").SetActive(false);
            }
            Vector3 pos2 = boardRef.transform.position + new Vector3(0, -20 * (loadedData["history"].Count + 1));
            boardRef.transform.position = pos2;
            //Destroy(boardRef);
        }
        else
        {
            var loadedData = JSON.Parse(File.ReadAllText(filePathSudoku)); // Répartition des données dans loadedData
            if (!loadedData["history"] && loadedData["history"].Count != 0)
            {
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    GameObject boardTab = GameObject.Find("board" + (i + 1));
                    boardTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = " AnonymeSudoku";
                    boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][3];
                }
            }
        }
    }

    private void updateLeaderboardBingo()
    {
        if (boardRef)
        {
            if (parent == null || leaderboard == null) // Vérification de présence des objets
            {
                Debug.LogError("Problème objet");
                return;
            }
            var loadedData = JSON.Parse(File.ReadAllText(filePathBingo)); // Répartition des données dans loadedData
            if (!loadedData["history"] && loadedData["history"].Count != 0)
            {
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    Vector3 pos = boardRef.transform.position + new Vector3(0, -20 * i);
                    GameObject boardTab = Instantiate(boardRef, pos, boardRef.transform.rotation, parent.transform);
                    boardTab.name = "board" + (numBoard + 1);
                    numBoard++;
                    boardTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][0];
                    boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][1];
                }
                /*
                if (loadedData["history"].Count < 5) // Vérification qui permet la disparition de la barre de scroll si le tableau n'est pas assez grand
                {
                    GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                    GameObject.Find("Scrollbar Vertical").SetActive(false);
                }
                */
            }
            else
            {
                GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                GameObject.Find("Scrollbar Vertical").SetActive(false);
            }
            Vector3 pos2 = boardRef.transform.position + new Vector3(0, -20 * (loadedData["history"].Count + 1));
            boardRef.transform.position = pos2;
            //Destroy(boardRef);
        }
        else
        {
            var loadedData = JSON.Parse(File.ReadAllText(filePathBingo)); // Répartition des données dans loadedData
            if (!loadedData["history"] && loadedData["history"].Count != 0)
            {
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    GameObject boardTab = GameObject.Find("board" + (i + 1));
                    boardTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = " AnonymeSudoku";
                    boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][1];
                }
            }
        }
    }

    private void updateLeaderboardPoker()
    {
        if (boardRef)
        {
            if (parent == null || leaderboard == null) // Vérification de présence des objets
            {
                Debug.LogError("Problème objet");
                return;
            }
            var loadedData = JSON.Parse(File.ReadAllText(filePathPoker)); // Répartition des données dans loadedData
            if (!loadedData["history"] && loadedData["history"].Count != 0)
            {
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    Vector3 pos = boardRef.transform.position + new Vector3(0, -20 * i);
                    GameObject boardTab = Instantiate(boardRef, pos, boardRef.transform.rotation, parent.transform);
                    boardTab.name = "board" + (numBoard + 1);
                    numBoard++;
                    boardTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][0];
                    boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][3];
                }
                /*
                if (loadedData["history"].Count < 5) // Vérification qui permet la disparition de la barre de scroll si le tableau n'est pas assez grand
                {
                    GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                    GameObject.Find("Scrollbar Vertical").SetActive(false);
                }
                */
            }
            else
            {
                GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
                GameObject.Find("Scrollbar Vertical").SetActive(false);
            }
            Destroy(boardRef);
        }
        else
        {
            var loadedData = JSON.Parse(File.ReadAllText(filePathPoker)); // Répartition des données dans loadedData
            if (!loadedData["history"] && loadedData["history"].Count != 0)
            {
                for (int i = 0; i < loadedData["history"].Count; i++)
                {
                    GameObject boardTab = GameObject.Find("board" + (i + 1));
                    boardTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][0];
                    boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][3];
                }
            }
        }
    }

    void updateLeaderboard()
    {
        var loadedDataS = JSON.Parse(File.ReadAllText(filePathSudoku));
        var loadedDataB = JSON.Parse(File.ReadAllText(filePathBingo));
        var loadedDataP = JSON.Parse(File.ReadAllText(filePathPoker));
        for (int i = 0; i < taille; i++)
        {
            GameObject boardTab = GameObject.Find("board" + (i + 1));
            boardTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = history[i, 0];
            boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = history[i, 1];
        }
    }

    // Méthode de tri du tableau des scores
    public void triLeaderboard(string type)
    {
        Transform g;
        var loadedDataS = JSON.Parse(File.ReadAllText(filePathSudoku));
        var loadedDataB = JSON.Parse(File.ReadAllText(filePathBingo));
        var loadedDataP = JSON.Parse(File.ReadAllText(filePathPoker));
        if (taille != 0)
        {
            for (int i = 0; i < taille; i++)
            {
                GameObject boardTab = GameObject.Find("board" + (i + 1));
                history[i, 0] = boardTab.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text;
                history[i, 1] = boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text;
            }
            Array2DSort comparer = null;
            switch (type)
            {
                case "name":
                    g = GameObject.Find("nomJoueur").transform.GetChild(0).transform;
                    GameObject.Find("score").transform.GetChild(0).transform.rotation = new Quaternion(0, 0, 0, 0);
                    comparer = new Array2DSort(history, 0);
                    break;
                case "score":
                    g = GameObject.Find("score").transform.GetChild(0).transform;
                    GameObject.Find("nomJoueur").transform.GetChild(0).transform.rotation = new Quaternion(0, 0, 0, 0);
                    comparer = new Array2DSort(history, 1);
                    break;
                default:
                    g = null;
                    break;
            }
            g.rotation = Quaternion.Euler(0, 0, 180);
            string[,] sortedData = comparer.ToSortedArray(); //Tri
            for (int i = 0; i < taille; i++)
            {
                history[i, 0] = sortedData[i, 0];
                history[i, 1] = sortedData[i, 1];
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
            int res = _sortArray[x, col].CompareTo(_sortArray[y, col]);
            if (col == 0) // Cas où on check le nom
            {
                if (_sortArray[x, col] == _sortArray[y, col])
                {
                    res = _sortArray[x, col + 1].CompareTo(_sortArray[y, col + 1]);
                }
            }
            if (col == 1) // Cas où on check le score
            {
                res = int.Parse(_sortArray[x, col]).CompareTo(int.Parse(_sortArray[y, col]));
            }
            return res;
        }
    }
}
