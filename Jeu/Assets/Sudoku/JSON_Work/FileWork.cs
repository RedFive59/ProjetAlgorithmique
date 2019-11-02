using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileWork : MonoBehaviour
{
    void Start()
    {
        GameData g = new GameData();
        SaveGameData(g);
        LoadGameData();
    }

    public class GameData
    {
        public int[,] tab = {
            { 1, 2, 3 },
            { 4, 5, 6 },
            { 7, 8, 9 }
        };
        public int[,] tab2 = {
            { 0, 0, 1 },
            { 1, 0, 1 },
            { 0, 0, 0 }
        };
    }


    private GameData LoadGameData()
    {
        GameData loadedData = null;
        string gameDataFileName = "Sudoku/JSON_Work/Saves/saveFile.json";
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            int rowLength = loadedData.tab.GetLength(0);
            int colLength = loadedData.tab.GetLength(1);

            string res = "" , res2 = "";
            res += "  [\n";
            res2 += "  [\n";
            for (int i = 0; i < rowLength; i++)
            {
                res += "\t[";
                res2 += "\t[";
                for (int j = 0; j < colLength; j++)
                {
                    if(j != colLength - 1)
                    {
                        res += loadedData.tab.GetValue(i, j) + ", ";
                        res2 += loadedData.tab2.GetValue(i, j) + ", ";
                    } else
                    {
                        res += loadedData.tab.GetValue(i, j) + "]";
                        res2 += loadedData.tab2.GetValue(i, j) + "]";
                    }
                }
                res += "\n";
                res2 += "\n";
            }
            res += "  ]";
            res2 += "  ]";

            Debug.Log("Chargement effectué :\n" + res + "\n" + res2);
        }
        else Debug.Log("Fichier introuvable");
        return loadedData;
    }

    private void SaveGameData(GameData g)
    {
        string gameDataFileName = "Sudoku/JSON_Work/Saves/saveFile.json";
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string jsonText = JsonUtility.ToJson(g);
            File.WriteAllText(filePath, jsonText);
        }
        else Debug.Log("Fichier introuvable / A créer");
    }
}
