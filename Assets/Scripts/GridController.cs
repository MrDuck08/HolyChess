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
    int maxNumberOfTries = 50;
    int currentAmountOfTries;

    int currentXOfPiece;
    int currentYOfPiece;
    int numberOfEnemys;

    int firstPositionSearchingHorse;
    int secondPositionSearchingHorse;
    int thirdPositionSearchingHorse;
    int fourthPositionSearchingHorse;
    int fifthPositionSearchingHorse;
    int sixtPositionSearchingHorse;
    int seventhPositionSearchingHorse;
    int eightPositionSearchingHorse;

    List<int> currentXOfHorseList = new List<int>();
    List<int> currentYOfHorseList = new List<int>();

    List<int> currentXOfHorseListComplete = new List<int>();
    List<int> currentYOfHorseListComplete = new List<int>();

    List<int> moveToLocationHorseListX = new List<int>();
    List<int> moveToLocationHorseListY = new List<int>();

    bool horseFoundPlayer = false;

    bool firstSearchCompleteHorse;
    bool secondSearchCompleteHorse;
    bool thirdSearchCompleteHorse;
    bool fourthSearchCompleteHorse;
    bool fifthSearchCompleteHorse;
    bool sixtSearchCompleteHorse;
    bool seventhSearchCompleteHorse;
    bool eightSearchCompleteHorse;

    List<GameObject> enemyHorseObjectList = new List<GameObject>();

    #endregion

    int infoInt = 0;

    private void Start()
    {
        StartCoroutine(DelayStart());
    }

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

    #region Enemy Horse Movment

    public void EnemyHorseMovmentCall(int xPos, int yPos, GameObject calledObject)
    {
        enemyHorseObjectList.Add(calledObject);
        
        if(enemyHorseObjectList.Count >= numberOfEnemys)
        {
            for (int i = 0; i < enemyHorseObjectList.Count; i++)
            {
                EnemyHorseMovment(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos);
            }
        }

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

                    allPieces.enemyPieceHere = true;

                    //break;
                }
            }

            if (xPos == currentX + 1 && yPos == currentY - 2)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    allPieces.enemyPieceHere = true;

                    //break;
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

                    allPieces.enemyPieceHere = true;

                    //break;
                }
            }


            if (xPos == currentX + 2 && yPos == currentY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    allPieces.enemyPieceHere = true;

                    //break;
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

                    allPieces.enemyPieceHere = true;

                    //break;
                }
            }

            if (xPos == currentX - 2 && yPos == currentY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    allPieces.enemyPieceHere = true;

                    //break;
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

                    allPieces.enemyPieceHere = true;

                    //break;
                }
            }

            if (xPos == currentX + 1 && yPos == currentY + 2)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    allPieces.enemyPieceHere = true;

                    //break;
                }
            }

            #endregion

        }

        #endregion

        numberOfTimesLookingForPlayerLeft--;

        if (!horseFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {
            numberOfTimesLookingForPlayer = 1;

            currentYOfHorseList.Clear();
            currentXOfHorseList.Clear();

            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            #region Search For Player A New

            for (int i = 0; i < enemyHorseObjectList.Count; i++)
            {
                infoInt++;
                Debug.Log(infoInt + " Round");

                Debug.Log("CAll 1");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2,   enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i] ,1);
                Debug.Log("CAll 2");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2,   enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i], 2);
                Debug.Log("CAll 3");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1,   enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i], 3);
                Debug.Log("CAll 4");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1,   enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i], 4);
                Debug.Log("CAll 5");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1,   enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i], 5);
                Debug.Log("CAll 6");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1,   enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i], 6);
                Debug.Log("CAll 7");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2,   enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i], 7);
                Debug.Log("CAll 8");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2,   enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i], 8);
            }

            #endregion

        }
    }

    public void FindPlayerHorse(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY,   int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int whatPositionNumber)
    {
        //Debug.Log(posToMoveToAfterFindingPlayerX + " X");
        //Debug.Log(posToMoveToAfterFindingPlayerY + " Y");
        Debug.Log("RECIVE");
        horseFoundPlayer = false;

        currentXOfPiece = posToLookAtX;
        currentYOfPiece = posToLookAtY;

        currentXOfHorseList.Add(posToLookAtX);
        currentYOfHorseList.Add(posToLookAtY);

        #region Search For Player

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            int xPos = allPieces.xPos;
            int yPos = allPieces.yPos;

            #region Down Movment

            if (xPos == posToLookAtX - 1 && yPos == posToLookAtY - 2)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X Found");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y Found");

                    moveToLocationHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    #region What Search Was Completed

                    if (whatPositionNumber == 1)
                    {
                        firstPositionSearchingHorse++;

                        firstSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 2)
                    {
                        secondPositionSearchingHorse++;

                        secondSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 3)
                    {
                        thirdPositionSearchingHorse++;

                        thirdSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 4)
                    {
                        fourthPositionSearchingHorse++;

                        fourthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 5)
                    {
                        fifthPositionSearchingHorse++;

                        fifthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 6)
                    {
                        sixtPositionSearchingHorse++;

                        sixtSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 7)
                    {
                        seventhPositionSearchingHorse++;

                        seventhSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 8)
                    {
                        eightPositionSearchingHorse++;

                        eightSearchCompleteHorse = true;
                    }

                    #endregion

                    #region Move To Location

                    if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
                    {
                        Debug.Log("Move");

                        while (true)
                        {
                            firstPositionSearchingHorse--;
                            secondPositionSearchingHorse--;
                            thirdPositionSearchingHorse--;
                            fourthPositionSearchingHorse--;
                            fifthPositionSearchingHorse--;
                            sixtPositionSearchingHorse--;
                            seventhPositionSearchingHorse--;
                            eightPositionSearchingHorse--;

                            if (firstPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[0] && yPosToGO == moveToLocationHorseListY[0])
                                    {
                                        Debug.Log("YES 111");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (secondPositionSearchingHorse <= 0)
                            {

                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[1] && yPosToGO == moveToLocationHorseListY[1])
                                    {
                                        Debug.Log("YES 222");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (thirdPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[2] && yPosToGO == moveToLocationHorseListY[2])
                                    {
                                        Debug.Log("YES 333");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();


                                break;
                            }

                            if (fourthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[3] && yPosToGO == moveToLocationHorseListY[3])
                                    {
                                        Debug.Log("YES 444");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (fifthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[4] && yPosToGO == moveToLocationHorseListY[4])
                                    {
                                        Debug.Log("YES 555");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (sixtPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[5] && yPosToGO == moveToLocationHorseListY[5])
                                    {
                                        Debug.Log("YES 666");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (seventhPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[6] && yPosToGO == moveToLocationHorseListY[6])
                                    {
                                        Debug.Log("YES 777");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (eightPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[7] && yPosToGO == moveToLocationHorseListY[7])
                                    {
                                        Debug.Log("YES 888");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY - 2)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X Found");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y Found");

                    moveToLocationHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    #region What Search Was Completed

                    if (whatPositionNumber == 1)
                    {
                        firstPositionSearchingHorse++;

                        firstSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 2)
                    {
                        secondPositionSearchingHorse++;

                        secondSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 3)
                    {
                        thirdPositionSearchingHorse++;

                        thirdSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 4)
                    {
                        fourthPositionSearchingHorse++;

                        fourthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 5)
                    {
                        fifthPositionSearchingHorse++;

                        fifthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 6)
                    {
                        sixtPositionSearchingHorse++;

                        sixtSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 7)
                    {
                        seventhPositionSearchingHorse++;

                        seventhSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 8)
                    {
                        eightPositionSearchingHorse++;

                        eightSearchCompleteHorse = true;
                    }

                    #endregion

                    #region Move To Location

                    if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
                    {
                        Debug.Log("Move");

                        while (true)
                        {
                            firstPositionSearchingHorse--;
                            secondPositionSearchingHorse--;
                            thirdPositionSearchingHorse--;
                            fourthPositionSearchingHorse--;
                            fifthPositionSearchingHorse--;
                            sixtPositionSearchingHorse--;
                            seventhPositionSearchingHorse--;
                            eightPositionSearchingHorse--;

                            if (firstPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[0] && yPosToGO == moveToLocationHorseListY[0])
                                    {
                                        Debug.Log("YES 111");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (secondPositionSearchingHorse <= 0)
                            {

                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[1] && yPosToGO == moveToLocationHorseListY[1])
                                    {
                                        Debug.Log("YES 222");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (thirdPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[2] && yPosToGO == moveToLocationHorseListY[2])
                                    {
                                        Debug.Log("YES 333");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();


                                break;
                            }

                            if (fourthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[3] && yPosToGO == moveToLocationHorseListY[3])
                                    {
                                        Debug.Log("YES 444");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (fifthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[4] && yPosToGO == moveToLocationHorseListY[4])
                                    {
                                        Debug.Log("YES 555");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (sixtPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[5] && yPosToGO == moveToLocationHorseListY[5])
                                    {
                                        Debug.Log("YES 666");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (seventhPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[6] && yPosToGO == moveToLocationHorseListY[6])
                                    {
                                        Debug.Log("YES 777");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (eightPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[7] && yPosToGO == moveToLocationHorseListY[7])
                                    {
                                        Debug.Log("YES 888");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }

            #endregion

            #region Right Movment

            if (xPos == posToLookAtX + 2 && yPos == posToLookAtY - 1)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X Found");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y Found");

                    moveToLocationHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    #region What Search Was Completed

                    if (whatPositionNumber == 1)
                    {
                        firstPositionSearchingHorse++;

                        firstSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 2)
                    {
                        secondPositionSearchingHorse++;

                        secondSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 3)
                    {
                        thirdPositionSearchingHorse++;

                        thirdSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 4)
                    {
                        fourthPositionSearchingHorse++;

                        fourthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 5)
                    {
                        fifthPositionSearchingHorse++;

                        fifthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 6)
                    {
                        sixtPositionSearchingHorse++;

                        sixtSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 7)
                    {
                        seventhPositionSearchingHorse++;

                        seventhSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 8)
                    {
                        eightPositionSearchingHorse++;

                        eightSearchCompleteHorse = true;
                    }

                    #endregion

                    #region Move To Location

                    if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
                    {
                        Debug.Log("Move");

                        while (true)
                        {
                            firstPositionSearchingHorse--;
                            secondPositionSearchingHorse--;
                            thirdPositionSearchingHorse--;
                            fourthPositionSearchingHorse--;
                            fifthPositionSearchingHorse--;
                            sixtPositionSearchingHorse--;
                            seventhPositionSearchingHorse--;
                            eightPositionSearchingHorse--;

                            if (firstPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[0] && yPosToGO == moveToLocationHorseListY[0])
                                    {
                                        Debug.Log("YES 111");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (secondPositionSearchingHorse <= 0)
                            {

                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[1] && yPosToGO == moveToLocationHorseListY[1])
                                    {
                                        Debug.Log("YES 222");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (thirdPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[2] && yPosToGO == moveToLocationHorseListY[2])
                                    {
                                        Debug.Log("YES 333");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();


                                break;
                            }

                            if (fourthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[3] && yPosToGO == moveToLocationHorseListY[3])
                                    {
                                        Debug.Log("YES 444");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (fifthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[4] && yPosToGO == moveToLocationHorseListY[4])
                                    {
                                        Debug.Log("YES 555");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (sixtPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[5] && yPosToGO == moveToLocationHorseListY[5])
                                    {
                                        Debug.Log("YES 666");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (seventhPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[6] && yPosToGO == moveToLocationHorseListY[6])
                                    {
                                        Debug.Log("YES 777");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (eightPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[7] && yPosToGO == moveToLocationHorseListY[7])
                                    {
                                        Debug.Log("YES 888");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }


            if (xPos == posToLookAtX + 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X Found");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y Found");

                    moveToLocationHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    #region What Search Was Completed

                    if (whatPositionNumber == 1)
                    {
                        firstPositionSearchingHorse++;

                        firstSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 2)
                    {
                        secondPositionSearchingHorse++;

                        secondSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 3)
                    {
                        thirdPositionSearchingHorse++;

                        thirdSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 4)
                    {
                        fourthPositionSearchingHorse++;

                        fourthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 5)
                    {
                        fifthPositionSearchingHorse++;

                        fifthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 6)
                    {
                        sixtPositionSearchingHorse++;

                        sixtSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 7)
                    {
                        seventhPositionSearchingHorse++;

                        seventhSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 8)
                    {
                        eightPositionSearchingHorse++;

                        eightSearchCompleteHorse = true;
                    }

                    #endregion

                    #region Move To Location

                    if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
                    {
                        Debug.Log("Move");

                        while (true)
                        {
                            firstPositionSearchingHorse--;
                            secondPositionSearchingHorse--;
                            thirdPositionSearchingHorse--;
                            fourthPositionSearchingHorse--;
                            fifthPositionSearchingHorse--;
                            sixtPositionSearchingHorse--;
                            seventhPositionSearchingHorse--;
                            eightPositionSearchingHorse--;

                            if (firstPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[0] && yPosToGO == moveToLocationHorseListY[0])
                                    {
                                        Debug.Log("YES 111");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (secondPositionSearchingHorse <= 0)
                            {

                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[1] && yPosToGO == moveToLocationHorseListY[1])
                                    {
                                        Debug.Log("YES 222");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (thirdPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[2] && yPosToGO == moveToLocationHorseListY[2])
                                    {
                                        Debug.Log("YES 333");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();


                                break;
                            }

                            if (fourthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[3] && yPosToGO == moveToLocationHorseListY[3])
                                    {
                                        Debug.Log("YES 444");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (fifthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[4] && yPosToGO == moveToLocationHorseListY[4])
                                    {
                                        Debug.Log("YES 555");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (sixtPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[5] && yPosToGO == moveToLocationHorseListY[5])
                                    {
                                        Debug.Log("YES 666");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (seventhPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[6] && yPosToGO == moveToLocationHorseListY[6])
                                    {
                                        Debug.Log("YES 777");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (eightPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[7] && yPosToGO == moveToLocationHorseListY[7])
                                    {
                                        Debug.Log("YES 888");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }

            #endregion

            #region Left Movment

            if (xPos == posToLookAtX - 2 && yPos == posToLookAtY - 1)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X Found");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y Found");

                    moveToLocationHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    #region What Search Was Completed

                    if (whatPositionNumber == 1)
                    {
                        firstPositionSearchingHorse++;

                        firstSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 2)
                    {
                        secondPositionSearchingHorse++;

                        secondSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 3)
                    {
                        thirdPositionSearchingHorse++;

                        thirdSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 4)
                    {
                        fourthPositionSearchingHorse++;

                        fourthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 5)
                    {
                        fifthPositionSearchingHorse++;

                        fifthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 6)
                    {
                        sixtPositionSearchingHorse++;

                        sixtSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 7)
                    {
                        seventhPositionSearchingHorse++;

                        seventhSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 8)
                    {
                        eightPositionSearchingHorse++;

                        eightSearchCompleteHorse = true;
                    }

                    #endregion

                    #region Move To Location

                    if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
                    {
                        Debug.Log("Move");

                        while (true)
                        {
                            firstPositionSearchingHorse--;
                            secondPositionSearchingHorse--;
                            thirdPositionSearchingHorse--;
                            fourthPositionSearchingHorse--;
                            fifthPositionSearchingHorse--;
                            sixtPositionSearchingHorse--;
                            seventhPositionSearchingHorse--;
                            eightPositionSearchingHorse--;

                            if (firstPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[0] && yPosToGO == moveToLocationHorseListY[0])
                                    {
                                        Debug.Log("YES 111");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (secondPositionSearchingHorse <= 0)
                            {

                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[1] && yPosToGO == moveToLocationHorseListY[1])
                                    {
                                        Debug.Log("YES 222");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (thirdPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[2] && yPosToGO == moveToLocationHorseListY[2])
                                    {
                                        Debug.Log("YES 333");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();


                                break;
                            }

                            if (fourthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[3] && yPosToGO == moveToLocationHorseListY[3])
                                    {
                                        Debug.Log("YES 444");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (fifthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[4] && yPosToGO == moveToLocationHorseListY[4])
                                    {
                                        Debug.Log("YES 555");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (sixtPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[5] && yPosToGO == moveToLocationHorseListY[5])
                                    {
                                        Debug.Log("YES 666");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (seventhPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[6] && yPosToGO == moveToLocationHorseListY[6])
                                    {
                                        Debug.Log("YES 777");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (eightPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[7] && yPosToGO == moveToLocationHorseListY[7])
                                    {
                                        Debug.Log("YES 888");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }

            if (xPos == posToLookAtX - 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X Found");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y Found");

                    moveToLocationHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    #region What Search Was Completed

                    if (whatPositionNumber == 1)
                    {
                        firstPositionSearchingHorse++;

                        firstSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 2)
                    {
                        secondPositionSearchingHorse++;

                        secondSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 3)
                    {
                        thirdPositionSearchingHorse++;

                        thirdSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 4)
                    {
                        fourthPositionSearchingHorse++;

                        fourthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 5)
                    {
                        fifthPositionSearchingHorse++;

                        fifthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 6)
                    {
                        sixtPositionSearchingHorse++;

                        sixtSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 7)
                    {
                        seventhPositionSearchingHorse++;

                        seventhSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 8)
                    {
                        eightPositionSearchingHorse++;

                        eightSearchCompleteHorse = true;
                    }

                    #endregion

                    #region Move To Location

                    if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
                    {
                        Debug.Log("Move");

                        while (true)
                        {
                            firstPositionSearchingHorse--;
                            secondPositionSearchingHorse--;
                            thirdPositionSearchingHorse--;
                            fourthPositionSearchingHorse--;
                            fifthPositionSearchingHorse--;
                            sixtPositionSearchingHorse--;
                            seventhPositionSearchingHorse--;
                            eightPositionSearchingHorse--;

                            if (firstPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[0] && yPosToGO == moveToLocationHorseListY[0])
                                    {
                                        Debug.Log("YES 111");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (secondPositionSearchingHorse <= 0)
                            {

                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[1] && yPosToGO == moveToLocationHorseListY[1])
                                    {
                                        Debug.Log("YES 222");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (thirdPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[2] && yPosToGO == moveToLocationHorseListY[2])
                                    {
                                        Debug.Log("YES 333");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();


                                break;
                            }

                            if (fourthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[3] && yPosToGO == moveToLocationHorseListY[3])
                                    {
                                        Debug.Log("YES 444");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (fifthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[4] && yPosToGO == moveToLocationHorseListY[4])
                                    {
                                        Debug.Log("YES 555");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (sixtPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[5] && yPosToGO == moveToLocationHorseListY[5])
                                    {
                                        Debug.Log("YES 666");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (seventhPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[6] && yPosToGO == moveToLocationHorseListY[6])
                                    {
                                        Debug.Log("YES 777");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (eightPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[7] && yPosToGO == moveToLocationHorseListY[7])
                                    {
                                        Debug.Log("YES 888");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }

            #endregion

            #region Up Movment

            if (xPos == posToLookAtX - 1 && yPos == posToLookAtY + 2)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;
                        
                    pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X Found");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y Found");

                    moveToLocationHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    #region What Search Was Completed

                    if (whatPositionNumber == 1)
                    {
                        firstPositionSearchingHorse++;

                        firstSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 2)
                    {
                        secondPositionSearchingHorse++;

                        secondSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 3)
                    {
                        thirdPositionSearchingHorse++;

                        thirdSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 4)
                    {
                        fourthPositionSearchingHorse++;

                        fourthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 5)
                    {
                        fifthPositionSearchingHorse++;

                        fifthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 6)
                    {
                        sixtPositionSearchingHorse++;

                        sixtSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 7)
                    {
                        seventhPositionSearchingHorse++;

                        seventhSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 8)
                    {
                        eightPositionSearchingHorse++;

                        eightSearchCompleteHorse = true;
                    }

                    #endregion

                    #region Move To Location

                    if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
                    {
                        Debug.Log("Move");

                        while (true)
                        {
                            firstPositionSearchingHorse--;
                            secondPositionSearchingHorse--;
                            thirdPositionSearchingHorse--;
                            fourthPositionSearchingHorse--;
                            fifthPositionSearchingHorse--;
                            sixtPositionSearchingHorse--;
                            seventhPositionSearchingHorse--;
                            eightPositionSearchingHorse--;

                            if (firstPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[0] && yPosToGO == moveToLocationHorseListY[0])
                                    {
                                        Debug.Log("YES 111");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (secondPositionSearchingHorse <= 0)
                            {

                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[1] && yPosToGO == moveToLocationHorseListY[1])
                                    {
                                        Debug.Log("YES 222");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (thirdPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[2] && yPosToGO == moveToLocationHorseListY[2])
                                    {
                                        Debug.Log("YES 333");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();


                                break;
                            }

                            if (fourthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[3] && yPosToGO == moveToLocationHorseListY[3])
                                    {
                                        Debug.Log("YES 444");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (fifthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[4] && yPosToGO == moveToLocationHorseListY[4])
                                    {
                                        Debug.Log("YES 555");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (sixtPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[5] && yPosToGO == moveToLocationHorseListY[5])
                                    {
                                        Debug.Log("YES 666");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (seventhPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[6] && yPosToGO == moveToLocationHorseListY[6])
                                    {
                                        Debug.Log("YES 777");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (eightPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[7] && yPosToGO == moveToLocationHorseListY[7])
                                    {
                                        Debug.Log("YES 888");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY + 2)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X Found");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y Found");

                    moveToLocationHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    #region What Search Was Completed

                    if (whatPositionNumber == 1)
                    {
                        firstPositionSearchingHorse++;

                        firstSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 2)
                    {
                        secondPositionSearchingHorse++;

                        secondSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 3)
                    {
                        thirdPositionSearchingHorse++;

                        thirdSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 4)
                    {
                        fourthPositionSearchingHorse++;

                        fourthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 5)
                    {
                        fifthPositionSearchingHorse++;

                        fifthSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 6)
                    {
                        sixtPositionSearchingHorse++;

                        sixtSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 7)
                    {
                        seventhPositionSearchingHorse++;

                        seventhSearchCompleteHorse = true;
                    }

                    if (whatPositionNumber == 8)
                    {
                        eightPositionSearchingHorse++;

                        eightSearchCompleteHorse = true;
                    }

                    #endregion

                    #region Move To Location

                    if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
                    {
                        Debug.Log("Move");

                        while (true)
                        {
                            firstPositionSearchingHorse--;
                            secondPositionSearchingHorse--;
                            thirdPositionSearchingHorse--;
                            fourthPositionSearchingHorse--;
                            fifthPositionSearchingHorse--;
                            sixtPositionSearchingHorse--;
                            seventhPositionSearchingHorse--;
                            eightPositionSearchingHorse--;

                            if (firstPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[0] && yPosToGO == moveToLocationHorseListY[0])
                                    {
                                        Debug.Log("YES 111");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (secondPositionSearchingHorse <= 0)
                            {

                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[1] && yPosToGO == moveToLocationHorseListY[1])
                                    {
                                        Debug.Log("YES 222");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (thirdPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[2] && yPosToGO == moveToLocationHorseListY[2])
                                    {
                                        Debug.Log("YES 333");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();


                                break;
                            }

                            if (fourthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[3] && yPosToGO == moveToLocationHorseListY[3])
                                    {
                                        Debug.Log("YES 444");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (fifthPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[4] && yPosToGO == moveToLocationHorseListY[4])
                                    {
                                        Debug.Log("YES 555");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (sixtPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[5] && yPosToGO == moveToLocationHorseListY[5])
                                    {
                                        Debug.Log("YES 666");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (seventhPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[6] && yPosToGO == moveToLocationHorseListY[6])
                                    {
                                        Debug.Log("YES 777");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }

                            if (eightPositionSearchingHorse <= 0)
                            {
                                foreach (GridPiece pieceToMoveTo in gridPieces)
                                {

                                    int xPosToGo = pieceToMoveTo.xPos;
                                    int yPosToGO = pieceToMoveTo.yPos;

                                    if (xPosToGo == moveToLocationHorseListX[7] && yPosToGO == moveToLocationHorseListY[7])
                                    {
                                        Debug.Log("YES 888");

                                        pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                        pieceToMoveTo.enemyPieceHere = true;
                                    }
                                }

                                firstPositionSearchingHorse = 0;
                                secondPositionSearchingHorse = 0;
                                thirdPositionSearchingHorse = 0;
                                fourthPositionSearchingHorse = 0;
                                fifthPositionSearchingHorse = 0;
                                sixtPositionSearchingHorse = 0;
                                seventhPositionSearchingHorse = 0;
                                eightPositionSearchingHorse = 0;

                                firstSearchCompleteHorse = false;
                                secondSearchCompleteHorse = false;
                                thirdSearchCompleteHorse = false;
                                fourthSearchCompleteHorse = false;
                                fifthSearchCompleteHorse = false;
                                seventhSearchCompleteHorse = false;
                                eightSearchCompleteHorse = false;

                                moveToLocationHorseListX.Clear();
                                moveToLocationHorseListY.Clear();

                                break;
                            }
                        }
                    }

                    #endregion
                }
            }

            #endregion

        }

        #endregion

        numberOfTimesLookingForPlayerLeft--;
        //Debug.Log(numberOfTimesLookingForPlayerLeft + " Left");

        if (!horseFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {
            #region How Many Times Searched

            if(whatPositionNumber == 1)
            {
                firstPositionSearchingHorse++;
            }

            if (whatPositionNumber == 2)
            {
                secondPositionSearchingHorse++;
            }

            if (whatPositionNumber == 3)
            {
                thirdPositionSearchingHorse++;
            }

            if (whatPositionNumber == 4)
            {
                fourthPositionSearchingHorse++;
            }

            if (whatPositionNumber == 5)
            {
                fifthPositionSearchingHorse++;
            }

            if (whatPositionNumber == 6)
            {
                sixtPositionSearchingHorse++;
            }

            if (whatPositionNumber == 7)
            {
                seventhPositionSearchingHorse++;
            }

            if (whatPositionNumber == 8)
            {
                eightPositionSearchingHorse++;
            }

            #endregion

            numberOfTimesLookingForPlayer *= 8;


            currentXOfHorseListComplete.AddRange(currentXOfHorseList);
            currentYOfHorseListComplete.AddRange(currentYOfHorseList);

            currentYOfHorseList.Clear();
            currentXOfHorseList.Clear();

            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            Debug.Log(numberOfTimesLookingForPlayer + " Max");
            Debug.Log(currentXOfHorseListComplete.Count + " List");

            #region Search For Player A New

            for (int i = 0; i < currentXOfHorseListComplete.Count; i++)
            {
                if (!horseFoundPlayer)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfHorseListComplete[i] - 1, currentYOfHorseListComplete[i] - 2, pieceToMove, whatPositionNumber);
                }
                if (!horseFoundPlayer)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] + 1, currentYOfHorseListComplete[i] - 2, pieceToMove, whatPositionNumber);
                }

                if (!horseFoundPlayer)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] + 2, currentYOfHorseListComplete[i] - 1, pieceToMove, whatPositionNumber);
                }
                if (!horseFoundPlayer)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] + 2, currentYOfHorseListComplete[i] + 1, pieceToMove, whatPositionNumber);
                }

                if (!horseFoundPlayer)
                {
                FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] - 2, currentYOfHorseListComplete[i] - 1, pieceToMove, whatPositionNumber);
                }
                if (!horseFoundPlayer)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] - 2, currentYOfHorseListComplete[i] + 1, pieceToMove, whatPositionNumber);
                }

                if (!horseFoundPlayer)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] - 1, currentYOfHorseListComplete[i] + 2, pieceToMove, whatPositionNumber);
                }
                if (!horseFoundPlayer)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] + 1, currentYOfHorseListComplete[i] + 2, pieceToMove, whatPositionNumber);
                }

            }

            #endregion

        }

    }



    #endregion

    #region Delay Start

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.1f);

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece piece in gridPieces)
        {
            if (piece.enemyPieceHere)
            {
                numberOfTimesLookingForPlayer++;
                numberOfEnemys++;
            }
        }



        Debug.Log(numberOfEnemys + " Enemys");
        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;
    }

    #endregion

}
