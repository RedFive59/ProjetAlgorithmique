using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    //Attributs
    public string nom;
    public List<GameObject> main;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        updateAffichageMain();
    }
    public void updateAffichageMain()//Permet l'affichage des cartes du joueur si c'est à son tour
    {
        GameObject c1 = GameObject.Find("Carte_1");
        GameObject c2 = GameObject.Find("Carte_2");
        Poker poker = GameObject.Find("Poker").GetComponent<Poker>();
        if (poker.joueurs[poker.getTour()].GetComponent<Joueur>().Equals(this))
        {
            this.main[0].transform.position = c1.transform.position;
            this.main[1].transform.position = c2.transform.position;
        }
        else
        {
            this.main[0].transform.position = poker.cardPrefab.transform.position;
            this.main[1].transform.position = poker.cardPrefab.transform.position;
        }
    }
}
