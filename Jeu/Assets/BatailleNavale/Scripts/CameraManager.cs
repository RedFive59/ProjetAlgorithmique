using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera CameraM=this.GetComponent<Camera>();//Declare une variable de type camera et récupère la camera à laquelle le script est attaché
        CameraM.backgroundColor = Color.black;
        CameraM.enabled = true; //la camera est active
        CameraM.orthographic = true; //camera est en mode orthographique
        CameraM.orthographicSize = 5.8f; //déclare la taille (diagonale) du rectangle que couvre la camera
        //la position initiale n'est pas changée la caméra est centrée sur l'origine de la scène
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
