using NUnit.Framework.Internal;
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

    List<int> numberOfTriesToFindPlayer = new List<int>();
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
    PieceVisual enemyWhoCalled;

    #region Tower

    bool breakLoop = false;
    bool foundSomething;
    int numberOfRound = 1;

    #endregion

    #region Pawn



    #endregion

    #endregion

    #region Upgrades

    #region Player

    #region Pawn

    int pawnHowManyExtraSteeps = 0;
    int pawnMaxHowManyExtraSteeps = 0;

    bool pawnMoveAllDirections = false;
    bool pawnMoveWhereAttack = false;

    #endregion

    #region Horse

    bool horseCanMoveWhereHeJumpsOver = false;
    bool horseCanAttackWhereHeJumpsOver = false;
    bool horseAerialStrike = false;

    int twoMovementInDirectionHorse = 0;
    int oneMovementInDirectionHorse = 0;
    int xDifference;
    int yDifference;

    #endregion

    #region Tower

    bool towerArtillery = false;
    bool towerProtector = false;

    #endregion

    #region Bishop



    #endregion

    #region Queen



    #endregion

    #endregion

    #region Enemy

    #region Pawn

    int enemyPawnHowManyExtraSteeps = 0;

    bool enemyPawnMoveAllDirections = false;
    bool enemyPawnMoveWhereAttack = false;

    bool enemymoveEverywhereFirstTurnUp = false;

    #endregion

    #region Horse



    #endregion

    #region Tower



    #endregion

    #region Bishop



    #endregion

    #region Queen



    #endregion

    #endregion

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

        ReciveUpgrades(); 


        StartCoroutine(DelayStart());
    }

    private void Update()
    {

        if (numberOfEnemys == 0 && delayStartHasRun)
        {
            gameManager = FindAnyObjectByType<GameManagerSr>();

            gameManager.howManyPointsForEnemys += 5;
            gameManager.money += 10;
            gameManager.whatRound++;

            if(gameManager.whatRound >= 6)
            {
                gameManager.whatRound = 0;
                gameManager.money = 5;
                gameManager.howManyPointsForEnemys = 7;

                Debug.Log("WIN");
                sceneLoader.ChangeScene(0);

                return;
            }

            sceneLoader.ChangeScene(2);
        }

        if(howManyPlayerPieces == 0 && Inventory.gameHasStarted) 
        {
            Debug.Log("DED");

            gameManager = FindAnyObjectByType<GameManagerSr>();

            gameManager.whatRound = 0;
            gameManager.money = 5;
            gameManager.howManyPointsForEnemys = 7;

            sceneLoader.ChangeScene(0);
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
        ReciveUpgrades();

        moveFromTileObject = callerGameObject;

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        while (true)
        {

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                if (xPos == currentX && yPos == currentY + 1 + pawnHowManyExtraSteeps)
                {

                    if (allPieces.enemyPieceHere == false)
                    {

                        // maxY Visar en mindre 
                        if (boardCreator.topPosY - 1 <= currentY + 1 + pawnHowManyExtraSteeps)
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

                #region Move All Directions Upgrade

                if (pawnMoveAllDirections)
                {

                    if (xPos == currentX && yPos == currentY - 1 - pawnHowManyExtraSteeps) // Search Down
                    {

                        if (allPieces.enemyPieceHere == false)
                        {

                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Pawn;
                            allPieces.anticipateMovment = true;

                        }
                    }

                    if (xPos == currentX - 1 - pawnHowManyExtraSteeps && yPos == currentY) // Search Left
                    {

                        if (allPieces.enemyPieceHere == false)
                        {

                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Pawn;
                            allPieces.anticipateMovment = true;

                        }
                    }

                    if (xPos == currentX + 1 + pawnHowManyExtraSteeps && yPos == currentY) // Search Right
                    {

                        if (allPieces.enemyPieceHere == false)
                        {

                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Pawn;
                            allPieces.anticipateMovment = true;

                        }
                    }


                }

                    #endregion

            }

            pawnHowManyExtraSteeps--;

            if(pawnHowManyExtraSteeps < 0)
            {
                pawnHowManyExtraSteeps = pawnMaxHowManyExtraSteeps;
                break;

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

                if (allPieces.enemyPieceHere == true)
                {

                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;
                        if (boardCreator.topPosY - 1 <= currentY + 1 + pawnHowManyExtraSteeps)
                        {

                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;
                        }
                        else
                        {
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Pawn;
                        }

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }

                }
                else if (allPieces.playerPieceHere == false && pawnMoveWhereAttack)
                {

                    allPieces.anticipateMovment = true;

                    // maxY Visar en mindre 
                    if (boardCreator.topPosY - 1 <= currentY + 1 + pawnHowManyExtraSteeps)
                    {

                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;
                    }
                    else
                    {
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Pawn;
                    }

                    attackFromTileObjectList.Add(allPieces.gameObject);

                }

            }

            if (xPos == currentX - 1 && yPos == currentY + 1)
            {

                if (allPieces.enemyPieceHere == true)
                {
                    if (firstRoundDone)
                    {

                        allPieces.anticipatePlayerAttack = true;

                        if (boardCreator.topPosY - 1 <= currentY + 1 + pawnHowManyExtraSteeps)
                        {

                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Queen;
                        }
                        else
                        {
                            allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Pawn;
                        }

                        attackFromTileObjectList.Add(allPieces.gameObject);

                    }
                }
                else if (allPieces.playerPieceHere == false && pawnMoveWhereAttack)
                {

                    allPieces.anticipateMovment = true;
                    // maxY Visar en mindre 
                    if (boardCreator.topPosY - 1 <= currentY + 1 + pawnHowManyExtraSteeps)
                    {

                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Queen;
                    }
                    else
                    {
                        allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Pawn;
                    }

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

        ReciveUpgrades();

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        firstTimeSearching = true;

        twoMovementInDirectionHorse = 2;
        oneMovementInDirectionHorse = 1;

        if (!horseCanMoveWhereHeJumpsOver && !horseCanAttackWhereHeJumpsOver)
        {

            twoMovementInDirectionHorse = 2;
            oneMovementInDirectionHorse = 1;

        }

        while (true)
        {

            foreach (GridPiece allPieces in gridPieces)
            {
                int xPos = allPieces.xPos;
                int yPos = allPieces.yPos;

                #region Up

                if (xPos == currentX + oneMovementInDirectionHorse && yPos == currentY + twoMovementInDirectionHorse)
                {
                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone)
                        {

                            if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }
                            else if (horseCanAttackWhereHeJumpsOver) // S� han kan attackera d�r han h�ppar �ver
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }

                        }

                    }
                    else
                    {
                        if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                        else if (horseCanMoveWhereHeJumpsOver) // S� han kan G� d�r han h�ppar �ver
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                    }
                }

                if (xPos == currentX - oneMovementInDirectionHorse && yPos == currentY + twoMovementInDirectionHorse)
                {
                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone)
                        {

                            if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }
                            else if (horseCanAttackWhereHeJumpsOver) // S� han kan attackera d�r han h�ppar �ver
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }

                        }

                    }
                    else
                    {
                        if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                        else if (horseCanMoveWhereHeJumpsOver) // S� han kan G� d�r han h�ppar �ver
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                    }
                }

                #endregion

                #region Down

                if (xPos == currentX + oneMovementInDirectionHorse && yPos == currentY - twoMovementInDirectionHorse)
                {
                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone)
                        {

                            if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }
                            else if (horseCanAttackWhereHeJumpsOver) // S� han kan attackera d�r han h�ppar �ver
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }

                        }

                    }
                    else
                    {
                        if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                        else if (horseCanMoveWhereHeJumpsOver) // S� han kan G� d�r han h�ppar �ver
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                    }
                }

                if (xPos == currentX - oneMovementInDirectionHorse && yPos == currentY - twoMovementInDirectionHorse)
                {
                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone)
                        {

                            if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }
                            else if (horseCanAttackWhereHeJumpsOver) // S� han kan attackera d�r han h�ppar �ver
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }

                        }

                    }
                    else
                    {
                        if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                        else if (horseCanMoveWhereHeJumpsOver) // S� han kan G� d�r han h�ppar �ver
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                    }
                }

                #endregion

                #region Right

                if (xPos == currentX + twoMovementInDirectionHorse && yPos == currentY + oneMovementInDirectionHorse)
                {
                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone)
                        {

                            if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }
                            else if (horseCanAttackWhereHeJumpsOver) // S� han kan attackera d�r han h�ppar �ver
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }

                        }

                    }
                    else
                    {
                        if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                        else if (horseCanMoveWhereHeJumpsOver) // S� han kan G� d�r han h�ppar �ver
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                    }
                }

                if (xPos == currentX + twoMovementInDirectionHorse && yPos == currentY - oneMovementInDirectionHorse)
                {
                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone)
                        {

                            if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }
                            else if (horseCanAttackWhereHeJumpsOver) // S� han kan attackera d�r han h�ppar �ver
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }

                        }

                    }
                    else
                    {
                        if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                        else if (horseCanMoveWhereHeJumpsOver) // S� han kan G� d�r han h�ppar �ver
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                    }
                }

                #endregion

                #region Left

                if (xPos == currentX - twoMovementInDirectionHorse && yPos == currentY + oneMovementInDirectionHorse)
                {
                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone) // So Not Kill Anybody
                        {

                            if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }
                            else if (horseCanAttackWhereHeJumpsOver) // S� han kan attackera d�r han h�ppar �ver
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }


                        }

                    }
                    else
                    {
                        if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                        else if (horseCanMoveWhereHeJumpsOver) // S� han kan G� d�r han h�ppar �ver
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                    }
                }

                if (xPos == currentX - twoMovementInDirectionHorse && yPos == currentY - oneMovementInDirectionHorse)
                {
                    if (allPieces.enemyPieceHere == true)
                    {

                        if (firstRoundDone)
                        {

                            if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }
                            else if (horseCanAttackWhereHeJumpsOver) // S� han kan attackera d�r han h�ppar �ver
                            {

                                allPieces.anticipatePlayerAttack = true;
                                allPieces.currentPlayerAttackType = AnticipatePlayerAttackType.Horse;

                                attackFromTileObjectList.Add(allPieces.gameObject);

                            }

                        }

                    }
                    else
                    {
                        if (firstTimeSearching) // S� den kollar bara efter fiender p� sista platsen du g�r
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                        else if (horseCanMoveWhereHeJumpsOver) // S� han kan G� d�r han h�ppar �ver
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Horse;

                            attackFromTileObjectList.Add(allPieces.gameObject);

                        }
                    }
                }

                #endregion
            }

            firstTimeSearching = false;

            if (!horseCanMoveWhereHeJumpsOver && !horseCanAttackWhereHeJumpsOver)
            {
                firstTimeSearching = true;
                break;

            }

            if(twoMovementInDirectionHorse == 1)
            {
                firstTimeSearching = true;
                break;

            }

            if(oneMovementInDirectionHorse == 1)
            {

                oneMovementInDirectionHorse--;

            }
            else
            {

                twoMovementInDirectionHorse--;

            }

        }

    }

    #endregion

    #region Tower

    public void AnticipateTowerMovment(int currentX, int currentY, GameObject callerGameObject)
    {
        moveFromTileObject = callerGameObject;

        ReciveUpgrades();

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

                        if (towerProtector)
                        {

                            allPieces.anticipateProtection = true;

                        }

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

                        if (towerProtector)
                        {

                            allPieces.anticipateProtection = true;

                        }

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

                        if (towerProtector)
                        {

                            allPieces.anticipateProtection = true;

                        }

                        break;
                    }

                    if (allPieces.enemyPieceHere == true)
                    {

                        foundSomething = true;
                        breakLoop = true;

                        if (firstRoundDone && !towerArtillery)
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

                        if (towerProtector)
                        {

                            allPieces.anticipateProtection = true;

                        }

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

                        if (!towerArtillery)
                        {

                            allPieces.anticipateMovment = true;
                            allPieces.currentPlayerMovmentType = AnticipatePlayerMovmentType.Tower;

                        }

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

    public void movePiece(PlayerType whoCalled, GameObject objectToMoveTo, bool protectingNow)
    {

        #region Upgrades

        switch (whoCalled)
        {

            #region Horse

            case PlayerType.Horse:

                if (horseAerialStrike)
                {

                    xDifference = objectToMoveTo.GetComponent<GridPiece>().xPos - moveFromTileObject.GetComponent<GridPiece>().xPos;
                    yDifference = objectToMoveTo.GetComponent<GridPiece>().yPos - moveFromTileObject.GetComponent<GridPiece>().yPos;

                    int TestInt = 0;
                    if (xDifference == 1 ||  xDifference == -1) // Dosent WOrk properly
                    {

                        #region Up & Down


                        while (true)
                        {
                            TestInt++;
                            foreach (GridPiece allPieces in gridPieces)
                            {
                                int xPos = allPieces.xPos;
                                int yPos = allPieces.yPos;


                                if (xPos == moveFromTileObject.GetComponent<GridPiece>().xPos + xDifference && yPos == moveFromTileObject.GetComponent<GridPiece>().yPos + yDifference)
                                {

                                    if(allPieces.GetComponent<GridPiece>().enemyPieceHere == true)
                                    {

                                        allPieces.GetComponent<GridPiece>().enemyPieceHere = false;
                                        Destroy(allPieces.GetComponent<GridPiece>().currentPieceVisuals);


                                        numberOfEnemys--;

                                    }

                                }

                            }

                            if(xDifference != 0)
                            {

                                xDifference = 0;

                            }
                            else
                            {

                                yDifference--;

                            }

                            if(yDifference == 0)
                            {

                                break;

                            }

                            if (TestInt >= 20) { break; }

                        }

                        #endregion

                    }
                    else
                    {

                        #region Left & Right

                        while (true)
                        {
                            TestInt++;
                            foreach (GridPiece allPieces in gridPieces)
                            {
                                int xPos = allPieces.xPos;
                                int yPos = allPieces.yPos;


                                if (xPos == moveFromTileObject.GetComponent<GridPiece>().xPos + xDifference && yPos == moveFromTileObject.GetComponent<GridPiece>().yPos + yDifference)
                                {

                                    if (allPieces.GetComponent<GridPiece>().enemyPieceHere == true)
                                    {

                                        allPieces.GetComponent<GridPiece>().enemyPieceHere = false;
                                        Destroy(allPieces.GetComponent<GridPiece>().currentPieceVisuals);


                                        numberOfEnemys--;

                                    }

                                }

                            }

                            if (yDifference != 0)
                            {

                                yDifference = 0;

                            }
                            else
                            {

                                xDifference--;

                            }

                            if (xDifference == 0)
                            {
                                break;

                            }

                            if (TestInt >= 20) { break; }

                        }

                        #endregion

                    }

                }

                break;

            #endregion

        }

        if (towerProtector && protectingNow)
        {

            objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(moveFromTileObject, true, false, objectToMoveTo, towerProtector);

        }

        #endregion

        if (!towerProtector)
        {

            moveFromTileObject.GetComponent<GridPiece>().currentPlayerType = whoCalled; // I movePiece() S�ts den falsk efter

        }

        moveFromTileObject.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(objectToMoveTo, true, false, moveFromTileObject, protectingNow);

    }

    public void AttackPiece(PlayerType whoCalled, GameObject objectToMoveTo)
    {

        #region Upgrades

        switch (whoCalled)
        {

            case PlayerType.Tower:

                if (towerArtillery)
                {

                    yDifference = objectToMoveTo.GetComponent<GridPiece>().yPos - moveFromTileObject.GetComponent<GridPiece>().yPos;

                    if(yDifference > 0) // �ver den allts� 1 eller st�rre
                    {

                        objectToMoveTo.GetComponent<GridPiece>().enemyPieceHere = false;
                        Destroy(objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals);

                        moveFromTileObject.GetComponent<GridPiece>().movedOnce = true;

                        numberOfEnemys--;

                        return;

                    }

                }

                break;

        }

        #endregion

        moveFromTileObject.GetComponent<GridPiece>().currentPlayerType = whoCalled;
        moveFromTileObject.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(objectToMoveTo, true, true, moveFromTileObject, false);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i]);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 2, enemyHorseObjectList[i]);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i]);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i]);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyHorseObjectList[i]);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 2, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyHorseObjectList[i]);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i]);

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
            FindPlayerHorseLoop(enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyHorseObjectList[i].GetComponent<GridPiece>().yPos + 2, enemyHorseObjectList[i]);

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

    public void FindPlayerHorseLoop(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY,   int posToLookAtX, int posToLookAtY, GameObject pieceToMove)
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


                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 1, currentYOfenemyListComplete[i] - 2, pieceToMove);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 1, currentYOfenemyListComplete[i] - 2, pieceToMove);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 2, currentYOfenemyListComplete[i] - 1, pieceToMove);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 2, currentYOfenemyListComplete[i] + 1, pieceToMove);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 2, currentYOfenemyListComplete[i] - 1, pieceToMove);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 2, currentYOfenemyListComplete[i] + 1, pieceToMove);
                }

                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] - 1, currentYOfenemyListComplete[i] + 2, pieceToMove);
                }
                if (!enemyFoundPlayer && currentAmountOfTries <= maxNumberOfTries)
                {
                    FindPlayerHorseLoop(posToMoveToAfterFindingPlayerX, posToMoveToAfterFindingPlayerY,    currentXOfEnemyListComplete[i] + 1, currentYOfenemyListComplete[i] + 2, pieceToMove);
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

        for (int i = 0; i < numberOfTriesToFindPlayer.Count; i++)
        {

            if (numberOfTriesToFindPlayer[i] < currentXWhereTowerIsGoingToGo)
            {

                foreach (GridPiece pieceToMoveTo in gridPieces)
                {

                    int xPosToGo = pieceToMoveTo.xPos;
                    int yPosToGO = pieceToMoveTo.yPos;

                    if (xPosToGo == moveToLocationAfterEnemyListX[i] && yPosToGO == moveToLocationAfterEnemyListY[i])
                    {

                        if (pieceToMoveTo.enemyPieceHere == false)
                        {


                            //Kollar Om Hur F�rs�ken �r H�gre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayer[i];

                            //N�r Man V�l Ska Flytta Spelaren S� Beh�ver Jag Veta Vilken i Den Var P�
                            towerWithTheLeastTries = i;


                        }
                        else
                        {

                            numberOfTriesToFindPlayer.RemoveAt(i);
                            moveToLocationAfterEnemyListX.Remove(i);
                            moveToLocationAfterEnemyListY.Remove(i);

                            // Eftersom jag tar bort fr�n listan m�ste jag kolla om jag ska sluta s�ka s� jag inte s�ker med i som �r h�gre �n listorna
                            if (i >= numberOfTriesToFindPlayer.Count)
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
                pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove, false);


                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                numberOfTriesToFindPlayer.Clear();

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

                        // 2 F�r det subtraheras 1 p� slutet
                        numberOfTimesLookingForPlayer = 1;
                        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;


                        numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

        for (int i = 0; i < numberOfTriesToFindPlayer.Count; i++)
        {


            if (numberOfTriesToFindPlayer[i] < currentXWhereTowerIsGoingToGo)
            {

                foreach (GridPiece pieceToMoveTo in gridPieces)
                {

                    int xPosToGo = pieceToMoveTo.xPos;
                    int yPosToGO = pieceToMoveTo.yPos;

                    if (xPosToGo == moveToLocationAfterEnemyListX[i] && yPosToGO == moveToLocationAfterEnemyListY[i])
                    {

                        if (pieceToMoveTo.enemyPieceHere == false)
                        {

                            //Kollar Om Hur F�rs�ken �r H�gre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayer[i];
                            //N�r Man V�l Ska Flytta Spelaren S� Beh�ver Jag Veta Vilken i Den Var P�
                            towerWithTheLeastTries = i;


                        }
                        else
                        {

                            numberOfTriesToFindPlayer.RemoveAt(i);
                            moveToLocationAfterEnemyListX.Remove(i);
                            moveToLocationAfterEnemyListY.Remove(i);

                            // Eftersom jag tar bort fr�n listan m�ste jag kolla om jag ska sluta s�ka s� jag inte s�ker med i som �r h�gre �n listorna
                            if (i >= numberOfTriesToFindPlayer.Count)
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
                pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove, false);

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                numberOfTriesToFindPlayer.Clear();

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

                        // 2 F�r det subtraheras 1 p� slutet
                        numberOfTimesLookingForPlayer = 1;
                        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;


                        numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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



        for (int i = 0; i < numberOfTriesToFindPlayer.Count; i++)
        {

            if (numberOfTriesToFindPlayer[i] < currentXWhereTowerIsGoingToGo)
            {

                foreach (GridPiece pieceToMoveTo in gridPieces)
                {

                    int xPosToGo = pieceToMoveTo.xPos;
                    int yPosToGO = pieceToMoveTo.yPos;

                    if (xPosToGo == moveToLocationAfterEnemyListX[i] && yPosToGO == moveToLocationAfterEnemyListY[i])
                    {

                        if (pieceToMoveTo.enemyPieceHere == false)
                        {


                            //Kollar Om Hur F�rs�ken �r H�gre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayer[i];

                            //N�r Man V�l Ska Flytta Spelaren S� Beh�ver Jag Veta Vilken i Den Var P�
                            towerWithTheLeastTries = i;


                        }
                        else
                        {

                            numberOfTriesToFindPlayer.RemoveAt(i);
                            moveToLocationAfterEnemyListX.Remove(i);
                            moveToLocationAfterEnemyListY.Remove(i);

                            // Eftersom jag tar bort fr�n listan m�ste jag kolla om jag ska sluta s�ka s� jag inte s�ker med i som �r h�gre �n listorna
                            if (i >= numberOfTriesToFindPlayer.Count)
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
                pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove, false);

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                numberOfTriesToFindPlayer.Clear();

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

                        // 2 F�r det subtraheras 1 p� slutet
                        numberOfTimesLookingForPlayer = 1;
                        numberOfTimesLookingForPlayerLeft = numberOfTimesLookingForPlayer;


                        numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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
                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                                numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

        for (int i = 0; i < numberOfTriesToFindPlayer.Count; i++)
        {

            if (numberOfTriesToFindPlayer[i] < currentXWhereTowerIsGoingToGo)
            {

                foreach (GridPiece pieceToMoveTo in gridPieces)
                {

                    int xPosToGo = pieceToMoveTo.xPos;
                    int yPosToGO = pieceToMoveTo.yPos;

                    if (xPosToGo == moveToLocationAfterEnemyListX[i] && yPosToGO == moveToLocationAfterEnemyListY[i])
                    {

                        if (pieceToMoveTo.enemyPieceHere == false)
                        {


                            //Kollar Om Hur F�rs�ken �r H�gre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayer[i];

                            //N�r Man V�l Ska Flytta Spelaren S� Beh�ver Jag Veta Vilken i Den Var P�
                            towerWithTheLeastTries = i;


                        }
                        else
                        {

                            numberOfTriesToFindPlayer.RemoveAt(i);
                            moveToLocationAfterEnemyListX.Remove(i);
                            moveToLocationAfterEnemyListY.Remove(i);

                            // Eftersom jag tar bort fr�n listan m�ste jag kolla om jag ska sluta s�ka s� jag inte s�ker med i som �r h�gre �n listorna
                            if (i >= numberOfTriesToFindPlayer.Count)
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
                pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove, false);

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                enemyQueenObjectList.Clear();

                numberOfTriesToFindPlayer.Clear();

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

        ReciveEnemyUpgrades();

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

            FindPlayerPawn(enemyPawnObjectList[i].GetComponent<GridPiece>().xPos, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyPawnObjectList[i].GetComponent<GridPiece>().xPos, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos, enemyPawnObjectList[i], true);

            #endregion

            #region Upgrades

            #region Move all directions

            if (enemyPawnMoveAllDirections)
            {
                FindPlayerPawn(enemyPawnObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos, enemyPawnObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos, enemyPawnObjectList[i], false);

                FindPlayerPawn(enemyPawnObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos, enemyPawnObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos, enemyPawnObjectList[i], false);

                enemymoveEverywhereFirstTurnUp = true;
                FindPlayerPawn(enemyPawnObjectList[i].GetComponent<GridPiece>().xPos, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyPawnObjectList[i].GetComponent<GridPiece>().xPos, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos + 1, enemyPawnObjectList[i], false);

            }

            #endregion

            #region Move Where Attack

            if (enemyPawnMoveWhereAttack)
            {

                FindPlayerPawn(enemyPawnObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyPawnObjectList[i].GetComponent<GridPiece>().xPos - 1, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyPawnObjectList[i], false);

                FindPlayerPawn(enemyPawnObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyPawnObjectList[i].GetComponent<GridPiece>().xPos + 1, enemyPawnObjectList[i].GetComponent<GridPiece>().yPos - 1, enemyPawnObjectList[i], false);

            }

            #endregion

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

    public void FindPlayerPawn(int posToMoveToAfterFindingPlayerX, int posToMoveToAfterFindingPlayerY, int posToLookAtX, int posToLookAtY, GameObject pieceToMove, bool moveDown)
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

                        numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                        numberOfTriesToFindPlayer.Add(currentAmountOfTries);
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

                if (xPos == posToLookAtX && yPos == posToLookAtY - numberOfRoundsContinuation)
                {

                    if (allPieces.playerPieceHere == true)
                    {
                        // Spelare d�r
                        if (firstTimeSearching) // Den �r rakt framf�r mig, allts� sluta s�k
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
                        if (!enemymoveEverywhereFirstTurnUp) // Upgraderingen som kollar ett steg upp k�r s� den inte avbryter f�r den uppt�cker sin d�tida position
                        {
                            // Fiende D�r
                            if (firstTimeSearching) // Den �r rakt framf�r mig, allts� sluta s�k
                            {
                                foundSomething = true;
                                breakLoop = true;
                            }
                            foundSomething = true;

                            break;

                        }


                    }
                    else
                    {
                        // Empty Board Piece
                        foundSomething = true;
                        breakLoop = false;

                    }

                }

                #endregion

            }

            foundSomething = false;
            firstTimeSearching = false;
            enemymoveEverywhereFirstTurnUp = false;
            numberOfRoundsContinuation++;

            if (breakLoop || enemyFoundPlayer)
            {
                #region Move extra Steps Upgrade

                if (moveDown && enemyPawnHowManyExtraSteeps != 0)
                {

                    moveToForwardPawnListX.Add(posToMoveToAfterFindingPlayerX);

                    int withHowManyLessSteps = 0;
                    bool breakExtraSteeps = false;

                    for (int i = 0; i < enemyPawnHowManyExtraSteeps; i++)
                    {

                        foreach (GridPiece allPieces2 in gridPieces)
                        {
                            int xPos2 = allPieces2.xPos;
                            int yPos2 = allPieces2.yPos;

                            if (xPos2 == posToLookAtX && yPos2 == posToLookAtY - i - 1) // -1 f�r annars kollar den p� sin egan pos
                            {

                                if (allPieces2.playerPieceHere || allPieces2.enemyPieceHere)
                                {
                                    // Piece here
                                    breakExtraSteeps = true;
                                    break;

                                }
                                if (allPieces2.enemyPieceHere == false && allPieces2.playerPieceHere == false)
                                {
    
                                    withHowManyLessSteps = i;

                                }


                            }

                        }

                        if (breakExtraSteeps)
                        {

                            break;
                        }

                    }

                    moveToForwardPawnListY.Add(posToMoveToAfterFindingPlayerY - withHowManyLessSteps); // L�gger till ett extra steg(Den startar p� 0)

                    numberOfTriesForPawnToGoForward.Add(420 - withHowManyLessSteps); // G�r S� Att om den kan g� fram extra steg s� tar den prio

                }

                #endregion

                foundSomething = false;
                breakLoop = false;

                enemyPawnNotMoveForward = false;

                numberOfRoundsContinuation = 1;
                enemyPawnHowManyExtraSteeps = 0;

                moveToLocationAfterEnemyListX.AddRange(moveToForwardPawnListX);
                moveToLocationAfterEnemyListY.AddRange(moveToForwardPawnListY);

                numberOfTriesToFindPlayer.AddRange(numberOfTriesForPawnToGoForward);

                numberOfTriesForPawnToGoForward.Clear();

                moveToForwardPawnListX.Clear();
                moveToForwardPawnListY.Clear();

                break;
            }

        }

        #endregion

        #endregion



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

        for (int i = 0; i < numberOfTriesToFindPlayer.Count; i++)
        {


            if (numberOfTriesToFindPlayer[i] < currentXWhereTowerIsGoingToGo)
            {

                foreach (GridPiece pieceToMoveTo in gridPieces)
                {

                    int xPosToGo = pieceToMoveTo.xPos;
                    int yPosToGO = pieceToMoveTo.yPos;

                    if (xPosToGo == moveToLocationAfterEnemyListX[i] && yPosToGO == moveToLocationAfterEnemyListY[i])
                    {

                        if (pieceToMoveTo.enemyPieceHere == false)
                        {


                            //Kollar Om Hur F�rs�ken �r H�gre Eller Mindre
                            currentXWhereTowerIsGoingToGo = numberOfTriesToFindPlayer[i];
                            //N�r Man V�l Ska Flytta Spelaren S� Beh�ver Jag Veta Vilken i Den Var P�
                            towerWithTheLeastTries = i;


                        }
                        else
                        {

                            numberOfTriesToFindPlayer.RemoveAt(i);
                            moveToLocationAfterEnemyListX.RemoveAt(i);
                            moveToLocationAfterEnemyListY.RemoveAt(i);

                            // Eftersom jag tar bort fr�n listan m�ste jag kolla om jag ska sluta s�ka s� jag inte s�ker med i som �r h�gre �n listorna
                            if (i >= numberOfTriesToFindPlayer.Count)
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

                if (boardCreator.bottomPosY == moveToLocationAfterEnemyListY[towerWithTheLeastTries]) // Kollar om det �r l�ngst ner (allts� om man ska f�rvandla sig sj�lv till en drottning)
                {

                    //pieceToMoveTo.currentEnemyType = EnemyType.Queen;
                    pieceToMove.GetComponent<GridPiece>().currentEnemyType = EnemyType.Queen;

                    pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove, false);
                }
                else
                {

                    pieceToMove.GetComponent<GridPiece>().currentPieceVisuals.GetComponent<PieceVisual>().MovePiece(pieceToMoveTo.gameObject, false, false, pieceToMove, false);
                }

                currentXWhereTowerIsGoingToGo = 1337;
                towerWithTheLeastTries = 1337;

                numberOfTriesToFindPlayer.Clear();

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

    public void AktivateReviveCanvas(GridPiece whoCalled, PieceVisual EnemyWhoCalled)
    {

        Time.timeScale = 0;

        reviveObject.SetActive(true);

        reviveHappened = true;

        whoCalledForRevive = whoCalled;
        enemyWhoCalled = EnemyWhoCalled;

    }

    public void DenyRevive()
    {

        Time.timeScale = 1;

        whoCalledForRevive.GetComponent<GridPiece>().playerPieceHere = false;
        enemyWhoCalled.waitUntilReviveDone = false;

        howManyPlayerPieces--;

        reviveHappened = false;

        whoCalledForRevive.currentPieceVisuals.GetComponent<PieceVisual>().DenyRevive();

        reviveObject.SetActive(false);

    }

    public void Revive()
    {

        gameManager = FindAnyObjectByType<GameManagerSr>();

        if(gameManager.money >= 3)
        {

            Time.timeScale = 1;

            reviveObject.SetActive(false);

            whoCalledForRevive.currentPieceVisuals.GetComponent<PieceVisual>().GoBack();
            enemyWhoCalled.waitUntilReviveDone = false;

            gameManager.money -= 3;

        }

    }

    #endregion

    #region Recive Upgrades

    void ReciveUpgrades() // G�r till gamemanager och f�r alla upgraderingar 
    {
        gameManager = FindAnyObjectByType<GameManagerSr>();

        pawnMaxHowManyExtraSteeps = gameManager.howManyExtraSteepsPawn;
        pawnHowManyExtraSteeps = pawnMaxHowManyExtraSteeps;
        pawnMoveAllDirections = gameManager.pawmMoveAllDirections;
        pawnMoveWhereAttack = gameManager.pawnMoveWhereAttack;

        horseCanMoveWhereHeJumpsOver = gameManager.horseCanMoveWhereHeJumpsOver;
        horseCanAttackWhereHeJumpsOver = gameManager.horseCanAttackWhereHeJumpsOver;
        horseAerialStrike = gameManager.horseAerialStrike;

        towerArtillery = gameManager.towerArtillery;
        towerProtector = gameManager.towerProtect;

    }

    public void ReciveEnemyUpgrades()
    {

        gameManager = FindObjectOfType<GameManagerSr>();

        enemyPawnHowManyExtraSteeps = gameManager.enemyPawnHowManyExtraSteeps;
        enemyPawnMoveAllDirections = gameManager.enemyPawnMoveAllDirections;
        enemyPawnMoveWhereAttack = gameManager.enemyPawnMoveWhereAttack;

    }

    #endregion

}
