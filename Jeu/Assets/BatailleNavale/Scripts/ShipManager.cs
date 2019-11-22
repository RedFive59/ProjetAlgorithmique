using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipManager
{
    private GameObject SM;
    private List<Ship> LShip;
    private VisualManager VM;

    public ShipManager(string nom, Vector3 pos)
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        SM = new GameObject(nom);
        LShip = new List<Ship>();
        Ship Torpilleur0 = new Ship(SM, "Torpilleur0", 2, new Vector3(2, 1, 1), "txtTp", "ShipLayer");
        LShip.Add(Torpilleur0);
        Ship ContreToprilleur1 = new Ship(SM, "ContreTorpilleur1", 3, new Vector3(3, 1, 1), "txtCtp", "ShipLayer");
        LShip.Add(ContreToprilleur1);
        Ship SousMarin2 = new Ship(SM, "SousMarin2", 3, new Vector3(3, 1, 1), "txtSm", "ShipLayer");
        LShip.Add(SousMarin2);
        Ship Croiseur3 = new Ship(SM, "Croiseur3", 4, new Vector3(4, 1, 1), "txtCs", "ShipLayer");
        LShip.Add(Croiseur3);
        Ship PorteAvion4 = new Ship(SM, "PorteAvion4", 5, new Vector3(5, 1, 1), "txtPa", "ShipLayer");
        LShip.Add(PorteAvion4);

        for (int i = 0; i < 5; i++)
        {
            SM.transform.GetChild(i).transform.position = new Vector3(pos.x + 12.5f, pos.y + 1 + i * 2, pos.z + 0);
            getClassShip(i).updateG();
        }
    }

    public void moveShip(float x, float y, float z)
    {
        Vector3 V;
        for (int i = 0; i < 5; i++)
        {
            if (SM.transform.GetChild(i).GetComponent<Draggable>().getMag())
            {
                V = SM.transform.GetChild(i).position;
                SM.transform.GetChild(i).position = new Vector3(V.x + x, V.y + y, V.z + z);
            }
        }
    }

    public Ship getClassShip(int i)
    {
        return LShip[i];
    }

    public GameObject getShip(int i)
    {
        return SM.transform.GetChild(i).gameObject;
    }


    public bool checkContact(int ix)
    {
        for (int i = 0; i < 5; i++)
        {
            if (LShip[i].Equals(LShip[ix]) != true)//!
            {
                for (int j = 0; j < LShip[ix].getLength(); j++)
                {
                    for (int k = 0; k < LShip[i].getLength(); k++)
                    {
                        if (Vector3.Distance(LShip[ix].getVecteur().getVal(j), LShip[i].getVecteur().getVal(k)) == 0)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool checkTir(Vector3 V)
    {
        V = new Vector3(V.x, V.y - 30, V.z);
        for (int i = 0; i < 5; i++)
        {
            for (int k = 0; k < LShip[i].getLength(); k++)
            {
                Debug.Log(V + "  +  " + LShip[i].getVecteur().getVal(k));
                if (Vector3.Distance(V, LShip[i].getVecteur().getVal(k)) == 0)
                {
                    LShip[i].hit();
                    return true;
                }
            }
        }
        for (int i = 1; i < 5; i++)
        {
            if (VM.getCameraVM(i).GetComponent<Camera>().enabled == true)
            {
                GameObject.Find("MissPanel").transform.position = new Vector3(VM.getCameraVM(i).transform.position.x, VM.getCameraVM(i).transform.position.y, 0);
                return false;
            }
        }
        return false;
    }
}

