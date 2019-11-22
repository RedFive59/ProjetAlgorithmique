using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSlider : MonoBehaviour
{
    private float timeRemain;
    private const float timeMax = 3f;
    public Slider sliderx;
    // Start is called before the first frame update
    void Start()
    {
        sliderx = GameObject.Find("Slider1").GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        sliderx.value = calculSliderValue();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            timeRemain = timeMax;

        }

        if (timeRemain <= 0)
        {
            timeRemain = 0;
            gameObject.transform.position = new Vector3(-10, -10, 0);
        }
            if (timeRemain > 0)
        {
            timeRemain -= Time.deltaTime;
        }
    }

    float calculSliderValue()
    {
        return (timeRemain / timeMax);
    }
}
