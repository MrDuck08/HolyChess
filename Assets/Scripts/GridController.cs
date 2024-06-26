using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridPiece[] gridPieces;

    GameObject moveFromTileObject;
    GameObject moveToTile;

    #region Horse movemnt 

    int numberOfTimesLookingForPlayer = 0;
    int numberOfTimesLookingForPlayerLeft = 0;
    int maxNumberOfTries = 10;
    int currentAmountOfTries;

    int currentXOfPiece;
    int currentYOfPiece;

    List<int> currentXOfHorseList = new List<int>();
    List<int> currentYOfHorseList = new List<int>();

    List<int> currentXOfHorseListComplete = new List<int>();
    List<int> currentYOfHorseListComplete = new List<int>();

    bool horseFoundPlayer = false;

    #endregion

    #region Pawn Movment

    private void Start()
    {
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

    public void EnemyHorseMovmentCall(int xPos, int yPos)
    {
        //numberOfTimesLookingForPlayer = 1;
        //numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;


        //currentYOfHorseList.Clear();
        //currentXOfHorseList.Clear();
        //currentXOfHorseListComplete.Clear();
        //currentYOfHorseListComplete.Clear();

        //Debug.Log("Call");

        //EnemyHorseMovment(xPos, yPos);
    }

    public void EnemyHorseMovment(int currentX, int currentY)
    {
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;

        currentXOfHorseList.Add(currentXOfPiece);
        currentYOfHorseList.Add(currentYOfPiece);

        #region Search For Player

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            int xPos = allPieces.xPos;
            int yPos = allPieces.yPos;

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

        }

        #endregion

        numberOfTimesLookingForPlayerLeft--;
        //Debug.Log(numberOfTimesLookingForPlayerLeft + " Left");

        if (!horseFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {
            numberOfTimesLookingForPlayer *= 8;
            currentAmountOfTries++;

            currentXOfHorseListComplete.AddRange(currentXOfHorseList);
            currentYOfHorseListComplete.AddRange(currentYOfHorseList);

            currentYOfHorseList.Clear();
            currentXOfHorseList.Clear();

            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            Debug.Log(numberOfTimesLookingForPlayer + " Max");
            Debug.Log(currentXOfHorseListComplete.Count + " List");

            for (int i = 0; i < currentXOfHorseListComplete.Count; i++)
            {
                Debug.Log(i + " IIII");
                EnemyHorseMovment(currentXOfHorseListComplete[i] - 1, currentYOfHorseListComplete[i] - 2);
                EnemyHorseMovment(currentXOfHorseListComplete[i] + 1, currentYOfHorseListComplete[i] - 2);

                EnemyHorseMovment(currentXOfHorseListComplete[i] + 2, currentYOfHorseListComplete[i] - 1);
                EnemyHorseMovment(currentXOfHorseListComplete[i] + 2, currentYOfHorseListComplete[i] + 1);

                EnemyHorseMovment(currentXOfHorseListComplete[i] - 2, currentYOfHorseListComplete[i] - 1);
                EnemyHorseMovment(currentXOfHorseListComplete[i] - 2, currentYOfHorseListComplete[i] + 1);

                EnemyHorseMovment(currentXOfHorseListComplete[i] - 1, currentYOfHorseListComplete[i] + 2);
                EnemyHorseMovment(currentXOfHorseListComplete[i] + 1, currentYOfHorseListComplete[i] + 2);

            }

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
