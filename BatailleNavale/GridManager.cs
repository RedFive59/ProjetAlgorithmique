using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private int rows = 11;
    private int cols = 11;
    private Grille<int> col0; //vecteur qui contiendra la colonne des chiffres (juste visuel) 
    private Grille<char> row0; //vecteur qui contiendra la ligne des lettres (juste visuel)
    private Grille<int> grille; //matrice qui contiendra les valeurs de l'eau/bateau, libre/raté/touché
    public Sprite WaterDiffuseMini; //var texture des cases d'eau

    // Start is called before the first frame update
    void Start()
    {
        this.grille = new Grille<int>(rows-1, cols-1);
        this.col0 = new Grille<int>(cols);
        this.row0 = new Grille<char>(rows);


        for (int i = 0; i < rows; i++)
        {
            col0.ajoutVect(i, i);
            row0.ajoutVect(i, System.Convert.ToChar(65 + i)); //convertit un entier en char
            }
        grille.setVal(0);
        ShowGrid(col0, row0, grille);
    }

    void ShowGrid(Grille<int> col00, Grille<char> row00, Grille<int> grille00)//affiche les différentes matrices/vecteurs
    {
        int i, j = 0;
        for (i = 0; i < rows; i++)
        {
            CreateTileNumber(i, 0, col00.getVal(i));//creer un sprite avec un textechiffre
            CreateTileChar(0, i, row00.getVal(i));//creer un sprite avec un texte lettre
        }
        for (i = 0; i < rows - 1; i++)
        {
            for (j = 0; j < cols - 1; j++)
                CreateTileWater(i+1, j+1, grille.getVal(i, j));//creer les cases d'eau
        }
    }

        void CreateTileNumber(int i, int j, int v)//voir CreateTileWater
        {
            GameObject t = new GameObject("X:" + i + "Y:" + j);
            t.transform.position = new Vector3(i - rows / 2, j - cols / 2);
            t.AddComponent<SpriteRenderer>().sprite = WaterDiffuseMini;
         }

    void CreateTileChar(int i, int j, char v)//voir CreateTileWater
    {
        GameObject t = new GameObject("X:" + i + "Y:" + j);
        t.transform.position = new Vector3(i - rows / 2, j - cols / 2);
        t.AddComponent<SpriteRenderer>().sprite = WaterDiffuseMini;
    }

    void CreateTileWater(int i, int j, int v)
        {
            GameObject t = new GameObject("X:" + i + "Y:" + j);//creer un gameObject sprite avec un nom donné
            t.transform.position = new Vector3(i - rows / 2, j - cols / 2);//place le gameObject dans la scene
            t.AddComponent<SpriteRenderer>().sprite = WaterDiffuseMini;//creer et attache un rendu au sprite et lui attribut la texture de l'eau
            BoxCollider2D b = new BoxCollider2D();//creer un collider rectangulaire 2D
            t.AddComponent<BoxCollider2D>().autoTiling = b;//attache le collider précédent au sprite en le resizant automatiquement au sprite auquel il est attaché
            t.AddComponent<Selection>();//attache au sprite le script selection
        }

        // Update is called once per frame
        void Update()
        {
        }
    }