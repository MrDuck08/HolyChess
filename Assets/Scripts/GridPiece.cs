using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums

public enum PlayerType
{
    Pawn = 0,
    Tower = 1,
    Bishop = 2,
    Queen = 3,
    Horse = 4,
    total,
    none,

}

public enum EnemyType
{
    Pawn = 0,
    Tower = 1,
    Bishop = 2,
    Queen = 3,
    Horse = 4,
    total,
    none,

}

public enum AnticipatePlayerMovmentType
{
    Pawn = 0,
    Tower = 1,
    Bishop = 2,
    Queen = 3,
    Horse = 4,
    total,
    none,

}

public enum AnticipatePlayerAttackType
{
    Pawn = 0,
    Tower = 1,
    Bishop = 2,
    Queen = 3,
    Horse = 4,
    total,
    none,

}

public enum SpawnType
{
    Pawn = 0,
    Tower = 1,
    Bishop = 2,
    Queen = 3,
    Horse = 4,
    total,
    none,

}

#endregion

public class GridPiece : MonoBehaviour
{

    [Header("Piece")]

    [SerializeField] GameObject pieceVisuals;
    public GameObject currentPieceVisuals;
    [SerializeField] GameObject soundObject;
 
    [Header("Position")]

    public int xPos;
    public int yPos;

    bool playerSpawnGrid = false;

    bool mouseOver = false;

    #region Player Pieces

    [Header("Player")]

    public PlayerType currentPlayerType;
    public bool playerPieceHere = false;
    public bool refreshingTurn = false;

    #endregion

    #region Enemy Pieces

    [Header("Enemy")]

    public bool stunningEnemy = false;
    bool ignoreNextTurn = false;

    public EnemyType currentEnemyType;
    public bool enemyPieceHere = false;

    #endregion

    #region Player Movment & Attack

    [Header("Player Movment")]

    public AnticipatePlayerMovmentType currentPlayerMovmentType; 
    public bool anticipateMovment;
    public bool anticipateProtection = false;

    [Header("Player Attack")]

    public AnticipatePlayerAttackType currentPlayerAttackType;
    public bool anticipatePlayerAttack;

    public bool movedOnce = false;

    #endregion

    #region Spawning & Turns

    [Header("Spawn")]

    public SpawnType currentSpawnType;

    public bool gameHasStarted = false;
    public bool placingDownAUnitNow = false;
    public bool playerTurn = true;

    #endregion

    #region Info

    bool infoIsHere = false;
    bool noLongerShowInfo = false;

    #endregion

    Color32 anticipateMovemtVisualls;

    GridController controller;
    GameManagerSr gameManagerSr;

    GridPiece[] gridPieces;

    #region Start & Awake

    private void Awake()
    {

        currentPlayerType = PlayerType.none;
        currentEnemyType = EnemyType.none;
        currentPlayerMovmentType = AnticipatePlayerMovmentType.none;
        currentPlayerAttackType = AnticipatePlayerAttackType.none;
        currentSpawnType = SpawnType.none;

    }

    private void Start()
    {
        controller = FindObjectOfType<GridController>();

    }

    #endregion

    #region Spawn info

    public void SpawnLocation(int x, int y, int spawnWho, Color32 whatVisualls)
    {
        xPos = x;
        yPos = y;
        anticipateMovemtVisualls = whatVisualls;

        if(spawnWho == 0)
        {
            //Player Grid

            playerSpawnGrid = true;
        }

        gameObject.GetComponent<SpriteRenderer>().color = anticipateMovemtVisualls;

        transform.position = new Vector2 (xPos, yPos);
    }

    public void SpawnEnemy(int whatEnemyToSpawn)
    {
        enemyPieceHere = true;

        switch (whatEnemyToSpawn)
        {

            case 0:

                currentEnemyType = EnemyType.Pawn;

                currentPieceVisuals = Instantiate(pieceVisuals);

                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentEnemyType, new Vector2(xPos, yPos), false);

                break;

            case 1:

                currentEnemyType = EnemyType.Horse;

                currentPieceVisuals = Instantiate(pieceVisuals);

                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentEnemyType, new Vector2(xPos, yPos), false);

