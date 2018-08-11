using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

A class for helper functions commonly used when dealing with Directions.

 */
public static class DirectionHelper
{

    /// <summary>
    /// Converts Direction to Vector2
    /// </summary>
    /// <param name="direction">Direction</param>
    public static Vector2 DirectionToVector(BaseConstants.Direction direction)
    {
        switch (direction)
        {
            case BaseConstants.Direction.Up:
                return Vector2.up;

            case BaseConstants.Direction.Down:
                return Vector2.down;

            case BaseConstants.Direction.Left:
                return Vector2.left;

            case BaseConstants.Direction.Right:
                return Vector2.right;

            default:
                return Vector2.zero;
        }
    }

    /// <summary>
    /// Converts Vector2 to Direction
    /// </summary>
    /// <param name="vector">Vector2</param>
    public static BaseConstants.Direction VectorToDirection(Vector2 vector)
    {
        if (vector == Vector2.up)
        {
            return BaseConstants.Direction.Up;
        }
        else if (vector == Vector2.down)
        {
            return BaseConstants.Direction.Down;
        }
        else if (vector == Vector2.left)
        {
            return BaseConstants.Direction.Left;
        }
        else
        {
            return BaseConstants.Direction.Right;
        }
    }

    /// <summary>
	/// Returns true if direction is vertical
	/// </summary>
	/// <param name="direction">Direction</param>
	public static bool IsVertical(BaseConstants.Direction direction)
    {
        return (direction == BaseConstants.Direction.Up || direction == BaseConstants.Direction.Down);
    }
}