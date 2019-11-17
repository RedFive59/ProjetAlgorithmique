using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    private static int nbGrilles = 1, waitTime = 5, score = 0, gameMode = 0;
    private static string userName = "Anonyme";

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

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
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

    public static string UserName
    {
        get
        {
            return userName;
        }
        set
        {
            userName = value;
        }
    }

    public static void reset()
    {
        nbGrilles = 1;
        waitTime = 5;
        score = 0;
        gameMode = 0;
        userName = "Anonyme";
    }

    public static void initial()
    {
        nbGrilles = 1;
        waitTime = 5;
        gameMode = 0;
    }
}

