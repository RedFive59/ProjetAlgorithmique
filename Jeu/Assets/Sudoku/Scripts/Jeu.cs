using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using SimpleJSON;

public class Jeu : MonoBehaviour
{
    private UIManager UIManager; // Référence au script UIManager
    private Transform parent; // Référence pour placer les cases proprement dans la hiérarchie d'Unity
    private GrilleSudoku grille = null; // Référence à la grille
    public string numGrille; // Numéro de la grille
    public string difficulte; // Difficulté de la grille
    public float temps = 0; // Temps qui va changer au fur et à mesure
    public string affichageTemps = "00:00"; //Chaine de caractères pour l'affichage du temps
    private GameObject infos; // Référence à l'object Infos pour afficher dans son élément texte les informations de la partie

    void Start()
    {
        grille = new GrilleSudoku(9, 9); // Définition de la grille de 9 par 9
        grille.initVal(0); // Toutes les cases sont à 0
        GameObject diffManager = GameObject.Find("DifficultyManager");
        if (diffManager) // Vérification que la scène SudokuMenu a fait son travail
        {
            if (diffManager.GetComponent<sceneManager>().resumeGame)
            {
                // Cas où l'on veut reprendre la partie du fichier sauvegardeSudoku
                var loadedData = JSON.Parse(File.ReadAllText(defineSudoku.cheminSauvegarde));
                numGrille = loadedData["num"].ToString();
                difficulte = loadedData["difficulte"];
                temps = float.Parse(loadedData["timer"]);
                affichageTemps = loadedData["timerString"];
                grille.chargementGrilleSauvegarde();
                Destroy(diffManager);
            } else
            {
                difficulte = diffManager.GetComponent<sceneManager>().difficulty; // Récupération de la difficulté choisit dans la scène SudokuMenu
                Destroy(diffManager);

                //Choix d'un niveau au hasard selon la difficulté précedement choisie
                int cpt = 0;
                string directoryPath = defineSudoku.getCheminDifficulte(difficulte);
                var info = new DirectoryInfo(directoryPath);
                var fileInfo = info.GetFiles();
                foreach (FileInfo f in fileInfo) if (f.Extension == ".json") cpt++;
                int level = UnityEngine.Random.Range(1, cpt + 1);
                numGrille = level.ToString();
            }
        }
        else // Afin de pouvoir lancer la scène Sudoku sans problème
        {
            string[] level = SelectionNiveauAleatoire(); // Chaine de caractères avec la difficulté et le numéro de grille
            difficulte = level[0];
            numGrille = level[1]; // Numéro de grille choisit au hasard
        }
        infos = GameObject.Find("Infos");
        infos.GetComponent<TextMeshProUGUI>().text = "Difficulty : " + difficulte + "           Level : " + numGrille + "\nTimer : " + affichageTemps; // Changement du texte des infos
        grille.chargementGrille(numGrille, difficulte); // Chargement de la grille avec la difficulté et son numéro de grille
        UIManager = GameObject.Find("Jeu").GetComponent<UIManager>();
        UIManager.Init(); // Initialisation des objets visuels
        parent = GameObject.Find("GridManager").transform;
        UIManager.GenerateGrid(0f, 0f, parent); // Génération de la grille sur la scène
        grille.sauvegardeGrille(); // Sauvegarde de la grille dès le lancement
    }

    // Méthode qui met à jour notre timer
    private void Update()
    {
        if (GameObject.Find("Infos"))
        {
            int secondes, minutes;
            temps += Time.deltaTime;
            if((int)temps%2 == 0) grille.sauvegardeGrille(); // Sauvegarde de la grille toutes les 2 secondes
            secondes = (int)temps % 60;
            minutes = (int)temps / 60;
            if (secondes < 10)
            {
                if (minutes < 10) affichageTemps = "0" + minutes + ":0" + secondes;
                else affichageTemps = minutes + ":0" + secondes;
            }
            else
            {
                if (minutes < 10) affichageTemps = "0" + minutes + ":" + secondes;
                else affichageTemps = minutes + ":" + secondes;
            }
            UIManager.tempsFin = affichageTemps;
            GameObject.Find("Infos").GetComponent<TextMeshProUGUI>().text = "Difficulty : " + difficulte + "           Level : " + numGrille + "\nTimer : " + affichageTemps;
            // Raccourci de débug
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("remplirGrille()");
                grille.remplirGrille(numGrille, difficulte);
                UIManager.UpdateGrid();
                if (grille.verifGrille()) UIManager.finishGame();
            }
        }
    }

    // Méthode qui choisit une difficulté random et un niveau random selon le nombre de niveaux présents dans le dossier
    private string[] SelectionNiveauAleatoire()
    {
        string[] res = new string[2];
        int difficulty = UnityEngine.Random.Range(1, 4);
        switch (difficulty)
        {
            case 1:
                res[0] = "Easy";
                break;
            case 2:
                res[0] = "Medium";
                break;
            case 3:
                res[0] = "Hard";
                break;
        }
        int cpt = 0;
        string directoryPath = defineSudoku.getCheminDifficulte(res[0]);
        var info = new DirectoryInfo(directoryPath);
        var fileInfo = info.GetFiles();
        foreach (FileInfo f in fileInfo) if(f.Extension == ".json") cpt++;
        int level = UnityEngine.Random.Range(1, cpt+1);
        res[1] = level.ToString();
        return res;
    }

    // Méthode interne pour récupérer la grille de cette objet
    internal GrilleSudoku getGrille()
    {
        return grille;
    }
}
