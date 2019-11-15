using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    bool dessus = false;//Booléen indiquant si la souris est sur l'objet
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        select();
        
    }
    private void select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //GameObject tile = GameObject.Find(hit.transform.gameObject.name);
            if(this.name == hit.transform.gameObject.name){
                if (!dessus)
                {
                    print("Flip " + hit.transform.gameObject.name);
                    dessus = true;
                }
            }   
        }
        else
        {
            if (dessus)
            {
                print("Flip inverse " + this.name);
                dessus = false;
            }
        }
    }
}
