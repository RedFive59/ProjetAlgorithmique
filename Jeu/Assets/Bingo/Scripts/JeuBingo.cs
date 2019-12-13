using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;
using System.IO;

public class JeuBingo : MonoBehaviour
{
    /* Attributs
        userName, correspond au pseudo par defaut du joueur s'il n'a pas rentré de nom
        grilles, correspond aux cartons des joueurs
        grillesSelections, indique les éléments selectionné valides
        grid ainsi que gridTirage, correspondent aux grilles qui seront affichées dans la scene
    */ 
    private int ligne = 3, colonne = 9, nbgrilles = 1, score = 0, modeJeu = 0, nombreTirage = 0;
    private string userName = "Anonyme";
    private Cartons[] grilles;
    private Cartons[] grillesSelection;
    private GridManagerBingo[] grid;
    private GridManagerBingo gridTirage;

    //represente l'image du temps de cahrgement entre deux tirages
    private Image fillImg;
    //listes correspondant aux nombres tirés et à tirer
    private List<int> tirage;
    private List<int> tire;
    //correspond au temps d'attente entre deux tirage
    private float timer, waitTime = 0.0f;
    
    private bool fini = false;
    
    //definie des objets physique
    GameObject tile;
    GameObject ObjectMenuGagne;

    //Appel au moment ou le jeu est sur l'ecran
    void Awake()
    {
        this.tile = GameObject.Find(0 + ":Case" + 0 + "_" + 0);
        this.ObjectMenuGagne = GameObject.Find("ObjectToHide");

        if (!this.tile)
        {
            this.fillImg = GameObject.Find("WaitBar").GetComponent<Image>();

            getStats();

            initBingo();
            creerBingo();
        }
    }

    //fonction mise à jour à chaque image par seconde
    private void Update()
    {
        if (!this.fini)
        {
            if (this.tirage.Count > 0)
            {
                //timer de "tempsPioche" secondes
                timer += Time.deltaTime;
                this.fillImg.fillAmount = timer / waitTime;
                if (timer > waitTime)
                {
                    tirer();
                    timer = timer - waitTime;
                }
            }
            //vérifie si on selectionne une case
            select();
            //appel la fonction pour verifier si on à gagné
            if (Input.GetKeyDown("g"))
            {
                finDuJeu();
            }
            //permet de retirer la temporisation entre deux tirages pour alller plus vite
            if (Input.GetKeyDown("s"))
            {
                this.waitTime = 0;
            }
            //permet de remettre la temporisation entre deux tirages pour alller plus vite
            if (Input.GetKeyDown("n"))
            {
                this.waitTime = PlayerStats.WaitTime;
            }

        }
    }

    //verifie si le joueur à gagné lorsqu'il demande à verifier sa grille
    public void finDuJeu()
    {
        if (gagne(this.modeJeu))
        {
            ajoutDuScore(1);
        }
        else
        {
            if (this.tirage.Count == 0)
            {
                ajoutDuScore(8);
            }
            else
            {
                this.score--;
                GameObject score = GameObject.Find("Score");
                score.transform.GetComponent<TextMeshProUGUI>().text = this.score.ToString();
            }
        }
    }

    //met à jour le score et le transforme pour qu'il puisse etre ajoute au scoreBoard
    public void ajoutDuScore(int i)
    {
        this.fini = true;
        menuEndGame(this.ObjectMenuGagne, i);
        if (i == 8)
            this.score = 0;
        updateScore(i);
        if (this.score < 0)
            this.score = 0;
        this.score = (this.score * 100) / 140;


        if (this.userName != "Anonyme")
            ajoutDonneesLeaderboard(this.userName, this.score.ToString());
    }

    //recupere le nombre de grille
    private void getStats()
    {
        this.nbgrilles = PlayerStats.NbGrilles;
        this.waitTime = PlayerStats.WaitTime;
        this.modeJeu = PlayerStats.GameMode;
        this.score = PlayerStats.Score;
        this.userName = PlayerStats.UserName;
        GameObject name = GameObject.Find("NameDisplay");
        name.transform.GetComponent<TextMeshProUGUI>().text = this.userName;
    }

    //permet d'afficher le menu de fin de jeu soit pour le gagnat soit pour le perdant
    private void menuEndGame(GameObject parent, int indexMenu)
    {
        Transform[] go = parent.GetComponentsInChildren<RectTransform>(true);
        go[indexMenu].gameObject.SetActive(true);
        if(GameObject.Find("Bingo"))
            GameObject.Find("Bingo").SetActive(false);
    }

