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
        string gameDataFileName = "Sudoku/JSON_Work/Saves/file.json";
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            string res = "[";
            string res2 = "[";
            for (int i = 0; i < loadedData.tab.Length; i++)
            {
                if (i != loadedData.tab.Length - 1)
                {
                    res += loadedData.tab.GetValue(i) + ", ";
                    res2 += loadedData.tab2.GetValue(i) + ", ";
                } else {
                    res += loadedData.tab.GetValue(i) + "]";
                    res2 += loadedData.tab2.GetValue(i) + "]";
                }
            }
            Debug.Log("tab : " + res);
            Debug.Log("tab2 : " + res2);
        }
        else Debug.Log("Fichier introuvable");
    }

    private void SaveGameData()
    {
        string gameDataFileName = "JSON_Work/Saves/file.json";
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);
    }
}
