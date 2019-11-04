using System;
using System.Collections;
using System.Collections.Generic;

class GrilleSudoku : Grille<Case>
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
        for (int i = 1; i < 10; i++)
        {
            if (!verifLigne(i) ||
            !verifColonne(i) ||
            !verifCarre(i)) return false;
        }
        return true;
    }

    private Boolean verifLigne(int nbLigne)
    {
        List<int> list = new List<int>();
        int val;
        for (int n = 0; n < 9; n++)
        {
            val = this.getVal(nbLigne - 1, n).getValeur();
            if (list.Contains(val) || val < 1 || val > 9) return false;
            else list.Add(val);
        }
        return true;
    }

    private Boolean verifColonne(int nbColonne)
    {
        List<int> list = new List<int>();
        int val;
        for (int n = 0; n < 9; n++)
        {
            val = this.getVal(n, nbColonne - 1).getValeur();
            if (list.Contains(val) || val < 1 || val > 9) return false;
            else list.Add(val);
        }
        return true;
    }

    private int numCarre(int i, int j)
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

    private Boolean verifCarre(int nCarre)
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
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            case 2:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            case 3:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            case 4:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            case 5:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            case 6:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 3; j < 6; j++)
                    {
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            case 7:
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            case 8:
                for (int i = 3; i < 6; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            case 9:
                for (int i = 6; i < 9; i++)
                {
                    for (int j = 6; j < 9; j++)
                    {
                        val = this.getVal(i, j).getValeur();
                        if (list.Contains(val)) return false;
                        else list.Add(val);
                    }
                }
                break;
            default:
                return false;
        }
        return true;
    }

    public void remplirGrille()
    {
        int[,] tab = new int[,] {
        {7,6,3, 9,1,2, 4,8,5},
        {2,8,9, 3,5,4, 1,7,6},
        {4,5,1, 7,6,8, 3,2,9},

        {5,7,8, 6,3,1, 2,9,4},
        {6,3,4, 5,2,9, 8,1,7},
        {9,1,2, 8,4,7, 6,5,3},

        {8,2,6, 4,7,5, 9,3,1},
        {1,4,7, 2,9,3, 5,6,8},
        {3,9,5, 1,8,6, 7,4,2}
        };

        for(int i=0; i<9; i++)
        {
            for(int j=0; j<9; j++)
            {
                this.getVal(i, j).setChangeable(false);
                this.getVal(i, j).setValeur(tab[i, j]);
            }
        }
    }

    public void remplirGrilleAvecTrou()
    {
        int[,] tab = new int[,] {
        {7,6,3, 9,1,2, 4,8,5},
        {2,8,9, 3,5,4, 1,7,6},
        {4,5,1, 7,6,8, 3,2,9},

        {5,7,8, 6,3,1, 2,9,4},
        {6,3,4, 5,2,9, 8,1,7},
        {9,1,2, 8,4,7, 6,5,3},

        {8,2,6, 4,7,5, 9,3,1},
        {1,4,7, 2,9,3, 5,6,8},
        {3,9,5, 1,8,6, 7,4,2}
        };

        int[,] tabTrou = new int[,] {
        {0,1,0, 0,1,0, 1,0,1},
        {1,0,1, 0,0,0, 0,0,1},
        {0,1,0, 0,0,1, 0,0,0},

        {0,1,0, 1,1,0, 0,1,0},
        {1,0,0, 1,0,1, 1,0,0},
        {0,1,1, 0,0,1, 1,0,0},

        {0,0,0, 1,1,0, 1,0,0},
        {1,0,1, 0,0,0, 0,0,1},
        {0,0,1, 0,0,0, 1,0,0}
        };

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (tabTrou[i, j] == 1)
                {
                    this.getVal(i, j).setChangeable(false);
                    this.getVal(i, j).setValeur(tab[i, j]);
                }
            }
        }
    }
    
    public void remplirMauvaixSudoku()
    {
        int[] array = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> list = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (list.Count == 0) list.AddRange(array);
                int rand = UnityEngine.Random.Range(0, list.Count);
                int val = list[rand];
                list.RemoveAt(rand);
                this.getVal(i, j).setValeur(val);
                this.getVal(i, j).setChangeable(false);
            }
        }
    }
}
