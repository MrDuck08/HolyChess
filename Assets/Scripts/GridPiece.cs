using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    int xPos;
    int yPos;

    #region Start position

    public void SpawnLocation(int x, int y)
    {
        xPos = x;
        yPos = y;

        transform.position = new Vector2 (xPos, yPos);
    }

    #endregion


    private void OnMouseOver()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);

    }

    private void OnMouseExit()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }
}
