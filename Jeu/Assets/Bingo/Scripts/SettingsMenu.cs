using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private void Start()
    {
        updateScore();
        updateSettings();
    }

    //recupere le nombre de grilles du menu des options
    public void NumberOfGrid(float value)
    {
        int nouv = (int)(value * 6);
        if (nouv != 6) nouv++;
        PlayerStats.NbGrilles = nouv;
    }

    //met à jour le score avec le score contenu dans les stats du joueur
    public void updateScore()
    {
        GameObject score = GameObject.Find("Score");

        score.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.Score.ToString();
    }

    //recupere le temps d'attente du menu des options
    public void getTempsAttente(float value)
    {
        int nouv = (int)(value);
        PlayerStats.WaitTime = nouv;
        setTempsAttente();
    }

    //affiche la valeur du temps d'attente selectionné
    public void setTempsAttente()
    {
        GameObject value = GameObject.Find("VariableAttenteDisp");

        value.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.WaitTime.ToString();
    }

    //recupere le mode de jeu du menu des options
    public void ModeDeJeu(float value)
    {
        int nouv = (int)(value * 3);
        if (nouv != 3) nouv++;
        PlayerStats.GameMode = nouv - 1;
    }

    //
    public void setInputName()
    {
        string username = GameObject.Find("InputName").transform.GetComponent<TextMeshProUGUI>().text;
        if (username.Length > 4 && username.Length < 21)
        {
            PlayerStats.UserName = username;
            if(GameObject.Find("ErreurNom"))
                GameObject.Find("ErreurNom").transform.GetComponent<TextMeshProUGUI>().enabled = false;
        }
        else
        {
            GameObject.Find("ErreurNom").transform.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    //met à jour les paramètres avec les données du joueur
    public void updateSettings()
    {
        float temp;
        GameObject score = GameObject.Find("Score");

        Scrollbar gamemode = GameObject.Find("GameMode").transform.GetComponent<Scrollbar>();
        Scrollbar nbGrilles = GameObject.Find("NbGrilles").transform.GetComponent<Scrollbar>();

        Slider waitTime = GameObject.Find("WaitTime").transform.GetComponent<Slider>();

        score.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.Score.ToString();

        temp = (float)(PlayerStats.GameMode - 0) / (2 - 0);
        gamemode.value = temp;

        temp = (float)(PlayerStats.NbGrilles - 1) / (6 - 1);
        nbGrilles.value = temp;

        waitTime.value = PlayerStats.WaitTime;
    }
}
