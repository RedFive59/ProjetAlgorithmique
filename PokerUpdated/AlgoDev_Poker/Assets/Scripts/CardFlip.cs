using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CardModel model;

    public AnimationCurve scaleCurve;
    public float duree = 0.5f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        model = GetComponent<CardModel>();
    }

    public void flipCard(Sprite startImage, Sprite endImage, int pointeurCarte)
    {
        StopCoroutine(Flip(startImage, endImage, pointeurCarte));
        StartCoroutine(Flip(startImage, endImage, pointeurCarte));
    }

    IEnumerator Flip(Sprite startImage, Sprite endImage, int pointeurCarte)
    {
        spriteRenderer.sprite = startImage;
        float time = 0f;
        while (time <= 1f)
        {
            float scale = scaleCurve.Evaluate(time);
            time += Time.deltaTime / duree;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if (time >= 0.5f)
            {
                spriteRenderer.sprite = endImage;
            }

            yield return new WaitForFixedUpdate();
        }

        if (pointeurCarte == -1)
        {
            model.changerFace(false);
        }
        else
        {
            model.pointeurCartes = pointeurCarte;
            model.changerFace(true);
        }
    }
}
