using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    public static Vector3 topLeft
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, -Camera.main.transform.position.z));
        }
    }

    public static Vector3 bottomRight
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, -Camera.main.transform.position.z));
        }
    }
}
