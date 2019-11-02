using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileWork : MonoBehaviour
{
    void Start()
    {
        /*
        GameData g = new GameData{};
        string json = JsonUtility.ToJson(g);
        Debug.Log(json);
        */
        LoadGameData();
    }

    public class GameData
    {
        public int[] tab = { 1, 2, 3, 4 };
        public int[] tab2 = { 0, 1, 0, 1 };
    }


    private void LoadGameData()
    {
        string gameDataFileName = "JSON_Work/Saves/file.json";
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            Debug.Log(dataAsJson);
            for(int i = 0; i < loadedData.tab.Length; i++)
            {
                Debug.Log(loadedData.tab.GetValue(i));
                Debug.Log(loadedData.tab2.GetValue(i));
            }
        }
        else Debug.Log("Fichier introuvable");
    }
}
