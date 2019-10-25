using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Couleur 
{
    Trefle = 0,
    Carreau = 1,
    Coeur = 2,
    Pique = 3
};
public enum Valeur
{
    Deux = 0,
    Trois = 1,
    Quatre = 2,
    Cinq = 3,
    Six = 4,
    Sept = 5,
    Huit = 6,
    Neuf = 7,
    Dix = 8,
    Vallee = 9,
    Dame = 10,
    Roi = 11,
    As = 12
};

public class Carte : MonoBehaviour
{ 
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer sr;
    public bool isFaceUp;
    private Poker poker;
    private Couleur couleur = Couleur.Trefle;
    private Valeur valeur = Valeur.Deux;
    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = Poker.generatedDeck();
        this.poker = FindObjectOfType<Poker>();
        int i = 0;
        this.couleur = 0;
        this.valeur = 0;
        foreach(string card in deck)
        {
            if(this.name == card)
            {
                this.cardFace = this.poker.cardFaces[i];
                for (int j = 0; j <= 12; j++)
                {
                    for (int k = 0; k <= 3; k++)
                    {
                        if (this.name == ((this.valeur + k) + " de " + (this.couleur + j)))
                        {
                            this.valeur += k;
                            this.couleur += j;
                            //print(this.name + " = " +this.valeur + " de " + this.couleur);
                        }
                    }
                }
            }
            i++;
        }
        this.sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isFaceUp)
        {
            this.sr.sprite = cardFace;
        }
        else
        {
            this.sr.sprite = cardBack;
        }
    }
}
