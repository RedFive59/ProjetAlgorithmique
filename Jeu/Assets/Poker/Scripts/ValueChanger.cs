using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChanger : MonoBehaviour
{
    private int valeur = 0;
    private string sliderName = "Slider";
    private string inputName = "InputField";
    private string placeholderName = "Placeholder";

    public void changeValueFromSlider()
    {
        GameObject.Find(placeholderName).GetComponent<Text>().text = "";
        GameObject.Find(inputName).GetComponent<InputField>().text = GameObject.Find(sliderName).GetComponent<Slider>().value.ToString();
        valeur = (int) GameObject.Find(sliderName).GetComponent<Slider>().value;
    }

    public void changeValueFromInput()
    {
        if(GameObject.Find(inputName).GetComponent<InputField>().text != "")
        {
            if (int.Parse(GameObject.Find(inputName).GetComponent<InputField>().text) > GameObject.Find(sliderName).GetComponent<Slider>().maxValue) GameObject.Find(inputName).GetComponent<InputField>().text = ((int)GameObject.Find(sliderName).GetComponent<Slider>().maxValue).ToString();
            GameObject.Find(sliderName).GetComponent<Slider>().value = int.Parse(GameObject.Find(inputName).GetComponent<InputField>().text);
            valeur = int.Parse(GameObject.Find(inputName).GetComponent<InputField>().text);
        }
    }

    public void resetValueOfInput()
    {
        if (GameObject.Find(inputName).GetComponent<InputField>().text == "")
        {
            changeValueFromSlider();
        }
    }
}
