using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public string difficulty;

    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("DifficultyManager"));
    }

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
