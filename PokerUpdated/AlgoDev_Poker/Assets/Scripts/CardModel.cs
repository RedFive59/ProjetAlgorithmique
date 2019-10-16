using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

	public Sprite[] faces;
	public Sprite dosCarte;

	public int pointeurCartes; //permet de naviguer à travers les sprites

	public void changerFace (bool montrerFace)
	{
		if (montrerFace)
		{
            // Montrer la face
            spriteRenderer.sprite = faces[pointeurCartes];
		}
		else
		{
            // Montrer le dos
            spriteRenderer.sprite = dosCarte;
		}
	}

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
		
}
