using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magasin : MonoBehaviour
{
    private Canvas myCanvas2;
    private GameObject cvs2;
    private RectTransform rect2;
    private RectTransform rect3;
    private int Magasinpos;//var qui dit si le magasin est ouvert ou fermé
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        Magasinpos = 1;//ouvre le magasin au début de la scène
        cvs2 = new GameObject("Canvas Panel");
        cvs2.AddComponent<Canvas>();
        cvs2.AddComponent<CanvasScaler>().dynamicPixelsPerUnit=50;//pixelisation du texte dans le canvas (plus élevé pour éviter la bouillie)
        cvs2.AddComponent<GraphicRaycaster>();
        Color sgrey;//la couleur qui sera utilisée pour le panel

        myCanvas2 = cvs2.GetComponent<Canvas>();
        //myCanvas2.renderMode = RenderMode.ScreenSpaceCamera; //canvas en mode resize sur la camera
        myCanvas2.renderMode = RenderMode.WorldSpace;//rend le canvas indépendant de la caméra ou de la résolution
        myCanvas2.transform.position = new Vector3(5f, -5f, 0);
        //myCanvas2.worldCamera = FindObjectOfType<Camera>(); //cherche la premiere camera dispo dans les GO 
        myCanvas2.planeDistance = 10;//"aucune idée de la traduction", a permit de faire repasser les panneaux devant la grille car la caméra était trop proche= fusion des éléments
        myCanvas2.sortingLayerName = "PanelLayer";//applique le bon sortingLayer sur le canvas
        rect3 = myCanvas2.GetComponent<RectTransform>();
        rect3.sizeDelta = new Vector2(11f,11f);//resize le canvas largeur/hauteur

        GameObject panel = new GameObject("Panel");
        panel.transform.parent = myCanvas2.transform;//range le panel dans son canvas
        panel.AddComponent<CanvasRenderer>();
        Image I = panel.AddComponent<Image>();//récupération du pseudo rendu du panel

        cvs2.transform.SetParent(this.transform, false);//range le GO cvs2 dans le GO du script

        rect2 = panel.GetComponent<RectTransform>();//recup le recttransform du panel
        rect2.sizeDelta = new Vector2(6.532f, 11.6f);//resize le panel
        rect2.localPosition = new Vector3(7.026f, 0, 0);

        sgrey.r = 180;//set la valeur du rvb rouge
        sgrey.b = 180;//set la valeur du rvb bleu
        sgrey.g = 180;//set la valeur du rvb vert
        sgrey.a = 0.5f;//set la transparence de la couleur de min 0 à max 1
        I.color = sgrey;

        panel.AddComponent<VerticalLayoutGroup>();

        GameObject panel2 = new GameObject("PanelButton");
        panel2.transform.parent = myCanvas2.transform;//range panel2 dans son canvas
        //début positionnement du panel2
        panel2.AddComponent<CanvasRenderer>();
        panel2.AddComponent<RectTransform>();
        panel2.GetComponent<RectTransform>().sizeDelta = new Vector2(1f, 11.6f);
        panel2.GetComponent<RectTransform>().localPosition = new Vector3(4.26f, 0, 0);
        panel2.AddComponent<Button>().onClick.AddListener(MoveMagasin);
        //fin positionnement
        //début change la couleur du panel
        Image I2 = panel2.AddComponent<Image>();//récupération du pseudo rendu du panel
        sgrey.r = 10;
        sgrey.b = 10;
        sgrey.g = 10;
        sgrey.a = 0.5f;
        I2.color = sgrey;
        //fin couleur

        //TEXT/////////////////////////
        // Text titre du Magasin
        GameObject myText;
        RectTransform rectTransform;
        myText = new GameObject("Magasin");
        myText.transform.parent = panel2.transform;//attache le text au panel2
        text = myText.AddComponent<Text>();
        text.color = Color.black;//couleur de texte noire
        text.font = (Font)Resources.GetBuiltinResource<Font>("Arial.ttf");//utilise la police Arial pour afficher le texte
        text.text = "Magasin (fermer)";
        text.fontSize = 1;//taille police
        text.alignment = TextAnchor.MiddleCenter;//attache le text au centre de sa box, ici le panel2
        

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(9, 2);
        rectTransform.localScale = new Vector3(0.5f, 0.5f, 1);
        //rectTransform.rotation = new Quaternion(0f,0f,0f,90f);
        rectTransform.Rotate(new Vector3(0, 0, 90f));//permet de place le text à la verticale (rotation z à 90 degrés)
                                                     //TEXT/////////////////////////

        void MoveMagasin()//fonction qui répond à l'action du bouton du panel2
        {
            if (Magasinpos == 0)//si les panneaux sont rangés, déplace les deux panneaux pour l'ouverture
            {
                panel.transform.localPosition = new Vector3(7.026f, 0, 0);
                panel2.transform.localPosition = new Vector3(4.26f, 0, 0);
                text.text = "Magasin (fermer)";
                Magasinpos = 1;
            }
            else
            {
                if (Magasinpos == 1)//si les panneaux sont ouverts, déplace les deux panneaux pour la fermeture
                {
                    panel.transform.localPosition = new Vector3(11.466f, 0, 0);
                    panel2.transform.localPosition = new Vector3(8.7f, 0, 0);
                    text.text = "Magasin (ouvrir)";
                    Magasinpos = 0;
                }
            }
        }
    }

 
    // Update is called once per frame
    void Update()
    {
        
    }
}
