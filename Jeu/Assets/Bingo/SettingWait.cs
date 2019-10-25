using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingWait : MonoBehaviour
{
    private int nb;

    public void getTempsAttente(float value)
    {
        int nouv = (int)(value);
        this.nb = nouv;
        setTempsAttente();
    }

    public void setTempsAttente()
    {
        GameObject value = GameObject.Find("VariableAttente");
        GameObject value2 = GameObject.Find("VariableAttenteDisp");
        value.transform.GetComponent<TextMeshProUGUI>().text = this.nb.ToString();
        value2.transform.GetComponent<TextMeshProUGUI>().text = this.nb.ToString();
    }
}
