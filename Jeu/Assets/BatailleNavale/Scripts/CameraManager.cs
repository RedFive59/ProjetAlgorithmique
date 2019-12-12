using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    private GameObject CameraM;
    public CameraManager(string nom, Vector3 pos)
    {
        //Camera//
        CameraM = new GameObject(nom);
        CameraM.AddComponent<Camera>();
        Camera Cam = CameraM.GetComponent<Camera>();
        Cam.transform.position = new Vector3(pos.x + 5f, pos.y + 5f, pos.z - 10f);//position cam
        Cam.backgroundColor = Color.black;
        Cam.enabled = true; //la camera est active
        Cam.orthographic = true; //camera est en mode orthographique
        Cam.orthographicSize = 5.8f; //déclare la taille (diagonale) du rectangle que couvre la camera
        //la position initiale n'est pas changée la caméra est centrée sur l'origine de la scène
    }

    public GameObject getCameraC()
    {
        {
            return CameraM;
        }
    }
}