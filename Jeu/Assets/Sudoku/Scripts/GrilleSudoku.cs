using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

internal class GrilleSudoku : Grille<Case>
{

    public GrilleSudoku(int n, int m) : base(n, m)
    {
    }

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

    public void setVal(int i, int j, int val)
    {

    }

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

    public Boolean verifZeros()
    {
        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++)
                if (this.getVal(i, j).valeur == 0) return false;
        return true;
    }

    public void remplirGrille(int num, string difficulte)
    {
        if (!verifGrille())
        {
            string directoryPath = "Sudoku/Levels/" + difficulte + "/" + num + ".json";
            string filePath = Path.Combine(Application.dataPath, directoryPath);

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                var loadedData = JSON.Parse(dataAsJson);

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (i != 0 || j != 0)
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
            }
            else Debug.Log("Chargement impossible\nFichier " + filePath + " introuvable");
        }
    }

    public void chargementGrille(int num, string difficulte)
    {
        string directoryPath = "Sudoku/Levels/" + difficulte + "/" + num + ".json";
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
}
