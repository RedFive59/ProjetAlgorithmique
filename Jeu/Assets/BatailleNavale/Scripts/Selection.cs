using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Color startColor; //var qui contiendra la couleur originale du sprite ciblé
    private Sprite startSprite;
    private static bool selected = false;
    private VisualManager VM;
    private GameObject stk = null;
    private GameNavale GN;

    void Start()
    {
        GN = GameObject.FindObjectOfType<GameNavale>();
        VM = GameObject.FindObjectOfType<VisualManager>();
        startColor = this.GetComponent<SpriteRenderer>().color;   //récupère la couleur original depuis le rendu du sprite auquel le script est attaché
        startSprite= Resources.Load<Sprite>("Textures/WaterDiffuseMini2"); ;
    }

    void OnMouseOver()
    {
        if (selected != true)
        {
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/CibleMini2"); //la souris est sur le sprite sa nouvelle couleur devient rouge
        }
    }

    void OnMouseDown()
    {
        if (selected != true)
        {
            selected = true;
            stk = this.gameObject;
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/CibleMini3");
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
            this.GetComponent<SpriteRenderer>().sprite = startSprite;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            this.GetComponent<SpriteRenderer>().sprite = startSprite;
            selected = false;
            stk = null;
        }

        if (Input.GetKeyDown(KeyCode.F2))
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

    public bool checkTirS()
    {
        if (stk.GetComponent<BoxCollider2D>().enabled == false)
        {
            return false;
        }
        stk.GetComponent<BoxCollider2D>().enabled = false;

        if ((VM.getCameraVM(1).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(3).GetComponent<Camera>().enabled == true))
        {
           // VM.getShipM(2).checkTir(new Vector3(stk.transform.position.x + 30, stk.transform.position.y - 30, stk.transform.position.z));
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
            //VM.getShipM(1).checkTir(new Vector3(stk.transform.position.x - 30, stk.transform.position.y - 30, stk.transform.position.z));
            if (VM.getShipM(1).checkTir(new Vector3(stk.transform.position.x - 30, stk.transform.position.y - 30, stk.transform.position.z)) == true)
            {
                this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/CibleMini3");
                startSprite = Resources.Load<Sprite>("Textures/CibleMini2");
                stk.GetComponent<SpriteRenderer>().color = Color.red;
                startColor = Color.red;
               // startSprite = Resources.Load<Sprite>("Textures/CibleMini3");
                return true;
            }
            this.GetComponent<SpriteRenderer>().sprite = startSprite;
            startColor = Color.grey;
            stk.GetComponent<SpriteRenderer>().color = Color.grey;
            return false;
        }
        Debug.Log("FAIL CONDITION");
        return false;
    }
}

