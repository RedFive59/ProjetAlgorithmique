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

    public void NumberOfGrid(float value)
    {
        int nouv = (int)(value * 6);
        if (nouv != 6) nouv++;
        PlayerStats.NbGrilles = nouv;
    }

    public void updateScore()
    {
        GameObject scoreDisp = GameObject.Find("ScoreDisp");
        GameObject score = GameObject.Find("Score");

        scoreDisp.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.Score.ToString();
        score.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.Score.ToString();
    }

    public void getTempsAttente(float value)
    {
        int nouv = (int)(value);
        PlayerStats.WaitTime = nouv;
        setTempsAttente();
    }

    public void setTempsAttente()
    {
        GameObject value = GameObject.Find("VariableAttenteDisp");

        value.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.WaitTime.ToString();
    }

    public void ModeDeJeu(float value)
    {
        int nouv = (int)(value * 3);
        if (nouv != 3) nouv++;
        PlayerStats.GameMode = nouv - 1;
    }

    public void setInputName()
    {
        string username = GameObject.Find("InputName").transform.GetComponent<TextMeshProUGUI>().text;
        if (username.Length > 4)
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

    public void updateSettings()
    {
        float temp;
        GameObject score = GameObject.Find("Score");
        GameObject scoreDisp = GameObject.Find("ScoreDisp");

        Scrollbar gamemode = GameObject.Find("GameMode").transform.GetComponent<Scrollbar>();
        Scrollbar nbGrilles = GameObject.Find("NbGrilles").transform.GetComponent<Scrollbar>();

        Slider waitTime = GameObject.Find("WaitTime").transform.GetComponent<Slider>();

        score.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.Score.ToString();
        scoreDisp.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.Score.ToString();

        temp = (float)(PlayerStats.GameMode - 0) / (2 - 0);
        gamemode.value = temp;

        temp = (float)(PlayerStats.NbGrilles - 1) / (6 - 1);
        nbGrilles.value = temp;

        waitTime.value = PlayerStats.WaitTime;
    }
}
