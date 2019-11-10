using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public string difficulty; // Difficulté que l'utilisateur choisira

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("DifficultyManager")); // Permet de garder un élément de la scène après changement de la scène
    }

    // Fonction qui sert au bouton de la scène SudokuMenu afin de définir la difficulté
    public void setDifficulty(int num)
    {
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
}
