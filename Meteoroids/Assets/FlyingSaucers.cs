using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlyingSaucers : MonoBehaviour
{
    public FlyingSaucer flyingSaucerPrefab;

    private float margin = 0.9f;

    private void Start()
    {
        this.name = typeof(FlyingSaucers).Name;

        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 25.0f));

            Instantiate(this.flyingSaucerPrefab).At(Random.SomewhereAroundPerimeterOf(ScreenBounds.topLeft, ScreenBounds.bottomRight, this.margin));
        }
    }

    private void OnDestroy()
    {
        FlyingSaucer[] saucers = GameObject.FindObjectsOfType<FlyingSaucer>();

        foreach (FlyingSaucer f in saucers)
        {
            Destroy(f.gameObject);
        }
    }

    public void AsteroidWasHitByPlayer()
    {

    }

    public void FlyingSaucerWasHitByPlayer()
    {

    }
}
