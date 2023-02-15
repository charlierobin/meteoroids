using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : MonoBehaviour
{
    public enum Side
    {
        Top,
        Bottom,
        Left,
        Right
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static int RandomInRange(int from, int to)
    {
        return UnityEngine.Random.Range(from, to + 1);
    }

    public static Side RandomSide()
    {
        int side = RandomInRange(1, 4);

        if (side == 1)
        {
            return Side.Top;
        }
        else if (side == 2)
        {
            return Side.Bottom;
        }
        else if (side == 3)
        {
            return Side.Left;
        }
        else
        {
            return Side.Right;
        }
    }

    public static Direction RandomDirection()
    {
        int side = RandomInRange(1, 4);

        if (side == 1)
        {
            return Direction.Up;
        }
        else if (side == 2)
        {
            return Direction.Down;
        }
        else if (side == 3)
        {
            return Direction.Left;
        }
        else
        {
            return Direction.Right;
        }
    }

    public static Vector3 SomewhereAroundPerimeterOf(Vector3 topLeft, Vector3 bottomRight)
    {
        Side side = RandomSide();

        if (side == Side.Top)
        {
            return new Vector3(Random.Range(topLeft.x, bottomRight.x), topLeft.y, 0);
        }
        else if (side == Side.Bottom)
        {
            return new Vector3(Random.Range(topLeft.x, bottomRight.x), bottomRight.y, 0);
        }
        else if (side == Side.Left)
        {
            return new Vector3(topLeft.x, Random.Range(topLeft.y, bottomRight.y), 0);
        }
        else
        {
            return new Vector3(bottomRight.x, Random.Range(topLeft.y, bottomRight.y), 0);
        }
    }

    public static Vector3 SomewhereAroundPerimeterOf(Vector3 topLeft, Vector3 bottomRight, float margin)
    {
        Side side = RandomSide();

        if (side == Side.Top)
        {
            return new Vector3(Random.Range(topLeft.x, bottomRight.x), topLeft.y + margin, 0);
        }
        else if (side == Side.Bottom)
        {
            return new Vector3(Random.Range(topLeft.x, bottomRight.x), bottomRight.y - margin, 0);
        }
        else if (side == Side.Left)
        {
            return new Vector3(topLeft.x - margin, Random.Range(topLeft.y, bottomRight.y), 0);
        }
        else
        {
            return new Vector3(bottomRight.x + margin, Random.Range(topLeft.y, bottomRight.y), 0);
        }
    }

    public static float Range(float v1, float v2)
    {
        return UnityEngine.Random.Range(v1, v2);
    }

    public static int Range(int v1, int v2)
    {
        return UnityEngine.Random.Range(v1, v2);
    }

    public static Vector2 insideUnitCircle()
    {
        return UnityEngine.Random.insideUnitCircle;
    }

    public static Vector2 direction()
    {
        Vector2 direction = UnityEngine.Random.insideUnitCircle;

        direction.Normalize();

        return direction;
    }

}
