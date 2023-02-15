using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHighlightMove : MonoBehaviour
{
    public float delay = 2.0f;

    private void Start()
    {
        TMPro.TextMeshProUGUI t = this.GetComponent<TMPro.TextMeshProUGUI>();

        t.color = new Color(t.color.r, t.color.g, t.color.b, 0f);

        Invoke("LetsGo", this.delay);
    }

    private void LetsGo()
    {
        StartCoroutine(this.Animator());

        StartCoroutine(this.FadeIn());
    }

    IEnumerator Animator()
    {
        RectTransform t = this.GetComponent<RectTransform>();

        while (t.anchoredPosition.y > 81)
        {
            t.Translate(0, -2.5f * Time.deltaTime, 0);

            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        TMPro.TextMeshProUGUI t = this.GetComponent<TMPro.TextMeshProUGUI>();

        while (t.color.a < 1.0)
        {
            t.color = new Color(t.color.r, t.color.g, t.color.b, t.color.a + (0.15f * Time.deltaTime));

            yield return null;
        }
    }
}

