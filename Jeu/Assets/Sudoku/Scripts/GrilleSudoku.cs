using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

internal class GrilleSudoku : Grille<Case>
{
    // Constructeur de base faisant appel au constructeur de Grille
    public GrilleSudoku(int n, int m) : base(n, m)
    {
    }

    // Fonction pour initialiser toutes les cases à 0
    public void initVal(int val)
    {
        for (int i = 0; i < this.rows; i++)
        {
            for (int j = 0; j < this.cols; j++)
            {
                Case c = new Case(val);
                this.setVal(i, j, c);
            }
        }
    }

    // Fonction global pour vérifier la grille
    public Boolean verifGrille()
    {
        for (int i = 0; i < 9; i++)
        {
            if (!verifLigne(i) ||
            !verifColonne(i) ||
            !verifCarre(i+1) ||
            !verifZeros())
            {
                return false;
            }
        }
        return true;
    }

    // Check la ligne nbLigne pour vérifier les doublons
    public Boolean verifLigne(int nbLigne)
    {
        List<int> list = new List<int>();
        int val;
        for (int n = 0; n < 9; n++)
        {
            val = this.getVal(nbLigne, n).valeur;
            if(val != 0)
            {
                if (list.Contains(val)) return false;
                else list.Add(val);
            }
        }
        return true;
    }

    // Check la colonne nbColonne pour vérifier les doublons
    public Boolean verifColonne(int nbColonne)
    {
        List<int> list = new List<int>();
        int val;
        for (int n = 0; n < 9; n++)
        {
            val = this.getVal(n, nbColonne).valeur;
            if (val != 0)
            {
                if (list.Contains(val)) return false;
                else list.Add(val);
            }
        }
        return true;
    }

    // Retourne le numéro du carré où la case [i,j] est présente. Les numéros de carré sont numérotés dans l'ordre de la gauche vers la droite puis orsqu'on passe à la ligne suivante on ajoute 1 jusqu'au carré 9.
    public int numCarre(int i, int j)
    {
        if (i < 3)
        {
            if (j < 3) return 1;
            if (j < 6) return 4;
            if (j < 9) return 7;
        }
        if (i < 6)
        {
            if (j < 3) return 2;
            if (j < 6) return 5;
            if (j < 9) return 8;
        }
        if (i < 9)
        {
            if (j < 3) return 3;
            if (j < 6) return 6;
            if (j < 9) return 9;
        }
        return -1;
    }

