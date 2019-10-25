using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System;

public class colorofsprite : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        select();
    }

    public void select()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject tile = GameObject.Find(hit.transform.gameObject.name);
                getind(tile);
            }
        }
    }

    public void getind(GameObject sprite)
    {
        string name = sprite.name;
        string result = Regex.Replace(name, "[^0-9]", "");
        Debug.Log(result[0]);
    }
}