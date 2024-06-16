using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridPiece[] gridPieces;

    GameObject moveFromTile;
    GameObject moveToTile;

    public void AnticipatePawnMovment(int currentX, int currentY, GameObject callerGameObject)
    {
        moveFromTile = callerGameObject;

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            int xPos = allPieces.xPos;
            int yPos = allPieces.yPos;

            if (xPos == currentX && yPos == currentY + 1)
            {
                allPieces.anticipateMovment = true;

                moveToTile = allPieces.gameObject;
            }
        }
    }

    public void movePiece(int whatToMove)
    {
        if(whatToMove == 0)
        {
            foreach (Transform child in moveFromTile.transform)
            {
                if (child.tag == "Pawn")
                {
                    child.gameObject.SetActive(false);


                }

            }

            foreach (Transform child in moveToTile.transform)
            {
                if (child.tag == "Pawn")
                {
                    child.gameObject.SetActive(true);
                }

            }
        }
    }
}
