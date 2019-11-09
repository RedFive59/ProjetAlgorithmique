using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class CanvasGenerator : MonoBehaviour
public class CanvasGenerator{

    private GameObject Cvs;
    private Canvas mCvs;

    //Class generant un canvasg
    //Si pas de sortingLayer mettre default
    //Si pas de pld mettre 0
    public CanvasGenerator(string nom, Vector3 pos, Vector2 taille, RenderMode render, int pld, string slayer, GameObject parent)
    {
        Cvs = new GameObject(nom);
        Cvs.transform.SetParent(parent.transform, false);//range le canvas dans son parent
        Cvs.AddComponent<Canvas>();
        mCvs = Cvs.GetComponent<Canvas>();
        mCvs.renderMode = render;//render mode, RenderMode.WorldSpace,RenderMode.Camera etc...
        mCvs.transform.localPosition = pos;//position dans la scene
        mCvs.GetComponent<RectTransform>().sizeDelta = taille;//dimension h w du canvas
        mCvs.planeDistance = pld;//"aucune idée de la traduction", a permit de faire repasser les panneaux devant la grille car la caméra était trop proche= fusion des éléments
        mCvs.sortingLayerName = slayer;//applique le bon sortingLayer sur le canvas
        Cvs.AddComponent<CanvasScaler>();//modifie la pixelisation des lettres
        Cvs.AddComponent<GraphicRaycaster>();//useless
        Cvs.GetComponent<CanvasScaler>().dynamicPixelsPerUnit = 50f;//pixelation des characters dans le canvas
    }

    //méthode qui génère un texte dans le canvas
    public void addText(string nom,Vector3 pos,int taille,string text,string police,Color C,TextAnchor TA)
    {
        GameObject mText = new GameObject(nom);
        mText.transform.SetParent(Cvs.transform, false);
        mText.transform.localPosition = pos;
        Text xtext = mText.AddComponent<Text>();
        xtext.color = C; //couleur du texte
        xtext.font = (Font)Resources.GetBuiltinResource<Font>(police+".ttf");//utilise la police Arial pour afficher le texte
        xtext.text = text;//texte à afficher
        xtext.fontSize = taille;//taille police
        xtext.alignment = TA;//ancrage du text dans son canvas
    }

    //méthode qui génère un panel dans le canvas
    public void addPanel(string nom, Vector3 pos, Vector2 taille, Color C)
    {
        GameObject panel = new GameObject(nom);
        panel.transform.parent = mCvs.transform;//range le panel dans son canvas
        panel.AddComponent<CanvasRenderer>();
        panel.GetComponent<RectTransform>().sizeDelta = taille;
        panel.GetComponent<RectTransform>().localPosition = pos;
        panel.AddComponent<Image>().color=C;//récupération du pseudo rendu du panel
    }
}
