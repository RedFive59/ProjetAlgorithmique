using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection2 : MonoBehaviour
{
    RectTransform rects2;
    Vector3 vs2;

    // Start is called before the first frame update
    void Start()
    {
        rects2 = GetComponent<RectTransform>();
        vs2 = rects2.localPosition;
    }

    void OnMouseEnter()
    {
        rects2.localPosition = new Vector3(271f, 0, 0);

    }

    void OnMouseExit()
    {
        rects2.localPosition = vs2;
    }
}
