using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magasin : MonoBehaviour
{
    Canvas myCanvas2;
    GameObject cvs2;
    Color sgrey;//la couleur qui sera utilisée pour le panel

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rect2;
        RectTransform rect3;
        cvs2 = new GameObject("Canvas Panel");
        cvs2.AddComponent<Canvas>();

        myCanvas2 = cvs2.GetComponent<Canvas>();
        myCanvas2.renderMode = RenderMode.ScreenSpaceCamera; //canvas en mode resize sur la camera
        myCanvas2.transform.position = new Vector3(5f, -5f, 0);
        myCanvas2.worldCamera = FindObjectOfType<Camera>(); //cherche la premiere camera dispo dans les GO 

        rect3 = myCanvas2.GetComponent<RectTransform>();
        rect3.sizeDelta = new Vector2(11f,11f);

        GameObject panel = new GameObject("Panel");
        panel.AddComponent<CanvasRenderer>();
        Image I = panel.AddComponent<Image>();//récupération du pseudo rendu du panel
        panel.transform.SetParent(myCanvas2.transform, false);
        cvs2.transform.SetParent(this.transform, false);

        rect2 = panel.GetComponent<RectTransform>();//recup le recttransform du panel
        rect2.sizeDelta = new Vector2(750, 100);//resize le panel

        sgrey.r = 140;//set la valeur du rvb rouge
        sgrey.b = 140;//set la valeur du rvb bleu
        sgrey.g = 140;//set la valeur du rvb vert
        sgrey.a = 0.35f;//set la transparence de la couleur de min 0 à max 1
        I.color = sgrey;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
