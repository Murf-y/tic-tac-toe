using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square
{
    public bool isPressed = false;
    public Vector2 squarePos;
    public string state = null;

    public Square(bool isPressed, Vector2 squarePos)
    {
        this.isPressed = isPressed;
        this.squarePos = squarePos;
    }
}