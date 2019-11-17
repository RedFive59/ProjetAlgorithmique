using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void initOrigin(float x, float y,float z)
    {
        OriginalPos = new Vector3(x, y, z);
    }
    private void Start()
    {
        VM = GameObject.FindObjectOfType<VisualManager>();

        string test = this.gameObject.transform.parent.name;
        Debug.Log("NOM : "+ test);
        if (test[10] == '1')
        {
            Debug.Log("debug : " + test[10]);
            SM = VM.getShipM(1);
            mag = VM.getMagM(1);
            pos = VM.getposGVM(1);
            C = VM.getCameraVM(1);
        }
        else
        {
            if (test[10] == '2')
            {
                Debug.Log("debug : " + test[10]);
                SM = VM.getShipM(2);
                mag = VM.getMagM(2);
                pos = VM.getposGVM(2);
                C = VM.getCameraVM(2);
            }
            else
            {
                Debug.Log("NO_LINK_FOUND");
            }
        }

        initOrigin(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
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
            rotv = true;
        }
        else
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, 90f));
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


        void OnMouseDown()
    {
        magv = false;
        mOffset = this.gameObject.transform.position - GetMouseWorldPos();//enregistre l'offset entre la souris et l'objet
        if (mag.getMagasinpos() == 1)
        {
            mag.setFermer();
        }
    }

        private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition; //coord pixels
        return C.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        this.gameObject.transform.position = GetMouseWorldPos() + mOffset;
        if (Input.GetKeyDown(KeyCode.R))//Press tab pour rotate un bateau
        {
            changeRot();
        }
    }

    private int getTaille()
    {
        if (this.gameObject.name == "Torpilleur")
        {
            return 2;
        }
        if ((this.gameObject.name == "ContreTorpilleur")||(this.name == "SousMarin"))
        {
            return 3;
        }
        if (this.gameObject.name == "Croiseur")
        {
            return 4;
        }
        if (this.gameObject.name == "PorteAvion")
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
        if ((this.gameObject.transform.position.x < pos.x+0) || (this.gameObject.transform.position.x >pos.y+9) || (this.gameObject.transform.position.y <pos.y+0) || (this.gameObject.transform.position.y > pos.y+9))
        {
            resetPos();
            return;
            }

            if ((this.gameObject.transform.position.x <=pos.x+0 + (int)(getTaille() / 2)-1)&& (rotv == false))
            {
            resetPos();
            return;
        }
        if ((this.gameObject.transform.position.x >=pos.x+9 - (int)(getTaille() / 2) + 1) && (rotv == false))
        {
            resetPos();
            return;
        }
        if ((this.gameObject.transform.position.y <=pos.y+ 0 + (int)(getTaille() / 2) - 1) && (rotv == true))
        {
            resetPos();
            return;
        }
        if ((this.gameObject.transform.position.y>=pos.y+9 - (int)(getTaille() / 2) + 1) && (rotv == true))
        {
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

    private void OnMouseUp()
    {
        this.gameObject.transform.position = cutVector(this.gameObject.transform.position);
        this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer2";
        checkPos();
        mag.setOuvrir();
    }

    // Update is called once per frame
    void Update()
    {
    }
}