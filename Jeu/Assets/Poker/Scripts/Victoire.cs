using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victoire : MonoBehaviour
{
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        GameObject poker = GameObject.Find("Poker");
        text.GetComponent<TextMeshProUGUI>().text = "Victoire de "+ poker.GetComponent<Poker>().nomDuGagnant;
        Destroy(poker);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
