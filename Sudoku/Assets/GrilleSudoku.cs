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
                Case c = new Case(val, this);
                this.setVal(i, j, c);
            }
        }
    }
}
