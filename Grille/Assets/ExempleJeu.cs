using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExempleJeu : MonoBehaviour
{
    void Start()
    {
        Grille<int> grille = new Grille<int>(9, 9); // Création d'une grille de type int, de taille 9x9
        grille.setVal(0); // Initialisation à 0 de toutes les cases
        GridManager g = new GridManager(grille); // Création d'un GridManager qui permet l'affichage de la grille appelée grille
        Transform parent = GameObject.Find("GridManager").transform; // Définition de l'objet parent dans lequel les cases vont se stocker (point d'origine)
        g.GenerateGrid(0f, 0f, parent); // Affichage de la grille sur la scène
    }
}
