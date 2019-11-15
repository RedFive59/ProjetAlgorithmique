using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManagerNavale : MonoBehaviour
{
    private int rows = 11;
    private int cols = 11;
    private GenVisualManager GVM;
    private Grille<int> col0; //vecteur qui contiendra la colonne des chiffres (juste visuel) 
    private Grille<int> row0; //vecteur qui contiendra la ligne des lettres (juste visuel)
    private Grille<int> grille; //matrice qui contiendra les valeurs de l'eau/bateau, libre/raté/touché
    private CanvasGenerator Cvs;
 
    // Start is called before the first frame update
    void Start()
    {
        GVM = GameObject.FindObjectOfType<GenVisualManager>();
        GenGrille(GVM.getposGVM());
    }

    void GenGrille(int pos)
    {
        //Camera//
        GameObject Camera0 = new GameObject("Camera : "+pos);
        Camera0.AddComponent<Camera>();
        Camera Cam = Camera0.GetComponent<Camera>();
        Cam.transform.position = new Vector3(pos+5f,pos+5f,-10f);
        Camera0.AddComponent<CameraManager>();

        this.grille = new Grille<int>(rows - 1, cols - 1);
        this.col0 = new Grille<int>(cols - 1);
        this.row0 = new Grille<int>(rows - 1);

        for (int i = 0; i < rows - 1; i++)
        {
            col0.setVal(i, i);
             row0.setVal(i, i + 65); //convertit un entier en char
        }
        grille.initVal(0);
        Cvs = new CanvasGenerator("CanvasCases", new Vector3(pos, pos, 0), new Vector2(1, 1), RenderMode.WorldSpace, Cam, 10, "SpriteLayer", gameObject);
        ShowGrid(pos, col0, row0, grille);
    }

    void ShowGrid(int pos,Grille<int> col00, Grille<int> row00, Grille<int> grille00)//affiche les différentes matrices/vecteurs
    {
        int i, j = 0;
        for (i = 0; i < rows-1; i++)
        {
            CreateTileNumber(pos, 0, i, col00.getVal(i)) ;//creer un sprite avec un textechiffre
            CreateTileChar(pos, i, cols, row00.getVal(i));//creer un sprite avec un texte lettre
        }
        for (i = 0; i < rows - 1; i++)
        {
            for (j = 0; j < cols - 1; j++)
            {
                CreateTileWater(pos, i, j, grille.getVal(i, j));//creer les cases d'eau
            }
        }
    }

    void CreateTileNumber(int pos,int i, int j, int v)//voir CreateTileWater
    {
        GameObject t = new GameObject("X:" + i + "Y:" + j);
        t.transform.position = new Vector3(pos+i-1,pos+j);
        t.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/cadre");//lie la texture cadre aux sprites Chiffres
        GenText(pos, i-1, j, v);//appelle la fonction qui genere le texte de cette case
    }

    void CreateTileChar(int pos, int i, int j, int v)//voir CreateTileWater
    {
        GameObject t = new GameObject("X:" + i + "Y:" + j);
        t.transform.position = new Vector3(pos+i,pos+j-1);
        t.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/cadre");//lie la texture cadre aux sprites Lettres
        GenText(pos, i, j-1, v);//appelle la fonction qui genere le texte de ces cases
    }

    void CreateTileWater(int pos, int i, int j, int v)
    {
        GameObject t = new GameObject("X:" + i + "Y:" + j);//creer un gameObject sprite avec un nom donné
        t.transform.position = new Vector3(pos+i, pos+j,0);//place le gameObject dans la scene
        t.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/WaterDiffuseMini2");//creer et attache un rendu au sprite et lui attribut la texture de l'eau
        t.AddComponent<BoxCollider2D>().autoTiling=true;//attache un collider 2D au sprite en le resizant automatiquement au sprite auquel il est attaché
       // t.GetComponent<BoxCollider2D>().isTrigger = true;//test debug
        t.AddComponent<Selection>();//attache au sprite le script selection
        t.transform.parent = this.transform;//range les cases d'eau dans l'objet auquel le script est lié (GridHodler)
    }

    void GenText(int pos, int i, int j, int v)
    {
        string temp;

        if (v > 10)
        {
            temp=System.Convert.ToString(System.Convert.ToChar(v));//convertit le int en char puis en string
            Cvs.addText(Cvs.getCanvas(),"Cadre" + i + j, new Vector3(i, j, 0), new Vector2(2,2),1, temp, Color.black, TextAnchor.MiddleCenter);
        }
        else
        {
            temp=System.Convert.ToString(10-v);
            Cvs.addText(Cvs.getCanvas(),"Cadre" + i + j, new Vector3(i, j, 0), new Vector2(2, 2), 1, temp, Color.black, TextAnchor.MiddleCenter);
        }
    }
}