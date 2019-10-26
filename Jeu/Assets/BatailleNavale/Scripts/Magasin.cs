using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magasin : MonoBehaviour
{
    Canvas myCanvas2;
    GameObject cvs2;
    RectTransform rect2;
    RectTransform rect3;
    private int Magasinpos;

    // Start is called before the first frame update
    void Start()
    {
        Magasinpos = 1;
        cvs2 = new GameObject("Canvas Panel");
        cvs2.layer = 9;
        cvs2.AddComponent<Canvas>();
        cvs2.AddComponent<CanvasScaler>().dynamicPixelsPerUnit=50;
        cvs2.AddComponent<GraphicRaycaster>();
        Color sgrey;//la couleur qui sera utilisée pour le panel

        myCanvas2 = cvs2.GetComponent<Canvas>();
        //myCanvas2.renderMode = RenderMode.ScreenSpaceCamera; //canvas en mode resize sur la camera
        myCanvas2.renderMode = RenderMode.WorldSpace;
        myCanvas2.transform.position = new Vector3(5f, -5f, 0);
        //myCanvas2.worldCamera = FindObjectOfType<Camera>(); //cherche la premiere camera dispo dans les GO 
        myCanvas2.planeDistance = 10;
        myCanvas2.sortingLayerName = "PanelLayer";
        rect3 = myCanvas2.GetComponent<RectTransform>();
        rect3.sizeDelta = new Vector2(11f,11f);

        GameObject panel = new GameObject("Panel");
        panel.transform.SetParent(myCanvas2.transform, false);
        panel.AddComponent<CanvasRenderer>();
        Image I = panel.AddComponent<Image>();//récupération du pseudo rendu du panel
        panel.layer = 9;

        cvs2.transform.SetParent(this.transform, false);

        rect2 = panel.GetComponent<RectTransform>();//recup le recttransform du panel
        rect2.sizeDelta = new Vector2(6.127f, 11.6f);//resize le panel
        rect2.localPosition = new Vector3(5.686f, 0, 0);

        sgrey.r = 180;//set la valeur du rvb rouge
        sgrey.b = 180;//set la valeur du rvb bleu
        sgrey.g = 180;//set la valeur du rvb vert
        sgrey.a = 0.5f;//set la transparence de la couleur de min 0 à max 1
        I.color = sgrey;

        panel.AddComponent<VerticalLayoutGroup>();

        GameObject panel2 = new GameObject("PanelButton");
        panel2.transform.SetParent(myCanvas2.transform, false);
        panel2.AddComponent<CanvasRenderer>();
        panel2.AddComponent<RectTransform>();
        panel2.GetComponent<RectTransform>().sizeDelta = new Vector2(1f, 11.6f);
        panel2.GetComponent<RectTransform>().localPosition = new Vector3(3.122499f, 0, 0);
        panel2.AddComponent<Button>().onClick.AddListener(MoveMagasin);
        Image I2 = panel2.AddComponent<Image>();//récupération du pseudo rendu du panel
        sgrey.r = 10;
        sgrey.b = 10;
        sgrey.g = 10;
        sgrey.a = 0.5f;
        I2.color = sgrey;

        void MoveMagasin()
        {
            if (Magasinpos == 0)
            {
                panel.transform.localPosition = new Vector3(5.686f, 0, 0);
                panel2.transform.localPosition = new Vector3(3.122499f, 0, 0);
                Magasinpos = 1;
            }
            else { if (Magasinpos == 1)
                {
                    panel.transform.localPosition = new Vector3(10.43f, 0, 0);
                    panel2.transform.localPosition = new Vector3(7.8665f, 0, 0);
                    Magasinpos = 0;
                }
         }
        }

        GameObject myText;
        Text text;
        RectTransform rectTransform;
        myText = new GameObject("Magasin");
        myText.transform.parent = panel2.transform;
        text = myText.AddComponent<Text>();
        text.color = Color.black;//couleur de texte noire
        text.font = (Font)Resources.GetBuiltinResource<Font>("Arial.ttf");//utilise la police Arial pour afficher le texte
        text.text = myText.name;
        text.fontSize = 1;
        text.alignment = TextAnchor.MiddleCenter;
        

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(5, 2);
        rectTransform.localScale = new Vector3(0.5f, 0.5f, 1);
        //rectTransform.rotation = new Quaternion(0f,0f,0f,90f);
        rectTransform.Rotate(new Vector3(0, 0, 90f));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
