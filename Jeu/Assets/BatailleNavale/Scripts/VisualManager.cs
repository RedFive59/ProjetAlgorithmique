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
    GameObject C1;
    GameObject C2;
    GameObject C3;
    GameObject C4;
    ShipManager SM1;
    ShipManager SM2;
    MagManager MG1;
    MagManager MG2;
    CanvasGenerator Cvs;
    CanvasGenerator Cvs2;

    // Start is called before the first frame update
    void Start()
    {
        GenPlateau();
        GameObject CanvasVM = new GameObject("CanvasVM");
        Cvs = new CanvasGenerator("CanvasInstruMarker", new Vector3(0, 0, 0), new Vector2(1, 1), RenderMode.WorldSpace, C1.GetComponent<Camera>(), 10, "PanelLayer", CanvasVM);
        Cvs.addPanel("Markertab1", new Vector3(-3, 10.1f, 0), new Vector2(4, 1), new Color32(192, 72, 73, 255));
        Cvs.addText(Cvs.getPanel(0), "textMarker1", Cvs.getPanel(0).transform.position, new Vector2(12f, 4.2f), 1,0.3f, "Afficher Grille\nmarquage\n(Press TAB)", Color.black, TextAnchor.MiddleCenter);
        Cvs2 = new CanvasGenerator("CanvasInstruMarker", new Vector3(0, 0, 0), new Vector2(1, 1), RenderMode.WorldSpace, C2.GetComponent<Camera>(), 10, "PanelLayer", CanvasVM);
        Cvs2.addPanel("Markertab1", new Vector3(27, 10.1f, 0), new Vector2(4, 1), new Color32(192, 72, 73, 255));
        Cvs2.addText(Cvs.getPanel(0), "textMarker1", Cvs2.getPanel(0).transform.position, new Vector2(12f, 4.2f), 1, 0.3f, "Afficher Grille\nmarquage\n(Press TAB)", Color.black, TextAnchor.MiddleCenter);
        Cvs.getCanvas().GetComponent<Canvas>().enabled = false;
        Cvs2.getCanvas().GetComponent<Canvas>().enabled = false;
    }

    void GenPlateau()
    {
        GMN1= new GridManagerNavale("GridHolderNavale1",pos1);
        GMN2 = new GridManagerNavale("GridHolderNavale2",pos2);
        GMN3 = new GridManagerNavale("GridMarker3",pos3);
        GMN4 = new GridManagerNavale("GridMarker4",pos4);

        C1 = GMN1.getCamera();
        C2 = GMN2.getCamera();
        C3 = GMN3.getCamera();
        C4 = GMN4.getCamera();

        C1.GetComponent<Camera>().enabled = true;
        C2.GetComponent<Camera>().enabled = false;
        C3.GetComponent<Camera>().enabled = false;
        C4.GetComponent<Camera>().enabled = false;

        SM1 = new ShipManager("ShipHolder1",pos1);
        SM2 = new ShipManager("ShipHolder2",pos2);
        MG1 = new MagManager("MagHolder1",pos1,SM1,C1.GetComponent<Camera>());
        MG2 = new MagManager("MagHolder2",pos2,SM2,C2.GetComponent<Camera>());
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

    public GameObject getCameraVM(int i)
    {
        if (i == 1)
        {
            return C1;
        }
        if (i == 2)
        {
            return C2;
        }
        if (i == 3)
        {
            return C3;
        }
        if (i == 4)
        {
            return C4;
        }
        return C1;
    }


    public void switchCam()
    {
        if (GMN1.getCamera().GetComponent<Camera>().enabled == true)
        {
            GMN1.getCamera().GetComponent<Camera>().enabled = false;
            GMN3.getCamera().GetComponent<Camera>().enabled = true;
            Cvs.ChangeCam(GMN3.getCamera().GetComponent<Camera>());
            return;
        }
        if(GMN3.getCamera().GetComponent<Camera>().enabled == true)
        {
            GMN3.getCamera().GetComponent<Camera>().enabled = false;
            GMN1.getCamera().GetComponent<Camera>().enabled = true;
            Cvs.ChangeCam(GMN1.getCamera().GetComponent<Camera>());
            return;
        }
        if (GMN2.getCamera().GetComponent<Camera>().enabled == true)
        {
            GMN2.getCamera().GetComponent<Camera>().enabled = false;
            GMN4.getCamera().GetComponent<Camera>().enabled = true;
            Cvs.ChangeCam(GMN4.getCamera().GetComponent<Camera>());
            return;
        }
        if (GMN4.getCamera().GetComponent<Camera>().enabled == true)
        {
            GMN4.getCamera().GetComponent<Camera>().enabled = false;
            GMN2.getCamera().GetComponent<Camera>().enabled = true;
            Cvs.ChangeCam(GMN2.getCamera().GetComponent<Camera>());
            return;
        }
    }

    public void switchPlayer()
    {
        if ((C1.GetComponent<Camera>().enabled == true)||(C3.GetComponent<Camera>().enabled == true))
        {
            C1.GetComponent<Camera>().enabled = false;
            C2.GetComponent<Camera>().enabled = true;
            C3.GetComponent<Camera>().enabled = false;
            C4.GetComponent<Camera>().enabled = false;
            return;
        }
        else
        {
            C1.GetComponent<Camera>().enabled = true;
            C2.GetComponent<Camera>().enabled = false;
            C3.GetComponent<Camera>().enabled = false;
            C4.GetComponent<Camera>().enabled = false;
        }
    }

    public void EnableCvs()
    {
        Cvs.getCanvas().GetComponent<Canvas>().enabled = true;
        Cvs2.getCanvas().GetComponent<Canvas>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switchCam();
        }
    }
}
