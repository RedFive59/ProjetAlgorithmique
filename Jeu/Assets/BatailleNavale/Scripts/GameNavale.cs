using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNavale : MonoBehaviour
{
    string sCam;
    GameObject Camx;
    VisualManager VM;
    CanvasGenerator Cvs;
    GameObject Slider1;

    // Start is called before the first frame update
    void Start()
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
        Cvs = new CanvasGenerator("CanvasJEU", new Vector3(0, 0, 0), new Vector2(0, 0), RenderMode.WorldSpace, null, 10, "UILayer", this.gameObject);
        Cvs.addPanel("MarkerInstru1", new Vector3(-3, 40.1f, 0), new Vector2(4, 1), new Color32(192, 72, 73, 255));
        Cvs.addText(Cvs.getPanel(0), "textMarker1", Cvs.getPanel(0).transform.position, new Vector2(11, 4), 1, 0.4f, "F2 : confirmer\nF3 : editer", Color.black, TextAnchor.MiddleCenter);
        Cvs.addPanel("MarkerInstru2", new Vector3(27, 40.1f, 0), new Vector2(4, 1), new Color32(192, 72, 73, 255));
        Cvs.addText(Cvs.getPanel(1), "textMarker2", Cvs.getPanel(1).transform.position, new Vector2(11, 4), 1, 0.4f, "F2 : confirmer\nF3 : editer", Color.black, TextAnchor.MiddleCenter);
        Cvs.addPanel("Victoire", new Vector3(-10, -10, 0), new Vector2(22f, 13f), Color.white);
        Cvs.getPanel(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/FinPartie");
        Cvs.addText(Cvs.getPanel(2), "textVictoire", Cvs.getPanel(2).transform.position, new Vector2(17, 6), 1, 1, "Victoire", Color.white ,TextAnchor.MiddleCenter);
        Cvs.addPanel("Changement", new Vector3(-10, -10, 0), new Vector2(21f, 12f), Color.white);
        Cvs.addText(Cvs.getPanel(3), "textchangement", Cvs.getPanel(3).transform.position, new Vector2(16, 5), 1, 1, "Changement Joueur", Color.black, TextAnchor.MiddleCenter);
        Cvs.getPanel(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/Panel");
        Cvs.addPanel("Menu", new Vector3(VM.getCameraVM(1).transform.position.x,VM.getCameraVM(1).transform.position.y,0), new Vector2(21f, 12f), Color.white);
        Cvs.addText(Cvs.getPanel(4), "textMenu", new Vector3(Cvs.getPanel(4).transform.position.x, Cvs.getPanel(4).transform.position.y+3,0), new Vector2(16, 5), 1, 1, "Lancer Partie\n->PRESS SPACE<-", Color.black, TextAnchor.UpperCenter);
        Cvs.getPanel(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/MenuTest");
        Cvs.getPanel(3).AddComponent<TimerSlider>();
        Slider1 = GameObject.Find("Slider1");
        Slider1.transform.SetParent(Cvs.getPanel(3).transform, false);
        Vector3 posslider = Cvs.getPanel(3).transform.position;
        Slider1.transform.position = new Vector3(posslider.x, posslider.y - 2, 1);
        // Cvs.getPanel.transform
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject.Find("Menu").transform.position = new Vector3(-50, -50, 0);
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {

            for (int i = 0; i < 2; i++)
            {
                if (VM.getCameraVM(i + 1).GetComponent<Camera>().enabled == true)
                {
                    sCam = VM.getCameraVM(i + 1).name;
                }
            }
            if (sCam[sCam.Length - 1] == '1')
            {
                Cvs.ChangeCam(VM.getCameraVM(2).GetComponent<Camera>());
                Cvs.getPanel(3).transform.position = new Vector3(VM.getCameraVM(2).transform.position.x, VM.getCameraVM(2).transform.position.y, 0);
                VM.getMagM(1).FinPlacement();
                return;
            }

            if (sCam[sCam.Length - 1] == '2')
            {
                Cvs.ChangeCam(VM.getCameraVM(1).GetComponent<Camera>());
                Cvs.getPanel(3).transform.position = new Vector3(VM.getCameraVM(1).transform.position.x, VM.getCameraVM(1).transform.position.y, 0);
                VM.getMagM(2).FinPlacement();
                return;
            }

        }
    }

    public void switchX()
    {
        if ((VM.getCameraVM(1).GetComponent<Camera>().enabled == true) || (VM.getCameraVM(3).GetComponent<Camera>().enabled == true)){
            VM.switchPlayer();
            Cvs.getPanel(3).transform.position = new Vector3(VM.getCameraVM(2).transform.position.x, VM.getCameraVM(2).transform.position.y, 0);
        }
        else
            {
                VM.switchPlayer();
                Cvs.getPanel(3).transform.position = new Vector3(VM.getCameraVM(1).transform.position.x, VM.getCameraVM(1).transform.position.y, 0);
            }
    }

    public CanvasGenerator getCvsGN() {
        return Cvs;
    }

}