    // Check que le carré nCarre n'a pas 2 fois la même valeur en lui
    public Boolean verifCarre(int nCarre)
    {
        List<int> list = new List<int>();
        int val;
        switch (nCarre)
        {
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            case 2:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            case 3:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            case 4:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            case 5:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            case 6:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            case 7:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            case 8:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            case 9:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        val = this.getVal(i, j).valeur;
                        if (val != 0)
                        {
                            if (list.Contains(val)) return false;
                            else list.Add(val);
                        }
                    }
                }
                break;
            default:
                return false;
        }
        return true;
    }

    // Check la présence de 0 dans la grille
    public Boolean verifZeros()
    {
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if (this.getVal(i, j).valeur == 0) return false;
        return true;
    }

    // Fonction de debug pour remplir la grille directement
    public void remplirGrille(string num, string difficulte)
    {
        if (!verifGrille())
        {
            string directoryPath = "StreamingAssets/SudokuLevels/" + difficulte + "/" + num + ".json";
            string filePath = Path.Combine(Application.dataPath, directoryPath);

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                var loadedData = JSON.Parse(dataAsJson);

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (this.getVal(i, j).valeur == 0)
                        {
                            this.getVal(i, j).changeable = true;
                            this.getVal(i, j).setValeur(loadedData["tab"][i][j]);
                            this.getVal(i, j).retraitIndices();
                        }
                    }
                }
            }
            else Debug.Log("Chargement impossible\nFichier " + filePath + " introuvable");
        }
    }

    // Fonction a appeler pour sauvegarde la grille, sa difficulté et son numéro de grille
    // A ajouter dans cette fonction : Save des notes et du timer
    public void sauvegardeGrille()
    {
        Jeu jeu = GameObject.Find("Jeu").GetComponent<Jeu>();
        string num = jeu.numGrille;
        string difficulte = jeu.difficulte;
        string timer = jeu.temps.ToString();
        string timerString = jeu.affichageTemps;
        string directoryPath = "Saves/sauvegardeSudoku.json";
        string filePath = Path.Combine(Application.dataPath, directoryPath);
        string save = "{\n\t//Fichier de sauvegarde de la grille du Sudoku afin de reprendre une partie abandonnée\n";

        if (File.Exists(filePath))
        {
            save += "\t\"timer\": \"" + timer + "\",\n";
            save += "\t\"timerString\": \"" + timerString + "\",\n";
            save += "\t\"num\": " + num + ",\n";
            save += "\t\"difficulte\": \"" + difficulte + "\",\n";
            save += this.ToString();
            File.WriteAllText(filePath, save);
        }
        else Debug.Log("Sauvegarde impossible\nFichier " + filePath + " introuvable");
    }

    // Fonction a appeler pour charger les différents niveaux qui ont été enregistrés
    public void chargementGrille(string num, string difficulte)
    {
        string directoryPath = "StreamingAssets/SudokuLevels/" + difficulte + "/" + num + ".json";
        string filePath = Path.Combine(Application.dataPath, directoryPath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            var loadedData = JSON.Parse(dataAsJson);
            
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (loadedData["tabTrou"][i][j] == 1)
                    {
                        this.getVal(i, j).changeable = false;
                        this.getVal(i, j).setValeur(loadedData["tab"][i][j]);
                    }
                }
            }
        }
        else Debug.Log("Chargement impossible\nFichier " + filePath + " introuvable");
    }

    // Rempli la grille avec le fichier sauvegardeSudoku
    public void chargementGrilleSauvegarde()
    {
        string directoryPath = "Saves/sauvegardeSudoku.json";
        string filePath = Path.Combine(Application.dataPath, directoryPath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            var loadedData = JSON.Parse(dataAsJson);
            chargementGrille(loadedData["num"].ToString(), loadedData["difficulte"]);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (this.getVal(i, j).valeur == 0 && loadedData["tabTrou"][i][j] == 1)
                    {
                        this.getVal(i, j).changeable = true;
                        this.getVal(i, j).setValeur(loadedData["tab"][i][j]);
                    }
                }
            }
        }
        else Debug.Log("Chargement impossible\nFichier " + filePath + " introuvable");
    }

    // Override du ToString pour faciliter la sauvegarde (et la lecture humaine) dans un fichier .json
    public override string ToString()
    {
        string res = "\t\"tab\": [\n";
        for (int i = 0; i < 9; i++)
        {
            res += "\t\t[";
            for (int j = 0; j < 9; j++)
            {
                if (j != 0 && j % 3 == 0) res += " ";
                if(j != 8) res += this.getVal(i, j).valeur + ",";
                else res += this.getVal(i, j).valeur;
            }
            if (i != 8)
            {
                res += "],\n";
                if (i != 0 && (i + 1) % 3 == 0) res += "\n";
            }
            else res += "]\n";
        }
        res += "\t],\n" +
            "\t\"tabTrou\": [\n";
        for (int i = 0; i < 9; i++)
        {
            res += "\t\t[";
            for (int j = 0; j < 9; j++)
            {
                if (j != 0 && j % 3 == 0) res += " ";
                if (j != 8)
                    if(this.getVal(i, j).valeur != 0) res += "1" + ",";
                    else res += "0" + ",";
                else
                    if (this.getVal(i, j).valeur != 0) res += "1";
                    else res += "0";
            }
            if (i != 8)
            {
                res += "],\n";
                if (i != 0 && (i + 1) % 3 == 0) res += "\n";
            }
            else res += "]\n";
        }
        res += "\t]\n}";
        return res;
    }
}
