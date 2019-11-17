using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class defineSudoku
{
    public static string cheminSauvegarde = Path.Combine(Application.dataPath, "StreamingAssets/SudokuLevels/sauvegardeSudoku.json");
    public static string cheminLeaderboard = Path.Combine(Application.dataPath, "StreamingAssets/Leaderboard/leaderboardSudoku.json");

    public static string getCheminDifficulte(string difficulte)
    {
        return Path.Combine(Application.dataPath, ("StreamingAssets/SudokuLevels/" + difficulte + "/"));
    }

    public static string getCheminDifficulteNum(string difficulte, string num)
    {
        return Path.Combine(Application.dataPath, ("StreamingAssets/SudokuLevels/" + difficulte + "/" + num + ".json"));
    }
}
