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

    void initOrigin(float x, float y,float z)
    {
        OriginalPos = new Vector3(x, y, z);
    }
    private void Start()
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        SM = this.GetComponentInParent<ShipManager>();

        string test = this.transform.parent.name;
        if (test[4] == '0')
        {
            mag = VM.GetMagM(1);
            pos = VM.getposGVM(1);
        }
        if (test[4] == '3')
        {
            mag = VM.GetMagM(2);
            pos = VM.getposGVM(2);
        }

        initOrigin(this.transform.position.x, this.transform.position.y, this.transform.position.z);
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
            this.transform.Rotate(new Vector3(0, 0, 90f));
            rotv = true;
        }
        else
        {
            this.transform.Rotate(new Vector3(0, 0, 90f));
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
                V = this.transform.position;
                this.transform.position = new Vector3(V.x + x, V.y + y, V.z + z);
            }
    }

    private Vector3 mOffset;
    private MagManager mag;
    private Camera C;

    // Start is called before the first frame update
   
        void OnMouseDown()
    {
        magv = false;
        C = GameObject.FindObjectOfType<Camera>();
        mOffset = this.transform.position - GetMouseWorldPos();//enregistre l'offset entre la souris et l'objet
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
        this.transform.position = GetMouseWorldPos() + mOffset;
        if (Input.GetKeyDown(KeyCode.R))//Press tab pour rotate un bateau
        {
            changeRot();
        }
    }

    private int getTaille()
    {
        if (this.name == "Torpilleur")
        {
            return 2;
        }
        if ((this.name == "ContreTorpilleur")||(this.name == "SousMarin"))
        {
            return 3;
        }
        if (this.name == "Croiseur")
        {
            return 4;
        }
        if (this.name == "PorteAvion")
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
        this.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer";
        this.transform.position = OriginalPos;
        moveShip(4.5f, 0, 0);
    }
    private void checkPos()
    {
        if ((this.transform.position.x < pos.x+0) || (this.transform.position.x >pos.y+9) || (this.transform.position.y <pos.y+0) || (this.transform.position.y > pos.y+9))
        {
            resetPos();
            return;
            }

            if ((this.transform.position.x <=pos.x+0 + (int)(getTaille() / 2)-1)&& (rotv == false))
            {
            resetPos();
            return;
        }
        if ((this.transform.position.x >=pos.x+9 - (int)(getTaille() / 2) + 1) && (rotv == false))
        {
            resetPos();
            return;
        }
        if ((this.transform.position.y <=pos.y+ 0 + (int)(getTaille() / 2) - 1) && (rotv == true))
        {
            resetPos();
            return;
        }
        if ((this.transform.position.y>=pos.y+9 - (int)(getTaille() / 2) + 1) && (rotv == true))
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
        Debug.Log(decix);
        Debug.Log(deciy);
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
        this.transform.position = cutVector(this.transform.position);
        this.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer2";
        checkPos();
        mag.setOuvrir();
    }

    // Update is called once per frame
    void Update()
    {
    }
}