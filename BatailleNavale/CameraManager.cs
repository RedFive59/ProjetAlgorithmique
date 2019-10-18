using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera CameraM;

    // Start is called before the first frame update
    void Start()
    {
        CameraM = Camera.main;
        CameraM.enabled = true;
        CameraM.orthographic = true;
        CameraM.orthographicSize = 5.8f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
