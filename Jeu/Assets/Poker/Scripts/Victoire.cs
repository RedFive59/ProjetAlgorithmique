using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Victoire : MonoBehaviour
{
    public GameObject text;//Nom du gagnant de la partie de poker
    // Start is called before the first frame update
    void Start()
    {
        GameObject poker = GameObject.Find("Poker");
        text.GetComponent<TextMeshProUGUI>().text = "Victoire de "+ poker.GetComponent<Poker>().nomDuGagnant;
        Destroy(poker);
    }
}
