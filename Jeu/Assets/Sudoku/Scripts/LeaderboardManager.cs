using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
        for (int i = 0; i < loadedData["taille"]; i++)
        {
            Vector3 pos = boardRef.transform.position + new Vector3(0, -30*i);
            GameObject boardTab = Instantiate(boardRef, pos, boardRef.transform.rotation, parent.transform);
            boardTab.name = "board" + (i+1);
            boardTab.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][0];
            boardTab.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][1];
            boardTab.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = " " + loadedData["history"][i][2];
        }
        if(loadedData["taille"] < 16) // Vérification qui permet la disparition de la barre de scroll si le tableau n'est pas assez grand
        {
            GameObject.Find("Scroll View").GetComponent<ScrollRect>().enabled = false;
            GameObject.Find("Scrollbar Vertical").SetActive(false);
        }
        Destroy(boardRef);
    }
}
