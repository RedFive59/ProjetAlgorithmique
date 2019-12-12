using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Color startColor; //var qui contiendra la couleur originale du sprite ciblé
    private Sprite startSprite; //Image de base des cases
    private static bool selected = false;//Indique si une case est selectionnée pour un tir
    private VisualManager VM;//VisualManager (centralisation)
    private GameObject stk = null;//Stock la case (le GO) en cours de ciblage
    private GameNavale GN; //GameNavale (centralisation)

    void Start()
    {
        GN = GameObject.FindObjectOfType<GameNavale>();
        VM = GameObject.FindObjectOfType<VisualManager>();
        startColor = this.GetComponent<SpriteRenderer>().color;   //récupère la couleur original depuis le rendu du sprite auquel le script est attaché
        startSprite= Resources.Load<Sprite>("Textures/WaterDiffuseMini2"); ;
    }

    void OnMouseOver()//Si la souris est sur une case
    {
        if (selected != true)
        {
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/CibleMini2"); //la souris est sur le sprite la case devient une cible rouge
        }
    }

    void OnMouseDown()//Si la bouton de la souris est enfoncée lorsqu'elle est sur une case
    {
        if (selected != true)
        {
            selected = true;
            stk = this.gameObject;
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/CibleMini3");//la case devient une cible rouge avec halo jaune
        }
    }

    public Vector3 getpos() //retourne la postion de la case auquel le script est attaché 
    {
        return this.transform.position;
    }

    void OnMouseExit()//Si la souris quitte la case
    {
        if (selected != true)
        {
            this.GetComponent<SpriteRenderer>().sprite = startSprite; //La case reprend sa couleur bleue
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3)) //Annule la selection de la case selectionnée
        {
            this.GetComponent<SpriteRenderer>().sprite = startSprite;
            selected = false;
            stk = null;
        }

        if (Input.GetKeyDown(KeyCode.F2)) // Valide le tir sur la case selectionnée
        {
            if (selected == true)
            {
                if (stk ?? false)
                {
                    checkTirS();
                    selected = false;
                    stk = null;
                    GN.switchX();
                }
            }
        }
    }

    public bool checkTirS() // F2 Appel les fontions de vérification des tirs, et change applique les nouveaux sprites définitivement
    {
        if (stk.GetComponent<BoxCollider2D>().enabled == false)
        {
            return false;
        }
        stk.GetComponent<BoxCollider2D>().enabled = false;

        if ((VM.getCameraVM(1).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(3).GetComponent<Camera>().enabled == true))
        {
            if (VM.getShipM(2).checkTir(new Vector3(stk.transform.position.x + 30, stk.transform.position.y - 30, stk.transform.position.z)) == true)
            {
                this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/CibleMini3");
                startSprite = Resources.Load<Sprite>("Textures/CibleMini2"); ;
                stk.GetComponent<SpriteRenderer>().color = Color.red;
                startColor = Color.red;
                return true;
            }
            this.GetComponent<SpriteRenderer>().sprite = startSprite;
            startColor = Color.grey;
            stk.GetComponent<SpriteRenderer>().color = Color.grey;
            return false;
        }
        if ((VM.getCameraVM(2).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(4).GetComponent<Camera>().enabled == true))
        {
            if (VM.getShipM(1).checkTir(new Vector3(stk.transform.position.x - 30, stk.transform.position.y - 30, stk.transform.position.z)) == true)
            {
                this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/CibleMini3");
                startSprite = Resources.Load<Sprite>("Textures/CibleMini2");
                stk.GetComponent<SpriteRenderer>().color = Color.red;
                startColor = Color.red;
                return true;
            }
            this.GetComponent<SpriteRenderer>().sprite = startSprite;
            startColor = Color.grey;
            stk.GetComponent<SpriteRenderer>().color = Color.grey;
            return false;
        }
        return false;
    }
}

