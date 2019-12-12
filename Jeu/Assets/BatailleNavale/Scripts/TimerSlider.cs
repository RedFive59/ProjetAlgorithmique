using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    private float timeRemain;
    private const float timeMax = 3f;
    public Slider sliderx;
    public Slider sliderx2;
    // Start is called before the first frame update
    void Start()
    {
        sliderx = GameObject.Find("Slider1").GetComponent<Slider>(); //Slider 1 dans la scene
        sliderx2 = GameObject.Find("Slider2").GetComponent<Slider>(); //Slider 2 dans la sene
    }

    // Update is called once per frame
    void Update()
    {
        sliderx.value = calculTime(); //fonction qui à chaque frame calcul le temps restant du slider
        sliderx2.value = calculTime(); //fonction qui à chaque frame calcul le temps restant du slider

        if (Input.GetKeyDown(KeyCode.F1)&&(timeRemain==0))
        {
            timeRemain = timeMax; //la touche F1 est pressée le timer se lance
        }
        if (Input.GetKeyDown(KeyCode.F2) && (timeRemain == 0))
        {
            timeRemain = timeMax; //la touche F2 est pressée le timer se lance

        }

        if (timeRemain <= 0)
        {
            timeRemain = 0;
            gameObject.transform.position = new Vector3(-10, -10, 0); //deplace à chaque frame le slider vers la gauche en fonction du temps
        }
            if (timeRemain > 0)
        {
            timeRemain -= Time.deltaTime;
        }
    }

    float calculTime()
    {
        return (timeRemain / timeMax);
    }
}
