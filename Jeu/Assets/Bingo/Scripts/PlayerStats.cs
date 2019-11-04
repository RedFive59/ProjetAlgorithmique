using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    private static int nbGrilles = 1, waitTime = 5, jetons = 0, gameMode = 0;

    public static int NbGrilles
    {
        get
        {
            return nbGrilles;
        }
        set
        {
            nbGrilles = value;
        }
    }

    public static int WaitTime
    {
        get
        {
            return waitTime;
        }
        set
        {
            waitTime = value;
        }
    }

    public static int Jetons
    {
        get
        {
            return jetons;
        }
        set
        {
            jetons = value;
        }
    }

    public static int GameMode
    {
        get
        {
            return gameMode;
        }
        set
        {
            gameMode = value;
        }
    }
}

