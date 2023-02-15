using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float lifetime = 2.0f;

    private void Start()
    {
        Destroy(this.gameObject, this.lifetime);
    }
}
