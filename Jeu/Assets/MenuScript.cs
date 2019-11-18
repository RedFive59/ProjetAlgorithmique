using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
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
}
