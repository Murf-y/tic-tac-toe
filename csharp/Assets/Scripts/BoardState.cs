using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardState
{
    public const int SIZE = 3;
    public static bool isXTurn = true;
    public static bool isGameOver = false;
    public static bool isGameStarted = false;
    public static List<Square> squares = new List<Square>();

    public static Vector2 vect3To2(this Vector3 vector3)
    {
        return new Vector2(vector3.x, vector3.y);
    }
}
