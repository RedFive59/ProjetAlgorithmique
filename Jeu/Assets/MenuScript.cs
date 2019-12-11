using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    
    void Start()
    {
        filePathSudoku = defineSudoku.cheminLeaderboard; // Définition du chemin du fichier
        leaderboard = GameObject.Find("Leaderboard");
        boardRef = GameObject.Find("BoardReference");
        parent = GameObject.Find("Content");
        if (File.Exists(filePathSudoku))
        {
            var loadedData = JSON.Parse(File.ReadAllText(filePathSudoku)); // Répartition des données dans loadedData
            updateLeaderboard();
        }
        else Debug.LogError("Chargement de " + filePathSudoku + " impossible");
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

    private void updateLeaderboard()
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
                    boardTab.name = "board" + (i + 1);
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
            Destroy(boardRef);
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
}
