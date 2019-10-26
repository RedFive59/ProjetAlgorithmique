using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Color startColor; //var qui contiendra la couleur originale du sprite ciblé
    private SpriteRenderer rend; //var qui contiendra le rendu (spriteRenderer) du sprite ciblé

    void Start()
    {
        startColor = this.GetComponent<SpriteRenderer>().color;   //récupère la couleur original depuis le rendu du sprite auquel le script est attaché

    }
    void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red; //la souris est sur le sprite sa nouvelle couleur devient rouge
    }
    void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = startColor; //la souris sort du sprite il reprend sa couleur d'origine
    }
}
