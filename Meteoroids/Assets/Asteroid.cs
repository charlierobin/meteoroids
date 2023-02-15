using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject sparksPrefab;
    public GameObject explosionPrefab;

    public float spin = 2.0f;
    public float baseSpeed = 2.0f;

    public float randomLowerRange = 0.5f;
    public float randomUpperRange = 4.0f;

    private int generation = 1;

    private Rigidbody rb;

    private void Awake()
    {
        this.RoughUp();

        this.rb = this.GetComponent<Rigidbody>();

        rb.AddRelativeTorque(Random.Range(-this.spin, this.spin), Random.Range(-this.spin, this.spin), Random.Range(-this.spin, this.spin), ForceMode.Impulse);
    }

    public Asteroid At(Vector3 pos)
    {
        this.transform.position = pos;

        return this;
    }

    public Asteroid MovingTowards(Vector3 target, float velocityFactor)
    {
        Vector3 dir = target - this.transform.position;

        dir.Normalize();

        this.rb.AddForce(dir * this.baseSpeed * velocityFactor, ForceMode.Impulse);

        return this;
    }

    public Asteroid SetGeneration(int generation)
    {
        this.generation = generation;

        this.transform.localScale = new Vector3(1.0f / this.generation, 1.0f / this.generation, 1.0f / this.generation);

        return this;
    }

    public Asteroid WithRandomDirection()
    {
        this.rb.AddForce(Random.direction() * Random.Range(this.randomLowerRange, this.randomUpperRange), ForceMode.Impulse);

        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("clearer")) return;

        Instantiate(this.explosionPrefab, this.transform.position, this.transform.rotation);

        Destroy(this.gameObject);
    }

    private void HitByBullet(GameObject sender)
    {
        Asteroids a = GameObject.FindAnyObjectByType<Asteroids>();

        a.AsteroidWasHitByPlayer();

        GameObject.FindAnyObjectByType<FlyingSaucers>()?.AsteroidWasHitByPlayer();

        GameObject.FindAnyObjectByType<Player>()?.AsteroidWasHitByPlayer();

        if (this.generation < 4)
        {
            Asteroid prefab = a.asteroidPrefab;

            int numberOfBits = Random.Range(2, 4);

            for (int i = 0; i < numberOfBits; i++)
            {
                Instantiate(prefab).At(this.transform.position).WithRandomDirection().SetGeneration(this.generation + 1);
            }

            Instantiate(this.sparksPrefab, sender.transform.position, sender.transform.rotation);
        }
        else
        {
            Instantiate(this.explosionPrefab, sender.transform.position, sender.transform.rotation);
        }

        GameObject.FindAnyObjectByType<Game>()?.Score(this.generation * 100);

        Destroy(this.gameObject);
    }

    private void RoughUp()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = mesh.vertices;

        List<int> skip = new List<int>();

        for (int i = 0; i < vertices.Length; i++)
        {
            if (skip.Contains(i)) continue;

            int option = Random.Range(0, 3);

            float f = 1.0f;

            if (option == 0)
            {
                f = Random.Range(1.05f, 1.3f);
            }
            else if (option == 1)
            {
                f = Random.Range(0.7f, 0.95f);
            }
            else
            {
                f = 1.0f;
            }

            Vector3 orig = vertices[i];

            vertices[i] = vertices[i] * f;

            for (int j = i + 1; j < vertices.Length; j++)
            {
                if (vertices[j] == orig)
                {
                    vertices[j] = vertices[i];
                    skip.Add(j);
                }
            }
        }

        mesh.vertices = vertices;

        mesh.RecalculateBounds();
    }
}
