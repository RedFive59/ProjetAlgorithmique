using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ValueChanger : MonoBehaviour
{
    private int valeur = 0;
    private string sliderName = "Slider";//Nom de l'objet slider dans la scène Poker
    private string inputName = "InputField";//Nom de l'objet inputfield dans la scène Poker
    private string placeholderName = "Placeholder";

    public void changeValueFromSlider()//Permet de changer la valeur du champs d'insertion grâce au slider de relance
    {
        GameObject.Find(placeholderName).GetComponent<TextMeshProUGUI>().text = "";
        GameObject.Find(inputName).GetComponent<TMP_InputField>().text = GameObject.Find(sliderName).GetComponent<Slider>().value.ToString();
        valeur = (int) GameObject.Find(sliderName).GetComponent<Slider>().value;
    }

    public void changeValueFromInput()//Permet de changer la valeur du slider de relance grâce au champs d'insertion
    {
        if(GameObject.Find(inputName).GetComponent<TMP_InputField>().text != "")
        {
            if (int.Parse(GameObject.Find(inputName).GetComponent<TMP_InputField>().text) > GameObject.Find(sliderName).GetComponent<Slider>().maxValue) GameObject.Find(inputName).GetComponent<TMP_InputField>().text = ((int)GameObject.Find(sliderName).GetComponent<Slider>().maxValue).ToString();
            GameObject.Find(sliderName).GetComponent<Slider>().value = int.Parse(GameObject.Find(inputName).GetComponent<TMP_InputField>().text);
            valeur = int.Parse(GameObject.Find(inputName).GetComponent<TMP_InputField>().text);
        }
    }

    public void resetValueOfInput()//Réinitialise la valeur du champs d'insertion
    {
        if (GameObject.Find(inputName).GetComponent<InputField>().text == "")
        {
            changeValueFromSlider();
        }
    }
}
