using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Color startColor; //var qui contiendra la couleur originale du sprite ciblé
    private static bool selected = false;
    private VisualManager VM;
    private GameObject stk = null;
    private GameNavale GN;

    void Start()
    {
        GN = GameObject.FindObjectOfType<GameNavale>();
        VM = GameObject.FindObjectOfType<VisualManager>();
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
            stk = this.gameObject;
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
                stk.GetComponent<SpriteRenderer>().color = Color.red;
                startColor = Color.red;
                return true;
            }
            startColor = Color.grey;
            stk.GetComponent<SpriteRenderer>().color = Color.grey;
            return false;
        }
        if ((VM.getCameraVM(2).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(4).GetComponent<Camera>().enabled == true))
        {
            //VM.getShipM(1).checkTir(new Vector3(stk.transform.position.x - 30, stk.transform.position.y - 30, stk.transform.position.z));
            if (VM.getShipM(1).checkTir(new Vector3(stk.transform.position.x - 30, stk.transform.position.y - 30, stk.transform.position.z)) == true)
            {
                stk.GetComponent<SpriteRenderer>().color = Color.red;
                startColor = Color.red;
                return true;
            }
            startColor = Color.grey;
            stk.GetComponent<SpriteRenderer>().color = Color.grey;
            return false;
        }
        Debug.Log("FAIL CONDITION");
        return false;
    }
}

