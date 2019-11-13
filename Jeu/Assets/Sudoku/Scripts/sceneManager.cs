using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class sceneManager : MonoBehaviour
{
    public string difficulty; // Difficulté que l'utilisateur choisira
    public bool resumeGame = false; // Booléen pour savoir si l'on peut reprendre la dernière partie
    private string filePath;

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("DifficultyManager")); // Permet de garder un élément de la scène après changement de la scène
        filePath = Path.Combine(Application.dataPath, "StreamingAssets/SudokuLevels/sauvegardeSudoku.json");
        resumeUpdate();
    }

    // Méthode qui sert au bouton de la scène SudokuMenu afin de définir la difficulté
    public void setDifficulty(int num)
    {
        resumeGame = false;
        switch(num) {
            case 1:
                difficulty = "Easy";
                break;
            case 2:
                difficulty = "Medium";
                break;
            case 3:
                difficulty = "Hard";
                break;
            default:
                break;
        }
    }

    public void resumeUpdate()
    {
        if (File.Exists(filePath))
        {
            string infos = File.ReadAllText(filePath);
            var loadedData = JSON.Parse(infos);
            if (loadedData.Count == 0)
            {
                GameObject.Find("Resume").SetActive(false);
                resumeGame = false;
            }
            else
            {
                difficulty = loadedData["difficulty"];
                resumeGame = true;
            }
        }
        else
        {
            Debug.Log("Fichier " + filePath + " introuvable");
            resumeGame = false;
        }
    }

    public void destroyData()
    {
        GameObject data = GameObject.Find("DifficultyManager");
        if (data) Destroy(data);
    }
}
