using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;
using System.IO;

public class JeuBingo : MonoBehaviour
{
    private int ligne = 3, colonne = 9, nbgrilles = 1, score = 0, modeJeu = 0;
    private string userName = "Anonyme";
    private Cartons[] grilles;
    private Cartons[] grillesSelection;
    private GridManagerBingo[] grid;
    private GridManagerBingo gridTirage;

    private Image fillImg;

    private List<int> tirage;
    private List<int> tire;

    private float timer, waitTime = 0.0f;

    private bool fini = false, commencer = false;
    
    //definie des objets physique
    GameObject tile;
    GameObject scroll;
    GameObject ObjectMenuGagne;

    //Appel au moment ou le jeu est sur l'ecran
    void Awake()
    {
        this.tile = GameObject.Find(0 + ":Case" + 0 + "_" + 0);
        this.scroll = GameObject.Find("Scrollbar");
        this.ObjectMenuGagne = GameObject.Find("ObjectToHide");

        if (!this.tile)
        {
            this.fillImg = GameObject.Find("WaitBar").GetComponent<Image>();

            getStats();
            this.waitTime = 0;

            initBingo();
            creerBingo();
        }
    }

    //fonction mise à jour à chaque image par seconde
    private void Update()
    {
        if (commencer)
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
            }
        }
        else
        {
            if (Input.GetKeyDown("space"))
            {
                commencer = true; ;
            }
            //vérifie si on selectionne une case
            select();
        }
    }

    //verifie si le joueur à gagné lorsqu'il demande à verifier sa grille
    public void finDuJeu()
    {
        if (gagne(this.modeJeu))
        {
            this.fini = true;
            afficherBINGO(this.ObjectMenuGagne);
            updateScore();
            Debug.Log(this.userName);
            if (this.userName != "Anonyme")
                ajoutDonneesLeaderboard(this.userName, this.score.ToString());
        }
        else
        {
            this.score--;

            GameObject score = GameObject.Find("Score");
            score.transform.GetComponent<TextMeshProUGUI>().text = this.score.ToString();
        }
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

    private void afficherBINGO(GameObject parent)
    {
        Transform[] go = parent.GetComponentsInChildren<RectTransform>(true);
        go[1].gameObject.SetActive(true);
    }

    //Ajoute le nouveau score
    private void updateScore()
    {
        if (this.score < 140)
            this.score = -140;
        this.score += (this.modeJeu + 5) * 20 / this.nbgrilles;

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
            //trieVal(vals);
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
            
            do
            {
                if (nbcase != 0)
                {
                    ind = Random.Range(0, this.ligne * this.nbgrilles);

                    if (t[ind] != -1)
                    {
                        t[ind] = -1;
                        nbcase--;
                    }
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
            this.tire.Add(this.tirage[ind]);
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
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

        int numGrille = ind[0] - 48;
        int n = ind[1] - 48;
        int m = ind[2] - 48;

        if(grillesSelection[numGrille].getVal(n, m) != -1)
        {
            if (isSelect(numGrille, n, m))
            {
                grillesSelection[numGrille].setVal(n, m, 0);
                changeColor(sprite, Color.white);
            }
            else
            {
                grillesSelection[numGrille].setVal(n, m, grilles[numGrille].getVal(n, m));
                changeColor(sprite, Color.Lerp(Color.black, Color.grey, 0.6f));
            }
        }

        //debug pour verifier si la fonction gagne() fonctionne
        //this.tire.Add(grilles[numGrille].getVal(n, m));

        if (!this.tire.Contains(grilles[numGrille].getVal(n, m)) && grilles[numGrille].getVal(n, m) != -1)
            this.score--;
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

    //permet de visualiser un tableau dans la console de debug
    private void afficher(int[] val)
    {
        foreach (int i in val) Debug.Log("valeur: " + i);
    }

}