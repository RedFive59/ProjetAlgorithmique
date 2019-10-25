using System.Collections;
using System.Collections.Generic;

public class Grille<T>
{
    //initialisation d'un vecteur ainsi qu'une matrice
    private T[] vecteur;
    private T[,] matrice;
    //permet de retrouver facilement les dimentions d'une grille
    protected int rows, cols;
    //bool qui permet de savoir si la grille est une matrice ou un vecteur
    private bool isMat = false;

    public Grille(int n)
    {
        this.vecteur = new T[n];
        this.rows = n;
    }

    public Grille(int n, int m)
    {
        this.matrice = new T[n, m];
        this.rows = n;
        this.cols = m;
        this.isMat = true;
    }

    //ajoute une valeur val de type T a l'indice n dans le vecteur
    public void setVal(int n, T val)
    {
        this.vecteur[n] = val;
    }

    //ajoute une valeur val de type T aux indices n et m dans la matrice
    public void setVal(int n, int m, T val)
    {
        this.matrice[n, m] = val;
    }
    
    //get retourne la colonne m sous forme d'un vecteur
    public T[] getCol(int m)
    {
        T[] col = new T[this.rows];
        for (int i = 0; i < this.rows; i++)
        {
            col[i] = this.matrice[i, m];
        }
        return col;
    }

    //get retourne la ligne n sous forme d'un vecteur
    public T[] getRow(int n)
    {
        T[] row = new T[this.cols];
        for (int i = 0; i < this.cols; i++)
        {
            row[i] = this.matrice[n, i];
        }
        return row;
    }
    
    public void initVal(T val)
    {
        for (int i = 0; i < this.rows; i++)
        {
            if (this.isMat)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this.setVal(i, j, val);
                }
            }
            else
            {
                this.setVal(i, val);
            }
        }
    }

    public T getVal(int n)
    {
        return this.vecteur[n];
    }

    public T getVal(int n, int m)
    {
        return this.matrice[n, m];
    }

    public int getRows()
    {
        return this.rows;
    }

    public int getCols()
    {
        return this.cols;
    }
}