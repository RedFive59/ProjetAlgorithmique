using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNavale : MonoBehaviour
{
    string sCam;
    VisualManager VM;

    // Start is called before the first frame update
    void Start()
    {
        VM = GameObject.FindObjectOfType<VisualManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            for(int i = 0; i <2; i++)
            {
                if (VM.getCameraVM(i+1).GetComponent<Camera>().enabled == true)
                {
                    sCam = VM.getCameraVM(i + 1).name;
                }
            }
            Debug.Log("PRESSED F1");
            if (sCam[sCam.Length - 1] == '1')
            {
                VM.getMagM(1).FinPlacement();
            }

            if (sCam[sCam.Length - 1] == '2')
            {
                VM.getMagM(2).FinPlacement();
            }
        }
    }
}
