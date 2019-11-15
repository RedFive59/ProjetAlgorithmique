using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class defineSudoku
{
    #if UNITY_EDITOR
    public static string cheminSauvegarde = Path.Combine(Application.dataPath, "StreamingAssets/SudokuLevels/sauvegardeSudoku.json");
    #elif UNITY_IOS
    public static string cheminSauvegarde = Path.Combine(Application.dataPath, "StreamingAssets/SudokuLevels/sauvegardeSudoku.json");
    #elif UNITY_ANDROID
    public static string cheminSauvegarde = Path.Combine("jar:file://" + Application.dataPath, "StreamingAssets/SudokuLevels/sauvegardeSudoku.json");
    #endif
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
