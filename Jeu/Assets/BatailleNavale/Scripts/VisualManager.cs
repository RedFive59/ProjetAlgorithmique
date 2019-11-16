using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    private Vector3 pos1 = new Vector3(0, 0, 0);
    private Vector3 pos2 = new Vector3(30, 0, 0);
    GridManagerNavale GMN1;
    GridManagerNavale GMN2;
    ShipManager SM1;
    ShipManager SM2;
    MagManager MG1;
    MagManager MG2;

    // Start is called before the first frame update
    void Start()
    {
        GenPlateau();
    }

    void GenPlateau()
    {
        GMN1= new GridManagerNavale("GridHolderNavale1",new Vector3(0,0,0));
        GMN2 = new GridManagerNavale("GridHolderNavale2", new Vector3(30, 0, 0));
        SM1 = new ShipManager("ShipHolder1",new Vector3(0, 0, 0));
        SM2 = new ShipManager("ShipHolder2",new Vector3(30, 0, 0));
        MG1 = new MagManager("MagHolder1", new Vector3(0, 0, 0),SM1);
        MG2 = new MagManager("MagHolder2", new Vector3(30, 0, 0),SM2);

        GMN1.getCamera().enabled =true;
        GMN2.getCamera().enabled = false;
    }

    public MagManager GetMagM(int i)
    {
        if (i == 1)
        {
            return MG1;
        }
        if (i == 2) { 
            return MG2;
        }
        return MG1;
    }

    public Vector3 getposGVM(int i)
    {
        if (i == 1)
        {
            return pos1;
        }
        if (i == 2)
        {
            return pos2;
        }
        return pos1;
    }

    public void switchCam()
    {
        if(GMN1.getCamera().enabled == true)
        {
            GMN1.getCamera().enabled = false;
            GMN2.getCamera().enabled = true;
        }
        else
        {
            GMN1.getCamera().enabled = true;
            GMN2.getCamera().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            switchCam();
        }
    }
}
