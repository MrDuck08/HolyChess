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

    #region Enemy

    #region Tower

    List<GameObject> enemyTowerObjectList = new List<GameObject>();

    List<int> numberOfTriesToFindPlayerTower = new List<int>();
    List<int> yOrXMovmentList = new List<int>();
    List<int> yOrXMovmentListComplete = new List<int>();

    int towerWithTheLeastTries = 1337;
    int currentXWhereTowerIsGoingToGo = 1337;
    int numberOfRoundsContinuation = 1;

    #endregion

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

    #endregion

    #region Player Tower

    bool breakLoop = false;
    bool foundSomething;
    bool hitSomething = false;
    int numberOfRound = 1;

    #endregion

    int infoInt = 0;

    int testInt;
    bool testBool;

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
                    if (allPieces.playerPieceHere == true)
                    {
                        breakLoop = true;

                        break;
                    }

                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        breakLoop = true;

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.anticipatingPlayerTower = true;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.anticipatingPlayerTower = true;

                    }
                }
                else
                {
                    if (!foundSomething)
                    {
                        breakLoop = true;
                    }
                }
            }

            foundSomething = false;

            numberOfRound++;

            if (breakLoop)
            {
                foundSomething = false;
                breakLoop = false;

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
                    if (allPieces.playerPieceHere == true)
                    {
                        breakLoop = true;

                        break;
                    }

                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        breakLoop = true;

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.anticipatingPlayerTower = true;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.anticipatingPlayerTower = true;

                    }
                }
                else
                {
                    if (!foundSomething)
                    {
                        breakLoop = true;
                    }
                }
            }

            foundSomething = false;

            numberOfRound++;

            if (breakLoop)
            {
                foundSomething = false;
                breakLoop = false;

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
                    if (allPieces.playerPieceHere == true)
                    {
                        breakLoop = true;

                        break;
                    }

                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        breakLoop = true;

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.anticipatingPlayerTower = true;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.anticipatingPlayerTower = true;

                    }
                }
                else
                {
                    if (!foundSomething)
                    {
                        breakLoop = true;
                    }
                }
            }

            foundSomething = false;

            numberOfRound++;

            if (breakLoop)
            {
                foundSomething = false;
                breakLoop = false;

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
                    if(allPieces.playerPieceHere == true)
                    {
                        breakLoop = true;

                        break;
                    }

                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        breakLoop = true;

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.anticipatingPlayerTower = true;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                        break;

                    }

                    if(allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.anticipatingPlayerTower = true;

                    }
                }
                else
                {
                    if (!foundSomething)
                    {
                        breakLoop = true;
                    }
                }
            }

            foundSomething = false;

            numberOfRound++;

            if (breakLoop)
            {
                foundSomething = false;
                breakLoop = false;

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
        // 2 = Tower

        moveFromTileObject.GetComponent<GridPiece>().playerPieceHere = false;

        if (whatToMove == 0)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerPawnHere = false;
        }

        if(whatToMove == 1)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerHorseHere = false;
        }

        if (whatToMove == 2)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerTowerHere = false;
        }
    }

    public void AttackPiece(int whatToMove)
    {
        // What Piece Is Attacking
        // 0 = Pawn
        // 1 = Horse
        // 2 = Tower

        moveFromTileObject.GetComponent<GridPiece>().playerPieceHere = false;

        if (whatToMove == 0)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerPawnHere = false;
        }

        if(whatToMove == 1)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerHorseHere = false;
        }

        if(whatToMove == 2)
        {
            moveFromTileObject.GetComponent<GridPiece>().playerTowerHere = false;
        }

    }

    #endregion

    #endregion

    #region Enemy

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

    #region Tower

    public void EnemyTowerMovmentCall(int xPos, int yPos, GameObject calledObject)
    {
        enemyTowerObjectList.Add(calledObject);

        if (enemyTowerObjectList.Count >= numberOfEnemys)
        {
            for (int i = 0; i < enemyTowerObjectList.Count; i++)
            {
                EnemyTowerMovment(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos);
            }
        }

    }

    public void EnemyTowerMovment(int currentX, int currentY)
    {
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;

        currentXOfHorseList.Add(currentXOfPiece);
        currentYOfHorseList.Add(currentYOfPiece);

        numberOfTimesLookingForPlayerLeft--;

        if (!horseFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {
            numberOfTimesLookingForPlayer = 1;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            currentYOfHorseList.Clear();
            currentXOfHorseList.Clear();


            #region Search For Player

            for (int i = 0; i < enemyTowerObjectList.Count; i++)
            {
                #region Start Reset

                numberOfRound = 1;

                #endregion

                infoInt++;
                //Debug.Log(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + " X Original Pos" + enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + " Y Original Pos");

                //Fixa Så Att Resten Fungerar

                #region Left Search

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
                while (true)
                {
                    Debug.Log(numberOfRound + " Rounds Left");

                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == currentX - numberOfRound && yPos == currentY)
                        {

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;
                                Debug.Log("Enemy");
                                break;

                            }
                            else
                            {
                                int x = enemyTowerObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound;
                                Debug.Log("Completly New Search Left On " + x + " X " + enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + " Y");
                                FindPlayerTower(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos, enemyTowerObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos, enemyTowerObjectList[i], 0, false);
                                foundSomething = true;
                                breakLoop = false;
                            }
                        }
                        else
                        {

                            if (!foundSomething)
                            {
                                breakLoop = true;

                            }
                        }
                    }

                    foundSomething = false;

                    numberOfRound++;
                    
                    if (breakLoop)
                    {
                        foundSomething = false;
                        breakLoop = false;
                        Debug.Log("Exit");
                        numberOfRound = 1;

                        break;
                    }

                }

                #endregion

                #region Right Search

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
                while (true)
                {

                    Debug.Log(numberOfRound + " Rounds Right");
                    Debug.Log(currentX + numberOfRound + " X " + currentY + " Y Searching For");
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == currentX + numberOfRound && yPos == currentY)
                        {

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;
                                Debug.Log("Enemy");
                                break;

                            }
                            else
                            {
                                int x = enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound;
                                Debug.Log("Completly New Search Right On " + x + " X " + enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + " Y");
                                FindPlayerTower(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos, enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos, enemyTowerObjectList[i], 0, false);
                                foundSomething = true;
                                breakLoop = false;
                            }
                        }
                        else
                        {
                            if (!foundSomething)
                            {
                                breakLoop = true;
                            }
                        }
                    }

                    foundSomething = false;

                    numberOfRound++;

                    if (breakLoop)
                    {
                        foundSomething = false;
                        breakLoop = false;
                        Debug.Log("Exit");
                        numberOfRound = 1;

                        break;
                    }

                }

                #endregion

                #region Up Search

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
                while (true)
                {

                    Debug.Log(numberOfRound + " Rounds Up");
                    int y = currentY + numberOfRound;
                    Debug.Log(currentX + " X " + y + " Y Searching For");
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == currentX && yPos == currentY + numberOfRound)
                        {

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;
                                Debug.Log("Enemy");
                                break;

                            }
                            else
                            {
                                int yDebug = enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound;
                                Debug.Log("Completly New Search Up On " + enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + " X " + yDebug + " Y");
                                FindPlayerTower(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().xPos, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyTowerObjectList[i], 1, false);
                                foundSomething = true;
                                breakLoop = false;
                            }
                        }
                        else
                        {
                            if (!foundSomething)
                            {
                                breakLoop = true;
                            }
                        }
                    }

                    foundSomething = false;

                    numberOfRound++;

                    if (breakLoop)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRound = 1;
                        Debug.Log("Exit");
                        break;
                    }

                }
                fourthSearchCompleteHorse = true;

                #endregion

                #region Down Search

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
                while (true)
                {

                    Debug.Log(numberOfRound + " Rounds Down");
                    int y = currentY - numberOfRound;
                    Debug.Log(currentX + " X " + y + " Y Searching For");
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == currentX && yPos == currentY - numberOfRound)
                        {

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;
                                Debug.Log("Enemy");
                                break;

                            }
                            else
                            {
                                int yDebug = enemyTowerObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound;
                                Debug.Log("Completly New Search Down On " + enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + " X " + yDebug + " Y");
                                FindPlayerTower(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().xPos, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyTowerObjectList[i], 1, false);
                                foundSomething = true;
                                breakLoop = false;
                            }
                        }
                        else
                        {
                            if (!foundSomething)
                            {
                                breakLoop = true;
                            }
                        }
                    }

                    foundSomething = false;

                    numberOfRound++;

                    if (breakLoop)
                    {
                        foundSomething = false;
                        breakLoop = false;
                        Debug.Log("Exit");
                        numberOfRound = 1;

                        break;
                    }

                }

                #endregion

                MoveToLocationTower(enemyTowerObjectList[i]);

            }

            #endregion

            #region End Reset

            foreach (GridPiece allPieces in gridPieces)
            {
                allPieces.CheckIfEnemyAttackedPlayer();

                allPieces.playerTurn = true;
            }

            enemyTowerObjectList.Clear();

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

    #region Searching For PLayer Loop

    public void FindPlayerTower(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY, int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int yOrXMovment, bool lastMovment)
    {
        if (!lastMovment)
        {
            horseFoundPlayer = false;


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
                        Debug.Log("Found Player At " + posToLookAtX + " X " + posToLookAtY + " Y");
                        horseFoundPlayer = true;

                        // 2 För det subtraheras 1 på slutet
                        numberOfTimesLookingForPlayer = 1;
                        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                        Debug.Log(currentAmountOfTries + " Amount Of Tries Added");
                        numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                        currentAmountOfTries = 0;

                        currentYOfHorseListComplete.Clear();
                        currentXOfHorseListComplete.Clear();
                        currentYOfHorseList.Clear();
                        currentXOfHorseList.Clear();
                        yOrXMovmentListComplete.Clear();
                        yOrXMovmentList.Clear();

                        //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                        //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                        moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                        moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                    }
                }

                #endregion

            }

            if(yOrXMovment == 1)
            {

                #region Left
                Debug.Log("Left Movment");
                while (true)
                {

                    infoInt = posToLookAtX - numberOfRoundsContinuation;

                    //Debug.Log(infoInt + " x " + posToLookAtY + " y Where I Am Looking Left COntinuation");
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX - numberOfRoundsContinuation && yPos == posToLookAtY)
                        {

                            if (allPieces.playerPieceHere == true)
                            {
                                Debug.Log("Found Player Left");
                                horseFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;
                                Debug.Log(currentAmountOfTries + " Amount Of Tries Added");
                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfHorseListComplete.Clear();
                                currentXOfHorseListComplete.Clear();
                                currentYOfHorseList.Clear();
                                currentXOfHorseList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();

                                //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                                //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                                moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                                foundSomething = true;
                                breakLoop = true;

                                break;
                            }

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;


                                break;

                            }

                            if (allPieces.enemyPieceHere == false)
                            {
                                foundSomething = true;
                                breakLoop = false;

                                didntFindAnytrhingOnce = false;
                                //Debug.Log("Found Normal Piece");

                                currentXOfHorseList.Add(posToLookAtX - numberOfRoundsContinuation);
                                currentYOfHorseList.Add(posToLookAtY);

                                yOrXMovmentList.Add(1);

                            }
                        }
                        else
                        {
                            if (!foundSomething)
                            {
                                breakLoop = true;
                            }
                        }
                    }

                    foundSomething = false;

                    numberOfRoundsContinuation++;

                    if (breakLoop || horseFoundPlayer)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRoundsContinuation = 1;

                        break;
                    }

                }

                #endregion

                #region Right

                Debug.Log("Right Movment");

                while (true)
                {

                    infoInt = posToLookAtX + numberOfRoundsContinuation;

                    //Debug.Log(infoInt + " x " + posToLookAtY + " y Where I Am Looking Right COntinuation");
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX + numberOfRoundsContinuation && yPos == posToLookAtY)
                        {

                            if (allPieces.playerPieceHere == true)
                            {
                                Debug.Log("Found Player Right");
                                horseFoundPlayer = true;

                                currentAmountOfTries += 1;
                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                Debug.Log(currentAmountOfTries + " Amount Of Tries Added");
                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfHorseListComplete.Clear();
                                currentXOfHorseListComplete.Clear();
                                currentYOfHorseList.Clear();
                                currentXOfHorseList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();

                                //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                                //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                                moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                                foundSomething = true;
                                breakLoop = true;

                                break;
                            }

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;


                                break;

                            }

                            if (allPieces.enemyPieceHere == false)
                            {
                                foundSomething = true;
                                breakLoop = false;

                                didntFindAnytrhingOnce = false;

                                currentXOfHorseList.Add(posToLookAtX + numberOfRoundsContinuation);
                                currentYOfHorseList.Add(posToLookAtY);

                                yOrXMovmentList.Add(1);

                            }
                        }
                        else
                        {
                            if (!foundSomething)
                            {
                                breakLoop = true;
                            }
                        }
                    }

                    foundSomething = false;

                    numberOfRoundsContinuation++;

                    if (breakLoop || horseFoundPlayer)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRoundsContinuation = 1;

                        break;
                    }

                }

                #endregion
            }

            if (yOrXMovment == 0)
            {

                #region Down
                Debug.Log("Down Movment");
                while (true)
                {

                    infoInt = posToLookAtY - numberOfRoundsContinuation;

                    //Debug.Log(posToLookAtX + " x " + infoInt + " y Where I Am Looking Down COntinuation");
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX && yPos == posToLookAtY - numberOfRoundsContinuation)
                        {

                            if (allPieces.playerPieceHere == true)
                            {
                                Debug.Log("Found Player Down");
                                horseFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;
                                Debug.Log(currentAmountOfTries + " Amount Of Tries Added");
                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfHorseListComplete.Clear();
                                currentXOfHorseListComplete.Clear();
                                currentYOfHorseList.Clear();
                                currentXOfHorseList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();

                                //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                                //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                                moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                                foundSomething = true;
                                breakLoop = true;

                                break;
                            }

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;


                                break;

                            }

                            if (allPieces.enemyPieceHere == false)
                            {
                                foundSomething = true;
                                breakLoop = false;

                                didntFindAnytrhingOnce = false;
                                //Debug.Log("Found Normal Piece");

                                currentXOfHorseList.Add(posToLookAtX);
                                currentYOfHorseList.Add(posToLookAtY - numberOfRoundsContinuation);

                                yOrXMovmentList.Add(0);

                            }
                        }
                        else
                        {
                            if (!foundSomething)
                            {
                                breakLoop = true;
                            }
                        }
                    }

                    foundSomething = false;

                    numberOfRoundsContinuation++;

                    if (breakLoop || horseFoundPlayer)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRoundsContinuation = 1;

                        break;
                    }

                }

                #endregion

                #region Up

                Debug.Log("Up Movment");

                while (true)
                {

                    infoInt = posToLookAtY + numberOfRoundsContinuation;

                    //Debug.Log(posToLookAtX + " x " + infoInt + " y Where I Am Looking Up COntinuation");
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX && yPos == posToLookAtY + numberOfRoundsContinuation)
                        {

                            if (allPieces.playerPieceHere == true)
                            {
                                Debug.Log("Found Player Up");
                                horseFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;
                                Debug.Log(currentAmountOfTries + " Amount Of Tries Added");
                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfHorseListComplete.Clear();
                                currentXOfHorseListComplete.Clear();
                                currentYOfHorseList.Clear();
                                currentXOfHorseList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();

                                //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                                //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                                moveToLocationAfterHorseListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterHorseListY.Add(posToMoveToAfterFindingPlayerY);

                                foundSomething = true;
                                breakLoop = true;

                                break;
                            }

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;


                                break;

                            }

                            if (allPieces.enemyPieceHere == false)
                            {
                                foundSomething = true;
                                breakLoop = false;

                                didntFindAnytrhingOnce = false;
                                //Debug.Log("Found Normal Piece");

                                currentXOfHorseList.Add(posToLookAtX);
                                currentYOfHorseList.Add(posToLookAtY + numberOfRoundsContinuation);

                                yOrXMovmentList.Add(0);

                            }
                        }
                        else
                        {
                            if (!foundSomething)
                            {
                                breakLoop = true;
                            }
                        }
                    }

                    foundSomething = false;

                    numberOfRoundsContinuation++;

                    if (breakLoop || horseFoundPlayer)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRoundsContinuation = 1;

                        break;
                    }

                }

                #endregion
            }

            #endregion
        }
        else
        {
            numberOfTimesLookingForPlayer = 1;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;
        }

        //Debug.Log(numberOfTimesLookingForPlayerLeft + " NumerOfTimesLeft");

        if (numberOfTimesLookingForPlayerLeft == 1)
        {
            if (!horseFoundPlayer && currentAmountOfTries < maxNumberOfTries)
            {

                currentAmountOfTries += 1;
                Debug.Log("NEW MoVment Continuation");
                numberOfTimesLookingForPlayer = 2;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                currentXOfHorseListComplete.Clear();
                currentYOfHorseListComplete.Clear();
                yOrXMovmentListComplete.Clear();

                yOrXMovmentListComplete.AddRange(yOrXMovmentList);
                currentXOfHorseListComplete.AddRange(currentXOfHorseList);
                currentYOfHorseListComplete.AddRange(currentYOfHorseList);


                yOrXMovmentList.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();

                #region Search For Player A New
                //Debug.Log(yOrXMovmentListComplete.Count + " How Many x Y Movment");
                //Debug.Log(currentXOfHorseListComplete.Count + " How Many X Positions");
                for (int i = 0; i < currentXOfHorseListComplete.Count; i++)
                {

                    // 0 = Check Horizontal (It Was A Left Or Right Movment)
                    // 1 = Check Vertical (It Was A Up Or Down Movment)

                    if (yOrXMovmentListComplete[i] == 1 && !horseFoundPlayer)
                    {
                        Debug.Log("New Movment Vertical Continuation");

                        FindPlayerTower(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfHorseListComplete[i], currentYOfHorseListComplete[i], pieceToMove, 0, false);
                    }

                    if (!horseFoundPlayer)
                    {

                        if(yOrXMovmentListComplete[i] == 0)
                        {
                            Debug.Log("New Movment Horizontal Continuation");

                            FindPlayerTower(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfHorseListComplete[i], currentYOfHorseListComplete[i], pieceToMove, 1, false);
                        }

                    }

                    #region Old System

                    //if (yOrXMovment == 0)
                    //{
                    //    if (!horseFoundPlayer && currentAmountOfTries < maxNumberOfTries)
                    //    {
                    //        #region Up

                    //        while (true)
                    //        {
                    //            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
                    //            Debug.Log(numberOfRoundsContinuation + " Number Of Rounds Up Continuation");
                    //            foreach (GridPiece allPieces in gridPieces)
                    //            {
                    //                int xPos = allPieces.xPos;
                    //                int yPos = allPieces.yPos;

                    //                if (xPos == posToLookAtX && yPos == posToLookAtY + numberOfRoundsContinuation)
                    //                {

                    //                    if (allPieces.enemyPieceHere == true)
                    //                    {
                    //                        foundSomething = true;
                    //                        breakLoop = true;

                    //                        break;

                    //                    }
                    //                    else
                    //                    {
                    //                        foundSomething = true;
                    //                        breakLoop = false;

                    //                        if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                    //                        {

                    //                            FindPlayerTower(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfHorseListComplete[i], currentYOfHorseListComplete[i] + numberOfRoundsContinuation, pieceToMove, 1, false);

                    //                        }

                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (!foundSomething)
                    //                    {
                    //                        breakLoop = true;
                    //                    }
                    //                }
                    //            }

                    //            foundSomething = false;

                    //            numberOfRoundsContinuation++;

                    //            if (breakLoop || horseFoundPlayer)
                    //            {
                    //                foundSomething = false;
                    //                breakLoop = false;

                    //                numberOfRoundsContinuation = 1;

                    //                break;
                    //            }


                    //        }

                    //        #endregion

                    //        #region Down

                    //        while (true)
                    //        {
                    //            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
                    //            Debug.Log(numberOfRoundsContinuation + " Number Of Rounds Down Continuation");
                    //            foreach (GridPiece allPieces in gridPieces)
                    //            {
                    //                int xPos = allPieces.xPos;
                    //                int yPos = allPieces.yPos;

                    //                if (xPos == posToLookAtX && yPos == posToLookAtY - numberOfRoundsContinuation)
                    //                {

                    //                    if (allPieces.enemyPieceHere == true)
                    //                    {
                    //                        foundSomething = true;
                    //                        breakLoop = true;

                    //                        break;

                    //                    }
                    //                    else
                    //                    {
                    //                        foundSomething = true;
                    //                        breakLoop = false;

                    //                        if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                    //                        {
                    //                            FindPlayerTower(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfHorseListComplete[i], currentYOfHorseListComplete[i] - numberOfRoundsContinuation, pieceToMove, 1, false);
                    //                        }

                    //                    }

                    //                }
                    //                else
                    //                {
                    //                    if (!foundSomething)
                    //                    {
                    //                        breakLoop = true;
                    //                    }
                    //                }
                    //            }

                    //            foundSomething = false;

                    //            numberOfRoundsContinuation++;

                    //            if (breakLoop || horseFoundPlayer)
                    //            {
                    //                foundSomething = false;
                    //                breakLoop = false;

                    //                numberOfRoundsContinuation = 1;

                    //                break;
                    //            }

                    //        }

                    //        #endregion

                    //    }
                    //}

                    //if (yOrXMovment == 1)
                    //{

                    //    if (!horseFoundPlayer && currentAmountOfTries < maxNumberOfTries)
                    //    {

                    //        #region Right

                    //        while (true)
                    //        {
                    //            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
                    //            Debug.Log(numberOfRoundsContinuation + " Number Of Rounds Right Continuation");

                    //            int InfoInt;

                    //            infoInt = posToLookAtY + numberOfRoundsContinuation;

                    //            Debug.Log(posToLookAtX + " x " + infoInt + " y Where I Am Looking COntinuation");

                    //            foreach (GridPiece allPieces in gridPieces)
                    //            {
                    //                int xPos = allPieces.xPos;
                    //                int yPos = allPieces.yPos;

                    //                if (xPos == posToLookAtX && yPos == posToLookAtY + numberOfRoundsContinuation)
                    //                {
                    //                    Debug.Log("HEllo 2");
                    //                    if (allPieces.enemyPieceHere == true)
                    //                    {
                    //                        foundSomething = true;
                    //                        breakLoop = true;

                    //                        break;

                    //                    }
                    //                    else
                    //                    {
                    //                        foundSomething = true;
                    //                        breakLoop = false;

                    //                        if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                    //                        {
                    //                            Debug.Log("Searching Right Continuation");
                    //                            FindPlayerTower(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfHorseListComplete[i] + numberOfRoundsContinuation, currentYOfHorseListComplete[i], pieceToMove, 0, false);

                    //                        }

                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (!foundSomething)
                    //                    {
                    //                        breakLoop = true;
                    //                    }
                    //                }
                    //            }

                    //            foundSomething = false;

                    //            numberOfRoundsContinuation++;

                    //            if (breakLoop || horseFoundPlayer)
                    //            {
                    //                foundSomething = false;
                    //                breakLoop = false;

                    //                numberOfRoundsContinuation = 1;

                    //                break;
                    //            }


                    //        }

                    //        #endregion

                    //        #region Left

                    //        while (true)
                    //        {
                    //            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
                    //            Debug.Log(numberOfRoundsContinuation + " Number Of Rounds Left Continuation");
                    //            int InfoInt;

                    //            infoInt = posToLookAtY - numberOfRoundsContinuation;

                    //            Debug.Log(posToLookAtX + " x " + infoInt + " y Where I Am Looking COntinuation");
                    //            foreach (GridPiece allPieces in gridPieces)
                    //            {
                    //                int xPos = allPieces.xPos;
                    //                int yPos = allPieces.yPos;


                    //                if (xPos == posToLookAtX && yPos == posToLookAtY - numberOfRoundsContinuation)
                    //                {
                    //                    Debug.Log("HEllo");
                    //                    if (allPieces.enemyPieceHere == true)
                    //                    {
                    //                        foundSomething = true;
                    //                        breakLoop = true;

                    //                        break;

                    //                    }
                    //                    else
                    //                    {
                    //                        foundSomething = true;
                    //                        breakLoop = false;

                    //                        if (!horseFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                    //                        {
                    //                            Debug.Log("Searching Left Continuation");
                    //                            FindPlayerTower(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfHorseListComplete[i] - numberOfRoundsContinuation, currentYOfHorseListComplete[i], pieceToMove, 1, false);
                    //                        }

                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    if (!foundSomething)
                    //                    {
                    //                        breakLoop = true;
                    //                    }
                    //                }
                    //            }

                    //            foundSomething = false;

                    //            numberOfRoundsContinuation++;

                    //            if (breakLoop || horseFoundPlayer)
                    //            {
                    //                foundSomething = false;
                    //                breakLoop = false;

                    //                numberOfRoundsContinuation = 1;

                    //                break;
                    //            }

                    //        }

                    //        #endregion

                    //    }

                    //}

                    #endregion

                }
                if (!horseFoundPlayer && currentAmountOfTries < maxNumberOfTries)
                {
                    Debug.Log("Done Searching Onto Next Continuation");
                    //// Spelar Ingen Roll Vad Jag Sätter In
                    //if (yOrXMovment == 0)
                    //{
                    //    FindPlayerTower(1337, 1337, 1337, 1337, pieceToMove, 1, true);
                    //}
                    //if (yOrXMovment == 1)
                    //{
                    //    FindPlayerTower(1337, 1337, 1337, 1337, pieceToMove, 0, true);
                    //}
                }
                #endregion

            }
        }
        //else
        //{
        //    numberOfTriesToFindPlayerTower.Add(1337);

        //    foundSomething = true;
        //    breakLoop = false;
        //    horseFoundPlayer = false;

        //    numberOfTimesLookingForPlayer = 1;
        //    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

        //    currentYOfHorseListComplete.Clear();
        //    currentXOfHorseListComplete.Clear();
        //    currentYOfHorseList.Clear();
        //    currentXOfHorseList.Clear();
        //}

    }

    #endregion

    #region Move Tower

    void MoveToLocationTower(GameObject pieceToMove)
    {
        Debug.Log(numberOfTriesToFindPlayerTower.Count + " Count");
        for (int i = 0; i < numberOfTriesToFindPlayerTower.Count; i++)
        {
            Debug.Log(numberOfTriesToFindPlayerTower[i] + " Amout Of Tries");

            if (numberOfTriesToFindPlayerTower[i] < currentXWhereTowerIsGoingToGo)
            {

                foreach (GridPiece pieceToMoveTo in gridPieces)
                {

                    int xPosToGo = pieceToMoveTo.xPos;
                    int yPosToGO = pieceToMoveTo.yPos;

                    if (xPosToGo == moveToLocationAfterHorseListX[i] && yPosToGO == moveToLocationAfterHorseListY[i])
                    {

                        if (pieceToMoveTo.enemyPieceHere == false)
                        {


                            //Kollar Om Hur Försöken Är Hägre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayerTower[i];
                            //När Man Väl Ska Flytta Spelaren Så Behöver Jag Veta Vilken i Den Var På
                            towerWithTheLeastTries = i;
                            Debug.Log(towerWithTheLeastTries + "What i To Move To");

                        }
                        else
                        {
                            Debug.Log("Enemy Piece Arleady There");
                        }

                    }
                }
            }

            numberOftimesLookingForPositionHorse++;
        }


        Debug.Log(towerWithTheLeastTries + " What i");
        Debug.Log(currentXWhereTowerIsGoingToGo + " Number Of Tries");
        Debug.Log(moveToLocationAfterHorseListX[towerWithTheLeastTries] + " x " + moveToLocationAfterHorseListY[towerWithTheLeastTries] + " y Where To Move To");
        foreach (GridPiece pieceToMoveTo in gridPieces)
        {

            int xPosToGo = pieceToMoveTo.xPos;
            int yPosToGO = pieceToMoveTo.yPos;

            if (xPosToGo == moveToLocationAfterHorseListX[towerWithTheLeastTries] && yPosToGO == moveToLocationAfterHorseListY[towerWithTheLeastTries])
            {
                // Glöm Inte Byta Till Tower
                pieceToMove.GetComponent<GridPiece>().enemyTowerPieceHere = false;
                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                pieceToMoveTo.enemyTowerPieceHere = true;
                pieceToMoveTo.enemyPieceHere = true;

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;
                Debug.Log("MOVE");
                numberOfTriesToFindPlayerTower.Clear();

                moveToLocationAfterHorseListX.Clear();
                moveToLocationAfterHorseListY.Clear();

                currentXOfHorseListComplete.Clear();
                currentYOfHorseListComplete.Clear();
                yOrXMovmentListComplete.Clear();

                yOrXMovmentList.Clear();
                currentYOfHorseList.Clear();
                currentXOfHorseList.Clear();

                break;
            }
        }
    }

    #endregion

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

            if (piece.enemyTowerPieceHere)
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
