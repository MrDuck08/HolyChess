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
    int maxNumberOfTries = 7;
    int currentAmountOfTries;
    int numberOftimesLookingForPositionHorse = 0;
    int maxNumberOfTimesLookingForPositionHorse = 6;

    int currentXOfPiece;
    int currentYOfPiece;
    int numberOfEnemys;

    int howManyTimesFirstPositionSearchedHorse = 1337;
    int howManyTimesSecondPositionSearchedHorse = 1337;
    int howManyTimesThirdPositionSearchedHorse = 1337;
    int howManyTimesFourthPositionSearchedHorse = 1337;
    int howManyTimesFifthPositionSearchedHorse = 1337;
    int howManyTimesSixtPositionSearchedHorse = 1337;
    int howManyTimesSeventhPositionSearchedHorse = 1337;
    int howManyTimesEightPositionSearchedHorse = 1337;

    List<int> currentXOfHorseList = new List<int>();
    List<int> currentYOfHorseList = new List<int>();

    List<int> currentXOfHorseListComplete = new List<int>();
    List<int> currentYOfHorseListComplete = new List<int>();

    List<int> moveToLocationAfterHorseListX = new List<int>();
    List<int> moveToLocationAfterHorseListY = new List<int>();

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

    int testInt;

    bool didntFindAnytrhingOnce = false;

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
                #region Start Reset

                howManyTimesFirstPositionSearchedHorse = 0;
                howManyTimesSecondPositionSearchedHorse = 0;
                howManyTimesThirdPositionSearchedHorse = 0;
                howManyTimesFourthPositionSearchedHorse = 0;
                howManyTimesFifthPositionSearchedHorse = 0;
                howManyTimesSixtPositionSearchedHorse = 0;
                howManyTimesSeventhPositionSearchedHorse = 0;
                howManyTimesEightPositionSearchedHorse = 0;

                #endregion

                infoInt++;
                Debug.Log(infoInt + " Round");
                Debug.Log(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + " X Original Pos" + enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + " Y Original Pos");

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                horseFoundPlayer = false;

                currentYOfHorseListComplete.Clear();
                currentXOfHorseListComplete.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();
                #endregion
                //Debug.Log("CAll 1");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i], 1);
                firstSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                horseFoundPlayer = false;

                currentYOfHorseListComplete.Clear();
                currentXOfHorseListComplete.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();
                #endregion
                //Debug.Log("CAll 2");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i], 2);
                secondSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                horseFoundPlayer = false;

                currentYOfHorseListComplete.Clear();
                currentXOfHorseListComplete.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();
                #endregion
                //Debug.Log("CAll 3");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i], 3);
                thirdSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                horseFoundPlayer = false;

                currentYOfHorseListComplete.Clear();
                currentXOfHorseListComplete.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();
                #endregion
                //Debug.Log("CAll 4");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i], 4);
                fourthSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                horseFoundPlayer = false;

                currentYOfHorseListComplete.Clear();
                currentXOfHorseListComplete.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();
                #endregion
                //Debug.Log("CAll 5");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i], 5);
                fifthSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                horseFoundPlayer = false;

                currentYOfHorseListComplete.Clear();
                currentXOfHorseListComplete.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();
                #endregion
                //Debug.Log("CAll 6");          
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i], 6);
                sixtSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                horseFoundPlayer = false;

                currentYOfHorseListComplete.Clear();
                currentXOfHorseListComplete.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();
                #endregion
                //Debug.Log("CAll 7");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i], 7);
                seventhSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                horseFoundPlayer = false;

                currentYOfHorseListComplete.Clear();
                currentXOfHorseListComplete.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();
                #endregion
                //Debug.Log("CAll 8");
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i], 8);
                eightSearchCompleteHorse = true;

                MoveToLocationHorse(enemyHorseObjectList[i]);

            }

            #endregion

        }
    }

    public void FindPlayerHorse(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY,   int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int whatPositionNumber)
    {
        //Debug.Log(posToMoveToAfterFindingPlayerX + " X");
        //Debug.Log(posToMoveToAfterFindingPlayerY + " Y");
        //Debug.Log("RECIVE");
        horseFoundPlayer = false;
        didntFindAnytrhingOnce = false;

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

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
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

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
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

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }


            if (xPos == posToLookAtX + 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
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

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            if (xPos == posToLookAtX - 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
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

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY + 2)
            {
                if (allPieces.playerPieceHere)
                {
                    Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            #endregion

        }

        #endregion

        numberOfTimesLookingForPlayerLeft--;
        //Debug.Log(numberOfTimesLookingForPlayerLeft + " Left");

        if (!horseFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {

            currentAmountOfTries += 2;

            numberOfTimesLookingForPlayer *= 8;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            currentXOfHorseListComplete.Clear();
            currentYOfHorseListComplete.Clear();

            currentXOfHorseListComplete.AddRange(currentXOfHorseList);
            currentYOfHorseListComplete.AddRange(currentYOfHorseList);

            currentYOfHorseList.Clear();
            currentXOfHorseList.Clear();

            WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);

            //Debug.Log(numberOfTimesLookingForPlayer + " Max");
            //Debug.Log(currentXOfHorseListComplete.Count + " List");

            #region Search For Player A New

            for (int i = 0; i < currentXOfHorseListComplete.Count; i++)
            {
                if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] - 1, currentYOfHorseListComplete[i] - 2, pieceToMove, whatPositionNumber);
                }
                if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] + 1, currentYOfHorseListComplete[i] - 2, pieceToMove, whatPositionNumber);
                }

                if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] + 2, currentYOfHorseListComplete[i] - 1, pieceToMove, whatPositionNumber);
                }
                if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] + 2, currentYOfHorseListComplete[i] + 1, pieceToMove, whatPositionNumber);
                }

                if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] - 2, currentYOfHorseListComplete[i] - 1, pieceToMove, whatPositionNumber);
                }
                if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] - 2, currentYOfHorseListComplete[i] + 1, pieceToMove, whatPositionNumber);
                }

                if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] - 1, currentYOfHorseListComplete[i] + 2, pieceToMove, whatPositionNumber);
                }
                if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfHorseListComplete[i] + 1, currentYOfHorseListComplete[i] + 2, pieceToMove, whatPositionNumber);
                }

                if (currentAmountOfTries >= maxNumberOfTries && !didntFindAnytrhingOnce && !horseFoundPlayer)
                {
                    moveToLocationAfterHorseListX.Add(1337);
                    moveToLocationAfterHorseListY.Add(1337);
                    //Debug.Log("NU Uh");

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1337);

                    didntFindAnytrhingOnce = true;
                    break;
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

    #region Move Enemy Horse

    void MoveToLocationHorse(GameObject pieceToMove)
    {
        if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
        {
            Debug.Log("Move");

            while (true)
            {
                howManyTimesFirstPositionSearchedHorse--;
                //Debug.Log(howManyTimesFirstPositionSearchedHorse + " First Position");

                howManyTimesSecondPositionSearchedHorse--;
                //Debug.Log(howManyTimesSecondPositionSearchedHorse + " Second Position");

                howManyTimesThirdPositionSearchedHorse--;
                //Debug.Log(howManyTimesThirdPositionSearchedHorse + " Third Position");

                howManyTimesFourthPositionSearchedHorse--;
                //Debug.Log(howManyTimesFourthPositionSearchedHorse + " Fourth Position");

                howManyTimesFifthPositionSearchedHorse--;
                //Debug.Log(howManyTimesFifthPositionSearchedHorse + " Fifth Position");

                howManyTimesSixtPositionSearchedHorse--;
                //Debug.Log(howManyTimesSixtPositionSearchedHorse + " Sixt Position");

                howManyTimesSeventhPositionSearchedHorse--;
                //Debug.Log(howManyTimesSeventhPositionSearchedHorse + " Seventh Position");

                howManyTimesEightPositionSearchedHorse--;
                //Debug.Log(howManyTimesEightPositionSearchedHorse + " Eight Position");

                if (howManyTimesFirstPositionSearchedHorse < 0 && fifthSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {
                        //Debug.Log("Seathcing For Position 1");
                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[0] && yPosToGO == moveToLocationAfterHorseListY[0])
                        {
                            //Debug.Log("YES 111");

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyPieceHere = true;
                            }
                            else
                            {
                                Debug.Log("Enemy Piece Arleady There");
                            }

                            Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            howManyTimesFirstPositionSearchedHorse = 1337;
                            howManyTimesSecondPositionSearchedHorse = 1337;
                            howManyTimesThirdPositionSearchedHorse = 1337;
                            howManyTimesFourthPositionSearchedHorse = 1337;
                            howManyTimesFifthPositionSearchedHorse = 1337;
                            howManyTimesSixtPositionSearchedHorse = 1337;
                            howManyTimesSeventhPositionSearchedHorse = 1337;
                            howManyTimesEightPositionSearchedHorse = 1337;

                            firstSearchCompleteHorse = false;
                            secondSearchCompleteHorse = false;
                            thirdSearchCompleteHorse = false;
                            fourthSearchCompleteHorse = false;
                            fifthSearchCompleteHorse = false;
                            seventhSearchCompleteHorse = false;
                            eightSearchCompleteHorse = false;

                            moveToLocationAfterHorseListX.Clear();
                            moveToLocationAfterHorseListY.Clear();

                            break;
                        }
                    }
                }

                if (howManyTimesSecondPositionSearchedHorse < 0 && secondSearchCompleteHorse)
                {

                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {
                        //Debug.Log("Seathcing For Position 2");
                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[1] && yPosToGO == moveToLocationAfterHorseListY[1])
                        {
                            Debug.Log("YES 222");

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyPieceHere = true;

                            }
                            else
                            {
                                Debug.Log("Enemy Piece Arleady There");
                            }

                            Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            howManyTimesFirstPositionSearchedHorse = 1337;
                            howManyTimesSecondPositionSearchedHorse = 1337;
                            howManyTimesThirdPositionSearchedHorse = 1337;
                            howManyTimesFourthPositionSearchedHorse = 1337;
                            howManyTimesFifthPositionSearchedHorse = 1337;
                            howManyTimesSixtPositionSearchedHorse = 1337;
                            howManyTimesSeventhPositionSearchedHorse = 1337;
                            howManyTimesEightPositionSearchedHorse = 1337;

                            firstSearchCompleteHorse = false;
                            secondSearchCompleteHorse = false;
                            thirdSearchCompleteHorse = false;
                            fourthSearchCompleteHorse = false;
                            fifthSearchCompleteHorse = false;
                            seventhSearchCompleteHorse = false;
                            eightSearchCompleteHorse = false;

                            moveToLocationAfterHorseListX.Clear();
                            moveToLocationAfterHorseListY.Clear();

                            break;
                        }
                    }
                }

                if (howManyTimesThirdPositionSearchedHorse < 0 && thirdSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {
                        //Debug.Log("Seathcing For Position 3");
                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[2] && yPosToGO == moveToLocationAfterHorseListY[2])
                        {
                            Debug.Log("YES 333");

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyPieceHere = true;

                            }
                            else
                            {
                                Debug.Log("Enemy Piece Arleady There");
                            }

                            Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            howManyTimesFirstPositionSearchedHorse = 1337;
                            howManyTimesSecondPositionSearchedHorse = 1337;
                            howManyTimesThirdPositionSearchedHorse = 1337;
                            howManyTimesFourthPositionSearchedHorse = 1337;
                            howManyTimesFifthPositionSearchedHorse = 1337;
                            howManyTimesSixtPositionSearchedHorse = 1337;
                            howManyTimesSeventhPositionSearchedHorse = 1337;
                            howManyTimesEightPositionSearchedHorse = 1337;

                            firstSearchCompleteHorse = false;
                            secondSearchCompleteHorse = false;
                            thirdSearchCompleteHorse = false;
                            fourthSearchCompleteHorse = false;
                            fifthSearchCompleteHorse = false;
                            seventhSearchCompleteHorse = false;
                            eightSearchCompleteHorse = false;

                            moveToLocationAfterHorseListX.Clear();
                            moveToLocationAfterHorseListY.Clear();

                            break;
                        }
                    }
                }

                if (howManyTimesFourthPositionSearchedHorse < 0 && fourthSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {
                        //Debug.Log("Seathcing For Position 4");
                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[3] && yPosToGO == moveToLocationAfterHorseListY[3])
                        {
                            Debug.Log("YES 444");

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyPieceHere = true;

                            }
                            else
                            {
                                Debug.Log("Enemy Piece Arleady There");
                            }

                            Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            howManyTimesFirstPositionSearchedHorse = 1337;
                            howManyTimesSecondPositionSearchedHorse = 1337;
                            howManyTimesThirdPositionSearchedHorse = 1337;
                            howManyTimesFourthPositionSearchedHorse = 1337;
                            howManyTimesFifthPositionSearchedHorse = 1337;
                            howManyTimesSixtPositionSearchedHorse = 1337;
                            howManyTimesSeventhPositionSearchedHorse = 1337;
                            howManyTimesEightPositionSearchedHorse = 1337;

                            firstSearchCompleteHorse = false;
                            secondSearchCompleteHorse = false;
                            thirdSearchCompleteHorse = false;
                            fourthSearchCompleteHorse = false;
                            fifthSearchCompleteHorse = false;
                            seventhSearchCompleteHorse = false;
                            eightSearchCompleteHorse = false;

                            moveToLocationAfterHorseListX.Clear();
                            moveToLocationAfterHorseListY.Clear();

                            break;
                        }
                    }
                }

                if (howManyTimesFifthPositionSearchedHorse < 0 && fifthSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {
                        //Debug.Log("Seathcing For Position 5");
                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[4] && yPosToGO == moveToLocationAfterHorseListY[4])
                        {
                            Debug.Log("YES 555");

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyPieceHere = true;

                            }
                            else
                            {
                                Debug.Log("Enemy Piece Arleady There");
                            }

                            Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            howManyTimesFirstPositionSearchedHorse = 1337;
                            howManyTimesSecondPositionSearchedHorse = 1337;
                            howManyTimesThirdPositionSearchedHorse = 1337;
                            howManyTimesFourthPositionSearchedHorse = 1337;
                            howManyTimesFifthPositionSearchedHorse = 1337;
                            howManyTimesSixtPositionSearchedHorse = 1337;
                            howManyTimesSeventhPositionSearchedHorse = 1337;
                            howManyTimesEightPositionSearchedHorse = 1337;

                            firstSearchCompleteHorse = false;
                            secondSearchCompleteHorse = false;
                            thirdSearchCompleteHorse = false;
                            fourthSearchCompleteHorse = false;
                            fifthSearchCompleteHorse = false;
                            seventhSearchCompleteHorse = false;
                            eightSearchCompleteHorse = false;

                            moveToLocationAfterHorseListX.Clear();
                            moveToLocationAfterHorseListY.Clear();

                            break;
                        }
                    }
                }

                if (howManyTimesSixtPositionSearchedHorse < 0 && sixtSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {
                        //Debug.Log("Seathcing For Position 6");
                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[5] && yPosToGO == moveToLocationAfterHorseListY[5])
                        {
                            Debug.Log("YES 666");

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyPieceHere = true;

                            }
                            else
                            {
                                Debug.Log("Enemy Piece Arleady There");
                            }

                            Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            howManyTimesFirstPositionSearchedHorse = 1337;
                            howManyTimesSecondPositionSearchedHorse = 1337;
                            howManyTimesThirdPositionSearchedHorse = 1337;
                            howManyTimesFourthPositionSearchedHorse = 1337;
                            howManyTimesFifthPositionSearchedHorse = 1337;
                            howManyTimesSixtPositionSearchedHorse = 1337;
                            howManyTimesSeventhPositionSearchedHorse = 1337;
                            howManyTimesEightPositionSearchedHorse = 1337;

                            firstSearchCompleteHorse = false;
                            secondSearchCompleteHorse = false;
                            thirdSearchCompleteHorse = false;
                            fourthSearchCompleteHorse = false;
                            fifthSearchCompleteHorse = false;
                            seventhSearchCompleteHorse = false;
                            eightSearchCompleteHorse = false;

                            moveToLocationAfterHorseListX.Clear();
                            moveToLocationAfterHorseListY.Clear();

                            break;
                        }
                    }
                }

                if (howManyTimesSeventhPositionSearchedHorse < 0 && seventhSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {
                        //Debug.Log("Seathcing For Position 7");
                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[6] && yPosToGO == moveToLocationAfterHorseListY[6])
                        {
                            Debug.Log("YES 777");

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyPieceHere = true;

                            }
                            else
                            {
                                Debug.Log("Enemy Piece Arleady There");
                            }

                            Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            howManyTimesFirstPositionSearchedHorse = 1337;
                            howManyTimesSecondPositionSearchedHorse = 1337;
                            howManyTimesThirdPositionSearchedHorse = 1337;
                            howManyTimesFourthPositionSearchedHorse = 1337;
                            howManyTimesFifthPositionSearchedHorse = 1337;
                            howManyTimesSixtPositionSearchedHorse = 1337;
                            howManyTimesSeventhPositionSearchedHorse = 1337;
                            howManyTimesEightPositionSearchedHorse = 1337;

                            firstSearchCompleteHorse = false;
                            secondSearchCompleteHorse = false;
                            thirdSearchCompleteHorse = false;
                            fourthSearchCompleteHorse = false;
                            fifthSearchCompleteHorse = false;
                            seventhSearchCompleteHorse = false;
                            eightSearchCompleteHorse = false;

                            moveToLocationAfterHorseListX.Clear();
                            moveToLocationAfterHorseListY.Clear();

                            break;
                        }
                    }
                }

                if (howManyTimesEightPositionSearchedHorse < 0 && eightSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {
                        //Debug.Log("Seathcing For Position 8");
                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[7] && yPosToGO == moveToLocationAfterHorseListY[7])
                        {
                            Debug.Log("YES 888");

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyPieceHere = true;

                            }
                            else
                            {
                                Debug.Log("Enemy Piece Arleady There");
                            }

                            Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            howManyTimesFirstPositionSearchedHorse = 1337;
                            howManyTimesSecondPositionSearchedHorse = 1337;
                            howManyTimesThirdPositionSearchedHorse = 1337;
                            howManyTimesFourthPositionSearchedHorse = 1337;
                            howManyTimesFifthPositionSearchedHorse = 1337;
                            howManyTimesSixtPositionSearchedHorse = 1337;
                            howManyTimesSeventhPositionSearchedHorse = 1337;
                            howManyTimesEightPositionSearchedHorse = 1337;

                            firstSearchCompleteHorse = false;
                            secondSearchCompleteHorse = false;
                            thirdSearchCompleteHorse = false;
                            fourthSearchCompleteHorse = false;
                            fifthSearchCompleteHorse = false;
                            seventhSearchCompleteHorse = false;
                            eightSearchCompleteHorse = false;

                            moveToLocationAfterHorseListX.Clear();
                            moveToLocationAfterHorseListY.Clear();

                            break;
                        }
                    }
                }

                if (numberOftimesLookingForPositionHorse >= maxNumberOfTimesLookingForPositionHorse)
                {
                    numberOftimesLookingForPositionHorse = 0;

                    howManyTimesFirstPositionSearchedHorse = 1337;
                    howManyTimesSecondPositionSearchedHorse = 1337;
                    howManyTimesThirdPositionSearchedHorse = 1337;
                    howManyTimesFourthPositionSearchedHorse = 1337;
                    howManyTimesFifthPositionSearchedHorse = 1337;
                    howManyTimesSixtPositionSearchedHorse = 1337;
                    howManyTimesSeventhPositionSearchedHorse = 1337;
                    howManyTimesEightPositionSearchedHorse = 1337;

                    firstSearchCompleteHorse = false;
                    secondSearchCompleteHorse = false;
                    thirdSearchCompleteHorse = false;
                    fourthSearchCompleteHorse = false;
                    fifthSearchCompleteHorse = false;
                    seventhSearchCompleteHorse = false;
                    eightSearchCompleteHorse = false;

                    moveToLocationAfterHorseListX.Clear();
                    moveToLocationAfterHorseListY.Clear();

                    Debug.Log("No Position Found");

                    break;
                }

                numberOftimesLookingForPositionHorse++;
            }
        }
    }

    #endregion

    #region What Search Was Completed Enemy Horse

    void WhatSearchCompletedEnemyHorse(int whatPositionNumber, int howMuchToAdd)
    {

        if (whatPositionNumber == 1)
        {
            howManyTimesFirstPositionSearchedHorse += howMuchToAdd;

            //Debug.Log(howManyTimesFirstPositionSearchedHorse + " How Many Times Searched  " + whatPositionNumber + " What Search");

            firstSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 2)
        {
            howManyTimesSecondPositionSearchedHorse += howMuchToAdd;

            //Debug.Log(howManyTimesSecondPositionSearchedHorse + " How Many Times Searched  " + whatPositionNumber + " What Search");

            secondSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 3)
        {
            howManyTimesThirdPositionSearchedHorse += howMuchToAdd;

            //Debug.Log(howManyTimesThirdPositionSearchedHorse + " How Many Times Searched  " + whatPositionNumber + " What Search");

            thirdSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 4)
        {
            howManyTimesFourthPositionSearchedHorse += howMuchToAdd;

            //Debug.Log(howManyTimesFourthPositionSearchedHorse + " How Many Times Searched  " + whatPositionNumber + " What Search");

            fourthSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 5)
        {
            howManyTimesFifthPositionSearchedHorse += howMuchToAdd;

            //Debug.Log(howManyTimesFifthPositionSearchedHorse + " How Many Times Searched  " + whatPositionNumber + " What Search");

            fifthSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 6)
        {
            howManyTimesSixtPositionSearchedHorse += howMuchToAdd;

            //Debug.Log(howManyTimesSixtPositionSearchedHorse + " How Many Times Searched  " + whatPositionNumber + " What Search");

            sixtSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 7)
        {
            howManyTimesSeventhPositionSearchedHorse += howMuchToAdd;

            //Debug.Log(howManyTimesSeventhPositionSearchedHorse + " How Many Times Searched  " + whatPositionNumber + " What Search");

            seventhSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 8)
        {
            howManyTimesEightPositionSearchedHorse += howMuchToAdd;

            //Debug.Log(howManyTimesEightPositionSearchedHorse + " How Many Times Searched  " + whatPositionNumber + " What Search");

            eightSearchCompleteHorse = true;
        }

    }

    #endregion
}