    //Ajoute le nouveau score
    private void updateScore(int i)
    {
        if(i != 8)
        {
            if (this.score < -140)
                this.score = -140;
            this.score += (this.modeJeu + 5) * 20 / this.nbgrilles;
        }

        PlayerStats.Score = this.score;
        GameObject score = GameObject.Find("Score");
        score.transform.GetComponent<TextMeshProUGUI>().text = this.score.ToString();
    }

    //initialise toutes les grilles ainsi que les listes
    private void initBingo()
    {

        this.grilles = new Cartons[this.nbgrilles];
        this.grillesSelection = new Cartons[this.nbgrilles];
        this.grid = new GridManagerBingo[this.nbgrilles];

        this.tirage = new List<int>();
        this.tire = new List<int>();
    }

    //initialise la case ainsi que la liste des nombres à tirer
    private void initTirage()
    {
        Transform parent;
        for (int i = 0; i < 90; i++) this.tirage.Add(i + 1);
        this.gridTirage = new GridManagerBingo();
        parent = GameObject.Find("GridTirage").transform;
        this.gridTirage.GenerateVal(parent.position.x, parent.position.y, parent);
    }

    //fonctione appelee pour creer le jeu de Bingo
    private void creerBingo()
    {
        Transform parent;
        List<int> ordreLigne;
        for (int i = 0; i < this.nbgrilles; i++)
        {
            this.grilles[i] = new Cartons(this.ligne, this.colonne);
            ordreLigne = genRandOrdreLigne();
            this.grilles[i].initGrille(ordreLigne);
            this.grillesSelection[i] = new Cartons(this.ligne, this.colonne);
            this.grillesSelection[i].copie(this.grilles[i]);

            this.grid[i] = new GridManagerBingo(this.grilles[i], i);
        }
        
        ajoutVal();

        for (int i = 0; i < nbgrilles; i++)
        {
            parent = GameObject.Find("GridManager " + i).transform;
            this.grid[i].GenerateGrid(parent.position.x, parent.position.y, parent);
        }

        initTirage();
    }

    //ordre aleatoire dans lequel on va remplir les lignes de la grille
    private List<int> genRandOrdreLigne()
    {
        List<int> ordreLigne = new List<int>();
        for (int i = 0; i < this.ligne; i++) ordreLigne.Add(i);

        return ordreLigne;
    }

    //ajoute les valeurs dans les grilles
    private void ajoutVal()
    {
        int[] vals = new int[this.ligne * this.nbgrilles];

        for (int i = 0; i < this.colonne; i++)
        {
            copieValCol(vals, i);
            genRand(vals, 9 + i * 10, i * 10);
            trieVal(vals);
            setValCol(vals, i);
        }
    }

    //copie les valeurs de la colonne col dans vals
    private void copieValCol(int[] vals, int col)
    {
        int i = 0;
        while (i < vals.Length)
        {
            for (int j = 0; j < this.grilles.Length; j++)
            {
                for (int k = 0; k < this.ligne; k++)
                {
                    vals[i] = this.grilles[j].getVal(k, col);
                    i++;
                }
            }
        }
    }

    //verifie si la valeur x est dans le tableau t
    private bool estdans(int x, int[] t)
    {
        foreach (int i in t)
        {
            if (i == -1) continue;
            if (x == i) return true;
        }
        return false;
    }

    //rempli la teableau t de valeur random entre min et max
    private void genRand(int[] t, int max, int min)
    {
        int ind;
        List<int> valLibre = new List<int>();
        if (min == 0) min = 1;
        for(int i = min; i < max + 1; i++)
        {
            valLibre.Add(i);
        }

        if (this.ligne * this.nbgrilles - (nbCaseVide(t)) > 9)
        {
            int nbcase = this.ligne * this.nbgrilles - (nbCaseVide(t)) - 9;
            bool fini = false;
            //liste contenant les indices des lignes pour repartire les nouvelles cases cahées
            List<int> casesACacher = new List<int>();
            for (int i = 0; i < this.ligne * this.nbgrilles; i++)
                casesACacher.Add(i);
            do
            {
                if (nbcase != 0)
                {
                    ind = Random.Range(0, casesACacher.Count);
                    if (t[casesACacher[ind]] != -1)
                    {
                        t[casesACacher[ind]] = -1;
                        nbcase--;
                    }
                    casesACacher.RemoveAt(ind);
                }
                else
                    fini = true;

            } while (!fini);
        }
        for (int i = 0; i < t.Length; i++)
        {
            //ne rempli que si la case peut l'etre (-1 correspond à une case vide)
            if (t[i] != -1)
            {
                ind = Random.Range(0, valLibre.Count);
                t[i] = valLibre[ind];
                valLibre.RemoveAt(ind);
            }
        }
    }

