using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenVisualManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GenPlateau(1);
    }

    void GenPlateau(int x)
    {
        GameObject GridHolder = new GameObject("GridHolder");
        GridHolder.AddComponent<GridManagerNavale>();
        GridHolder.transform.SetParent(this.transform, false);
        GameObject ShipHolder = new GameObject("ShipHolder");
        ShipHolder.AddComponent<GenShips>();
        ShipHolder.transform.SetParent(this.transform, false);
        GameObject UIHolder = new GameObject("UIHolder");
        UIHolder.AddComponent<Magasin>();
        UIHolder.transform.SetParent(this.transform, false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
