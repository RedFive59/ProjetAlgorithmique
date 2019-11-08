using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool magv = true;
    private bool rotv = false;
    private GenShips GS;
    private Vector3 OriginalPos;

    void initOrigin(float x, float y)
    {
        OriginalPos =new Vector3(x, y, 0);
        Debug.Log("Origine :"+OriginalPos);
    }
    private void Start()
    {
        initOrigin(this.transform.localPosition.x, this.transform.localPosition.y);
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
                V = this.transform.localPosition;
                this.transform.localPosition = new Vector3(V.x + x, V.y + y, V.z + z);
            }
    }

    private Vector3 mOffset;
    private Magasin mag;
    private Camera C;

    // Start is called before the first frame update
   
        void OnMouseDown()
    {
        magv = false;
        C = GameObject.FindObjectOfType<Camera>();
        mag = GameObject.FindObjectOfType<Magasin>();
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
        if (Input.GetKeyDown(KeyCode.Tab))//Press tab pour rotate un bateau
        {
            changeRot();
        }
    }

    private int getTaille()
    {
        if (this.name == "Torpilleur")
        {
            Debug.Log("Taille2");
            return 2;
        }
        if ((this.name == "ContreTorpilleur")||(this.name == "SousMarin"))
        {
            Debug.Log("Taille3");
            return 3;
        }
        if (this.name == "Croiseur")
        {
            Debug.Log("Taille4");
            return 4;
        }
        if (this.name == "PorteAvion")
        {
            Debug.Log("Taille5");
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
        this.transform.localPosition = OriginalPos;
        moveShip(4.5f, 0, 0);
    }
    private void checkPos()
    {
        GS = GameObject.FindObjectOfType<GenShips>();
        if ((this.transform.localPosition.x < 0) || (this.transform.localPosition.x > 9) || (this.transform.localPosition.y < 6) || (this.transform.localPosition.y > 15))
        {
            resetPos();
            return;
            }

            if ((this.transform.localPosition.x <= 0 + (int)(getTaille() / 2)-1)&& (rotv == false))
            {
            resetPos();
            return;
        }
        if ((this.transform.localPosition.x >= 9 - (int)(getTaille() / 2) + 1) && (rotv == false))
        {
            resetPos();
            return;
        }
        if ((this.transform.localPosition.y <= 6 + (int)(getTaille() / 2) - 1) && (rotv == true))
        {
            resetPos();
            return;
        }
        if ((this.transform.localPosition.y >= 15 - (int)(getTaille() / 2) + 1) && (rotv == true))
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
        this.transform.localPosition = cutVector(this.transform.localPosition);
        this.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer2";
        checkPos();
        mag.setOuvrir();
    }

    // Update is called once per frame
    void Update()
    {
    }
}