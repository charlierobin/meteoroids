using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnClearer : MonoBehaviour
{
    public float speed = 4f;
    public float lifetime = 1.2f;

    public float life = 0.0f;

    public Gradient gradient;

    private void Start()
    {
        StartCoroutine(Grow());

        Invoke("Finished", this.lifetime);
    }

    IEnumerator Grow()
    {
        SpriteRenderer s = this.transform.GetChild(0).GetComponent<SpriteRenderer>();

        while (true)
        {
            this.transform.localScale = this.transform.localScale * (1f + (this.speed * Time.deltaTime));

            s.color = this.gradient.Evaluate(this.life / this.lifetime);

            this.life = this.life + Time.deltaTime;

            yield return null;
        }
    }

    private void Finished()
    {
        Destroy(this.gameObject);
    }
}
