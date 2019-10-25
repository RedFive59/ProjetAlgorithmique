using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    public string nom;
    public List<GameObject> main;
    public bool sonTour = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sonTour)
        {
            foreach (GameObject g in main){
                g.GetComponent<Carte>().isFaceUp = true;
            }
        }
    }
}
