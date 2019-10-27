using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 mOffset;

    // Start is called before the first frame update
   
        void OnMouseDown()
    {
        mOffset = this.transform.position - GetMouseWorldPos();//enregistre l'offset entre la souris et l'objet
    }

        private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition; //coord pixels
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        this.transform.position = GetMouseWorldPos() + mOffset;
        if (Input.GetKeyDown(KeyCode.Tab))//Press tab pour rotate un bateau
        {
            this.transform.Rotate(new Vector3(0, 0, 90f));
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
}