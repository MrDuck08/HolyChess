using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    int xPos;
    int yPos;

    public void SpawnLocation(int x, int y)
    {
        xPos = x;
        yPos = y;

        transform.position = new Vector2 (xPos, yPos);
    }
}