                break;

            case 2:

                currentEnemyType = EnemyType.Tower;

                currentPieceVisuals = Instantiate(pieceVisuals);

                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentEnemyType, new Vector2(xPos, yPos), false);

                break;

            case 3:

                currentEnemyType = EnemyType.Bishop;

                currentPieceVisuals = Instantiate(pieceVisuals);

                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentEnemyType, new Vector2(xPos, yPos), false);

                break;

            case 4:

                currentEnemyType = EnemyType.Queen;

                currentPieceVisuals = Instantiate(pieceVisuals);

                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentEnemyType, new Vector2(xPos, yPos), false);

                break;

        }

    }

    #endregion

    #region Update

    private void Update()
    {

        #region Check If Dead

        if (!playerPieceHere)
        {
            currentPlayerType = PlayerType.none;
            //Destroy(currentPieceVisuals);
        }
        else if (currentPieceVisuals == null)
        {

            //currentPieceVisuals = Instantiate(pieceVisuals);

            //currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentPlayerType, new Vector2(xPos, yPos), false);

        }

        if (!enemyPieceHere)
        {

            currentEnemyType = EnemyType.none;
            //Destroy(currentPieceVisuals);

        }
        else if (currentPieceVisuals == null && currentEnemyType != EnemyType.none)
        {

            currentPieceVisuals = Instantiate(pieceVisuals);

            currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentEnemyType, new Vector2(xPos, yPos), false);

        }

        #endregion

        #region Anticipate Movment

        if (anticipateMovment && !movedOnce && !playerPieceHere)
        {
            anticipateMovemtVisualls.a = 144;
            gameObject.GetComponent<SpriteRenderer>().color = anticipateMovemtVisualls;
        }
        else
        {
            anticipateMovemtVisualls.a = 255;
            gameObject.GetComponent<SpriteRenderer>().color = anticipateMovemtVisualls;
        }

        if (anticipateProtection && !movedOnce)
        {

            gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        }

        if (anticipatePlayerAttack && !movedOnce)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }


        #endregion

        #region Movment + Spawn

        if (Input.GetMouseButtonDown(0))
        {
            
            if (mouseOver)
            {

                if (infoIsHere)
                {
                    gameManagerSr = FindAnyObjectByType<GameManagerSr>();

                    gameManagerSr.StopShowingInfo();
                    // Jag s�tter inte att det inte l�ngre �r info h�r f�r att lura programet att inte direkt aktivera den
                    noLongerShowInfo = true;
                }

                #region Play Sound

                switch (currentPlayerType)
                {

                    case PlayerType.Pawn:

                        GameObject sound = Instantiate(soundObject);

                        sound.GetComponent<SoundManager>().PlaySound("Pawn");

                        break;

                }

                #endregion

                if (playerTurn)
                {

                    #region Anticipate Movment && Refresh Turn

                    if (gameHasStarted && playerPieceHere)
                    {
                        if (!movedOnce && !anticipateProtection) // Kollar om man har r�rt sig en g�ng s� man inte kan g� 2 g�nger i rad
                        {
                            bool animationIsPlaying = false;

                            PieceVisual[] allPieceVisuals = FindObjectsOfType(typeof(PieceVisual)) as PieceVisual[];

                            foreach (var pieceVisual in allPieceVisuals)
                            {

                                if(pieceVisual.isItPlayer == false && pieceVisual.animationIsPlaying)
                                {
                                    animationIsPlaying = true;
                                }

                            }

                            if (!animationIsPlaying)
                            {

                                #region Stop Antiticating If Clicked On Other Player Piece

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                                foreach (GridPiece allPiece in gridPieces)
                                {
                                    allPiece.anticipateMovment = false;

                                    allPiece.currentPlayerAttackType = AnticipatePlayerAttackType.none;
                                }

                                foreach (GridPiece allPiece in gridPieces)
                                {
                                    allPiece.anticipatePlayerAttack = false;

                                    allPiece.currentPlayerAttackType = AnticipatePlayerAttackType.none;
                                }

                                #endregion

                                #region Checking Who Wants To Move

                                if (currentPlayerType == PlayerType.Pawn)
                                {
                                    controller.AnticipatePawnMovment(xPos, yPos, gameObject);
                                    controller.AnticipatePawnAttack(xPos, yPos, gameObject);
                                }

                                if (currentPlayerType == PlayerType.Horse)
                                {
                                    controller.AnticipateHorseMovment(xPos, yPos, gameObject);
                                }

                                if (currentPlayerType == PlayerType.Tower)
                                {
                                    controller.AnticipateTowerMovment(xPos, yPos, gameObject);
                                }

                                if (currentPlayerType == PlayerType.Bishop)
                                {
                                    controller.AnticipateBishopMovment(xPos, yPos, gameObject);
                                }

                                if (currentPlayerType == PlayerType.Queen)
                                {
                                    controller.AnticipateQueenMovment(xPos, yPos, gameObject);
                                }

                                #endregion

                            }

                        }
                        else if (refreshingTurn)
                        {

                            movedOnce = false;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {

                                allPiece.refreshingTurn = false;

                            }

                        }
                    }

                    #endregion

                    if (!playerPieceHere)
                    {

                        #region Start Spawn

                        if (playerSpawnGrid && !gameHasStarted)
                        {
                            if (currentSpawnType == SpawnType.Pawn)
                            {

                                playerPieceHere = true;
                                currentPlayerType = PlayerType.Pawn;
                                controller.howManyPlayerPieces++;

                                currentPieceVisuals = Instantiate(pieceVisuals);

                                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentPlayerType, new Vector2(xPos, yPos), true);

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                foreach (Transform child in gameObject.transform)
                                {
                                    if (child.tag == "Pawn")
                                    {
                                        child.gameObject.SetActive(false);
                                    }

                                }

                                foreach (GridPiece allPiece in gridPieces)
                                {
                                    allPiece.currentSpawnType = SpawnType.none;
                                    allPiece.placingDownAUnitNow = false;
                                }
                            }

                            if (currentSpawnType == SpawnType.Horse)
                            {

                                playerPieceHere = true;
                                currentPlayerType = PlayerType.Horse;
                                controller.howManyPlayerPieces++;

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                currentPieceVisuals = Instantiate(pieceVisuals);

                                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentPlayerType, new Vector2(xPos, yPos), true);

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                foreach (Transform child in gameObject.transform)
                                {
                                    if (child.tag == "PlayerHorse")
                                    {
                                        child.gameObject.SetActive(false);
                                    }

                                }


                                foreach (GridPiece allPiece in gridPieces)
                                {
                                    allPiece.currentSpawnType = SpawnType.none;
                                    allPiece.placingDownAUnitNow = false;
                                }
                            }

                            if (currentSpawnType == SpawnType.Tower)
                            {

                                playerPieceHere = true;
                                currentPlayerType = PlayerType.Tower;
                                controller.howManyPlayerPieces++;

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                currentPieceVisuals = Instantiate(pieceVisuals);

                                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentPlayerType, new Vector2(xPos, yPos), true);

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                foreach (Transform child in gameObject.transform)
                                {
                                    if (child.tag == "PlayerTower")
                                    {
                                        child.gameObject.SetActive(false);
                                    }

                                }


                                foreach (GridPiece allPiece in gridPieces)
                                {
                                    allPiece.currentSpawnType = SpawnType.none;
                                    allPiece.placingDownAUnitNow = false;
                                }
                            }

                            if (currentSpawnType == SpawnType.Bishop)
                            {

                                playerPieceHere = true;
                                currentPlayerType = PlayerType.Bishop;
                                controller.howManyPlayerPieces++;

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                currentPieceVisuals = Instantiate(pieceVisuals);

                                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentPlayerType, new Vector2(xPos, yPos), true);

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                foreach (Transform child in gameObject.transform)
                                {
                                    if (child.tag == "PlayerBishop")
                                    {
                                        child.gameObject.SetActive(false);
                                    }

                                }


                                foreach (GridPiece allPiece in gridPieces)
                                {
                                    allPiece.currentSpawnType = SpawnType.none;
                                    allPiece.placingDownAUnitNow = false;
                                }
                            }

                            if (currentSpawnType == SpawnType.Queen)
                            {

                                playerPieceHere = true;
                                currentPlayerType = PlayerType.Queen;
                                controller.howManyPlayerPieces++;

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                currentPieceVisuals = Instantiate(pieceVisuals);

                                currentPieceVisuals.GetComponent<PieceVisual>().SpawnInfo((int)currentPlayerType, new Vector2(xPos, yPos), true);

                                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];


                                foreach (Transform child in gameObject.transform)
                                {
                                    if (child.tag == "PlayerQueen")
                                    {
                                        child.gameObject.SetActive(false);
                                    }

                                }


                                foreach (GridPiece allPiece in gridPieces)
                                {
                                    allPiece.currentSpawnType = SpawnType.none;
                                    allPiece.placingDownAUnitNow = false;
                                }
                            }
                        }

                        #endregion

                        #region Movment

                        if (anticipateMovment)
                        {
                            #region Who Is Moving

                            if (currentPlayerMovmentType == AnticipatePlayerMovmentType.Pawn)
                            {

                                controller.movePiece(PlayerType.Pawn, gameObject, false);
                            }

                            if (currentPlayerMovmentType == AnticipatePlayerMovmentType.Horse)
                            {

                                controller.movePiece(PlayerType.Horse, gameObject, false);
                            }


                            if (currentPlayerMovmentType == AnticipatePlayerMovmentType.Tower)
                            {

                                controller.movePiece(PlayerType.Tower, gameObject, false);
                            }

                            if (currentPlayerMovmentType == AnticipatePlayerMovmentType.Bishop)
                            {

                                controller.movePiece(PlayerType.Bishop, gameObject, false);
                            }

                            if (currentPlayerMovmentType == AnticipatePlayerMovmentType.Queen)
                            {

                                controller.movePiece(PlayerType.Queen, gameObject, false);
                            }

                            #endregion

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.anticipateMovment = false;

                                allPiece.currentPlayerMovmentType = AnticipatePlayerMovmentType.none;
                            }

                        }
                        else
                        {
                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.anticipateMovment = false;

                                allPiece.currentPlayerMovmentType = AnticipatePlayerMovmentType.none;
                            }
                        }
                        #endregion

                        #region Attacking

                        if (anticipatePlayerAttack)
                        {

                            #region Who Is Attacking

                            if (currentPlayerAttackType == AnticipatePlayerAttackType.Pawn)
                            {

                                controller.AttackPiece(PlayerType.Pawn, gameObject);
                            }

                            if (currentPlayerAttackType == AnticipatePlayerAttackType.Horse)
                            {

                                controller.AttackPiece(PlayerType.Horse, gameObject);
                            }

                            if (currentPlayerAttackType == AnticipatePlayerAttackType.Tower)
                            {
                                Debug.Log("Attack Tower");
                                controller.AttackPiece(PlayerType.Tower, gameObject);
                            }

                            if (currentPlayerAttackType == AnticipatePlayerAttackType.Bishop)
                            {

                                controller.AttackPiece(PlayerType.Bishop, gameObject);
                            }

                            if (currentPlayerAttackType == AnticipatePlayerAttackType.Queen)
                            {

                                controller.AttackPiece(PlayerType.Queen, gameObject);
                            }

                            #endregion


                            enemyPieceHere = false;



                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.anticipatePlayerAttack = false;

                                allPiece.currentPlayerAttackType = AnticipatePlayerAttackType.none;
                            }

                        }
                        else
                        {
                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.anticipatePlayerAttack = false;

                                allPiece.currentPlayerAttackType = AnticipatePlayerAttackType.none;
                            }
                        }

                        #endregion

                    }


                    #region Protecting

                    if (anticipateProtection)
                    {

                        controller.movePiece(currentPlayerType, gameObject, true);

                        anticipateProtection = false;

                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipateMovment = false;

                            allPiece.currentPlayerMovmentType = AnticipatePlayerMovmentType.none;
                        }

                    }

                    #endregion

                    if (enemyPieceHere && stunningEnemy)
                    {

                        ignoreNextTurn = true;

                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {

                            allPiece.stunningEnemy = false;

                        }

                    }

                }

            }
        }

        #endregion

        #region Enemy Movments

        if (!playerTurn && enemyPieceHere)
        {
            playerTurn = true;

            if (!ignoreNextTurn)
            {

                if (currentEnemyType == EnemyType.Horse)
                {

                    controller.EnemyHorseMovmentCall(xPos, yPos, gameObject);

                }

                if (currentEnemyType == EnemyType.Tower)
                {

                    controller.EnemyTowerMovmentCall(xPos, yPos, gameObject);

                }

                if (currentEnemyType == EnemyType.Bishop)
                {

                    controller.EnemyBishopMovmentCall(xPos, yPos, gameObject);

                }

                if (currentEnemyType == EnemyType.Queen)
                {

                    controller.EnemyQueenMovmentCall(xPos, yPos, gameObject);

                }

                if (currentEnemyType == EnemyType.Pawn)
                {

                    controller.EnemyPawnMovmentCall(xPos, yPos, gameObject);

                }

            }

            ignoreNextTurn = false;

        }

        #endregion
    }

    #endregion

    #region Mouse Over & Exit

    private void OnMouseOver()
    {
        #region Spawning

        if (playerSpawnGrid && !gameHasStarted && !playerPieceHere)
        {
            if (currentSpawnType == SpawnType.Pawn)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "Pawn")
                    {
                        child.gameObject.SetActive(true);
                    }

                }
            }

            if (currentSpawnType == SpawnType.Horse)
            {
                foreach(Transform child in gameObject.transform)
                {
                    if(child.tag == "PlayerHorse")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            if (currentSpawnType == SpawnType.Tower)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerTower")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            if (currentSpawnType == SpawnType.Bishop)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerBishop")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            if (currentSpawnType == SpawnType.Queen)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerQueen")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }

        #endregion

        if (gameHasStarted && !noLongerShowInfo && !infoIsHere)
        {
            if(playerPieceHere || enemyPieceHere)
            {

                infoIsHere = true;

                gameManagerSr = FindAnyObjectByType<GameManagerSr>();

                gameManagerSr.ShowInfo(gameObject);

            }

        }

        mouseOver = true;
    }

    private void OnMouseExit()
    {
        if (playerSpawnGrid && !gameHasStarted && !playerPieceHere)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "Pawn")
                {
                    child.gameObject.SetActive(false);
                }

                if(child.tag == "PlayerHorse")
                {
                    child.gameObject.SetActive(false);
                }

                if (child.tag == "PlayerTower")
                {
                    child.gameObject.SetActive(false);
                }

                if (child.tag == "PlayerBishop")
                {
                    child.gameObject.SetActive(false);
                }

                if (child.tag == "PlayerQueen")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        if (infoIsHere)
        {
            
            gameManagerSr = FindAnyObjectByType<GameManagerSr>();

            gameManagerSr.StopShowingInfo();

            infoIsHere = false;
            noLongerShowInfo = false;

        }

        mouseOver = false;
    }

    #endregion

    #region Check Who Died

    public void CheckWhoDied(GridPiece whoCalled, PieceVisual VisuallsWhoCalled)
    {
        if (playerPieceHere)
        {

            if (controller.reviveHappened)
            {

                controller.howManyPlayerPieces--;
                playerPieceHere = false;

            }
            else
            {

                gameManagerSr = FindAnyObjectByType<GameManagerSr>();

                if(gameManagerSr.money >= 3)
                {

                    VisuallsWhoCalled.waitUntilReviveDone = true;
                    controller.AktivateReviveCanvas(whoCalled, VisuallsWhoCalled);

                }
                else
                {

                    controller.howManyPlayerPieces--;
                    playerPieceHere = false;

                }

            }


        }
    }

    #endregion

}
