using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    private int nb;

    public void NumberOfGrid(float value)
    {
        int nouv = (int)(value*4);
        if (nouv != 4) nouv++;
        this.nb = nouv;
        setNbGrilles();
    }

    public void setNbGrilles()
    {
        GameObject value = GameObject.Find("ValueScroll");
        value.transform.GetComponent<TextMeshProUGUI>().text = this.nb.ToString();
    }
}
