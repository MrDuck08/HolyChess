using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridPiece[] gridPieces;

    GameObject moveFromTileObject;
    GameObject moveToTile;

    #region Pawn Movment

    public void AnticipatePawnMovment(int currentX, int currentY, GameObject callerGameObject)
    {
        moveFromTileObject = callerGameObject;

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
            moveFromTileObject.GetComponent<GridPiece>().playerPieceHere = false;
            foreach (Transform child in moveFromTileObject.transform)
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

    #endregion


    public void EnemyHorseMovment(int currentX, int currentY)
    {

        
            #region Down Movment

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - 1 && yPos == currentY - 2)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                    }
                    else
                    {
                        EnemyHorseMovment(currentX - 1, currentY - 2);
                    }
                }
            }

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + 1 && yPos == currentY - 2)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                    }
                    else
                    {
                        EnemyHorseMovment(currentX + 1, currentY - 2);
                    }
                }
            }

            #endregion

            #region Right Movment

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + 2 && yPos == currentY - 1)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                    }
                    else
                    {
                        EnemyHorseMovment(currentX + 2, currentY -1);
                    }
                }
            }

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + 2 && yPos == currentY + 1)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                    }
                    else
                    {
                        EnemyHorseMovment(currentX + 2, currentY + 1);
                    }
                }
            }

            #endregion

            #region Left Movment

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - 2 && yPos == currentY - 1)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                    }
                    else
                    {
                        EnemyHorseMovment(currentX - 2, currentY - 1);
                    }
                }
            }

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - 2 && yPos == currentY + 1)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                    }
                    else
                    {
                        EnemyHorseMovment(currentX - 2, currentY + 1);
                    }
                }
            }

            #endregion

            #region Up Movment

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - 1 && yPos == currentY + 2)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                    }
                    else
                    {
                        EnemyHorseMovment(currentX - 1, currentY + 2);
                    }
                }
            }

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + 1 && yPos == currentY + 2)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                    }
                    else
                    {
                        EnemyHorseMovment(currentX + 1, currentY + 2);
                    }
                }
            }

            #endregion

        

    }
}
