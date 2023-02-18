using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Quit : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Application.isEditor)
            {
                EditorApplication.isPlaying = false;
            }
            else
            {
                Application.Quit();
            }
        }
    }
}
