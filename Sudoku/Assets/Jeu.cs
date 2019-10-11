using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jeu : MonoBehaviour
{
    void Start()
    {
        Grille<int> grille = new Grille<int>(9, 9);
        grille.setVal(0);
        GridManager g = new GridManager(grille);
        Transform parent = GameObject.Find("GridManager").transform;
        g.GenerateGrid(0f, 0f, parent);
    }
}