    //compte le nombre de case vide
    private int nbCaseVide(int[] t)
    {
        int cpt = 0;
        foreach (int i in t)
        {
            if (i == -1) cpt++;
        }
        return cpt;
    }

    //rempli la colonne col des grilles avec les valeurs dans vals
    private void setValCol(int[] vals, int col)
    {
        int i = 0;
        while (i < vals.Length)
        {
            for (int j = 0; j < this.grilles.Length; j++)
            {
                for (int k = 0; k < this.ligne; k++)
                {
                    this.grilles[j].setVal(k, col, vals[i]);
                    if (vals[i] == -1)
                        this.grillesSelection[j].setVal(k, col, vals[i]);
                    i++;
                }
            }
        }
    }

    //tri les valeurs d'un tableau
    private void trieVal(int[] val)
    {
        int min, temp;
        for (int i = 0; i < val.Length - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < val.Length; j++)
            {
                //trie uniquement si la case n'est pas vide
                if (val[j] != -1)
                {
                    if (val[j] < val[min])
                    {
                        temp = val[j];
                        val[j] = val[min];
                        val[min] = temp;
                    }
                }
            }
        }
    }

    //tire un nombre random dans la liste des nombres a tirer
    private void tirer()
    {
        if(this.tirage.Count != 0)
        {
            int ind = Random.Range(0, this.tirage.Count);
            this.gridTirage.UpdateVal(this.tirage[ind]);
            this.nombreTirage = this.tirage[ind];
            this.tirage.RemoveAt(ind);
        }
    }

    //fonctionne qui verifie si un joueur à gagné en fonction du mode de jeu
    private bool gagne(int mode)
    {
        switch (mode)
        {
            case 0:
                return verifLigne();
            case 1:
                return verif2Ligne();
            case 2:
                return verifCarton();
            default:
                return false;
        }
    }

    //mode de jeu 1, verifie si une ligne est correct
    private bool verifLigne()
    {
        int cpt = 0;
        int val;
        
        for (int i = 0; i < this.nbgrilles; i++)
        {
            for (int j = 0; j < this.ligne; j++)
            {
                for (int k = 0; k < this.colonne; k++)
                {
                    val = this.grillesSelection[i].getVal(j, k);
                    if (val != -1)
                    {
                        if (this.tire.Contains(val))
                        {
                            cpt++;
                        }
                        else
                        {
                            cpt = 0;
                            break;
                        }
                    }
                }
                if (cpt != 0) return true;
            }
        }
        return false;
    }

    //mode de jeu 2, verifie si 2 lignes est corrects
    private bool verif2Ligne()
    {
        int cpt = 0;
        int surgrilles = 0;
        int val;
        
        for (int i = 0; i < this.nbgrilles; i++)
        {
            for (int j = 0; j < this.ligne; j++)
            {
                for (int k = 0; k < this.colonne; k++)
                {
                    val = this.grillesSelection[i].getVal(j, k);
                    if (val != -1)
                    {
                        if (this.tire.Contains(val))
                        {
                            cpt++;
                        }
                        else
                        {
                            cpt = 0;
                            break;
                        }
                    }
                }
                if (cpt != 0)
                {
                    surgrilles++;
                    if (surgrilles == 2)
                        return true;
                }
            }
        }
        return false;
    }

    //mode de jeu 2, verifie si un carton est correct
    private bool verifCarton()
    {
        int cpt = 0;
        int val;
        for (int i = 0; i < this.nbgrilles; i++)
        {
            for (int j = 0; j < this.ligne; j++)
            {
                for (int k = 0; k < this.colonne; k++)
                {
                    val = grillesSelection[i].getVal(j, k);
                    if (val != -1)
                    {
                        if (tire.Contains(val)) cpt++;
                        else
                        {
                            cpt = 0;
                            break;
                        }
                    }
                }
                if (cpt == 0) break;
            }
            if (cpt != 0) return true;
        }

        return false;
    }

    /*verifie si un joueur selectionne une case pour la changer de couleur
    et mettre la valeur contenu dans la case dans la grille de selection*/
    private void select()
    {
        //voit si le joueur a pressé la souris
        if (Input.GetMouseButtonDown(0))
        {
            //crée un objet Ray pardant du centre de la camera vers la souris pour savoir sur quoi l'utilisateur pointe
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //si le rayon touche une boite de colision
            if (Physics.Raycast(ray, out hit))
            {
                //on recupere l'objet grace à son nom
                GameObject tile = GameObject.Find(hit.transform.gameObject.name);
                
                getind(tile);
            }
        }
        GameObject score = GameObject.Find("Score");
        score.transform.GetComponent<TextMeshProUGUI>().text = this.score.ToString();
    }

    //verifie sur quelle case le joueur a cliqué
    private void getind(GameObject sprite)
    {
        string name = sprite.name;
        string ind = Regex.Replace(name, "[^0-9]", "");

        // recupere les indices correspondants au numéro de grille, la colonne et la ligne 
        // de l'élément sélectionné et le converti en entier
        int numGrille = ind[0] - 48;
        int n = ind[1] - 48;
        int m = ind[2] - 48;

        // debug pour verifier si la fonction gagne() fonctionne
        // this.nombreTirage = grilles[numGrille].getVal(n, m);

        //si l'élément correspond au nombre tiré
        if (grilles[numGrille].getVal(n, m) == this.nombreTirage)
        {
            if (grillesSelection[numGrille].getVal(n, m) != -1)
            {
                //s'il est déjà selectionné on change sa couleur et on le retire des éléments sélectionnés
                if (isSelect(numGrille, n, m))
                {
                    grillesSelection[numGrille].setVal(n, m, 0);
                    changeColor(sprite, Color.white);
                }
                //sinon on l'ajoute aux éléments sélectionnés
                else
                {
                    grillesSelection[numGrille].setVal(n, m, grilles[numGrille].getVal(n, m));
                    changeColor(sprite, Color.Lerp(Color.black, Color.grey, 0.6f));
                }
            }
            this.tire.Add(this.nombreTirage);
        }
        //sinon on retire un point
        else
        {
            this.score--;
        }
    }

    //change la couleur de la case selectionnée
    private void changeColor(GameObject sprite, Color couleur)
    {
        sprite.GetComponent<SpriteRenderer>().color = couleur;
    }

    //verifie si une case etait deja selectionnée
    private bool isSelect(int b, int n, int m)
    {
        int val = grillesSelection[b].getVal(n, m);
        if (val != 0)
        {
            return true;
        }
        return false;
    }

    // Méthode pour ajouter un résultat en fin de partie
    public void ajoutDonneesLeaderboard(string name, string score)
    {
        string filePath = Path.Combine(Application.dataPath, "StreamingAssets/Leaderboard/leaderboardBingo.json");
        if (File.Exists(filePath))
        {
            var loadedData = JSON.Parse(File.ReadAllText(filePath)); // Répartition des données dans loadedData
            if (loadedData["history"] || loadedData["history"].Count == 0)
            {
                string res = "{\n\t//Historique des games de Bingo\n\t\"history\": [\n\t\t[ \"" + name + "\", \"" + score + "\" ]\n\t]\n}";
                File.WriteAllText(filePath, res);
            }
            else
            {
                int line_to_edit = 3 + loadedData["history"].Count;
                string newText = (",\n\t\t[ \"" + name + "\", \"" + score + "\" ]");
                lineChanger(newText, filePath, line_to_edit);
            }
        }
        else Debug.Log("Fichier " + filePath + " introuvable");
    }

    // Méthode pour changer une ligne spécifique dans un fichier
    static void lineChanger(string newText, string fileName, int line_to_edit)
    {
        string[] arrLine = File.ReadAllLines(fileName);
        arrLine[line_to_edit - 1] = arrLine[line_to_edit - 1] + newText;
        File.WriteAllLines(fileName, arrLine);
    }

    private void affichermat(Cartons mat)
    {
        Debug.Log("nouvelle matrice");
        for(int i =0; i < this.ligne; i++)
        {
            Debug.Log(mat.getVal(i, 0) + " " + mat.getVal(i, 1) + " " + mat.getVal(i, 2) + " " + mat.getVal(i, 3) + " " + mat.getVal(i, 4) + " " + mat.getVal(i, 5) + " " + mat.getVal(i, 6) + " " + mat.getVal(i, 7) + " " + mat.getVal(i, 8));
        }
    }

    //permet de visualiser un tableau dans la console de debug
    private void afficher(int[] val)
    {
        foreach (int i in val) Debug.Log("valeur: " + i);
    }

}