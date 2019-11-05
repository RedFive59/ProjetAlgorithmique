using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManagerNavale : MonoBehaviour
{
    private int rows = 11;
    private int cols = 11;
    private Grille<int> col0; //vecteur qui contiendra la colonne des chiffres (juste visuel) 
    private Grille<int> row0; //vecteur qui contiendra la ligne des lettres (juste visuel)
    private Grille<int> grille; //matrice qui contiendra les valeurs de l'eau/bateau, libre/raté/touché
    //public Sprite WaterDiffuseMini; //var texture des cases d'eau !!! OUT
    //public Sprite Cadre; //var textures cases chiffres et lettres !!! OUT
    Canvas myCanvas;
    GameObject cvs;
    CanvasScaler CS;

    // Start is called before the first frame update
    void Start()
    {
        GenGrille(0);
    }
        void GenGrille(int pos)
        {
            // Canvas ///////////////////////////////
            cvs = new GameObject();
            cvs.name = ("Canvas Main");
            cvs.AddComponent<Canvas>();
            RectTransform Rect3;//var pour stocker le rectTransform du canvas
            myCanvas = cvs.GetComponent<Canvas>();//recupère le "pointeur" vers le Canvas du gameObject
            myCanvas.renderMode = RenderMode.WorldSpace;//set le canvas sur world space = est indépendant de la caméra ou de la résolution
            myCanvas.transform.position = new Vector3(0, 0, 0);//place le canvas dans la scene
            Rect3 = myCanvas.GetComponent<RectTransform>();//récupère le "pointeur" vers le RectTransform du Canvas
            Rect3.sizeDelta = new Vector2(1, 1);//resize le canvs, largeur/hauteur
            cvs.AddComponent<CanvasScaler>();//modifie la pixelisation des lettres
            cvs.AddComponent<GraphicRaycaster>();//useless
            CS = myCanvas.GetComponent<CanvasScaler>();
            CS.dynamicPixelsPerUnit = 50f;//set la pixelisation par unité des lettres
            this.grille = new Grille<int>(rows - 1, cols - 1);
            this.col0 = new Grille<int>(cols - 1);
            this.row0 = new Grille<int>(rows - 1);
            // Canvas ////////////////////////////////////

            for (int i = 0; i < rows - 1; i++)
            {
                col0.setVal(i, i);
                row0.setVal(i, i + 65); //convertit un entier en char
            }
            grille.initVal(0);
            ShowGrid(pos, col0, row0, grille);
        }

    void ShowGrid(int pos,Grille<int> col00, Grille<int> row00, Grille<int> grille00)//affiche les différentes matrices/vecteurs
    {
        int i, j = 0;
        for (i = 0; i < rows-1; i++)
        {
            CreateTileNumber(pos, 0, -i, col00.getVal(i)) ;//creer un sprite avec un textechiffre
            CreateTileChar(pos, i, 0, row00.getVal(i));//creer un sprite avec un texte lettre
        }
        for (i = 0; i < rows - 1; i++)
        {
            for (j = 0; j < cols - 1; j++)
                CreateTileWater(pos,i+1, j+1, grille.getVal(i, j));//creer les cases d'eau
        }
    }

        void CreateTileNumber(int pos,int i, int j, int v)//voir CreateTileWater
        {
            GameObject t = new GameObject("X:" + i + "Y:" + j);
            t.transform.position = new Vector3(pos+i,pos-9-j-1);
            t.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/cadre");//lie la texture cadre aux sprites Chiffres
            GenText(pos, i, j-1, v);//appelle la fonction qui genere le texte de cette case
    }

    void CreateTileChar(int pos, int i, int j, int v)//voir CreateTileWater
    {
        GameObject t = new GameObject("X:" + i + "Y:" + j);
        t.transform.position = new Vector3(pos+i+1,pos+j);
        t.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/cadre");//lie la texture cadre aux sprites Lettres
        GenText(pos, i+1, j, v);//appelle la fonction qui genere le texte de ces cases
    }

    void CreateTileWater(int pos, int i, int j, int v)
        {
            GameObject t = new GameObject("X:" + i + "Y:" + j);//creer un gameObject sprite avec un nom donné
            t.transform.position = new Vector3(pos+i, pos-j);//place le gameObject dans la scene
            t.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/WaterDiffuseMini2");//creer et attache un rendu au sprite et lui attribut la texture de l'eau
            t.AddComponent<BoxCollider2D>().autoTiling=true;//attache un collider 2D au sprite en le resizant automatiquement au sprite auquel il est attaché
            t.GetComponent<BoxCollider2D>().isTrigger = true;//test debug
            t.AddComponent<Selection>();//attache au sprite le script selection
            t.transform.parent = this.transform;//range les cases d'eau dans l'objet auquel le script est lié (GridHodler)
    }

    void GenText(int pos, int i, int j, int v)
    {
        GameObject myText;
        Text text;
        RectTransform rectTransform;

        // Text
        myText = new GameObject();
        myText.transform.parent = cvs.transform;
        if (v > 10)
        {
            myText.name = System.Convert.ToString(System.Convert.ToChar(v));//convertit le int en char puis en string
        }
        else
        {
            myText.name = System.Convert.ToString(v);
        }

        text = myText.AddComponent<Text>();
        text.color = Color.black;//couleur de texte noire
        text.font = (Font)Resources.GetBuiltinResource<Font>("Arial.ttf");//utilise la police Arial pour afficher le texte
        text.text = myText.name;
        text.fontSize = 1;//taille de la police
        text.alignment = TextAnchor.MiddleCenter;//attache le text au centre de sa box

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(i, j, 0);//place le text dans la scene
        rectTransform.sizeDelta = new Vector2(1, 2);//resize la box du text
        rectTransform.localScale = new Vector3(0.5f, 0.5f, 1);//resieze le text par rapport à son rectTransform
   

    }

        // Update is called once per frame
        void Update()
        {
        }
    }