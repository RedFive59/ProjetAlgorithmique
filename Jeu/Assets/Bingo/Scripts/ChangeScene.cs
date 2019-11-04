using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ReturnToMain()
    {
        PlayerStats.Jetons = 0;
        SceneManager.LoadScene(0);
    }

    public void PlayGame(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
