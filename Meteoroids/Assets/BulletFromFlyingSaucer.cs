using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFromFlyingSaucer : MonoBehaviour
{
    public BulletFromFlyingSaucer With(Transform shooter)
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();

        rb.AddRelativeForce(shooter.up * 5.0f, ForceMode.Impulse);

        return this;
    }

    public BulletFromFlyingSaucer UnParent()
    {
        this.transform.parent = null;

        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("clearer")) return;

        if (other.GetComponent<RespawnClearer>())
        {
            //Debug.Log("bullet cleared by spawn shield");
        }
        else
        {
            other.gameObject.SendMessage("HitByBullet", this.gameObject, SendMessageOptions.DontRequireReceiver);
        }

        Destroy(this.gameObject);
    }
}
