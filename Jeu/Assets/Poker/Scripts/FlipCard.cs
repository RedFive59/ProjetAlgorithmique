using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCard : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    public AnimationCurve anim;
    public float duree = 0.5f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void flipCard(Sprite debut, Sprite fin)
    {
        StopCoroutine(Flip(debut, fin));
        StartCoroutine(Flip(debut, fin));
    }

    IEnumerator Flip(Sprite debut, Sprite fin)
    {
        spriteRenderer.sprite = debut;
        float time = 0f;

        while (time <= 1f)
        {
            float scale = anim.Evaluate(time);
            time += Time.deltaTime / duree;

            Vector3 localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;

            if (time >= 0.5f)
            {
                spriteRenderer.sprite = fin;
            }

            yield return new WaitForFixedUpdate();
        }
    }

}
