using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //fonction appelé pour retourner à la scene principale
    public void ReturnToMain()
    {
        //on remet les stats par defauts
        PlayerStats.reset();
        SceneManager.LoadScene(0);
    }

    //fonction appelé pour remttre la scene en etat initiale
    public void Retry(string SceneName)
    {
        PlayerStats.reset();
        SceneManager.LoadScene(SceneName);
    }
}
