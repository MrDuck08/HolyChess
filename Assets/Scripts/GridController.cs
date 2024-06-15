using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridPiece[] gridPieces;


    public void MovePawn(int currentX, int currentY)
    {

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            //https://discussions.unity.com/t/how-to-find-a-certain-gameobject-using-a-variable-in-a-common-script-from-an-array-of-gameobjects/132458/2

            int xPos = allPieces.xPos;
            int yPos = allPieces.yPos;

            if (xPos == currentX && yPos == currentY + 1)
            {
                allPieces.gameObject.SetActive(false);
            }
        }
    }
}
