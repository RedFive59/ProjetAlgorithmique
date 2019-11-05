using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private int nbJetons;

    public void NumberOfGrid(float value)
    {
        int nouv = (int)(value*6);
        if (nouv != 6) nouv++;
        PlayerStats.NbGrilles = nouv;
        updateJetons();
    }

    public void updateJetons()
    {
        GameObject credits = GameObject.Find("Jetons");
        GameObject credits2 = GameObject.Find("JetonsMenu");
        this.nbJetons = PlayerStats.Jetons;

        credits.transform.GetComponent<TextMeshProUGUI>().text = (this.nbJetons - PlayerStats.NbGrilles*5).ToString();
        credits2.transform.GetComponent<TextMeshProUGUI>().text = (this.nbJetons - PlayerStats.NbGrilles * 5).ToString();
    }

    public void getTempsAttente(float value)
    {
        int nouv = (int)(value);
        PlayerStats.WaitTime= nouv;
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
        setModeDeJeu();
    }

    public void setModeDeJeu()
    {
        GameObject value = GameObject.Find("ModeDeJeu");

        value.transform.GetComponent<TextMeshProUGUI>().text = PlayerStats.GameMode.ToString();
    }

    public void setJetons(GameObject go)
    {
        TMP_InputField input = go.transform.GetComponent<TMP_InputField>();
        PlayerStats.Jetons = int.Parse(input.text);
        if(PlayerStats.Jetons > 0)
            updateJetons();
    }
}
