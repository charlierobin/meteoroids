using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapAroundScreen : MonoBehaviour
{
    private float margin = 1.0f;

    private void FixedUpdate()
    {
        if (this.transform.position.y > ScreenBounds.topLeft.y + this.margin)
        {
            this.transform.position = new Vector2(this.transform.position.x, ScreenBounds.bottomRight.y - this.margin);
        }
        else if (this.transform.position.y < ScreenBounds.bottomRight.y - this.margin)
        {
            this.transform.position = new Vector2(this.transform.position.x, ScreenBounds.topLeft.y + this.margin);
        }

        if (this.transform.position.x > ScreenBounds.bottomRight.x + this.margin)
        {
            this.transform.position = new Vector2(ScreenBounds.topLeft.x - this.margin, this.transform.position.y);
        }
        else if (this.transform.position.x < ScreenBounds.topLeft.x - this.margin)
        {
            this.transform.position = new Vector2(ScreenBounds.bottomRight.x + this.margin, this.transform.position.y);
        }
    }
}
