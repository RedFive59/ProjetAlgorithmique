using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    private Vector3 pos1 = new Vector3(0, 0, 0);
    private Vector3 pos2 = new Vector3(30, 0, 0);
    private Vector3 pos3 = new Vector3(0, 30, 0);
    private Vector3 pos4 = new Vector3(30, 30, 0);
    GridManagerNavale GMN1;
    GridManagerNavale GMN2;
    GridManagerNavale GMN3;
    GridManagerNavale GMN4;
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
        GMN1= new GridManagerNavale("GridHolderNavale1",pos1);
        GMN2 = new GridManagerNavale("GridHolderNavale2",pos2);
        GMN3 = new GridManagerNavale("GridMarker3",pos3);
        GMN4 = new GridManagerNavale("GridMarker4",pos4);

        SM1 = new ShipManager("ShipHolder1",pos1);
        SM2 = new ShipManager("ShipHolder2",pos2);
        MG1 = new MagManager("MagHolder1",pos1,SM1,GMN1.getCamera());
        MG2 = new MagManager("MagHolder2",pos2,SM2,GMN2.getCamera());

        GMN1.getCamera().enabled =true;
        GMN2.getCamera().enabled = false;
        GMN3.getCamera().enabled = false;
        GMN4.getCamera().enabled = false;
    }

    public MagManager getMagM(int i)
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

    public ShipManager getShipM(int i)
    {
        if (i == 1)
        {
            return SM1;
        }
        if (i == 2)
        {
            return SM2;
        }
        return SM1;
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

    public Camera getCameraVM(int i)
    {
        if (i == 1)
        {
            return GMN1.getCamera();
        }
        if (i == 2)
        {
            return GMN2.getCamera();
        }
        return GMN1.getCamera();
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

    public

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            switchCam();
        }
    }
}
