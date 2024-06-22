using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridPiece[] gridPieces;

    GameObject moveFromTileObject;
    GameObject moveToTile;

    #region Horse movemnt 

    int numberOfTimesLookingForPlayer = 0;
    int numberOfTimesLookingForPlayerLeft = 0;

    int currentXOfPiece;
    int currentYOfPiece;

    bool horseFoundPlayer = false;

    #endregion

    #region Pawn Movment

    private void Start()
    {
        //gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        //foreach (GridPiece piece in gridPieces)
        //{
        //    if (piece.enemyPieceHere)
        //    {
        //        numberOfTimesLookingForPlayer++;
        //    }
        //}
        //Debug.Log(numberOfTimesLookingForPlayer);
        //numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

        StartCoroutine(DelayStart());
    }

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
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;


        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            int xPos = allPieces.xPos;
            int yPos = allPieces.yPos;

            if (!horseFoundPlayer)
            {
               #region Down Movment

                 if (xPos == currentX - 1 && yPos == currentY - 2)
                 {
                     if (allPieces.playerPieceHere)
                     {
                         Debug.Log("Found Player");
                         horseFoundPlayer = true;

                        //break;
                     }
                     else
                     {
                         //EnemyHorseMovment(currentX - 1, currentY - 2);
                     }
                 }

                if (xPos == currentX + 1 && yPos == currentY - 2)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                        horseFoundPlayer = true;

                        //break;
                    }
                    else
                    {
                        //EnemyHorseMovment(currentX + 1, currentY - 2);
                    }
                }

                #endregion

               #region Right Movment

                if (xPos == currentX + 2 && yPos == currentY - 1)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                        horseFoundPlayer = true;

                        //break;
                    }
                    else
                    {
                        //EnemyHorseMovment(currentX + 2, currentY - 1);
                    }
                }


                if (xPos == currentX + 2 && yPos == currentY + 1)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                        horseFoundPlayer = true;

                        //break;
                    }
                    else
                    {
                        //EnemyHorseMovment(currentX + 2, currentY + 1);
                    }
                }

                #endregion

               #region Left Movment

                if (xPos == currentX - 2 && yPos == currentY - 1)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                        horseFoundPlayer = true;

                        //break;
                    }
                    else
                    {
                        //EnemyHorseMovment(currentX - 2, currentY - 1);
                    }
                }

                if (xPos == currentX - 2 && yPos == currentY + 1)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                        horseFoundPlayer = true;

                        //break;
                    }
                    else
                    {
                        //EnemyHorseMovment(currentX - 2, currentY + 1);
                    }
                }

                #endregion

               #region Up Movment

                if (xPos == currentX - 1 && yPos == currentY + 2)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                        horseFoundPlayer = true;

                        //break;
                    }
                    else
                    {
                        //EnemyHorseMovment(currentX - 1, currentY + 2);
                    }
                }

                if (xPos == currentX + 1 && yPos == currentY + 2)
                {
                    if (allPieces.playerPieceHere)
                    {
                        Debug.Log("Found Player");
                        horseFoundPlayer = true;

                        //break;
                    }
                    else
                    {
                        //EnemyHorseMovment(currentX + 1, currentY + 2);
                    }
                }

                #endregion

               //break;
            }
        }

        numberOfTimesLookingForPlayerLeft--;
        Debug.Log(numberOfTimesLookingForPlayerLeft + " Left");

        if (!horseFoundPlayer && numberOfTimesLookingForPlayerLeft == 0)
        {
            numberOfTimesLookingForPlayer *= 8;
            Debug.Log(numberOfTimesLookingForPlayer + " Max");

            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;


            EnemyHorseMovment(currentX - 1, currentY - 2);
            EnemyHorseMovment(currentX + 1, currentY - 2);

            EnemyHorseMovment(currentX + 2, currentY - 1);
            EnemyHorseMovment(currentX + 2, currentY + 1);

            EnemyHorseMovment(currentX - 2, currentY - 1);
            EnemyHorseMovment(currentX - 2, currentY + 1);

            EnemyHorseMovment(currentX - 1, currentY + 2);
            EnemyHorseMovment(currentX + 1, currentY + 2);
        }


    }

    public void FindPlayerHorse(int posisionToMoveToX, int possosonToMoveToY)
    {



    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.1f);

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece piece in gridPieces)
        {
            if (piece.enemyPieceHere)
            {
                numberOfTimesLookingForPlayer++;
            }
        }

        Debug.Log(numberOfTimesLookingForPlayer + " Enemys");
        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;
    }
}
