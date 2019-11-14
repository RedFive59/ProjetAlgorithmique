using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class CanvasGenerator : MonoBehaviour
public class CanvasGenerator{

    private GameObject Cvs;
    private Canvas mCvs;
    private List<GameObject> LText;
    private List<GameObject> LPanel;

    //Class generant un canvas
    //Si pas de sortingLayer mettre default
    //Si pas de pld mettre 0
    public CanvasGenerator(string nom, Vector3 pos, Vector2 delta, RenderMode render,Camera cam, int pld, string slayer, GameObject parent)
    {
        Cvs = new GameObject(nom);
        Cvs.transform.SetParent(parent.transform, false);//range le canvas dans son parent
        Cvs.transform.position = pos;
        Cvs.AddComponent<Canvas>();
        mCvs = Cvs.GetComponent<Canvas>();
        mCvs.renderMode = render;//render mode, RenderMode.WorldSpace,RenderMode.Camera etc...
        mCvs.worldCamera = null;
        mCvs.transform.position = pos;//position dans la scene
        mCvs.GetComponent<RectTransform>().sizeDelta = delta;//dimension h w du canvas
        mCvs.planeDistance = pld;//"aucune idée de la traduction", a permit de faire repasser les panneaux devant la grille car la caméra était trop proche= fusion des éléments
        mCvs.sortingLayerName = slayer;//applique le bon sortingLayer sur le canvas
        Cvs.AddComponent<CanvasScaler>();//modifie la pixelisation des lettres
        Cvs.AddComponent<GraphicRaycaster>();//useless
        Cvs.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 50f;//pixelation des characters dans le canvas
        LText = new List<GameObject>();
        LPanel = new List<GameObject>();
    }

    //méthode qui génère un texte dans le canvas
    public void addText(string nom,Vector3 pos,Vector2 delta,int taille,string text,string police,Color C,TextAnchor TA)
    {
        Debug.Log("Debug2");
        GameObject mText = new GameObject(nom);
        mText.transform.SetParent(Cvs.transform, false);
        mText.transform.position = pos;
        mText.AddComponent<RectTransform>().sizeDelta = delta;
        Text xtext = mText.AddComponent<Text>();
        xtext.color = C; //couleur du texte
        xtext.font = (Font)Resources.GetBuiltinResource<Font>(police+".ttf");//utilise la police Arial pour afficher le texte
        xtext.fontSize = taille;
        xtext.text = text;//texte à afficher
        xtext.fontSize = taille;//taille police
        xtext.alignment = TA;//ancrage du text dans son canvas
        LText.Add(mText);
    }

    //méthode qui génère un panel dans le canvas
    public void addPanel(string nom, Vector3 pos, Vector2 delta, Color C)
    {
        GameObject panel = new GameObject(nom);
        panel.transform.parent = mCvs.transform;//range le panel dans son canvas
        panel.AddComponent<CanvasRenderer>();
        panel.GetComponent<RectTransform>().sizeDelta = delta;
        panel.GetComponent<RectTransform>().position = pos;
        panel.AddComponent<Image>().color=C;//récupération du pseudo rendu du panel
        LPanel.Add(panel);
    }
    
    public GameObject getCanvas(){
        return Cvs;
    }
}
