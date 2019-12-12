using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Poker : MonoBehaviour
{
    //Attributs
    public Sprite[] cardFaces;//Tableau des textures de cartes
    public string[] nomJoueur;//Tableau contenant tous les noms des joueurs
    public GameObject cardPrefab;//Modèle de carte à dupliquer
    public GameObject playerPrefab;//Modèle de joueur à dupliquer
    public List<GameObject> deck;//Paquet de cartes
    public List<GameObject> joueurs;//Liste de tous les joueurs de la partie
    public static int nbJoueurs;//Nombre de joueurs dans la partie
    private int tour = 0;//Indice du joueur qui doit jouer
    public int tourGlobal = 0;//Indique le numéro du tour
    public List<GameObject> flop = new List<GameObject>(5);//Liste d'un maximum de 5 cartes composant la river
    public static int BOURSEDEPART;//Bourse de départ pour les joueurs
    public static int miseManche = 0;//Mise de la manche
    public int smallBlind;//Valeur de la petite blinde
    public int bigBlind;//Valeur de la grosse blinde
    public GameObject panel;//GameObject permettant de bloquer toutes interactions
    public GameObject text;//Texte d'affichage du/des gagnant(s)
    public string nomDuGagnant;//Stocke le nom du gagnant de la partie

    // Start is called before the first frame update
    void Start()
    {
        updateInfosMenu();
        startGame();
        DontDestroyOnLoad(gameObject);
        //StartCoroutine(affichageGagnant("Nicolas"));
    }
    private void updateInfosMenu()//Récupère les informatiions de la scène MenuPoker
    {
        GameObject gameSettings = GameObject.Find("GameSettings");
        if (gameSettings)//Paramètres du menu
        {
            nbJoueurs = gameSettings.GetComponent<PokerValue>().nbJoueurs;
            this.nomJoueur = new string[nbJoueurs];
            BOURSEDEPART = gameSettings.GetComponent<PokerValue>().bourse;
            for(int i = 0; i < nbJoueurs; i++)
            {
                switch (i)
                {
                    case 0:
                        nomJoueur[i] = gameSettings.GetComponent<PokerValue>().nomJoueur1;
                        break;
                    case 1:
                        nomJoueur[i] = gameSettings.GetComponent<PokerValue>().nomJoueur2;
                        break;
                    case 2:
                        nomJoueur[i] = gameSettings.GetComponent<PokerValue>().nomJoueur3;
                        break;
                    case 3:
                        nomJoueur[i] = gameSettings.GetComponent<PokerValue>().nomJoueur4;
                        break;
                    case 4:
                        nomJoueur[i] = gameSettings.GetComponent<PokerValue>().nomJoueur5;
                        break;
                }
            }
        }
        else //Paramètres si lancement sans menu
        {
            nbJoueurs = 2;
            this.nomJoueur = new string[nbJoueurs];
            BOURSEDEPART = 3000;
            for(int i = 0; i < nbJoueurs; i++)
            {
                this.nomJoueur[i] = "Joueur  " + (i+1);
            }
        }
        Destroy(gameSettings);
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
        smallBlind = 50;
        bigBlind = 2 * smallBlind;
        generationPaquet();
        generationJoueurs();
        randomSmallBlind();
        distribution();
    }
    public static List<string> generatedDeck()//Renvoie une liste contenant tous les noms de cartes dans l'ordre du tableau "cardFaces"
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
    public void shuffle<T>(List<T> list)//Mélange la liste de de carte "list" passée en paramètre
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
    public void generationPaquet()//Génère les cartes du deck et les stocke dans l'attribut "deck"
    {
        List<string> deck = generatedDeck();
        shuffle(deck);
        foreach(string s in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(this.cardPrefab.transform.position.x, this.cardPrefab.transform.position.y, this.cardPrefab.transform.position.z), Quaternion.identity);
            newCard.name = s;
            newCard.GetComponent<SpriteRenderer>().sprite = newCard.GetComponent<Carte>().cardBack;//Définit toutes les cartes comme étant de dos
            this.deck.Add(newCard);
        }
    }
    public void generationJoueurs()//Génère les joueurs de la partie et les stocke dans l'attribut "joueurs"
    {
        this.joueurs = new List<GameObject>(nbJoueurs);
        for(int i = 1; i <= nbJoueurs; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.name = "Joueur_" + i;
            newPlayer.GetComponent<Joueur>().nom = this.nomJoueur[i-1];
            newPlayer.GetComponent<Joueur>().setBourse(BOURSEDEPART);
            newPlayer.SetActive(true);
            this.joueurs.Add(newPlayer);
        }
    }
    public void distribution()//Distribue deux cartes du deck à chaque joueur
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
        //Gestion des Blindes
        rdm = random.Next(nbJoueurs - 1);//Désigne la petite blinde
        Joueur tempo;
        miseManche = bigBlind;
        for (int i = 0; i < nbJoueurs; i++)
        {
            if(joueurs[i].GetComponent<Joueur>().isSmallBlind)//Cherche la petite blinde
            {
                tempo = joueurs[i].GetComponent<Joueur>();
                tempo.isSmallBlind = true;
                tempo.isBigBlind = false;
                if (tempo.getBourse() > smallBlind)
                {
                    tempo.diminuerBourse(smallBlind);
                    tempo.mise = smallBlind;
                }
                else//Cas où la blinde est supérieur au tapis du joueur. Ici, décide que le joueur fait alors un tapis pour la blinde systèmatiquement
                {
                    tempo.mise = tempo.getBourse();
                    tempo.setBourse(0);
                }
            }
            else
            {
                if(joueurs[i].GetComponent<Joueur>().isBigBlind)//Cherche la grosse blinde
                {
                    tempo = joueurs[i].GetComponent<Joueur>();
                    tempo.isBigBlind = true;
                    tempo.isSmallBlind = false;
                    if (tempo.getBourse() > bigBlind)
                    {
                        tempo.diminuerBourse(bigBlind);
                        tempo.mise = bigBlind;
                    }
                    else//Cas où la blinde est supérieur au tapis du joueur. Ici, décide que le joueur fait alors un tapis pour la blinde systèmatiquement
                    {
                        tempo.mise = tempo.getBourse();
                        tempo.setBourse(0);
                    }
                }
                else
                {
                    joueurs[i].GetComponent<Joueur>().isSmallBlind = false;
                    joueurs[i].GetComponent<Joueur>().isBigBlind = false;
                }
            }
        }
        triBlinde();
    }
    public void rassemblementDeck()//Rassemble toutes les cartes du jeu et les stocke dans l'attribut "deck"
    {
        foreach(GameObject g in this.joueurs)
        {
            g.GetComponent<Joueur>().main[0].transform.position = this.cardPrefab.transform.position;
            g.GetComponent<Joueur>().main[1].transform.position = this.cardPrefab.transform.position;
            this.deck.Add(g.GetComponent<Joueur>().main[0]);
            this.deck.Add(g.GetComponent<Joueur>().main[1]);
            g.GetComponent<Joueur>().main[0].GetComponent<SpriteRenderer>().sprite = g.GetComponent<Joueur>().main[0].GetComponent<Carte>().cardBack;
            g.GetComponent<Joueur>().main[0].GetComponent<SpriteRenderer>().sprite = g.GetComponent<Joueur>().main[1].GetComponent<Carte>().cardBack;
        }
        foreach (GameObject g in this.flop)
        {
            g.transform.position = this.cardPrefab.transform.position;
            g.GetComponent<SpriteRenderer>().sprite = g.GetComponent<Carte>().cardBack;
            this.deck.Add(g);
        }
        this.flop.Clear();
    }
    public void flopper(int y)//Pioche "y" cartes aléatoires du deck afin de les dévoiler et de les stocker dans le fop
    {
        System.Random random;
        int rdm;
        GameObject carte;
        int i;
        GameObject pos ;
        while(y > 0)
        {
            random = new System.Random();
            rdm = random.Next(this.deck.Count);
            carte = this.deck[rdm];
            this.deck.Remove(carte);
            this.flop.Add(carte);
            carte.GetComponent<SpriteRenderer>().sprite = carte.GetComponent<Carte>().cardFace;
            i = this.flop.IndexOf(carte);
            pos = GameObject.Find("Tirage_" + (i + 1));
            carte.transform.position = pos.transform.position;
            y--;
        }
    }
    public List<Joueur> quiGagne()//Renvoie une liste de joueur correspondant au(x) gagnant(s) de la manche
    {
        List<GameObject> joueursManche = new List<GameObject>();
        foreach(GameObject g in joueurs)
        {
            if (!g.GetComponent<Joueur>().aPasse) joueursManche.Add(g);
        }
        Combinaison c = joueursManche[0].GetComponent<Joueur>().combinaison;
        Combinaison c2;
        List<Joueur> gagnants = new List<Joueur>();
        foreach(GameObject g in joueursManche)//On cherche la meilleur combinaison
        {
            c2 = g.GetComponent<Joueur>().combinaison;
            if (c2 > c) c = c2;
        }
        foreach (GameObject g in joueursManche)
        {
            c2 = g.GetComponent<Joueur>().combinaison;
            if (c2 == c) gagnants.Add(g.GetComponent<Joueur>());
        }
        switch (gagnants.Count)
        {
            case 1 :
                return victoire(gagnants);
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
                } while (gagnants.Count != 1 && gagnants.Count != 0);
                return victoire(gagnants);
        }
    }
    public List<Joueur> victoire(List<Joueur> j)//Répartie les mises de tous les joueurs au(x) gagnant(s) et renvoie la liste contenant le(s) gagnant(s)
    {
        int x = 0;
        List<Joueur> gagnant = new List<Joueur>();
        foreach(GameObject g in this.joueurs)
        {
            x += g.GetComponent<Joueur>().mise;
            g.GetComponent<Joueur>().mise = 0;
        }
        foreach (Joueur a in j)
        {
            a.setBourse(a.getBourse() + x/j.Count);
            gagnant.Add(a);
        }
        return gagnant;
    }
    public void nouvelleManche()//Permet de passer à la manche suivante s'il reste plus d'un joueur. Sinon, appelle la fonction finDeJeu
    {
        List<Joueur> list = quiGagne();
        int joueursRestant = 0;
        bool b = true;
        if (nombreDeJoueurPasse() == nbJoueurs - 1) b = false;
        foreach(GameObject g in joueurs)
        {
            if (g.GetComponent<Joueur>().getBourse() > 0)
            {
                g.GetComponent<Joueur>().aPasse = false;
                g.GetComponent<Joueur>().aJoue = false;
                g.GetComponent<Joueur>().nbManche++;
                joueursRestant++;
                nomDuGagnant = g.GetComponent<Joueur>().nom;
            }
            else g.GetComponent<Joueur>().aPasse = true;
        }
        if (joueursRestant > 1)
        {
            Poker.miseManche = 0;
            this.tourGlobal = 0;
            smallBlind = smallBlind + 25;
            bigBlind = 2 * smallBlind;
            rassemblementDeck();
            shuffle(this.deck);
            moveBlind();
            distribution();
            tour = nbJoueurs-1;
            gameObject.GetComponent<ButtonHandler>().ts();
            StartCoroutine(affichageGagnant(list,b,false));
        }
        else
        {
            StartCoroutine(affichageGagnant(list, b,true));
        }
    }
    public void finDeJeu()//Mets fin à la partie et enregistre les scores et statistiques dans un fichiers .JSON pour le leaderboard du poker
    {
        triClasssement();
        int nbMancheTotal = GameObject.Find(nomDuGagnant).GetComponent<Joueur>().nbManche;
        for(int i = 0; i < nbJoueurs; i++)
        {
            Joueur j = joueurs[i].GetComponent<Joueur>();
            int score = 100*(j.nbManche / nbMancheTotal);
            gameObject.GetComponent<PokerLeaderboard>().ajoutDonneesLeaderboard(j.nom,(i+1) + " / " + nbJoueurs,j.nbManche.ToString(),score);
        }
        SceneManager.LoadScene("Victoire");
    }
    public int nombreDeJoueurPasse()//Indique le nombre de joueur qui se sont couchés
    {
        int x = 0;
        foreach(GameObject g in joueurs)
        {
            if (g.GetComponent<Joueur>().aPasse) x++;
        }
        return x;
    }
    public bool toutLeMondeAJoue()//Renvoie vrai si tous les joueurs ont joué lors de ce tour
    {
        foreach(GameObject g in joueurs)
        {
            if (!g.GetComponent<Joueur>().aPasse && !g.GetComponent<Joueur>().aJoue) return false;
        }
        return true;
    }
    public void setJoueursAJoue(bool a)//Remplace la valeur du booléen "aJoué" de tous les joueurs, en même temps, par le paramètre "a".
    {
        foreach(GameObject g in joueurs)
        {
            g.GetComponent<Joueur>().aJoue = a;
        }
    }
    public void triBlinde()//Modifie l'ordre de passage des joueurs en mettant la petite blindes en dernière et la grosse blinde en avant dernière
    {
        GameObject tempo;
        while (!joueurs[nbJoueurs - 1].GetComponent<Joueur>().isBigBlind)
        {
            for(int i =0; i< nbJoueurs-1; i++)
            {
                tempo = joueurs[i];
                joueurs[i] = joueurs[i + 1];
                joueurs[i + 1] = tempo;
            }
        }
    }
    public void randomSmallBlind()//Donne le jeton de petite blinde à un joueur aléatoire
    {
        System.Random random = new System.Random();
        int rdm = random.Next(nbJoueurs - 1);
        Joueur tempo = joueurs[rdm].GetComponent<Joueur>();
        tempo.isSmallBlind = true;
        tempo.isBigBlind = false;
        tempo = joueurs[(rdm+1)%nbJoueurs].GetComponent<Joueur>();
        tempo.isSmallBlind = false;
        tempo.isBigBlind = true;
    }
    public void moveBlind()//Passe les blindes au joueur suivant
    {
        int small = 0, big = 0;
        bool c1 = true, c2 = true;
        for(int i = 0; i < nbJoueurs; i++)
        {
            if (joueurs[i].GetComponent<Joueur>().isSmallBlind)
            {
                small = i;
                joueurs[i].GetComponent<Joueur>().isSmallBlind = false;
            }
            if (joueurs[i].GetComponent<Joueur>().isBigBlind)
            {
                big = i;
                joueurs[i].GetComponent<Joueur>().isBigBlind = false;
            }
        }
        for(int i = 1; i <= nbJoueurs - 1; i++)
        {
            if (!joueurs[(nbJoueurs + small - i) % nbJoueurs].GetComponent<Joueur>().aPasse && c1)
            {
                c1 = false;
                joueurs[(nbJoueurs + small - i) % nbJoueurs].GetComponent<Joueur>().isSmallBlind = true;
                joueurs[(nbJoueurs + small - i) % nbJoueurs].GetComponent<Joueur>().isBigBlind = false;
            }
            if (!joueurs[(nbJoueurs + big - i) % nbJoueurs].GetComponent<Joueur>().aPasse && c2)
            {
                c2 = false;
                joueurs[(nbJoueurs + big - i) % nbJoueurs].GetComponent<Joueur>().isSmallBlind = false;
                joueurs[(nbJoueurs + big - i) % nbJoueurs].GetComponent<Joueur>().isBigBlind = true;
            }
            if (!c1 && !c2) break;
        }
    }
    public IEnumerator affichageGagnant(List<Joueur> j, bool b, bool d)//Gère l'écran de transition indiquant quel(s) joueur(s) a(ont) gagné la manche
    {
        string comb="", gagnant = "";
        for (int i=0; i < j.Count; i++)
        {
            if(i == j.Count - 1)
            {
                gagnant = gagnant + j[i].nom;
                comb = j[i].combinaison.ToString();
            }
            else gagnant = gagnant + j[i].nom +", ";
        }
        panel.SetActive(true);
        if (b)
        {
            GameObject.Find("Combinaison").GetComponent<TextMeshProUGUI>().text = "Avec " + j[0].combinaisonToString();
        }
        else
        {
            GameObject.Find("Combinaison").GetComponent<TextMeshProUGUI>().text = "Car tout le monde s'est couché";
        }
        GameObject.Find("Combinaison").GetComponent<TextMeshProUGUI>().enabled = true;
        text.GetComponent<TextMeshProUGUI>().text = gagnant + " a gagné";
        text.GetComponent<TextMeshProUGUI>().enabled = true;
        if (!d)
        {
            GameObject.Find("IncBlind").GetComponent<TextMeshProUGUI>().text = "Augmentation des blindes : " + smallBlind + " / " + bigBlind;
            GameObject.Find("IncBlind").GetComponent<TextMeshProUGUI>().enabled = true;
        }
        foreach (GameObject c in joueurs[tour].GetComponent<Joueur>().main)
        {
            c.GetComponent<BoxCollider>().enabled = false;
        }
        if (d)
        {
            rassemblementDeck();
        }
        yield return new WaitForSeconds(3);
        panel.SetActive(false);
        text.GetComponent<TextMeshProUGUI>().enabled = false;
        GameObject.Find("IncBlind").GetComponent<TextMeshProUGUI>().enabled = false;
        GameObject.Find("Combinaison").GetComponent<TextMeshProUGUI>().enabled = false;
        foreach (GameObject c in joueurs[tour].GetComponent<Joueur>().main)
        {
            c.GetComponent<BoxCollider>().enabled = true;
        }
        if (d)
        {
            finDeJeu();
        }
    }
    public void triClasssement()//Tri la liste "joueurs" en fonction du nombre de manches survécues par les joueurs. i = 0 -> 1er; i = 1 -> 2ème ...
    {
        int max;
        GameObject tempo;
        for (int i = 0; i < nbJoueurs - 1; i++)
        {
            max = i;
            for(int j = i; j < nbJoueurs; j++)
            {
                if(joueurs[j].GetComponent<Joueur>().nbManche >= joueurs[max].GetComponent<Joueur>().nbManche)
                {
                    max = j;
                }
            }
            tempo = joueurs[i];
            joueurs[i] = joueurs[max];
            joueurs[max] = tempo;
        }
    }
}
