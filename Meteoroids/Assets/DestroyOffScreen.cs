using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    private void Start()
    {
        //StartCoroutine(Killer());
    }

    private void LateUpdate()
    {
        if (this.transform.position.x > ScreenBounds.bottomRight.x)
        {
            Destroy(this.gameObject);
        }
        else if (this.transform.position.x < ScreenBounds.topLeft.x)
        {
            Destroy(this.gameObject);
        }
        else if (this.transform.position.y > ScreenBounds.topLeft.y)
        {
            Destroy(this.gameObject);
        }
        else if (this.transform.position.y < ScreenBounds.bottomRight.y)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator Killer()
    {
        yield return new WaitForSeconds(5.0f);

        Destroy(this.gameObject);
    }
}
