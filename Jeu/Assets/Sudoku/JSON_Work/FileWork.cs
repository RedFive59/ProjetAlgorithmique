using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class FileWork : MonoBehaviour
{
    void Start()
    {
        LoadGameData();
    }

    private void LoadGameData()
    {
        string gameDataFileName = "Sudoku/JSON_Work/Saves/saveFile.json";
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            var loadedData = JSON.Parse(dataAsJson);

            string res = "tab = [\n", res2 = "tabTrou = [\n";
            int tmp, tmp2;
            for (int i = 0; i < 9; i++)
            {
                res += "[";
                res2 += "[";
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        res += " ";
                        res2 += " ";
                    }
                    tmp = loadedData["tab"][i][j];
                    tmp2 = loadedData["tabTrou"][i][j];
                    res += tmp;
                    res2 += tmp2;
                }
                res += "]\n";
                res2 += "]\n";
            }
            res += "]";
            res2 += "]";
            Debug.Log("Chargement effectué :\n" + res + "\n" + res2);
        }
        else Debug.Log("Fichier introuvable");
    }
}
