using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core;
using UnityEngine;

public class UI_StartScreen : MonoBehaviour
{
    public List<GameObject> particles;

    public float delay = 5.0f;

    private void Start()
    {
        this.name = typeof(UI_StartScreen).Name;

        Invoke("launch", this.delay);
    }

    private void launch()
    {
        foreach (GameObject g in this.particles)
        {
            g.SetActive(true);
        }
    }
}

