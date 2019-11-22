using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Color startColor; //var qui contiendra la couleur originale du sprite ciblé
    private static bool selected = false;
    private VisualManager VM;
    private GameNavale GN;

    void Start()
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        GN = GameObject.FindObjectOfType<GameNavale>();
        startColor = this.GetComponent<SpriteRenderer>().color;   //récupère la couleur original depuis le rendu du sprite auquel le script est attaché
    }

    void OnMouseOver()
    {
        if (selected != true)
        {
            this.GetComponent<SpriteRenderer>().color = new Color32(133, 75, 222, 255); ; //la souris est sur le sprite sa nouvelle couleur devient rouge
        }
    }

    void OnMouseDown()
    {
        if (selected != true)
        {
            selected = true;
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public Vector3 getpos()
    {
        return this.transform.position;
    }

    void OnMouseExit()
    {
        if (selected != true)
        {
            this.GetComponent<SpriteRenderer>().color = startColor; //la souris sort du sprite il reprend sa couleur d'origine
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            this.GetComponent<SpriteRenderer>().color = startColor;
            selected = false;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            checkTirS();
        }
    }

    public bool checkTirS()
    {
        if(this.gameObject.GetComponent<BoxCollider2D>().enabled == false){
           return false;
        }
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (VM.getCameraVM(3).GetComponent<Camera>().enabled == true)
        {
            VM.getShipM(2).checkTir(this.gameObject.transform.position);
        }
        if (VM.getCameraVM(4).GetComponent<Camera>().enabled == true)
        {
            if (VM.getShipM(1).checkTir(this.gameObject.transform.position) == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(192, 72, 73, 255);
                return true;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                return false;
            }
        }
        return false;
    }
}

