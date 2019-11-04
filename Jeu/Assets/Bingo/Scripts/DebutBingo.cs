using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebutBingo : MonoBehaviour
{
    public void DemarerBingo(GameObject goBingo)
    {
        if(PlayerStats.Jetons > 0)
        {
            GameObject goMenu = GameObject.Find("MenuBingo");
            goMenu.SetActive(false);
            goBingo.SetActive(true);
        }
    }
}
