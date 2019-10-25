using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magasin : MonoBehaviour
{
    Canvas myCanvas2;
    GameObject cvs2;
    Color sgrey;//la couleur qui sera utilisée pour le panel
    RectTransform rect2;
    RectTransform rect3;

    // Start is called before the first frame update
    void Start()
    {
        cvs2 = new GameObject("Canvas Panel");
        cvs2.layer = 9;
        cvs2.AddComponent<Canvas>();
        cvs2.AddComponent<CanvasScaler>().dynamicPixelsPerUnit=50;
        cvs2.AddComponent<GraphicRaycaster>();

        myCanvas2 = cvs2.GetComponent<Canvas>();
        myCanvas2.renderMode = RenderMode.ScreenSpaceCamera; //canvas en mode resize sur la camera
        myCanvas2.transform.position = new Vector3(5f, -5f, 0);
        myCanvas2.worldCamera = FindObjectOfType<Camera>(); //cherche la premiere camera dispo dans les GO 
        myCanvas2.planeDistance = 10;
        rect3 = myCanvas2.GetComponent<RectTransform>();
        rect3.sizeDelta = new Vector2(11f,11f);

        GameObject panel = new GameObject("Panel");
        panel.AddComponent<CanvasRenderer>();
        Image I = panel.AddComponent<Image>();//récupération du pseudo rendu du panel
        panel.transform.SetParent(myCanvas2.transform, false);
        panel.layer = 9;

        cvs2.transform.SetParent(this.transform, false);

        rect2 = panel.GetComponent<RectTransform>();//recup le recttransform du panel
        rect2.sizeDelta = new Vector2(44f, 528f);//resize le panel
        rect2.localPosition = new Vector3(374f, 0, 0);
        sgrey = Color.black;
        sgrey.a = 0.6f;//set la transparence de la couleur de min 0 à max 1
        I.color = sgrey;

        GameObject myText;
        Text text;
        RectTransform rectTransform;
        myText = new GameObject();
        myText.transform.parent = panel.transform;
        text = myText.AddComponent<Text>();
        text.color = Color.black;//couleur de texte noire
        text.font = (Font)Resources.GetBuiltinResource<Font>("Arial.ttf");//utilise la police Arial pour afficher le texte
        text.text = myText.name;
        text.fontSize = 1;
        text.alignment = TextAnchor.MiddleCenter;
        

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(1, 2);
        rectTransform.localScale = new Vector3(0.5f, 0.5f, 1);
        rectTransform.rotation = new Quaternion(1f,1f,1f,1f);
    }
    /*
        sgrey.r = 140;//set la valeur du rvb rouge
        sgrey.b = 140;//set la valeur du rvb bleu
        sgrey.g = 140;//set la valeur du rvb vert
        */
    // Update is called once per frame
    void Update()
    {
        
    }
}
