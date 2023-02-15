using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public bool explodeOnStart = false;
    private bool exploded = false;

    private void Start()
    {
        if (this.explodeOnStart) this.Explode();
    }

    public void Explode()
    {
        if (this.exploded) return;

        this.exploded = true;

        ParticleSystem[] systems = this.GetComponentsInChildren<ParticleSystem>();

        foreach (ParticleSystem system in systems)
        {
            system.Play();
        }
    }
}

