using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private Color startColor;
    private SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;

    }
    void OnMouseEnter()
    {
        rend.color = Color.red;
    }
    void OnMouseExit()
    {
        rend.color = startColor;
    }
}
