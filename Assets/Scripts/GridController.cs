using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridPiece[] gridPieces;

    GameObject moveFromTileObject;
    GameObject moveToTileObject;
    List<GameObject> attackFromTileObjectList = new List<GameObject>();

    #region Enemy Horse movemnt 

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

    #region Player Tower

    bool foundNothing = false;
    bool foundSomething;
    bool hitSomething = false;
    int numberOfRound = 1;

    #endregion

    int infoInt = 0;

    int testInt;

    bool didntFindAnytrhingOnce = false;

    private void Start()
    {
        StartCoroutine(DelayStart());
    }

    #region Player Movment/Attack

    #region Pawn

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
                if (allPieces.enemyPieceHere == false)
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerPawn = true;

                }
            }
        }
    }

    public void AnticipatePawnAttack(int currentX, int currentY, GameObject callerGameObject)
    {
        moveFromTileObject = callerGameObject;

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            int xPos = allPieces.xPos;
            int yPos = allPieces.yPos;

            if (xPos == currentX + 1 && yPos == currentY + 1)
            {

                if(allPieces.enemyPieceHere == true)
                {
                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerPawn = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);
                }

            }

            if (xPos == currentX - 1 && yPos == currentY + 1)
            {

                if (allPieces.enemyPieceHere == true)
                {
                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerPawn = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);
                }

            }
        }

    }

    #endregion

    #region Horse

    public void AnticipateHorseMovment(int currentX, int currentY, GameObject callerGameObject)
    {
        moveFromTileObject = callerGameObject;

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            int xPos = allPieces.xPos;
            int yPos = allPieces.yPos;

            #region Upp

            if (xPos == currentX + 1 && yPos == currentY + 2)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerHorse = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerHorse = true;
                }
            }

            if (xPos == currentX - 1 && yPos == currentY + 2)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerHorse = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerHorse = true;
                }
            }

            #endregion

            #region Down

            if (xPos == currentX + 1 && yPos == currentY - 2)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerHorse = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerHorse = true;
                }
            }

            if (xPos == currentX - 1 && yPos == currentY - 2)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerHorse = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerHorse = true;
                }
            }

            #endregion

            #region Right

            if (xPos == currentX + 2 && yPos == currentY + 1)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerHorse = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerHorse = true;
                }
            }

            if (xPos == currentX + 2 && yPos == currentY - 1)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerHorse = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerHorse = true;
                }
            }

            #endregion

            #region Left

            if (xPos == currentX - 2 && yPos == currentY + 1)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerHorse = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerHorse = true;
                }
            }

            if (xPos == currentX - 2 && yPos == currentY - 1)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    allPieces.anticipatePlayerAttack = true;
                    allPieces.anticipatingPlayerHorse = true;

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.anticipatingPlayerHorse = true;
                }
            }

            #endregion
        }
    }

    #endregion

    #region Tower

    public void AnticipateTowerMovment(int currentX, int currentY, GameObject callerGameObject)
    {
        moveFromTileObject = callerGameObject;

        #region Left

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - numberOfRound && yPos == currentY)
                {
                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        foundNothing = false;

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.anticipatingPlayerHorse = true;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                        break;

                    }
                    else
                    {
                        foundSomething = true;
                        foundNothing = false;

                        allPieces.anticipateMovment = true;
                        allPieces.anticipatingPlayerHorse = true;

                        break;
                    }
                }
                else
                {
                    if (!foundSomething)
                    {
                        foundNothing = true;
                    }
                }
            }

            numberOfRound++;

            if (foundNothing)
            {
                foundSomething = false;
                foundNothing = false;
                numberOfRound = 1;

                break;
            }

        }

        #endregion

        #region Right

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + numberOfRound && yPos == currentY)
                {
                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        foundNothing = false;

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.anticipatingPlayerHorse = true;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                        break;

                    }
                    else
                    {
                        foundSomething = true;
                        foundNothing = false;

                        allPieces.anticipateMovment = true;
                        allPieces.anticipatingPlayerHorse = true;

                        break;

                    }
                }
                else
                {
                    if (!foundSomething)
                    {
                        foundNothing = true;
                    }
                }
            }

            numberOfRound++;

            if (foundNothing)
            {
                foundSomething = false;
                foundNothing = false;

                numberOfRound = 1;

                break;
            }

        }

        #endregion

        #region Down

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX && yPos == currentY - numberOfRound)
                {
                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        foundNothing = false;

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.anticipatingPlayerHorse = true;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                        break;

                    }
                    else
                    {
                        foundSomething = true;
                        foundNothing = false;

                        allPieces.anticipateMovment = true;
                        allPieces.anticipatingPlayerHorse = true;

                        break;

                    }
                }
                else
                {
                    if (!foundSomething)
                    {
                        foundNothing = true;
                    }
                }
            }

            numberOfRound++;

            if (foundNothing)
            {
                foundSomething = false;
                foundNothing = false;

                numberOfRound = 1;

                break;
            }

        }

        #endregion

        #region Up

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX && yPos == currentY + numberOfRound)
                {
                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        foundNothing = false;

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.anticipatingPlayerHorse = true;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                        break;

                    }
                    else
                    {
                        foundSomething = true;
                        foundNothing = false;

                        allPieces.anticipateMovment = true;
                        allPieces.anticipatingPlayerHorse = true;

                        break;

                    }
                }
                else
                {
                    if (!foundSomething)
                    {
                        foundNothing = true;
                    }
                }
            }

            numberOfRound++;

            if (foundNothing)
            {
                foundSomething = false;
                foundNothing = false;

                numberOfRound = 1;

                break;
            }

        }

        #endregion

    }

    #endregion

    #region General Stuff

    public void movePiece(int whatToMove)
    {
        // 0 = Pawn
        // 1 = Horse

        moveFromTileObject.GetComponent<GridPiece>().playerPieceHere = false;

        if (whatToMove == 0)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerPawnHere = false;
        }

        if(whatToMove == 1)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerHorseHere = false;
        }
    }

    public void AttackPiece(int whatToMove)
    {
        // What Piece Is Attacking
        // 0 = Pawn
        // 1 = Horse

        moveFromTileObject.GetComponent<GridPiece>().playerPieceHere = false;

        if (whatToMove == 0)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerPawnHere = false;
        }

        if(whatToMove == 1)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerHorseHere = false;
        }

    }

    #endregion

    #endregion

    #region Enemy Horse

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
                //Debug.Log(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + " X Original Pos" + enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + " Y Original Pos");

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
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i], 8);
                eightSearchCompleteHorse = true;

                MoveToLocationHorse(enemyHorseObjectList[i]);

            }

            #endregion

            #region End Reset

            foreach (GridPiece allPieces in gridPieces)
            {
                allPieces.CheckIfEnemyAttackedPlayer();

                allPieces.playerTurn = true;
            }

            enemyHorseObjectList.Clear();

            currentAmountOfTries = 0;
            numberOfTimesLookingForPlayer = 1;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            horseFoundPlayer = false;

            currentYOfHorseListComplete.Clear();
            currentXOfHorseListComplete.Clear();
            currentYOfHorseList.Clear();
            currentXOfHorseList.Clear();

            #endregion
        }
    }

    public void FindPlayerHorse(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY,   int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int whatPositionNumber)
    {

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

            #region Check First Position

            if (xPos == posToLookAtX && yPos == posToLookAtY)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            #endregion

            #region Down Movment

            if (xPos == posToLookAtX - 1 && yPos == posToLookAtY - 2)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY - 2)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

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
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }


            if (xPos == posToLookAtX + 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

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
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            if (xPos == posToLookAtX - 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

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
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY + 2)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    horseFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfHorseListComplete.Clear();
                    currentXOfHorseListComplete.Clear();
                    currentYOfHorseList.Clear();
                    currentXOfHorseList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            #endregion

        }

        #endregion

        numberOfTimesLookingForPlayerLeft--;

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

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1337);

                    didntFindAnytrhingOnce = true;
                    break;
                }

            }

            #endregion

        }

    }

    #endregion

    #region Move Enemy Horse

    void MoveToLocationHorse(GameObject pieceToMove)
    {
        if (firstSearchCompleteHorse && secondSearchCompleteHorse && thirdSearchCompleteHorse && fourthSearchCompleteHorse && fifthSearchCompleteHorse && sixtSearchCompleteHorse && seventhSearchCompleteHorse && eightSearchCompleteHorse)
        {

            while (true)
            {
                howManyTimesFirstPositionSearchedHorse--;

                howManyTimesSecondPositionSearchedHorse--;

                howManyTimesThirdPositionSearchedHorse--;

                howManyTimesFourthPositionSearchedHorse--;

                howManyTimesFifthPositionSearchedHorse--;

                howManyTimesSixtPositionSearchedHorse--;

                howManyTimesSeventhPositionSearchedHorse--;

                howManyTimesEightPositionSearchedHorse--;

                if (howManyTimesFirstPositionSearchedHorse < 0 && fifthSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {

                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[0] && yPosToGO == moveToLocationAfterHorseListY[0])
                        {

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyHorsePieceHere = false;
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyHorsePieceHere = true;
                                pieceToMoveTo.enemyPieceHere = true;


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
                            }
                            else
                            {
                                //Debug.Log("Enemy Piece Arleady There");
                            }

                            //Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            break;
                        }
                    }
                }

                if (howManyTimesSecondPositionSearchedHorse < 0 && secondSearchCompleteHorse)
                {

                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {

                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[1] && yPosToGO == moveToLocationAfterHorseListY[1])
                        {

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyHorsePieceHere = false;
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyHorsePieceHere = true;
                                pieceToMoveTo.enemyPieceHere = true;

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

                            }
                            else
                            {
                                //Debug.Log("Enemy Piece Arleady There");
                            }

                            //Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            break;
                        }
                    }
                }

                if (howManyTimesThirdPositionSearchedHorse < 0 && thirdSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {

                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[2] && yPosToGO == moveToLocationAfterHorseListY[2])
                        {

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyHorsePieceHere = false;
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyHorsePieceHere = true;
                                pieceToMoveTo.enemyPieceHere = true;

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

                            }
                            else
                            {
                                //Debug.Log("Enemy Piece Arleady There");
                            }

                            //Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            break;
                        }
                    }
                }

                if (howManyTimesFourthPositionSearchedHorse < 0 && fourthSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {

                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[3] && yPosToGO == moveToLocationAfterHorseListY[3])
                        {

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyHorsePieceHere = false;
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyHorsePieceHere = true;
                                pieceToMoveTo.enemyPieceHere = true;

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

                            }
                            else
                            {
                                //Debug.Log("Enemy Piece Arleady There");
                            }

                            //Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            break;
                        }
                    }
                }

                if (howManyTimesFifthPositionSearchedHorse < 0 && fifthSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {

                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[4] && yPosToGO == moveToLocationAfterHorseListY[4])
                        {

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyHorsePieceHere = false;
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyHorsePieceHere = true;
                                pieceToMoveTo.enemyPieceHere = true;

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

                            }
                            else
                            {
                                //Debug.Log("Enemy Piece Arleady There");
                            }

                            //Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            break;
                        }
                    }
                }

                if (howManyTimesSixtPositionSearchedHorse < 0 && sixtSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {

                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[5] && yPosToGO == moveToLocationAfterHorseListY[5])
                        {

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyHorsePieceHere = false;
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyHorsePieceHere = true;
                                pieceToMoveTo.enemyPieceHere = true;

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

                            }
                            else
                            {
                                //Debug.Log("Enemy Piece Arleady There");
                            }

                            //Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            break;
                        }
                    }
                }

                if (howManyTimesSeventhPositionSearchedHorse < 0 && seventhSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {

                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[6] && yPosToGO == moveToLocationAfterHorseListY[6])
                        {

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyHorsePieceHere = false;
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyHorsePieceHere = true;
                                pieceToMoveTo.enemyPieceHere = true;

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

                            }
                            else
                            {
                                //Debug.Log("Enemy Piece Arleady There");
                            }

                            //Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

                            break;
                        }
                    }
                }

                if (howManyTimesEightPositionSearchedHorse < 0 && eightSearchCompleteHorse)
                {
                    foreach (GridPiece pieceToMoveTo in gridPieces)
                    {

                        int xPosToGo = pieceToMoveTo.xPos;
                        int yPosToGO = pieceToMoveTo.yPos;

                        if (xPosToGo == moveToLocationAfterHorseListX[7] && yPosToGO == moveToLocationAfterHorseListY[7])
                        {

                            if (pieceToMoveTo.enemyPieceHere == false)
                            {
                                pieceToMove.GetComponent<GridPiece>().enemyHorsePieceHere = false;
                                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                                pieceToMoveTo.enemyHorsePieceHere = true;
                                pieceToMoveTo.enemyPieceHere = true;

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

                            }
                            else
                            {
                                //Debug.Log("Enemy Piece Arleady There");
                            }

                            //Debug.Log(xPosToGo + " Where I Move X " + yPosToGO + " Where I Move Y");

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

                    //Debug.Log("No Position Found");

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

            firstSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 2)
        {
            howManyTimesSecondPositionSearchedHorse += howMuchToAdd;

            secondSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 3)
        {
            howManyTimesThirdPositionSearchedHorse += howMuchToAdd;

            thirdSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 4)
        {
            howManyTimesFourthPositionSearchedHorse += howMuchToAdd;

            fourthSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 5)
        {
            howManyTimesFifthPositionSearchedHorse += howMuchToAdd;

            fifthSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 6)
        {
            howManyTimesSixtPositionSearchedHorse += howMuchToAdd;

            sixtSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 7)
        {
            howManyTimesSeventhPositionSearchedHorse += howMuchToAdd;

            seventhSearchCompleteHorse = true;
        }

        if (whatPositionNumber == 8)
        {
            howManyTimesEightPositionSearchedHorse += howMuchToAdd;

            eightSearchCompleteHorse = true;
        }

    }

    #endregion

    #endregion

    #region Delay Start

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.1f);

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece piece in gridPieces)
        {
            if (piece.enemyHorsePieceHere)
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
