using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Draggable : MonoBehaviour
{
    private bool magv = true;//magasin ouvert ou fermé
    private bool rotv = false;//Bateau attaché vert/hor
    private ShipManager SM;//ShipManager du bateau attaché (flotte)
    private Vector3 OriginalPos;//Position d'origine du bateau
    private VisualManager VM;//Visual Manager (centralisation)
    private Vector3 pos;//position du bateau à chaque frame
    private Vector3 mOffset;
    private MagManager mag;//Mag Manager du bateau
    private Camera C;//Camera en cours de fonctionnement (pour gérer l'offset souris)
    string test;

    private void Start()
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        test=this.gameObject.name;
        string test2 = this.gameObject.transform.parent.name;
        if (test2[10] == '1')//Fait correspondre les attributs en fonction du batea attaché
        {
            SM = VM.getShipM(1);
            mag = VM.getMagM(1);
            pos = VM.getposGVM(1);
            C = VM.getCameraVM(1).GetComponent<Camera>();
        }
        else
        {
            if (test2[10] == '2')
            {
                SM = VM.getShipM(2);
                mag = VM.getMagM(2);
                pos = VM.getposGVM(2);
                C = VM.getCameraVM(2).GetComponent<Camera>();
            }
        }

        initOrigin(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }


        void OnMouseDown()//Clic souris gauche
    {
        magv = false;
        mOffset = this.gameObject.transform.position - GetMouseWorldPos();//enregistre l'offset entre la souris et l'objet
        if (mag.getMagasinpos() == 1)
        {
            mag.setFermer();//ferme le magasin pendant le drag and drop
        }
    }

    private void OnMouseDrag()//Si le clic est enfoncé pendant que la souris se déplace
    {
        this.gameObject.transform.position = GetMouseWorldPos() + mOffset;
        if (Input.GetKeyDown(KeyCode.R))//Press tab pour rotate un bateau
        {
            changeRot();
        }
    }

    private void OnMouseUp()//Relache du bouton souris
    {
        this.gameObject.transform.position = cutVector(this.gameObject.transform.position);
        this.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "ShipLayer2";
        SM.getClassShip(test[test.Length-1]-48).updateG();
        checkPos();//Vérifie si le bateau ne chavauche pas/ ne sort pas de la grille
        mag.setOuvrir();//Ré-ouvre le magasin
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition; //coord pixels
        return C.ScreenToWorldPoint(mousePoint);
    }

    void initOrigin(float x, float y, float z)//Permet de copier les coords d'origine sans pointeur
    {
        OriginalPos = new Vector3(x, y, z);
    }

    public void changeMag()//Change la valeur d'ouverture/fermeture du magasin
    {
        if (magv == false)
        {
            magv = true;
        }
        else { magv = false; }
    }

    public void changeRot()//Permet de tourner le bateau (vert/horiz)
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

    public void moveShip(float x, float y, float z) //Décale le bateau en fonction des coords d'entrées x y z
    {
        Vector3 V;
        if (magv)
        {
            V = this.gameObject.transform.position;
            this.gameObject.transform.position = new Vector3(V.x + x, V.y + y, V.z + z);
        }
    }


    private int getTaille()//Retourne la taille du bateau en cours
    {
        if (this.gameObject.name == "Torpilleur 0")
        {
            return 2;
        }
        if ((this.gameObject.name == "ContreTorpilleur 1")||(this.gameObject.name == "SousMarin 2"))
        {
            return 3;
        }
        if (this.gameObject.name == "Croiseur 3")
        {
            return 4;
        }
        if (this.gameObject.name == "PorteAvion 4")
        {
            return 5;
        }
        return -1;
    }

    private void resetPos()//reset la positoin et rotation du bateau à l'origine
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

    private void checkPos()//Vérifie si le bateaux ne déborde pas de la grille /ne chevauche pas les autres bateaux
    {
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

    private Vector3 cutVector(Vector3 V) //Permet de découper les vecteurs de sorte à positionner les bateaux correctement dans les cases
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