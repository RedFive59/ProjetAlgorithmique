using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int rows = 11;
    private int cols = 11;
    private Grille<int> col0;
    private Grille<char> row0;
    private Grille<int> grille;
    public Sprite WaterDiffuseMini;

    // Start is called before the first frame update
    void Start()
    {
        this.grille = new Grille<int>(rows-1, cols-1);
        this.col0 = new Grille<int>(cols);
        this.row0 = new Grille<char>(rows);


        for (int i = 0; i < rows; i++)
        {
            col0.ajoutVect(i, i);
            row0.ajoutVect(i, System.Convert.ToChar(65 + i));
            }
        grille.setVal(0);
        ShowGrid(col0, row0, grille);
    }

    void ShowGrid(Grille<int> col00, Grille<char> row00, Grille<int> grille00)
    {
        int i, j = 0;
        for (i = 0; i < rows; i++)
        {
            CreateTileNumber(i, 0, col00.getVal(i));
            CreateTileChar(0, i, row00.getVal(i));
        }
        for (i = 0; i < rows - 1; i++)
        {
            for (j = 0; j < cols - 1; j++)
                CreateTileWater(i+1, j+1, grille.getVal(i, j));
        }
    }

        void CreateTileNumber(int i, int j, int v)
        {
            GameObject t = new GameObject("X:" + i + "Y:" + j);
            t.transform.position = new Vector3(i - rows / 2, j - cols / 2);
            t.AddComponent<SpriteRenderer>().sprite = WaterDiffuseMini;
         }

    void CreateTileChar(int i, int j, char v)
    {
        GameObject t = new GameObject("X:" + i + "Y:" + j);
        t.transform.position = new Vector3(i - rows / 2, j - cols / 2);
        t.AddComponent<SpriteRenderer>().sprite = WaterDiffuseMini;
    }

    void CreateTileWater(int i, int j, int v)
        {
            GameObject t = new GameObject("X:" + i + "Y:" + j);
            t.transform.position = new Vector3(i - rows / 2, j - cols / 2);
            t.AddComponent<SpriteRenderer>().sprite = WaterDiffuseMini;
            BoxCollider2D b = new BoxCollider2D();
            t.AddComponent<BoxCollider2D>().autoTiling = b;
            t.AddComponent<Selection>();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }