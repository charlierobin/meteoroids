using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;

    public GameObject explosionPrefab;

    private float timer;
    private float interval = 0.2f;

    private Rigidbody rb;

    private float speed = 200.0f;
    private bool engineOn = false;

    private void Start()
    {
        this.name = typeof(Player).Name;

        this.rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float rotation = Input.GetAxis("Horizontal") * this.speed * Time.deltaTime;

        this.transform.Rotate(0, 0, -rotation);

        float thrust = Input.GetAxis("Vertical") * 10.0f * Time.deltaTime;

        if (thrust > 0.0f)
        {
            if (this.rb.velocity.magnitude < 5.0f) this.rb.AddForce(this.transform.up * thrust, ForceMode.Impulse);

            if (!this.engineOn)
            {
                this.engineOn = true;
                this.BroadcastMessage("EngineOn", SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            if (this.engineOn)
            {
                this.engineOn = false;
                this.BroadcastMessage("EngineOff", SendMessageOptions.DontRequireReceiver);
            }
        }

        if (Input.GetButton("Fire1"))
        {
            this.gameObject.BroadcastMessage("Fire", SendMessageOptions.DontRequireReceiver);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            this.explode();
        }
    }

    private void Fire()
    {
        if (Time.time < this.timer) return;

        Instantiate(this.bulletPrefab, this.bulletSpawnPoint.position, Quaternion.identity).With(this.transform, 3.0f + this.rb.velocity.magnitude);

        this.timer = Time.time + this.interval;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("hittable")) return;

        this.explode();
    }

    private void explode()
    {
        Instantiate(this.explosionPrefab, this.transform.position, this.transform.rotation);

        Destroy(this.gameObject);

        GameObject.FindAnyObjectByType<Game>()?.PlayerWasDestroyed();
    }

    public void AsteroidWasHitByPlayer()
    {
        // TODO player gets faster, firing quicker, as game progresses?
    }

    public void FlyingSaucerWasHitByPlayer()
    {
        // as for above ...
    }

    private void HitByBullet(GameObject sender)
    {
        this.explode();
    }
}
