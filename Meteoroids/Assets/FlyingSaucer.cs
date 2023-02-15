using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Random;

public class FlyingSaucer : MonoBehaviour
{
    public float rotationSpeed = 25.0f;

    public int minSalvoCount = 1;
    public int maxSalvoCount = 7;

    public float salvoAccuracy = 4.0f;

    public GameObject sparksPrefab;
    public GameObject explosionPrefab;

    public BulletFromFlyingSaucer bulletPrefab;

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private Direction direction;

    private void Start()
    {
        StartCoroutine(this.spinner());

        StartCoroutine(this.shooter());

        StartCoroutine(this.mover());
    }

    private Direction newRandomDirection(Direction current)
    {
        List<Direction> directions = new List<Direction>();

        directions.Add(Direction.Up);
        directions.Add(Direction.Down);
        directions.Add(Direction.Left);
        directions.Add(Direction.Right);

        directions.Remove(current);

        return directions[UnityEngine.Random.Range(0, directions.Count)];
    }

    IEnumerator mover()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 7.0f));

            this.direction = this.newRandomDirection(this.direction);

            rb.velocity = Vector3.zero;

            if (this.direction == Direction.Up)
            {
                rb.AddForce(new Vector3(0, 4, 0), ForceMode.Impulse);
            }
            else if (this.direction == Direction.Down)
            {
                rb.AddForce(new Vector3(0, -4, 0), ForceMode.Impulse);
            }
            else if (this.direction == Direction.Left)
            {
                rb.AddForce(new Vector3(-4, 0, 0), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(new Vector3(4, 0, 0), ForceMode.Impulse);
            }
        }
    }

    IEnumerator spinner()
    {
        Transform t = this.transform.GetChild(0);

        while (true)
        {
            t.Rotate(new Vector3(0, this.rotationSpeed * Time.deltaTime, 0));

            yield return null;
        }
    }

    IEnumerator shooter()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();

        while (true)
        {
            yield return new WaitForSeconds(5.0f);

            Player p = GameObject.FindFirstObjectByType<Player>();

            if (p)
            {
                Transform t = this.transform.GetChild(1);

                Transform spawn = this.transform.GetChild(1).GetChild(0);

                int salvoCount = Random.RandomInRange(this.minSalvoCount, this.maxSalvoCount);

                for (int i = 0; i < salvoCount; i++)
                {
                    t.Rotate(Random.Range(-this.salvoAccuracy, +this.salvoAccuracy), 0, 0, Space.Self);

                    Instantiate(this.bulletPrefab, spawn).With(this.transform).UnParent();

                    yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
                }
            }
        }
    }

    private void Update()
    {
        Player p = GameObject.FindFirstObjectByType<Player>();

        if (p)
        {
            Transform t = this.transform.GetChild(1);

            t.LookAt(p.transform);
        }
    }

    public FlyingSaucer At(Vector3 pos)
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();

        this.transform.position = pos;

        if (this.transform.position.x > ScreenBounds.bottomRight.x)
        {
            rb.AddForce(new Vector3(-3, 0, 0), ForceMode.Impulse);

            this.direction = Direction.Left;
        }
        else if (this.transform.position.x < ScreenBounds.topLeft.x)
        {
            rb.AddForce(new Vector3(3, 0, 0), ForceMode.Impulse);

            this.direction = Direction.Right;
        }
        else if (this.transform.position.y > ScreenBounds.topLeft.x)
        {
            rb.AddForce(new Vector3(0, -3, 0), ForceMode.Impulse);

            this.direction = Direction.Down;
        }
        else
        {
            rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);

            this.direction = Direction.Up;
        }

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
        GameObject.FindAnyObjectByType<FlyingSaucers>()?.FlyingSaucerWasHitByPlayer();

        GameObject.FindAnyObjectByType<Asteroids>()?.FlyingSaucerWasHitByPlayer();

        GameObject.FindAnyObjectByType<Player>()?.FlyingSaucerWasHitByPlayer();

        Instantiate(this.sparksPrefab, sender.transform.position, sender.transform.rotation);

        Instantiate(this.explosionPrefab, this.transform.position, sender.transform.rotation);

        GameObject.FindAnyObjectByType<Game>()?.Score(1000);

        Destroy(this.gameObject);
    }
}
