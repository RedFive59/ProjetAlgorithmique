using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenVisualManager : MonoBehaviour
{
    private int pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        GenPlateau();
    }

    void GenPlateau()
    {
        GameObject GridHolder = new GameObject("GridHolder");
        GridHolder.AddComponent<GridManagerNavale>();
        GridHolder.transform.SetParent(this.transform, false);
        GameObject ShipHolder = new GameObject("ShipHolder0");
        ShipHolder.AddComponent<GenShips>();
        ShipHolder.transform.SetParent(this.transform, false);
        GameObject UIHolder = new GameObject("UIHolder");
        UIHolder.AddComponent<Magasin>();
        UIHolder.transform.SetParent(this.transform, false);
    }

    public int getposGVM()
    {
        return pos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
