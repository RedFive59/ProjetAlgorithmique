using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ReturnToMain()
    {
        PlayerStats.reset();
        SceneManager.LoadScene(0);
    }

    public void Retry(string SceneName)
    {
        PlayerStats.reset();
        SceneManager.LoadScene(SceneName);
    }
}
