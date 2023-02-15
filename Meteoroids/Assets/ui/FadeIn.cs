using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public float delay = 1.0f;
    public float speed = 1.0f;

    private void Start()
    {
        CanvasGroup g = this.GetComponent<CanvasGroup>();

        g.alpha = 0;

        Invoke("startFade", this.delay);
    }

    private void startFade()
    {
        if (!this.isActiveAndEnabled) return;

        StartCoroutine(this.doFade());
    }

    IEnumerator doFade()
    {
        CanvasGroup g = this.GetComponent<CanvasGroup>();

        while (g.alpha < 1.0f)
        {
            g.alpha = g.alpha + (this.speed * Time.deltaTime);

            yield return null;
        }
    }

}
