using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poker : MonoBehaviour
{
    //Attributs
    public Sprite[] cardFaces;//Tableau des textures de cartes
    public GameObject cardPrefab;//Modèle de carte à dupliquer
    public GameObject playerPrefab;//Modèle de joueur à dupliquer
    public List<GameObject> deck;//Paquet de cartes
    public List<GameObject> joueurs;//Liste de tous less joueurs
    public List<GameObject> joueursManche;//Liste de tous les joueurs restants
    public static int nbJoueurs = 3;//Nombre de joueurs dans la partie
    private int tour = 0;//Indice du joueur qui doit jouer
    public int tourGlobal = 0;//Indique le numéro du tour (augmente de 1 à chaque fois que les les nbJoueurs ont joué une fois)
    public List<GameObject> flop = new List<GameObject>();//Liste des cinq cartes composant le flop
    public readonly static int BOURSEDEPART = 2500;//Bourse de départ pour les joueurs
    public static int miseManche = 10;//Mise de la manche 

    // Start is called before the first frame update
    void Start()
    {
        startGame();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int getTour()//Retourne l'entier correspondant au tour
    {
        return this.tour;
    }
    public void setTour(int tour)//Modifie l'entier correspondant au tour
    {
        this.tour = tour;
    }
    public void startGame()//Permet le démarrage de la partie
    {
        generationPaquet();
        generationJoueurs();
        distribution();
    }
    public static List<string> generatedDeck()//Renvoie une liste contenant tous les noms de cartes dans l'ordre, du tableau cardFaces
    {
        List<string> newDeck = new List<string>();
        Couleur couleur = 0;
        Valeur valeur = 0;
        for(int i = 0; i <= 3; i++)
        {
            for(int j = 0; j <= 12; j++)
            {
                newDeck.Add((valeur + j) + " de " + (couleur + i));
            }
        }
        return newDeck;
    }
    public void shuffle<T>(List<T> list)//Mélange les cartes
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
    public void generationPaquet()//Crée les GameObjects correspondant aux cartes du deck
    {
        List<string> deck = generatedDeck();
        shuffle(deck);
        foreach(string s in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(this.cardPrefab.transform.position.x, this.cardPrefab.transform.position.y, this.cardPrefab.transform.position.z), Quaternion.identity);
            newCard.name = s;
            this.deck.Add(newCard);
        }
    }
    public void generationJoueurs()//Crée les GameObjects correspondant aux joueurs
    {
        this.joueurs = new List<GameObject>(nbJoueurs);
        this.joueursManche = new List<GameObject>();
        for(int i = 1; i <= nbJoueurs; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.name = "Joueur_" + i;
            newPlayer.GetComponent<Joueur>().nom = "Joueur " + i;
            newPlayer.SetActive(true);
            this.joueurs.Add(newPlayer);
            this.joueursManche.Add(newPlayer);
        }
    }
    public void distribution()//Distribue les cartes deux cartes du deck à chaque joueur
    {
        System.Random random = new System.Random();
        int rdm;
        GameObject player;
        for(int i = 1; i <= nbJoueurs; i++)
        {
            player = GameObject.Find("Joueur_" + i);
            player.GetComponent<Joueur>().main = new List<GameObject>(2);
            for (int j = 1; j <= 2; j++)
            {
                rdm = random.Next(this.deck.Count);
                player.GetComponent<Joueur>().main.Add(this.deck[rdm]);
                this.deck[rdm].transform.position = this.cardPrefab.transform.position;
                this.deck.RemoveAt(rdm);

            }
            player.GetComponent<Joueur>().determinaisonCombinaison();
        }
    }
    public void rassemblementDeck()//Rassemble toutes les cartes du jeu dans le deck
    {
        foreach(GameObject g in this.joueurs)
        {
            g.GetComponent<Joueur>().main[0].transform.position = this.cardPrefab.transform.position;
            g.GetComponent<Joueur>().main[1].transform.position = this.cardPrefab.transform.position;
            this.deck.Add(g.GetComponent<Joueur>().main[0]);
            this.deck.Add(g.GetComponent<Joueur>().main[1]);
        }
        foreach (GameObject g in this.flop)
        {
            g.transform.position = this.cardPrefab.transform.position;
            this.deck.Add(g);
        }
        this.flop.Clear();
    }
    public void flopper()//Permet de tirer une carte du deck, de la mettre dans le flop et de l'afficher
    {
        System.Random random = new System.Random();
        int rdm = random.Next(this.deck.Count);
        GameObject carte = this.deck[rdm];
        this.deck.Remove(carte);
        this.flop.Add(carte);
        int i = this.flop.IndexOf(carte);
        GameObject pos = GameObject.Find("Tirage_" + (i + 1));
        carte.transform.position = pos.transform.position;
    }
    public void quiGagne()//Désigne le joueur gagnant et modifie sa bourse en conséquence
    {
        Combinaison c = this.joueursManche[0].GetComponent<Joueur>().combinaison;
        Combinaison c2;
        List<Joueur> gagnants = new List<Joueur>();
        foreach(GameObject g in this.joueursManche)//On cherche la meilleur combinaison
        {
            c2 = g.GetComponent<Joueur>().combinaison;
            if (c2 > c) c = c2;
        }
        foreach (GameObject g in this.joueursManche)
        {
            c2 = g.GetComponent<Joueur>().combinaison;
            if (c2 == c) gagnants.Add(g.GetComponent<Joueur>());
        }
        switch (gagnants.Count)
        {
            case 1 :
                victoire(gagnants[0]);
                break;
            default :
                do
                {
                    List<Joueur> liste = new List<Joueur>();
                    Carte max = Joueur.max(gagnants[0].l);
                    Carte tempo;
                    foreach (Joueur j in gagnants)//On cherche la meilleur combinaison
                    {
                        tempo = Joueur.max(j.l);
                        if (tempo.superieurA(max)) max = tempo;
                    }
                    foreach (Joueur j in gagnants)
                    {
                        tempo = Joueur.max(j.l);
                        if (max.Equals(tempo))
                        {
                            liste.Add(j);
                            j.l.Remove(tempo);
                        }
                    }
                    gagnants.Clear();
                    gagnants = liste;
                } while (gagnants.Count != 1);
                victoire(gagnants[0]);
                break;
        }
    }
    public void victoire(Joueur j)//Attribue au joueur j une nouvelle bourse correspondant à la somme de toutes les mises
    {
        foreach(GameObject g in this.joueurs)
        {
            j.setBourse(g.GetComponent<Joueur>().mise + j.getBourse());
            g.GetComponent<Joueur>().mise = 0;

        }
        print("J'ai gagné - " + j.nom);
    }
    public void nouvelleManche()//Gère la fin d'une manche en décidant si on relance une partie ou si le jeu s'arrête car un joueur a gagné
    {
        quiGagne();
        this.joueursManche.Clear();
        Joueur j;
        foreach (GameObject g in this.joueurs)
        {
            j = g.GetComponent<Joueur>();
            if(j.getBourse() > 0)
            {
                this.joueursManche.Add(g);
            }
        }
        switch (this.joueursManche.Count)
        {
            case 1 :
                finDeJeu();
                break;
            default :
                rassemblementDeck();
                shuffle(this.deck);
                distribution();
                /*foreach(GameObject g in this.joueurs)
                {
                    j = g.GetComponent<Joueur>();
                    print(j.name + " Main : \n");
                    foreach(GameObject l in j.main)
                    {
                        print(l.name+"\n");
                    }
                    print("l :\n");
                    foreach(Carte c in j.l)
                    {
                        print(c.valeur + " de " + c.couleur);
                    }
                    print("Combinaison : " + j.combinaison+"\n");
                    j.determinaisonCombinaison();
                    print("Combinaison : " + j.combinaison + "\n");
                }
                foreach (GameObject l in this.flop)
                {
                    print(l.name + "\n");
                }*/
                Poker.miseManche = 0;
                this.tourGlobal = 0;
                break;
        }
    }
    public void finDeJeu()//Fonction utilisée pour mettre fin au jeu
    {
        print("Fin du jeu");
    }
}
