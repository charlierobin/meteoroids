using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float interval = 20.0f;
    public float intervalDecreaseRate = 0.999f;

    public float velocityFactor = 1.0f;
    public float velocityFactorIncreaseRate = 1.01f;

    public float inaccuracy = 4.0f;
    public float inaccuracyDecreaseRate = 0.95f;

    private float margin = 0.5f;

    private void Start()
    {
        this.name = typeof(Asteroids).Name;

        StartCoroutine(Spawner());
    }

    public void AsteroidWasHitByPlayer()
    {
        this.interval = this.interval * this.intervalDecreaseRate;

        this.velocityFactor = this.velocityFactor * this.velocityFactorIncreaseRate;

        this.inaccuracy = this.inaccuracy * this.inaccuracyDecreaseRate;
    }

    public void FlyingSaucerWasHitByPlayer()
    {

    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));

        this.spawn();

        while (true)
        {
            yield return new WaitForSeconds(this.interval);

            this.spawn();
        }
    }

    private void spawn()
    {
        Player player = GameObject.FindAnyObjectByType<Player>();

        Vector3 target = player ? player.transform.position : new Vector3(0, 0, 0);

        Vector3 variation = this.inaccuracy * Random.insideUnitCircle();

        target = target + variation;

        Instantiate(this.asteroidPrefab).At(Random.SomewhereAroundPerimeterOf(ScreenBounds.topLeft + new Vector3(-this.margin, this.margin), ScreenBounds.bottomRight + new Vector3(this.margin, -this.margin))).MovingTowards(target, this.velocityFactor);
    }

    private void OnDestroy()
    {
        Asteroid[] asteroids = GameObject.FindObjectsOfType<Asteroid>();

        foreach (Asteroid a in asteroids)
        {
            Destroy(a.gameObject);
        }
    }
}
