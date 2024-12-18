using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridPiece[] gridPieces;
    BoardCreator boardCreator;
    SceneLoader sceneLoader;
    GameManagerSr gameManager;
    Inventory Inventory;

    GameObject moveFromTileObject;
    List<GameObject> attackFromTileObjectList = new List<GameObject>();


    #region Enemy

    int maxNumberOfTries = 7;

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

    #region Queen

    List<GameObject> enemyQueenObjectList = new List<GameObject>();

    int maxNumberOfTriesQueen = 2;

    #endregion

    #region Pawn

    List<GameObject> enemyPawnObjectList = new List<GameObject>();

    bool firstTimeSearching = true;
    bool enemyPawnNotMoveForward = false;

    List<int> moveToForwardPawnListX = new List<int>();
    List<int> moveToForwardPawnListY = new List<int>();

    List<int> numberOfTriesForPawnToGoForward = new List<int>();

    #endregion

    #region Enemy Horse movemnt 

    int numberOfTimesLookingForPlayer = 0;
    int numberOfTimesLookingForPlayerLeft = 0;

    int currentAmountOfTries;
    int numberOftimesLookingForPositionHorse = 0;
    int maxNumberOfTriesHorse = 4;
    int currentXOfPiece;
    int currentYOfPiece;
    public int numberOfEnemys;

    List<int> currentXOfEnemyList = new List<int>();
    List<int> currentYOfEnemyList = new List<int>();

    List<int> currentXOfEnemyListComplete = new List<int>();
    List<int> currentYOfenemyListComplete = new List<int>();

    List<int> moveToLocationAfterEnemyListX = new List<int>();
    List<int> moveToLocationAfterEnemyListY = new List<int>();

    bool enemyFoundPlayer = false;

    List<GameObject> enemyHorseObjectList = new List<GameObject>();

    #endregion

    #endregion

    #region Player

    public int howManyPlayerPieces = 0;

    public bool firstRoundDone = false;
    public bool reviveHappened = false;

    GridPiece whoCalledForRevive;

    #region Tower

    bool breakLoop = false;
    bool foundSomething;
    int numberOfRound = 1;

    #endregion

    #region Pawn



    #endregion

    #region Upgrades



    #endregion

    #endregion

    #region Abilitys

    #endregion

    #region MidRoundBuffs

    [SerializeField] List<GameObject> midRoundBuffsButtons = new List<GameObject>();

    [SerializeField] Transform[] buffPos;

    bool aktivateBuffButtons = false;

    #endregion

    [Header("Revive")]

    [SerializeField] GameObject reviveObject;

    int infoInt = 0;

    bool delayStartHasRun = false;

    bool didntFindAnytrhingOnce = false;

    #region Start & Update

    private void Start()
    {
        boardCreator = FindObjectOfType<BoardCreator>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameManager = FindObjectOfType<GameManagerSr>();
        Inventory = FindObjectOfType<Inventory>();


        StartCoroutine(DelayStart());
    }

    private void Update()
    {

        if (numberOfEnemys == 0 && delayStartHasRun)
        {
            gameManager.howManyPointsForEnemys += 5;
            gameManager.money += 10;
            gameManager.whatRound++;

            switch (gameManager.whatRound)
            {

                case 13:

                    Debug.Log("WIN");

                break;

            }

            sceneLoader.ChangeScene(2);
        }

        if(howManyPlayerPieces == 0 && Inventory.gameHasStarted) 
        {
            Debug.Log("DED");

            sceneLoader.ChangeScene(1);
        }

        if (!aktivateBuffButtons && firstRoundDone)
        {

            #region Buff Buttons Aktivate

            int whatBuffButtonToAcktivate = Random.Range(0, midRoundBuffsButtons.Count);

            midRoundBuffsButtons[whatBuffButtonToAcktivate].SetActive(true);
            midRoundBuffsButtons[whatBuffButtonToAcktivate].transform.position = buffPos[0].position;

            midRoundBuffsButtons.RemoveAt(whatBuffButtonToAcktivate);

            whatBuffButtonToAcktivate = Random.Range(0, midRoundBuffsButtons.Count);

            midRoundBuffsButtons[whatBuffButtonToAcktivate].SetActive(true);
            midRoundBuffsButtons[whatBuffButtonToAcktivate].transform.position = buffPos[1].position;

            aktivateBuffButtons = true;

            #endregion

        }
    }

    #endregion

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

                    // maxY Visar en mindre 
                    if (boardCreator.topPosY - 1 == currentY + 1)
                    {

                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;
                    }
                    else
                    {
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Pawn;
                    }

                    allPieces.anticipateMovment = true;

                }
            }
        }
    }

    public void AnticipatePawnAttack(int currentX, int currentY, GameObject callerGameObject)
    {

        if (firstRoundDone)
        {

            moveFromTileObject = callerGameObject;

            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + 1 && yPos == currentY + 1)
                {

                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Pawn;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                    }

                }

                if (xPos == currentX - 1 && yPos == currentY + 1)
                {

                    if (allPieces.enemyPieceHere == true)
                    {
                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Pawn;

                        attackFromTileObjectList.Add(allPieces.gameObject);
                    }

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

            #region Up

            if (xPos == currentX + 1 && yPos == currentY + 2)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;
                }
            }

            if (xPos == currentX - 1 && yPos == currentY + 2)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;
                }
            }

            #endregion

            #region Down

            if (xPos == currentX + 1 && yPos == currentY - 2)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;
                }
            }

            if (xPos == currentX - 1 && yPos == currentY - 2)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;
                }
            }

            #endregion

            #region Right

            if (xPos == currentX + 2 && yPos == currentY + 1)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;
                }
            }

            if (xPos == currentX + 2 && yPos == currentY - 1)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;
                }
            }

            #endregion

            #region Left

            if (xPos == currentX - 2 && yPos == currentY + 1)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;
                }
            }

            if (xPos == currentX - 2 && yPos == currentY - 1)
            {
                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else
                {
                    allPieces.anticipateMovment = true;
                    allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Tower;

                            attackFromTileObjectList.Add(allPieces.gameObject);


                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Tower;

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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Tower;

                            attackFromTileObjectList.Add(allPieces.gameObject);


                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Tower;

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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Tower;

                            attackFromTileObjectList.Add(allPieces.gameObject);


                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Tower;

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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Tower;

                            attackFromTileObjectList.Add(allPieces.gameObject);


                        }

                        break;

                    }

                    if(allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Tower;

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

    #region Bishop

    public void AnticipateBishopMovment(int currentX, int currentY, GameObject callerGameObject)
    {
        moveFromTileObject = callerGameObject;

        #region Left Up

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - numberOfRound && yPos == currentY + numberOfRound)
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Bishop;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Bishop;

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

        #region Right Down

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + numberOfRound && yPos == currentY - numberOfRound)
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Bishop;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Bishop;

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


        #region Down Left

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - numberOfRound && yPos == currentY - numberOfRound)
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Bishop;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Bishop;

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

        #region Right Up

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + numberOfRound && yPos == currentY + numberOfRound)
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Bishop;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Bishop;

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

    #region Queen

    public void AnticipateQueenMovment(int currentX, int currentY, GameObject callerGameObject)
    {
        moveFromTileObject = callerGameObject;

        #region Left Up

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - numberOfRound && yPos == currentY + numberOfRound)
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;

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

        #region Right Down

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + numberOfRound && yPos == currentY - numberOfRound)
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;

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


        #region Down Left

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX - numberOfRound && yPos == currentY - numberOfRound)
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;

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

        #region Right Up

        while (true)
        {
            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX + numberOfRound && yPos == currentY + numberOfRound)
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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;

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
                    if (allPieces.playerPieceHere == true)
                    {
                        breakLoop = true;

                        break;
                    }

                    if (allPieces.enemyPieceHere == true)
                    {
                        foundSomething = true;
                        breakLoop = true;

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;

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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;

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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;

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

                        if (firstRoundDone)
                        {

                            allPieces.anticipatePlayerAttack = true;
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }

                        break;

                    }

                    if (allPieces.enemyPieceHere == false && allPieces.playerPieceHere == false)
                    {
                        foundSomething = true;
                        breakLoop = false;

                        allPieces.anticipateMovment = true;
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;

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

    public void movePiece(PlayerType whoCalled, GameObject objectToMoveTo)
    {

        moveFromTileObject.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(objectToMoveTo, true, false, moveFromTileObject);

    }

    public void AttackPiece(PlayerType whoCalled, GameObject objectToMoveTo)
    {

        moveFromTileObject.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(objectToMoveTo, true, true, moveFromTileObject);

    }

    #endregion

    #endregion

    #region Enemy

    #region Horse

    #region Enemy Horse Movment

    public void EnemyHorseMovmentCall(int xPos, int yPos, GameObject calledObject)
    {
        enemyHorseObjectList.Add(calledObject);

        for (int i = 0; i < enemyHorseObjectList.Count; i++)
        {
            EnemyHorseMovment(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos);
        }

    }

    public void EnemyHorseMovment(int currentX, int currentY)
    {
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;

        #region Search For Player For The First Time

        for (int i = 0; i < enemyHorseObjectList.Count; i++)
        {

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i], 1);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i], 2);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i], 3);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i], 4);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i], 5);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i], 6);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i], 7);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i], 8);

            MoveToLocationHorse(enemyHorseObjectList[i]);

        }

        #endregion

        #region End Reset

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

    public void FindPlayerHorseLoop(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY,   int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int whatPositionNumber)
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
                    enemyFoundPlayer = true;

                    numberOfTimesLookingForPlayer = 1;
                    numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;


                    numberOfTriesToFindPlayerTower.Add(currentAmountOfTries);
                    currentAmountOfTries = 0;

                    currentYOfenemyListComplete.Clear();
                    currentXOfEnemyListComplete.Clear();
                    currentYOfEnemyList.Clear();
                    currentXOfEnemyList.Clear();

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                }
            }

            #endregion

            #region Down Movment

            if (xPos == posToLookAtX - 1 && yPos == posToLookAtY - 2)
            {
                if (allPieces.playerPieceHere)
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

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY - 2)
            {
                if (allPieces.playerPieceHere)
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

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);

                }
            }

            #endregion

            #region Right Movment

            if (xPos == posToLookAtX + 2 && yPos == posToLookAtY - 1)
            {
                if (allPieces.playerPieceHere)
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

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                }
            }


            if (xPos == posToLookAtX + 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
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

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                }
            }

            #endregion

            #region Left Movment

            if (xPos == posToLookAtX - 2 && yPos == posToLookAtY - 1)
            {
                if (allPieces.playerPieceHere)
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

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                }
            }

            if (xPos == posToLookAtX - 2 && yPos == posToLookAtY + 1)
            {
                if (allPieces.playerPieceHere)
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

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                }
            }

            #endregion

            #region Up Movment

            if (xPos == posToLookAtX - 1 && yPos == posToLookAtY + 2)
            {
                if (allPieces.playerPieceHere)
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

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                }
            }

            if (xPos == posToLookAtX + 1 && yPos == posToLookAtY + 2)
            {
                if (allPieces.playerPieceHere)
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

                    moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                    moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                }
            }

            #endregion

        }

        #endregion

        numberOfTimesLookingForPlayerLeft--;

        if (!enemyFoundPlayer && numberOfTimesLookingForPlayerLeft == 0 && currentAmountOfTries < maxNumberOfTriesHorse)
        {

            currentAmountOfTries += 1;

            numberOfTimesLookingForPlayer *= 8;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

            currentXOfEnemyListComplete.Clear();
            currentYOfenemyListComplete.Clear();

            currentXOfEnemyListComplete.AddRange(currentXOfEnemyList);
            currentYOfenemyListComplete.AddRange(currentYOfEnemyList);

            currentYOfEnemyList.Clear();
            currentXOfEnemyList.Clear();

            #region Search For Player A New

            for (int i = 0; i < currentXOfEnemyListComplete.Count; i++)
            {
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 1, currentYOfenemyListComplete[i] - 2, pieceToMove, whatPositionNumber);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 1, currentYOfenemyListComplete[i] - 2, pieceToMove, whatPositionNumber);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 2, currentYOfenemyListComplete[i] - 1, pieceToMove, whatPositionNumber);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 2, currentYOfenemyListComplete[i] + 1, pieceToMove, whatPositionNumber);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 2, currentYOfenemyListComplete[i] - 1, pieceToMove, whatPositionNumber);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 2, currentYOfenemyListComplete[i] + 1, pieceToMove, whatPositionNumber);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 1, currentYOfenemyListComplete[i] + 2, pieceToMove, whatPositionNumber);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 1, currentYOfenemyListComplete[i] + 2, pieceToMove, whatPositionNumber);
                }

                if (currentAmountOfTries >= maxNumberOfTries && !didntFindAnytrhingOnce && !enemyFoundPlayer)
                {
                    //moveToLocationAfterEnemyListX.Add(1337);
                    //moveToLocationAfterEnemyListY.Add(1337);


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
                        else
                        {

                            numberOfTriesToFindPlayerTower.RemoveAt(i);
                            moveToLocationAfterEnemyListX.Remove(i);
                            moveToLocationAfterEnemyListY.Remove(i);

                            // Eftersom jag tar bort från listan måste jag kolla om jag ska sluta söka så jag inte söker med i som är högre än listorna
                            if (i >= numberOfTriesToFindPlayerTower.Count)
                            {

                                break;

                            }

                        }

                    }
                }
            }

            numberOftimesLookingForPositionHorse++;
        }

        if (moveToLocationAfterEnemyListX.Count == 0 || currentXWhereTowerIsGoingToGo == 1337 || towerWithTheLeastTries == 1337)
        {
            return;
        }

        foreach (GridPiece pieceToMoveTo in gridPieces)
        {

            int xPosToGo = pieceToMoveTo.xPos;
            int yPosToGO = pieceToMoveTo.yPos;

            if (xPosToGo == moveToLocationAfterEnemyListX[towerWithTheLeastTries] && yPosToGO == moveToLocationAfterEnemyListY[towerWithTheLeastTries])
            {
                Debug.Log("MOVE Horse");

                pieceToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;
                pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove);


                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                numberOfTriesToFindPlayerTower.Clear();

                moveToLocationAfterEnemyListX.Clear();
                moveToLocationAfterEnemyListY.Clear();

                currentXOfEnemyListComplete.Clear();
                currentYOfenemyListComplete.Clear();

                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();

                enemyHorseObjectList.Clear();

                break;
            }
        }
    }

    #endregion

    #endregion

    #region Tower

    public void EnemyTowerMovmentCall(int xPos, int yPos, GameObject calledObject)
    {

        enemyTowerObjectList.Add(calledObject);

        for (int i = 0; i < enemyTowerObjectList.Count; i++)
        {
            EnemyTowerMovment(enemyTowerObjectList[i].GetComponent<GridPiece>().xPos, enemyTowerObjectList[i].GetComponent<GridPiece>().yPos);
        }
    }

    public void EnemyTowerMovment(int currentX, int currentY)
    {
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;

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
                            currentAmountOfTries = 0;
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
                            currentAmountOfTries = 0;
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
                            currentAmountOfTries = 0;
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

                            currentAmountOfTries = 0;
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

            if(yOrXMovment == 1 && !enemyFoundPlayer)
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

            if (yOrXMovment == 0 && !enemyFoundPlayer)
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
                        else
                        {

                            numberOfTriesToFindPlayerTower.RemoveAt(i);
                            moveToLocationAfterEnemyListX.Remove(i);
                            moveToLocationAfterEnemyListY.Remove(i);

                            // Eftersom jag tar bort från listan måste jag kolla om jag ska sluta söka så jag inte söker med i som är högre än listorna
                            if (i >= numberOfTriesToFindPlayerTower.Count)
                            {

                                break;

                            }

                        }

                    }
                }
            }
        }


        if (moveToLocationAfterEnemyListX.Count == 0 || currentXWhereTowerIsGoingToGo == 1337 || towerWithTheLeastTries == 1337)
        {
            return;
        }

        foreach (GridPiece pieceToMoveTo in gridPieces)
        {

            int xPosToGo = pieceToMoveTo.xPos;
            int yPosToGO = pieceToMoveTo.yPos;

            if (xPosToGo == moveToLocationAfterEnemyListX[towerWithTheLeastTries] && yPosToGO == moveToLocationAfterEnemyListY[towerWithTheLeastTries])
            {
                Debug.Log("MOVE TOWER");

                pieceToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;
                pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove);

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
        enemyBishopObjectList.Add(calledObject);

        for (int i = 0; i < enemyBishopObjectList.Count; i++)
        {
            EnemyBishopMovment(enemyBishopObjectList[i].GetComponent<GridPiece>().xPos, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos);
        }

    }

    public void EnemyBishopMovment(int currentX, int currentY)
    {
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;

        #region Search For Player

        for (int i = 0; i < enemyBishopObjectList.Count; i++)
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
                            FindPlayerBishop(enemyBishopObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyBishopObjectList[i], 0, false);
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
                            FindPlayerBishop(enemyBishopObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyBishopObjectList[i], 0, false);
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

                            FindPlayerBishop(enemyBishopObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyBishopObjectList[i], 1, false);
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
                            FindPlayerBishop(enemyBishopObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyBishopObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyBishopObjectList[i], 1, false);
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

            MoveToLocationBishop(enemyBishopObjectList[i]);

        }

        #endregion

        #region End Reset

        enemyBishopObjectList.Clear();

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
                        else
                        {

                            numberOfTriesToFindPlayerTower.RemoveAt(i);
                            moveToLocationAfterEnemyListX.Remove(i);
                            moveToLocationAfterEnemyListY.Remove(i);

                            // Eftersom jag tar bort från listan måste jag kolla om jag ska sluta söka så jag inte söker med i som är högre än listorna
                            if (i >= numberOfTriesToFindPlayerTower.Count)
                            {

                                break;

                            }
                        }

                    }
                }
            }

            numberOftimesLookingForPositionHorse++;
        }


        if (moveToLocationAfterEnemyListX.Count == 0 || currentXWhereTowerIsGoingToGo == 1337 || towerWithTheLeastTries == 1337)
        {
            return;
        }

        foreach (GridPiece pieceToMoveTo in gridPieces)
        {

            int xPosToGo = pieceToMoveTo.xPos;
            int yPosToGO = pieceToMoveTo.yPos;

            if (xPosToGo == moveToLocationAfterEnemyListX[towerWithTheLeastTries] && yPosToGO == moveToLocationAfterEnemyListY[towerWithTheLeastTries])
            {
                Debug.Log("MOVE Bishop");

                pieceToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;
                pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove);

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

                enemyBishopObjectList.Clear();

                break;
            }
        }
    }

    #endregion

    #endregion

    #region Queen

    public void EnemyQueenMovmentCall(int xPos, int yPos, GameObject calledObject)
    {
        enemyQueenObjectList.Add(calledObject);

        for (int i = 0; i < enemyQueenObjectList.Count; i++)
        {
            EnemyQueenMovment(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos);
        }

    }

    public void EnemyQueenMovment(int currentX, int currentY)
    {
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;

        #region Search For Player

        for (int i = 0; i < enemyQueenObjectList.Count; i++)
        {
            #region Start Reset

            numberOfRound = 1;

            #endregion

            infoInt++;

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
                            FindPlayerQueen(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().xPos, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyQueenObjectList[i], 2, false);
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
                            FindPlayerQueen(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().xPos, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyQueenObjectList[i], 2, false);
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

                            FindPlayerQueen(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos, enemyQueenObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos, enemyQueenObjectList[i], 3, false);
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
                            FindPlayerQueen(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos, enemyQueenObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos, enemyQueenObjectList[i], 3, false);
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
                            FindPlayerQueen(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyQueenObjectList[i], 0, false);
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
                            FindPlayerQueen(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyQueenObjectList[i], 0, false);
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

                            FindPlayerQueen(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().xPos + numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos + numberOfRound, enemyQueenObjectList[i], 1, false);
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
                            FindPlayerQueen(enemyQueenObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().xPos - numberOfRound, enemyQueenObjectList[i].GetComponent<GridPiece>().yPos - numberOfRound, enemyQueenObjectList[i], 1, false);
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

            MoveToLocationQueen(enemyQueenObjectList[i]);

        }

        #endregion

        #region End Reset

        enemyQueenObjectList.Clear();

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

    #region Searching For PLayer Loop

    public void FindPlayerQueen(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY, int posToLookAtX, int posToLookAtY, GameObject pieceToMove, int yOrXMovment, bool lastMovment)
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

            if (yOrXMovment != 0)
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

            if (yOrXMovment != 1)
            {
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

            if (yOrXMovment != 2)
            {
                #region Up

                while (true)
                {


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

                                yOrXMovmentList.Add(2);

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

                #region Down


                while (true)
                {

                    //Debug.Log(infoInt + " x " + posToLookAtY + " y Where I Am Looking Right COntinuation");
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

                                currentXOfEnemyList.Add(posToLookAtX);
                                currentYOfEnemyList.Add(posToLookAtY - numberOfRoundsContinuation);

                                yOrXMovmentList.Add(2);

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

            if (yOrXMovment != 3)
            {
                #region Right


                while (true)
                {

                    foreach (GridPiece allPieces in gridPieces)
                    {
                        int xPos = allPieces.xPos;
                        int yPos = allPieces.yPos;

                        if (xPos == posToLookAtX + numberOfRoundsContinuation && yPos == posToLookAtY)
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
                                currentYOfEnemyList.Add(posToLookAtY);

                                yOrXMovmentList.Add(3);

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

                #region Left

                while (true)
                {


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

                                yOrXMovmentList.Add(3);

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
            if (!enemyFoundPlayer && currentAmountOfTries < maxNumberOfTriesQueen)
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
                        //if (yOrXMovmentListComplete[i] == 1)
                        //{

                        //    FindPlayerQueen(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfEnemyListComplete[i], currentYOfenemyListComplete[i], pieceToMove, 0, false);
                        //}

                        FindPlayerQueen(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfEnemyListComplete[i], currentYOfenemyListComplete[i], pieceToMove, yOrXMovmentListComplete[i], false);
                    }

                    if (!enemyFoundPlayer)
                    {

                        //if (yOrXMovmentListComplete[i] == 0)
                        //{

                        //    FindPlayerQueen(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, currentXOfEnemyListComplete[i], currentYOfenemyListComplete[i], pieceToMove, 1, false);
                        //}

                    }

                }

                #endregion
                FindPlayerQueen(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY, 1337, 1337, pieceToMove, 1337, true);
            }

        }

    }

    #endregion

    #region Move Queen

    void MoveToLocationQueen(GameObject pieceToMove)
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


                            //Kollar Om Hur Försöken Är Högre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayerTower[i];

                            //När Man Väl Ska Flytta Spelaren Så Behöver Jag Veta Vilken i Den Var På
                            towerWithTheLeastTries = i;


                        }
                        else
                        {

                            numberOfTriesToFindPlayerTower.RemoveAt(i);
                            moveToLocationAfterEnemyListX.Remove(i);
                            moveToLocationAfterEnemyListY.Remove(i);

                            // Eftersom jag tar bort från listan måste jag kolla om jag ska sluta söka så jag inte söker med i som är högre än listorna
                            if (i >= numberOfTriesToFindPlayerTower.Count)
                            {

                                break;

                            }

                        }

                    }
                }
            }

            numberOftimesLookingForPositionHorse++;
        }

        if (moveToLocationAfterEnemyListX.Count == 0 || currentXWhereTowerIsGoingToGo == 1337 || towerWithTheLeastTries == 1337)
        {
            return;
        }

        foreach (GridPiece pieceToMoveTo in gridPieces)
        {

            int xPosToGo = pieceToMoveTo.xPos;
            int yPosToGO = pieceToMoveTo.yPos;

            if (xPosToGo == moveToLocationAfterEnemyListX[towerWithTheLeastTries] && yPosToGO == moveToLocationAfterEnemyListY[towerWithTheLeastTries])
            {
                Debug.Log("MOVE QUEEN");

                pieceToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;
                pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove);

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                enemyQueenObjectList.Clear();

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

    #region pawn

    public void EnemyPawnMovmentCall(int xPos, int yPos, GameObject calledObject)
    {
        enemyPawnObjectList.Add(calledObject);

        for (int i = 0; i < enemyPawnObjectList.Count; i++)
        {
            EnemyPawnMovment(enemyPawnObjectList[i].GetComponent<GridPiece>().xPos, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos);
        }

    }

    public void EnemyPawnMovment(int currentX, int currentY)
    {
        currentXOfPiece = currentX;
        currentYOfPiece = currentY;

        #region Search For Player

        for (int i = 0; i < enemyPawnObjectList.Count; i++)
        {
            #region Start Reset

            numberOfRound = 1;
            firstTimeSearching = true;

            #endregion

            infoInt++;

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

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX && yPos == currentY - 1)
                {

                    FindPlayerPawn(enemyPawnObjectList[i].GetComponent<GridPiece>().xPos, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyPawnObjectList[i].GetComponent<GridPiece>().xPos, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos, enemyPawnObjectList[i], false);

                }
            }


            #endregion

            MoveToLocationPawn(enemyPawnObjectList[i]);

        }

        #endregion

        #region End Reset

        enemyPawnObjectList.Clear();

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

    #region Searching For PLayer Loop

    public void FindPlayerPawn(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY, int posToLookAtX, int posToLookAtY, GameObject pieceToMove, bool lastMovment)
    {
        if (!lastMovment)
        {
            enemyFoundPlayer = false;


            #region Search For Player

            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

            #region Down

            while (true)
            {

                foreach (GridPiece allPieces in gridPieces)
                {
                    int xPos = allPieces.xPos;
                    int yPos = allPieces.yPos;

                    #region Right Player Search

                    if (xPos == posToLookAtX + 1 && yPos == posToLookAtY - numberOfRoundsContinuation)
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

                            if (firstTimeSearching)
                            {
                                // Attack
                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX + 1);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY - numberOfRoundsContinuation + 1);
                            }
                            else
                            {
                                // Player Piece Is One Or More Away From Player So Move Forward
                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                            }

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
                            currentYOfEnemyList.Add(posToLookAtY - numberOfRoundsContinuation);

                        }
                    }
                    else
                    {
                        if (!foundSomething)
                        {
                            breakLoop = true;
                        }
                    }

                    #endregion

                    #region Left Player Search

                    if (xPos == posToLookAtX - 1 && yPos == posToLookAtY - numberOfRoundsContinuation)
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

                            if (firstTimeSearching)
                            {
                                // Attack
                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX - 1);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY - numberOfRoundsContinuation + 1);
                            }
                            else
                            {
                                // Player Piece Is One Or More Away From Player So Move Forward
                                moveToLocationAfterEnemyListX.Add(posToMoveToAfterFindingPlayerX);
                                moveToLocationAfterEnemyListY.Add(posToMoveToAfterFindingPlayerY);
                            }

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

                        }
                    }
                    else
                    {
                        if (!foundSomething)
                        {
                            breakLoop = true;
                        }
                    }

                    #endregion

                    #region Down End Search

                    if (xPos == posToLookAtX  && yPos == posToLookAtY - numberOfRoundsContinuation)
                    {

                        if (allPieces.playerPieceHere == true)
                        {
                            // Spelare där
                            if (firstTimeSearching) // Den är rakt framför mig, alltså sluta sök
                            {

                                foundSomething = true;
                                breakLoop = true;
                            }
                            enemyPawnNotMoveForward = true;
                            foundSomething = true;

                            moveToForwardPawnListX.Clear();
                            moveToForwardPawnListY.Clear();

                            numberOfTriesForPawnToGoForward.Clear();

                            break;
                        }

                        if (allPieces.enemyPieceHere == true)
                        {

                            // Fiende Där
                            if (firstTimeSearching) // Den är rakt framför mig, alltså sluta sök
                            {
                                foundSomething = true;
                                breakLoop = true;
                            }
                            foundSomething = true;
                            break;

                        }
                        else
                        {
                            // Empty Board Piece
                            foundSomething = true;
                            breakLoop = false;

                        }

                    }
                    else
                    {
                        if (!foundSomething && !enemyPawnNotMoveForward)
                        {

                            numberOfTriesForPawnToGoForward.Add(420);

                            moveToForwardPawnListX.Add(posToMoveToAfterFindingPlayerX);
                            moveToForwardPawnListY.Add(posToMoveToAfterFindingPlayerY);

                        }
                    }

                    #endregion

                }

                foundSomething = false;
                firstTimeSearching = false;

                numberOfRoundsContinuation++;

                if (breakLoop || enemyFoundPlayer)
                {
                    foundSomething = false;
                    breakLoop = false;

                    enemyPawnNotMoveForward = false;

                    numberOfRoundsContinuation = 1;

                    moveToLocationAfterEnemyListX.AddRange(moveToForwardPawnListX);
                    moveToLocationAfterEnemyListY.AddRange(moveToForwardPawnListY);

                    numberOfTriesToFindPlayerTower.AddRange(numberOfTriesForPawnToGoForward);

                    numberOfTriesForPawnToGoForward.Clear();

                    moveToForwardPawnListX.Clear();
                    moveToForwardPawnListY.Clear();

                    break;
                }

            }

            #endregion

            #endregion
        }
        else
        {
            numberOfTimesLookingForPlayer = 1;
            numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;
        }

        numberOfTimesLookingForPlayer = 2;
        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;

        currentXOfEnemyListComplete.Clear();
        currentYOfenemyListComplete.Clear();

        currentXOfEnemyListComplete.AddRange(currentXOfEnemyList);
        currentYOfenemyListComplete.AddRange(currentYOfEnemyList);

        currentYOfEnemyList.Clear();
        currentXOfEnemyList.Clear();



    }

    #endregion

    #region Move Pawn

    void MoveToLocationPawn(GameObject pieceToMove)
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
                        else
                        {

                            numberOfTriesToFindPlayerTower.RemoveAt(i);
                            moveToLocationAfterEnemyListX.RemoveAt(i);
                            moveToLocationAfterEnemyListY.RemoveAt(i);

                            // Eftersom jag tar bort från listan måste jag kolla om jag ska sluta söka så jag inte söker med i som är högre än listorna
                            if (i >= numberOfTriesToFindPlayerTower.Count)
                            {

                                break;

                            }

                        }

                    }
                }
            }

        }

        if (moveToLocationAfterEnemyListX.Count == 0 || currentXWhereTowerIsGoingToGo == 1337 || towerWithTheLeastTries == 1337)
        {
            return;
        }

        foreach (GridPiece pieceToMoveTo in gridPieces)
        {

            int xPosToGo = pieceToMoveTo.xPos;
            int yPosToGO = pieceToMoveTo.yPos;

            if (xPosToGo == moveToLocationAfterEnemyListX[towerWithTheLeastTries] && yPosToGO == moveToLocationAfterEnemyListY[towerWithTheLeastTries])
            {
                Debug.Log("MOVE Pawn");

                pieceToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;

                if (boardCreator.bottomPosY == moveToLocationAfterEnemyListY[towerWithTheLeastTries]) // Kollar om det är längst ner (alltså om man ska förvandla sig själv till en drottning)
                {

                    //pieceToMoveTo.currentEnemyType = EnemyType.Queen;
                    pieceToMove.GetComponent<GridPiece>().currentEnemyType = EnemyType.Queen;

                    pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove);
                }
                else
                {

                    pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove);
                }

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                numberOfTriesToFindPlayerTower.Clear();

                enemyPawnObjectList.Clear();

                moveToLocationAfterEnemyListX.Clear();
                moveToLocationAfterEnemyListY.Clear();

                currentXOfEnemyListComplete.Clear();
                currentYOfenemyListComplete.Clear();

                currentYOfEnemyList.Clear();
                currentXOfEnemyList.Clear();

                break;
            }
        }
    }

    #endregion

    #endregion

    #endregion

    #region Buffs

    public void RefreshTurnBuff()
    {

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece piece in gridPieces)
        {

            piece.refreshingTurn = true;

        }

    }

    public void StunEnemyBuff()
    {

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece piece in gridPieces)
        {

            piece.stunningEnemy = true;

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

        delayStartHasRun = true;

        Debug.Log(numberOfEnemys + " Enemys");
        numberOfTimesLookingForPlayerLeft = 1;
    }

    #endregion

    #region Revive

    public void AktivateReviveCanvas(GridPiece whoCalled)
    {

        Time.timeScale = 0;

        reviveObject.SetActive(true);

        whoCalledForRevive = whoCalled;

        Debug.Log("Dead Canvas Aktivate");

    }

    public void DenyRevive()
    {

        Time.timeScale = 1;

        whoCalledForRevive.GetComponent<GridPiece>().playerPieceHere = false;

        howManyPlayerPieces--;

        whoCalledForRevive.currentPieceVisuals.GetComponent<PieceVisual>().DentRevive();

        reviveObject.SetActive(false);

    }

    public void Revive()
    {

        Time.timeScale = 1;

        reviveObject.SetActive(false);

        reviveHappened = true;

        whoCalledForRevive.currentPieceVisuals.GetComponent<PieceVisual>().GoBack();

    }

    #endregion

}
