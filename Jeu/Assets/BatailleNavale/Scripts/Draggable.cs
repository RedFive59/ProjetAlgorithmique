using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    private bool magv = true;
    private bool rotv = false;
    private ShipManager SM;
    private Vector3 OriginalPos;
    private VisualManager VM;
    private Vector3 pos;
    private Vector3 mOffset;
    private MagManager mag;
    private Camera C;
    string test;

    private void Start()
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        test=this.gameObject.name;
        string test2 = this.gameObject.transform.parent.name;
       // Debug.Log("NOM : "+ test2);
        if (test2[10] == '1')
        {
           // Debug.Log("debug : " + test2[10]);
            SM = VM.getShipM(1);
            mag = VM.getMagM(1);
            pos = VM.getposGVM(1);
            C = VM.getCameraVM(1).GetComponent<Camera>();
        }
        else
        {
            if (test2[10] == '2')
            {
               // Debug.Log("debug : " + test2[10]);
                SM = VM.getShipM(2);
                mag = VM.getMagM(2);
                pos = VM.getposGVM(2);
                C = VM.getCameraVM(2).GetComponent<Camera>();
            }
            else
            {
                Debug.Log("NO_LINK_FOUND");
            }
        }

        initOrigin(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }


        void OnMouseDown()
    {
        Debug.Log(this.gameObject.name);
        Debug.Log(this.gameObject.transform.position);
        //SM.getClassShip(test[test.Length - 1] - 48).updateG();
        magv = false;
        mOffset = this.gameObject.transform.position - GetMouseWorldPos();//enregistre l'offset entre la souris et l'objet
        if (mag.getMagasinpos() == 1)
        {
            mag.setFermer();
        }
    }

    private void OnMouseDrag()
    {
        this.gameObject.transform.position = GetMouseWorldPos() + mOffset;
        if (Input.GetKeyDown(KeyCode.R))//Press tab pour rotate un bateau
        {
            changeRot();
        }
    }

    private void OnMouseUp()
    {
        Debug.Log(this.gameObject.transform.position);
        this.gameObject.transform.position = cutVector(this.gameObject.transform.position);
        this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer2";
        SM.getClassShip(test[test.Length-1]-48).updateG();
        checkPos();
        mag.setOuvrir();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition; //coord pixels
        return C.ScreenToWorldPoint(mousePoint);
    }

    void initOrigin(float x, float y, float z)
    {
        OriginalPos = new Vector3(x, y, z);
    }

    public void changeMag()
    {
        if (magv == false)
        {
            magv = true;
        }
        else { magv = false; }
    }

    public void changeRot()
    {
        if (rotv == false)
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, 90f));
            SM.getClassShip(test[test.Length - 1] - 48).changeRotShip();
            rotv = true;
        }
        else
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, 90f));
            SM.getClassShip(test[test.Length - 1] - 48).changeRotShip();
            rotv = false;
        }
    }

    public bool getMag()
    {
        return magv;
    }

    public bool getRot()
    {
        return rotv;
    }

    public void moveShip(float x, float y, float z)
    {
        Vector3 V;
        if (magv)
        {
            V = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(V.x + x, V.y + y, V.z + z);
        }
    }


    private int getTaille()
    {
        if (this.gameObject.name == "Torpilleur0")
        {
            return 2;
        }
        if ((this.gameObject.name == "ContreTorpilleur1")||(this.gameObject.name == "SousMarin2"))
        {
            return 3;
        }
        if (this.gameObject.name == "Croiseur3")
        {
            return 4;
        }
        if (this.gameObject.name == "PorteAvion4")
        {
            return 5;
        }
        return -1;
    }

    private void resetPos()
    {
        magv = true;
        if (rotv)
        {
            changeRot();
        }
        this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer";
        this.gameObject.transform.position = OriginalPos;
        moveShip(4.5f, 0, 0);
    }

    private void checkPos()
    {
        Debug.Log("POSDRAG: " + pos);
        Debug.Log("POSSHIP: " + this.gameObject.transform.position);
        Debug.Log(test[test.Length - 1]);
        if ((this.gameObject.transform.position.x < pos.x + 0) || (this.gameObject.transform.position.x > pos.x + 9) || (this.gameObject.transform.position.y < pos.y + 0) || (this.gameObject.transform.position.y > pos.y + 9))
        {
            Debug.Log("HorsMap");
            resetPos();
            return;
            }
            if ((this.gameObject.transform.position.x <=pos.x+0 + (int)(getTaille() / 2)-1)&& (rotv == false))
            {
            Debug.Log("HorsposXH");
            resetPos();
            return;
        }
        if ((this.gameObject.transform.position.x >=pos.x+9 - (int)(getTaille() / 2) + 1) && (rotv == false))
        {
            Debug.Log("HorsposXH");
            resetPos();
            return;
        }
        if ((this.gameObject.transform.position.y <= pos.y+ 0 + (int)(getTaille() / 2) - 1) && (rotv == true))
        {
            Debug.Log("HorsposYV");
            resetPos();
            return;
        }
        if ((this.gameObject.transform.position.y>=pos.y+9 - (int)(getTaille() / 2) + 1) && (rotv == true))
        {
            Debug.Log("HorsposYV");
            resetPos();
            return;
        }
        if ((SM.checkContact(test[test.Length - 1] - 48)))
        {
            Debug.Log("Chevauchage");
            resetPos();
            return;
        }
    }

    private Vector3 cutVector(Vector3 V)
    {
        int x;
        int y;
        double decix = V.x - System.Math.Truncate(V.x);
        double deciy = V.y - System.Math.Truncate(V.y);
        x = (int)V.x;
        y = (int)V.y;

        if ((getTaille() == 2) || (getTaille() == 4))
        {
            if ((rotv == true))
            {
                if ((decix >= 0.5) && (deciy >= 0.5))
                {
                    return new Vector3(x+ 1, y + 0.5f, 0);
                }
                if (decix >= 0.5)
                {
                    return new Vector3(x + 1, y - 0.5f, 0);
                }
                if (deciy >= 0.5)
                {
                    return new Vector3(x, y + 0.5f, 0);
                }
                return new Vector3(x, y-0.5f, 0);
            }
            else
            {
                if ((decix >= 0.5) && (deciy >= 0.5))
                {
                    return new Vector3(x + 0.5f, y + 1, 0);
                }
                if (decix >= 0.5)
                {
                    return new Vector3(x + 0.5f, y, 0);
                }
                if (deciy >= 0.5)
                {
                    return new Vector3(x-0.5f, y + 1, 0);
                }
                return new Vector3(x-0.5f, y, 0);
            }
        }
        if ((decix >= 0.5) && (deciy >= 0.5))
        {
            return new Vector3(x + 1, y + 1, 0);
        }
        if (decix >= 0.5)
        {
            return new Vector3(x + 1, y, 0);
        }
        if (deciy >= 0.5)
        {
            return new Vector3(x, y + 1, 0);
        }
        return new Vector3(x, y, 0);
    }


    // Update is called once per frame
    void Update()
    {
    }
}