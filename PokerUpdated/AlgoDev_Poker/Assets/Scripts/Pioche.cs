using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pioche : MonoBehaviour
{
    List<int> cartes;

    public IEnumerable<int> GetCartes()
    {
        foreach(int i in cartes)
        {
            yield return i;
        }
    }

    public void melanger()
    {
        if (cartes == null)
        {
            cartes = new List<int>();
        }
        else
        {
            cartes.Clear();
        }
        
        for(int i = 0; i < 52; i++)
        {
            cartes.Add(i);
        }

        int n = cartes.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int temp = cartes[k];
            cartes[k] = cartes[n];
            cartes[n] = temp;

        }
    }
    void Awake() // Awake() ? anciennement Start
    {
        melanger();
    }
}
