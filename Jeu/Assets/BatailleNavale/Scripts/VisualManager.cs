using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    private int pos = 0;
    // Start is called before the first frame update
    void Start()
    {
        GenPlateau();
    }

    void GenPlateau()
    {
        GridManagerNavale GNV1= new GridManagerNavale("GridHolderNavale1",new Vector3(0,0,0));
        GridManagerNavale GNV2 = new GridManagerNavale("GridHolderNavale2", new Vector3(30, 0, 0));
        ShipManager SM1 = new ShipManager("ShipHolder1",new Vector3(0, 0, 0));
        ShipManager SM2 = new ShipManager("ShipHolder2",new Vector3(30, 0, 0));
        MagManager MG1 = new MagManager("MagHolder1", new Vector3(0, 0, 0),SM1);
        MagManager MG2 = new MagManager("MagHolder2", new Vector3(30, 0, 0),SM2);
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
