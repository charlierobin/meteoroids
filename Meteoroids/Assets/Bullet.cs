using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Bullet With(Transform shooter, float force)
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddForce(shooter.up * force, ForceMode.Impulse);

        this.transform.rotation = shooter.rotation;

        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("hittable")) return;

        other.gameObject.SendMessage("HitByBullet", this.gameObject, SendMessageOptions.DontRequireReceiver);

        Destroy(this.gameObject);
    }
}
