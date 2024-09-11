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

    #region Bishop

    List<GameObject> enemyBishopObjectList = new List<GameObject>();
    int maxNumberOfTriesBishop = 2;

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

    List<int> currentXOfEnemyList = new List<int>();
    List<int> currentYOfEnemyList = new List<int>();

    List<int> currentXOfEnemyListComplete = new List<int>();
    List<int> currentYOfenemyListComplete = new List<int>();

    List<int> moveToLocationAfterEnemyListX = new List<int>();
    List<int> moveToLocationAfterEnemyListY = new List<int>();

    bool enemyFoundPlayer = false;

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

        currentXOfEnemyList.Add(currentXOfPiece);
        currentYOfEnemyList.Add(currentYOfPiece);

        numberOfTimesLookingForPlayerLeft--;

        if (!enemyFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {
            numberOfTimesLookingForPlayer = 1;

            currentYOfEnemyList.Clear();
            currentXOfEnemyList.Clear();

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

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i], 1);
                firstSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i], 2);
                secondSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i], 3);
                thirdSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i], 4);
                fourthSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i], 5);
                fifthSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion     
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i], 6);
                sixtSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                FindPlayerHorse(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i], 7);
                seventhSearchCompleteHorse = true;

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
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

            enemyFoundPlayer = false;

            currentYOfenemyListComplete.Clear();
            currentXOfEnemyListComplete.Clear();
            currentYOfEnemyList.Clear();
            currentXOfEnemyList.Clear();

            #endregion
        }
    }

    public void FindPlayerHorse(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY,   int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int whatPositionNumber)
    {

        enemyFoundPlayer = false;
        didntFindAnytrhingOnce = false;

        currentXOfEnemyList.Add(posToLookAtX);
        currentYOfEnemyList.Add(posToLookAtY);

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
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY - 2)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }


            if (xPos == posToLookAtX + 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            if (xPos == posToLookAtX - 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY + 2)
            {
                if (allPieces.playerPieceHere)
                {
                    //Debug.Log("Found Player");
                    enemyFoundPlayer = true;

                    // 2 För det subtraheras 1 på slutet
                    numberOfTimesLookingForPlayer = 2;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    //Debug.Log(posToMoveToAfterFindingPlayerX + " X To Move To");
                    //Debug.Log(posToMoveToAfterFindingPlayerY + " Y To Move To");

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

                    WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);
                }
            }

            #endregion

        }

        #endregion

        numberOfTimesLookingForPlayerLeft--;

        if (!enemyFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {

            currentAmountOfTries += 2;

            numberOfTimesLookingForPlayer *= 8;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            currentXOfEnemyListComplete.Clear();
            currentYOfenemyListComplete.Clear();

            currentXOfEnemyListComplete.AddRange(currentXOfEnemyList);
            currentYOfenemyListComplete.AddRange(currentYOfEnemyList);

            currentYOfEnemyList.Clear();
            currentXOfEnemyList.Clear();

            WhatSearchCompletedEnemyHorse(whatPositionNumber, 1);

            #region Search For Player A New

            for (int i = 0; i < currentXOfEnemyListComplete.Count; i++)
            {
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 1, currentYOfenemyListComplete[i] - 2, pieceToMove, whatPositionNumber);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 1, currentYOfenemyListComplete[i] - 2, pieceToMove, whatPositionNumber);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 2, currentYOfenemyListComplete[i] - 1, pieceToMove, whatPositionNumber);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 2, currentYOfenemyListComplete[i] + 1, pieceToMove, whatPositionNumber);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 2, currentYOfenemyListComplete[i] - 1, pieceToMove, whatPositionNumber);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 2, currentYOfenemyListComplete[i] + 1, pieceToMove, whatPositionNumber);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 1, currentYOfenemyListComplete[i] + 2, pieceToMove, whatPositionNumber);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorse(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 1, currentYOfenemyListComplete[i] + 2, pieceToMove, whatPositionNumber);
                }

                if (currentAmountOfTries >= maxNumberOfTries && !didntFindAnytrhingOnce && !enemyFoundPlayer)
                {
                    moveToLocationAfterEnemyListX.Add(1337);
                    moveToLocationAfterEnemyListY.Add(1337);

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

                        if (xPosToGo == moveToLocationAfterEnemyListX[0] && yPosToGO == moveToLocationAfterEnemyListY[0])
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

                                moveToLocationAfterEnemyListX.Clear();
                                moveToLocationAfterEnemyListY.Clear();
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

                        if (xPosToGo == moveToLocationAfterEnemyListX[1] && yPosToGO == moveToLocationAfterEnemyListY[1])
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

                                moveToLocationAfterEnemyListX.Clear();
                                moveToLocationAfterEnemyListY.Clear();

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

                        if (xPosToGo == moveToLocationAfterEnemyListX[2] && yPosToGO == moveToLocationAfterEnemyListY[2])
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

                                moveToLocationAfterEnemyListX.Clear();
                                moveToLocationAfterEnemyListY.Clear();

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

                        if (xPosToGo == moveToLocationAfterEnemyListX[3] && yPosToGO == moveToLocationAfterEnemyListY[3])
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

                                moveToLocationAfterEnemyListX.Clear();
                                moveToLocationAfterEnemyListY.Clear();

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

                        if (xPosToGo == moveToLocationAfterEnemyListX[4] && yPosToGO == moveToLocationAfterEnemyListY[4])
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

                                moveToLocationAfterEnemyListX.Clear();
                                moveToLocationAfterEnemyListY.Clear();

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

                        if (xPosToGo == moveToLocationAfterEnemyListX[5] && yPosToGO == moveToLocationAfterEnemyListY[5])
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

                                moveToLocationAfterEnemyListX.Clear();
                                moveToLocationAfterEnemyListY.Clear();

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

                        if (xPosToGo == moveToLocationAfterEnemyListX[6] && yPosToGO == moveToLocationAfterEnemyListY[6])
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

                                moveToLocationAfterEnemyListX.Clear();
                                moveToLocationAfterEnemyListY.Clear();

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

                        if (xPosToGo == moveToLocationAfterEnemyListX[7] && yPosToGO == moveToLocationAfterEnemyListY[7])
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

                                moveToLocationAfterEnemyListX.Clear();
                                moveToLocationAfterEnemyListY.Clear();

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

                    moveToLocationAfterEnemyListX.Clear();
                    moveToLocationAfterEnemyListY.Clear();

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

        currentXOfEnemyList.Add(currentXOfPiece);
        currentYOfEnemyList.Add(currentYOfPiece);

        numberOfTimesLookingForPlayerLeft--;

        if (!enemyFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {
            numberOfTimesLookingForPlayer = 1;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            currentYOfEnemyList.Clear();
            currentXOfEnemyList.Clear();


            #region Search For Player

            for (int i = 0; i < enemyTowerObjectList.Count; i++)
            {
                #region Start Reset

                numberOfRound = 1;

                #endregion

                infoInt++;

                #region Left Search

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
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
                                breakLoop = true;

                                break;

                            }
                            else
                            {
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

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
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
                                breakLoop = true;

                                break;

                            }
                            else
                            {
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

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
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
                                breakLoop = true;

                                break;

                            }
                            else
                            {

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

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
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
                                breakLoop = true;

                                break;

                            }
                            else
                            {
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

            enemyFoundPlayer = false;

            currentYOfenemyListComplete.Clear();
            currentXOfEnemyListComplete.Clear();
            currentYOfEnemyList.Clear();
            currentXOfEnemyList.Clear();

            #endregion
        }
    }

    #region Searching For PLayer Loop

    public void FindPlayerTower(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY, int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int yOrXMovment, bool lastMovment)
    {
        if (!lastMovment)
        {
            enemyFoundPlayer = false;


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

                        enemyFoundPlayer = true;

                        // 2 För det subtraheras 1 på slutet
                        numberOfTimesLookingForPlayer = 1;
                        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;


                        numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                        currentAmountOfTries = 0;

                        currentYOfenemyListComplete.Clear();
                        currentXOfEnemyListComplete.Clear();
                        currentYOfEnemyList.Clear();
                        currentXOfEnemyList.Clear();
                        yOrXMovmentListComplete.Clear();
                        yOrXMovmentList.Clear();


                        moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                        moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

                    }
                }

                #endregion

            }

            if(yOrXMovment == 1)
            {

                #region Left

                while (true)
                {

                    infoInt = posToLookAtX - numberOfRoundsContinuation;


                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX - numberOfRoundsContinuation && yPos == posToLookAtY)
                        {

                            if (allPieces.playerPieceHere == true)
                            {

                                enemyFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;

                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfenemyListComplete.Clear();
                                currentXOfEnemyListComplete.Clear();
                                currentYOfEnemyList.Clear();
                                currentXOfEnemyList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();

                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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


                                currentXOfEnemyList.Add(posToLookAtX - numberOfRoundsContinuation);
                                currentYOfEnemyList.Add(posToLookAtY);

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

                    if (breakLoop || enemyFoundPlayer)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRoundsContinuation = 1;

                        break;
                    }

                }

                #endregion

                #region Right


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

                                enemyFoundPlayer = true;

                                currentAmountOfTries += 1;
                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;
                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfenemyListComplete.Clear();
                                currentXOfEnemyListComplete.Clear();
                                currentYOfEnemyList.Clear();
                                currentXOfEnemyList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();


                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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

                                currentXOfEnemyList.Add(posToLookAtX + numberOfRoundsContinuation);
                                currentYOfEnemyList.Add(posToLookAtY);

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

                    if (breakLoop || enemyFoundPlayer)
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

                while (true)
                {

                    infoInt = posToLookAtY - numberOfRoundsContinuation;

                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX && yPos == posToLookAtY - numberOfRoundsContinuation)
                        {

                            if (allPieces.playerPieceHere == true)
                            {

                                enemyFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;

                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfenemyListComplete.Clear();
                                currentXOfEnemyListComplete.Clear();
                                currentYOfEnemyList.Clear();
                                currentXOfEnemyList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();


                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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

                                currentXOfEnemyList.Add(posToLookAtX);
                                currentYOfEnemyList.Add(posToLookAtY - numberOfRoundsContinuation);

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

                    if (breakLoop || enemyFoundPlayer)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRoundsContinuation = 1;

                        break;
                    }

                }

                #endregion

                #region Up

                while (true)
                {

                    infoInt = posToLookAtY + numberOfRoundsContinuation;

                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX && yPos == posToLookAtY + numberOfRoundsContinuation)
                        {

                            if (allPieces.playerPieceHere == true)
                            {

                                enemyFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;
                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfenemyListComplete.Clear();
                                currentXOfEnemyListComplete.Clear();
                                currentYOfEnemyList.Clear();
                                currentXOfEnemyList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();



                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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

                                currentXOfEnemyList.Add(posToLookAtX);
                                currentYOfEnemyList.Add(posToLookAtY + numberOfRoundsContinuation);

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

                    if (breakLoop || enemyFoundPlayer)
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


        if (numberOfTimesLookingForPlayerLeft == 1)
        {
            if (!enemyFoundPlayer && currentAmountOfTries < maxNumberOfTries)
            {

                currentAmountOfTries += 1;

                numberOfTimesLookingForPlayer = 2;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                currentXOfEnemyListComplete.Clear();
                currentYOfenemyListComplete.Clear();
                yOrXMovmentListComplete.Clear();

                yOrXMovmentListComplete.AddRange(yOrXMovmentList);
                currentXOfEnemyListComplete.AddRange(currentXOfEnemyList);
                currentYOfenemyListComplete.AddRange(currentYOfEnemyList);


                yOrXMovmentList.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();

                #region Search For Player A New

                for (int i = 0; i < currentXOfEnemyListComplete.Count; i++)
                {

                    // 0 = Check Horizontal (It Was A Left Or Right Movment)
                    // 1 = Check Vertical (It Was A Up Or Down Movment)

                    if (yOrXMovmentListComplete[i] == 1 && !enemyFoundPlayer)
                    {

                        FindPlayerTower(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfEnemyListComplete[i], currentYOfenemyListComplete[i], pieceToMove, 0, false);
                    }

                    if (!enemyFoundPlayer)
                    {

                        if(yOrXMovmentListComplete[i] == 0)
                        {

                            FindPlayerTower(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfEnemyListComplete[i], currentYOfenemyListComplete[i], pieceToMove, 1, false);
                        }

                    }

                }

                #endregion

            }
        }

    }

    #endregion

    #region Move Tower

    void MoveToLocationTower(GameObject pieceToMove)
    {

        for (int i = 0; i < numberOfTriesToFindPlayerTower.Count; i++)
        {


            if (numberOfTriesToFindPlayerTower[i] < currentXWhereTowerIsGoingToGo)
            {

                foreach (GridPiece pieceToMoveTo in gridPieces)
                {

                    int xPosToGo = pieceToMoveTo.xPos;
                    int yPosToGO = pieceToMoveTo.yPos;

                    if (xPosToGo == moveToLocationAfterEnemyListX[i] && yPosToGO == moveToLocationAfterEnemyListY[i])
                    {

                        if (pieceToMoveTo.enemyPieceHere == false)
                        {


                            //Kollar Om Hur Försöken Är Hägre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayerTower[i];
                            //När Man Väl Ska Flytta Spelaren Så Behöver Jag Veta Vilken i Den Var På
                            towerWithTheLeastTries = i;


                        }

                    }
                }
            }

            numberOftimesLookingForPositionHorse++;
        }


        if(moveToLocationAfterEnemyListX.Count == 0)
        {
            return;
        }

        foreach (GridPiece pieceToMoveTo in gridPieces)
        {

            int xPosToGo = pieceToMoveTo.xPos;
            int yPosToGO = pieceToMoveTo.yPos;

            if (xPosToGo == moveToLocationAfterEnemyListX[towerWithTheLeastTries] && yPosToGO == moveToLocationAfterEnemyListY[towerWithTheLeastTries])
            {

                pieceToMove.GetComponent<GridPiece>().enemyTowerPieceHere = false;
                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                pieceToMoveTo.enemyTowerPieceHere = true;
                pieceToMoveTo.enemyPieceHere = true;

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                numberOfTriesToFindPlayerTower.Clear();

                moveToLocationAfterEnemyListX.Clear();
                moveToLocationAfterEnemyListY.Clear();

                currentXOfEnemyListComplete.Clear();
                currentYOfenemyListComplete.Clear();
                yOrXMovmentListComplete.Clear();

                yOrXMovmentList.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();

                break;
            }
        }
    }

    #endregion

    #endregion

    #region Bishop

    public void EnemyBishopMovmentCall(int xPos, int yPos, GameObject calledObject)
    {
        enemyTowerObjectList.Add(calledObject);

        if (enemyTowerObjectList.Count >= numberOfEnemys)
        {
            for (int i = 0; i < enemyTowerObjectList.Count; i++)
            {
                EnemyBishopMovment(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos);
            }
        }

    }

    public void EnemyBishopMovment(int currentX, int currentY)
    {
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;

        currentXOfEnemyList.Add(currentXOfPiece);
        currentYOfEnemyList.Add(currentYOfPiece);

        numberOfTimesLookingForPlayerLeft--;

        if (!enemyFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTries)
        {
            numberOfTimesLookingForPlayer = 1;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            currentYOfEnemyList.Clear();
            currentXOfEnemyList.Clear();


            #region Search For Player

            for (int i = 0; i < enemyTowerObjectList.Count; i++)
            {
                #region Start Reset

                numberOfRound = 1;

                #endregion

                infoInt++;

                #region Left Up Search

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                while (true)
                {


                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == currentX - numberOfRound && yPos == currentY + numberOfRound)
                        {

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;

                                break;

                            }
                            else
                            {
                                FindPlayerBishop(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyTowerObjectList[i], 0, false);
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

                        break;
                    }

                }

                #endregion

                #region Right Down Search

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                while (true)
                {

                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == currentX + numberOfRound && yPos == currentY - numberOfRound)
                        {

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;

                                break;

                            }
                            else
                            {
                                FindPlayerBishop(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyTowerObjectList[i], 0, false);
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

                        break;
                    }

                }

                #endregion

                #region Right Up Search

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                while (true)
                {
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == currentX + numberOfRound && yPos == currentY + numberOfRound)
                        {

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;

                                break;

                            }
                            else
                            {

                                FindPlayerBishop(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyTowerObjectList[i], 1, false);
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

                        break;
                    }

                }
                fourthSearchCompleteHorse = true;

                #endregion

                #region Left Down Search

                #region Reset
                currentAmountOfTries = 0;
                numberOfTimesLookingForPlayer = 1;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                enemyFoundPlayer = false;

                currentYOfenemyListComplete.Clear();
                currentXOfEnemyListComplete.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();
                #endregion
                while (true)
                {

                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == currentX - numberOfRound && yPos == currentY - numberOfRound)
                        {

                            if (allPieces.enemyPieceHere == true)
                            {
                                foundSomething = true;
                                breakLoop = true;

                                break;

                            }
                            else
                            {
                                FindPlayerBishop(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyTowerObjectList[i], 1, false);
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

                        break;
                    }

                }

                #endregion

                MoveToLocationBishop(enemyTowerObjectList[i]);

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

            enemyFoundPlayer = false;

            currentYOfenemyListComplete.Clear();
            currentXOfEnemyListComplete.Clear();
            currentYOfEnemyList.Clear();
            currentXOfEnemyList.Clear();

            #endregion
        }
    }

    #region Searching For PLayer Loop

    public void FindPlayerBishop(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY, int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int yOrXMovment, bool lastMovment)
    {
        if (!lastMovment)
        {
            enemyFoundPlayer = false;


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

                        enemyFoundPlayer = true;

                        // 2 För det subtraheras 1 på slutet
                        numberOfTimesLookingForPlayer = 1;
                        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;


                        numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                        currentAmountOfTries = 0;

                        currentYOfenemyListComplete.Clear();
                        currentXOfEnemyListComplete.Clear();
                        currentYOfEnemyList.Clear();
                        currentXOfEnemyList.Clear();
                        yOrXMovmentListComplete.Clear();
                        yOrXMovmentList.Clear();


                        moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                        moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

                    }
                }

                #endregion

            }

            if (yOrXMovment == 1)
            {

                #region Left Up

                while (true)
                {


                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX - numberOfRoundsContinuation && yPos == posToLookAtY + numberOfRoundsContinuation)
                        {

                            if (allPieces.playerPieceHere == true)
                            {

                                enemyFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;

                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfenemyListComplete.Clear();
                                currentXOfEnemyListComplete.Clear();
                                currentYOfEnemyList.Clear();
                                currentXOfEnemyList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();

                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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


                                currentXOfEnemyList.Add(posToLookAtX - numberOfRoundsContinuation);
                                currentYOfEnemyList.Add(posToLookAtY + numberOfRoundsContinuation);

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

                    if (breakLoop || enemyFoundPlayer)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRoundsContinuation = 1;

                        break;
                    }

                }

                #endregion

                #region Right Down


                while (true)
                {

                    //Debug.Log(infoInt + " x " + posToLookAtY + " y Where I Am Looking Right COntinuation");
                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX + numberOfRoundsContinuation && yPos == posToLookAtY - numberOfRoundsContinuation)
                        {

                            if (allPieces.playerPieceHere == true)
                            {

                                enemyFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;
                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfenemyListComplete.Clear();
                                currentXOfEnemyListComplete.Clear();
                                currentYOfEnemyList.Clear();
                                currentXOfEnemyList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();


                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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

                                currentXOfEnemyList.Add(posToLookAtX + numberOfRoundsContinuation);
                                currentYOfEnemyList.Add(posToLookAtY - numberOfRoundsContinuation);

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

                    if (breakLoop || enemyFoundPlayer)
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

                #region Left Down

                while (true)
                {


                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX - numberOfRoundsContinuation && yPos == posToLookAtY - numberOfRoundsContinuation)
                        {

                            if (allPieces.playerPieceHere == true)
                            {

                                enemyFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;

                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfenemyListComplete.Clear();
                                currentXOfEnemyListComplete.Clear();
                                currentYOfEnemyList.Clear();
                                currentXOfEnemyList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();


                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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

                                currentXOfEnemyList.Add(posToLookAtX - numberOfRoundsContinuation);
                                currentYOfEnemyList.Add(posToLookAtY - numberOfRoundsContinuation);

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

                    if (breakLoop || enemyFoundPlayer)
                    {
                        foundSomething = false;
                        breakLoop = false;

                        numberOfRoundsContinuation = 1;

                        break;
                    }

                }

                #endregion

                #region Right Up

                while (true)
                {

                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX + numberOfRoundsContinuation && yPos == posToLookAtY + numberOfRoundsContinuation)
                        {

                            if (allPieces.playerPieceHere == true)
                            {

                                enemyFoundPlayer = true;

                                numberOfTimesLookingForPlayer = 1;
                                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                                currentAmountOfTries += 1;
                                numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                                currentAmountOfTries = 0;

                                currentYOfenemyListComplete.Clear();
                                currentXOfEnemyListComplete.Clear();
                                currentYOfEnemyList.Clear();
                                currentXOfEnemyList.Clear();
                                yOrXMovmentListComplete.Clear();
                                yOrXMovmentList.Clear();



                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

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

                                currentXOfEnemyList.Add(posToLookAtX + numberOfRoundsContinuation);
                                currentYOfEnemyList.Add(posToLookAtY + numberOfRoundsContinuation);

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

                    if (breakLoop || enemyFoundPlayer)
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


        if (numberOfTimesLookingForPlayerLeft == 1)
        {
            if (!enemyFoundPlayer && currentAmountOfTries < maxNumberOfTriesBishop)
            {

                currentAmountOfTries += 1;

                numberOfTimesLookingForPlayer = 2;
                numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

                currentXOfEnemyListComplete.Clear();
                currentYOfenemyListComplete.Clear();
                yOrXMovmentListComplete.Clear();

                yOrXMovmentListComplete.AddRange(yOrXMovmentList);
                currentXOfEnemyListComplete.AddRange(currentXOfEnemyList);
                currentYOfenemyListComplete.AddRange(currentYOfEnemyList);


                yOrXMovmentList.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();

                #region Search For Player A New

                for (int i = 0; i < currentXOfEnemyListComplete.Count; i++)
                {

                    // 0 = Check Horizontal (It Was A Left Or Right Movment)
                    // 1 = Check Vertical (It Was A Up Or Down Movment)


                    if (!enemyFoundPlayer)
                    {
                        if (yOrXMovmentListComplete[i] == 1)
                        {

                            FindPlayerBishop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfEnemyListComplete[i], currentYOfenemyListComplete[i], pieceToMove, 0, false);
                        }
                    }

                    if (!enemyFoundPlayer)
                    {

                        if (yOrXMovmentListComplete[i] == 0)
                        {

                            FindPlayerBishop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfEnemyListComplete[i], currentYOfenemyListComplete[i], pieceToMove, 1, false);
                        }

                    }

                }

                #endregion
                FindPlayerBishop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, 1337, 1337, pieceToMove, 1337, true);
            }

        }

    }

    #endregion

    #region Move Bishop

    void MoveToLocationBishop(GameObject pieceToMove)
    {

        if(moveToLocationAfterEnemyListX.Count == 0)
        {
            return;
        }

        for (int i = 0; i < numberOfTriesToFindPlayerTower.Count; i++)
        {

            if (numberOfTriesToFindPlayerTower[i] < currentXWhereTowerIsGoingToGo)
            {

                foreach (GridPiece pieceToMoveTo in gridPieces)
                {

                    int xPosToGo = pieceToMoveTo.xPos;
                    int yPosToGO = pieceToMoveTo.yPos;

                    if (xPosToGo == moveToLocationAfterEnemyListX[i] && yPosToGO == moveToLocationAfterEnemyListY[i])
                    {

                        if (pieceToMoveTo.enemyPieceHere == false)
                        {


                            //Kollar Om Hur Försöken Är Högre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayerTower[i];

                            //När Man Väl Ska Flytta Spelaren Så Behöver Jag Veta Vilken i Den Var På
                            towerWithTheLeastTries = i;


                        }

                    }
                }
            }

            numberOftimesLookingForPositionHorse++;
        }


        foreach (GridPiece pieceToMoveTo in gridPieces)
        {

            int xPosToGo = pieceToMoveTo.xPos;
            int yPosToGO = pieceToMoveTo.yPos;

            if (xPosToGo == moveToLocationAfterEnemyListX[towerWithTheLeastTries] && yPosToGO == moveToLocationAfterEnemyListY[towerWithTheLeastTries])
            {

                pieceToMove.GetComponent<GridPiece>().enemyBishopPieceHere = false;
                pieceToMove.GetComponent<GridPiece>().enemyPieceHere = false;
                pieceToMoveTo.enemyBishopPieceHere = true;
                pieceToMoveTo.enemyPieceHere = true;

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                numberOfTriesToFindPlayerTower.Clear();

                moveToLocationAfterEnemyListX.Clear();
                moveToLocationAfterEnemyListY.Clear();

                currentXOfEnemyListComplete.Clear();
                currentYOfenemyListComplete.Clear();
                yOrXMovmentListComplete.Clear();

                yOrXMovmentList.Clear();
                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();

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
