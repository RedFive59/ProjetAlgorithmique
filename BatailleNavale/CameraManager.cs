using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera CameraM; //Declare une variable de type camera et récupère la camera à laquelle le script est attaché

    // Start is called before the first frame update
    void Start()
    {
        CameraM = Camera.main; //la camere est déclarée en tant que caméra principale de la scene
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
