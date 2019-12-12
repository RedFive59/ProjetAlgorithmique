using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSons : MonoBehaviour
{

    public static GestionSons Instance;

    public AudioClip sonFlip;
    public AudioClip sonJetons;

    void Awake()
    {
        Instance = this;
    }

    public void SonFlip()
    {
        JouerSon(sonFlip);
    }

    public void SonJetons()
    {
        JouerSon(sonJetons);
    }

    private void JouerSon(AudioClip ac)
    {
        AudioSource.PlayClipAtPoint(ac, transform.position);
    }
}
