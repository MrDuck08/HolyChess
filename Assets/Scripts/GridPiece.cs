using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class GridPiece : MonoBehaviour
{

    public int xPos;
    public int yPos;



    #region bools

    bool playerSpawnGrid = false;
    bool enemySpawnGrid = false;

    bool mouseOver = false;

    #region Player Pieces

    [Header("Player")]


    public PlayerType currentPlayerType;


    public bool playerPieceHere = false;

    #endregion

    #region Enemy Pieces

    [Header("Enemy")]

    public EnemyType currentEnemyType;
    public bool enemyPieceHere = false;

    #endregion

    #region Player Movment

    [Header("Player Movment")]

    public bool anticipateMovment;
    public bool anticipatePlayerAttack;

    public bool anticipatingPlayerPawn;
    public bool anticipatingPlayerHorse;
    public bool anticipatingPlayerTower;
    public bool anticipatingPlayerBishop;
    public bool anticipatingPlayerQueen;

    public bool anticipatingPlayerAttackPawn;
    public bool anticipatingPlayerAttackHorse;
    public bool anticipatingPlayerAttackTower;
    public bool anticipatingPlayerAttackBishop;
    public bool anticipatingPlayerAttackQueen;

    public bool movedOnce = false;

    #endregion

    #region Spawning & Turns

    [Header("Spawn")]

    public bool spawningPawnNow = false;
    public bool spawningHorseNow = false;
    public bool spawningTowerNow = false;
    public bool spawningBishopNow = false;
    public bool spawningQueenNow = false;

    public bool gameHasStarted = false;
    public bool placingDownAUnitNow = false;
    public bool playerTurn = true;

    #endregion

    #endregion

    Color32 anticipateMovemtVisualls;
    Color32 PieceVisualls;

    GridController controller;

    GridPiece[] gridPieces;

    private void Awake()
    {

        currentPlayerType = PlayerType.none;
        currentEnemyType = EnemyType.none;

    }

    private void Start()
    {
        controller = FindObjectOfType<GridController>();

    }

    #region Spawn info

    public void SpawnLocation(int x, int y, int spawnWho, Color32 whatVisualls)
    {
        xPos = x;
        yPos = y;
        anticipateMovemtVisualls = whatVisualls;
        PieceVisualls = whatVisualls;

        if(spawnWho == 0)
        {
            //Player Grid

            playerSpawnGrid = true;
        }
        if(spawnWho == 1)
        {
            //Enemy Grid

            enemySpawnGrid = true;

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

                break;

            case 1:

                currentEnemyType = EnemyType.Horse;

                break;

            case 2:

                currentEnemyType = EnemyType.Tower;

                break;

            case 3:

                currentEnemyType = EnemyType.Bishop;

                break;

            case 4:

                currentEnemyType = EnemyType.Queen;

                break;

        }

    }

    #endregion

    private void Update()
    {

        #region Pieces Visuals

        #region Player Pieces

        if (gameHasStarted)
        {

            #region Pawn

            if (currentPlayerType == PlayerType.Pawn)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "Pawn")
                    {
                        child.gameObject.SetActive(true);
                    }

                }
            }
            else
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "Pawn")
                    {
                        child.gameObject.SetActive(false);
                    }

                }
            }

            #endregion

            #region Tower

            if (currentPlayerType == PlayerType.Tower)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerTower")
                    {
                        child.gameObject.SetActive(true);
                    }

                }
            }
            else
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerTower")
                    {
                        child.gameObject.SetActive(false);
                    }

                }
            }

            #endregion

            #region Horse

            if (currentPlayerType == PlayerType.Horse)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerHorse")
                    {
                        child.gameObject.SetActive(true);
                    }

                }
            }
            else
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerHorse")
                    {
                        child.gameObject.SetActive(false);
                    }

                }
            }

            #endregion

            #region Bishop

            if (currentPlayerType == PlayerType.Bishop)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerBishop")
                    {
                        child.gameObject.SetActive(true);
                    }

                }
            }
            else
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerBishop")
                    {
                        child.gameObject.SetActive(false);
                    }

                }
            }

            #endregion

            #region Queen

            if (currentPlayerType == PlayerType.Queen)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerQueen")
                    {
                        child.gameObject.SetActive(true);
                    }

                }
            }
            else
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerQueen")
                    {
                        child.gameObject.SetActive(false);
                    }

                }
            }

            #endregion
        }

        #endregion

        #region Enemy Pieces

        #region Horse

        if (currentEnemyType == EnemyType.Horse)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyHorse")
                {
                    child.gameObject.SetActive(true);
                }

            }
        }
        else
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyHorse")
                {
                    child.gameObject.SetActive(false);
                }

            }
        }

        #endregion

        #region Tower

        if (currentEnemyType == EnemyType.Tower)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyTower")
                {
                    child.gameObject.SetActive(true);
                }

            }
        }
        else
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyTower")
                {
                    child.gameObject.SetActive(false);
                }

            }
        }

        #endregion

        #region Bishop

        if (currentEnemyType == EnemyType.Bishop)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyBishop")
                {
                    child.gameObject.SetActive(true);
                }

            }
        }
        else
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyBishop")
                {
                    child.gameObject.SetActive(false);
                }

            }
        }

        #endregion

        #region Queen

        if (currentEnemyType == EnemyType.Queen)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyQueen")
                {
                    child.gameObject.SetActive(true);
                }

            }
        }
        else
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyQueen")
                {
                    child.gameObject.SetActive(false);
                }

            }
        }

        #endregion

        #region Pawn

        if (currentEnemyType == EnemyType.Pawn)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyPawn")
                {
                    child.gameObject.SetActive(true);
                }

            }
        }
        else
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyPawn")
                {
                    child.gameObject.SetActive(false);
                }

            }
        }

        #endregion

        #endregion

        #endregion

        #region Check If Dead

        if (!playerPieceHere)
        {
            currentPlayerType = PlayerType.none;
        }

        if (!enemyPieceHere)
        {

            currentEnemyType = EnemyType.none;

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

        if (anticipatePlayerAttack && !movedOnce)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }


        #endregion

        #region Movment + Spawn

        if (Input.GetMouseButton(0))
        {
            if(mouseOver && playerTurn)
            {

                #region Anticipate Movment

                if (gameHasStarted)
                {
                    if (!movedOnce)
                    {

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
                    }
                }

                #endregion

                if (!playerPieceHere)
                {

                    #region Start Spawn

                    if (playerSpawnGrid && !gameHasStarted)
                    {
                        if (spawningPawnNow)
                        {

                            playerPieceHere = true;
                            currentPlayerType = PlayerType.Pawn;
                           
                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningPawnNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }

                        if (spawningHorseNow)
                        {

                            playerPieceHere = true;
                            currentPlayerType = PlayerType.Horse;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningHorseNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }

                        if (spawningTowerNow)
                        {

                            playerPieceHere = true;
                            currentPlayerType = PlayerType.Tower;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningTowerNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }

                        if (spawningBishopNow)
                        {

                            playerPieceHere = true;
                            currentPlayerType= PlayerType.Bishop;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningBishopNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }

                        if (spawningQueenNow)
                        {

                            playerPieceHere = true;
                            currentPlayerType = PlayerType.Queen;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningQueenNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }
                    }

                    #endregion

                    #region Movment

                    if (anticipateMovment)
                    {
                        #region Who Is Moving

                        if (anticipatingPlayerPawn)
                        {
                            currentPlayerType = PlayerType.Pawn;
                            controller.movePiece();
                        }

                        if (anticipatingPlayerHorse)
                        {
                            currentPlayerType = PlayerType.Horse;
                            controller.movePiece();
                        }


                        if (anticipatingPlayerTower)
                        {
                            currentPlayerType = PlayerType.Tower;
                            controller.movePiece();
                        }

                        if (anticipatingPlayerBishop)
                        {
                            currentPlayerType = PlayerType.Bishop;
                            controller.movePiece();
                        }

                        if (anticipatingPlayerQueen)
                        {
                            currentPlayerType = PlayerType.Queen;
                            controller.movePiece();
                        }

                        #endregion

                        playerPieceHere = true;

                        movedOnce = true;

                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipateMovment = false;

                            allPiece.anticipatingPlayerPawn = false;
                            allPiece.anticipatingPlayerHorse = false;
                            allPiece.anticipatingPlayerTower = false;
                            allPiece.anticipatingPlayerBishop = false;
                            allPiece.anticipatingPlayerQueen = false;
                        }

                    }
                    else
                    {
                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipateMovment = false;

                            allPiece.anticipatingPlayerPawn = false;
                            allPiece.anticipatingPlayerHorse = false;
                            allPiece.anticipatingPlayerTower = false;
                            allPiece.anticipatingPlayerBishop = false;
                            allPiece.anticipatingPlayerQueen = false;
                        }
                    }
                    #endregion
    
                    #region Attacking

                    if (anticipatePlayerAttack)
                    {

                        #region Who Is Attacking

                        if (anticipatingPlayerAttackPawn)
                        {
                            currentPlayerType = PlayerType.Pawn;
                            controller.AttackPiece();
                        }

                        if (anticipatingPlayerAttackHorse)
                        {
                            currentPlayerType = PlayerType.Horse;
                            controller.AttackPiece();
                        }

                        if (anticipatingPlayerAttackTower)
                        {
                            currentPlayerType = PlayerType.Tower;
                            controller.AttackPiece();
                        }

                        if (anticipatingPlayerAttackBishop)
                        {
                            currentPlayerType = PlayerType.Bishop;
                            controller.AttackPiece();
                        }

                        if (anticipatingPlayerAttackQueen)
                        {
                            currentPlayerType = PlayerType.Queen;   
                            controller.AttackPiece( );
                        }

                        #endregion

                        playerPieceHere = true;

                        movedOnce = true;

                        enemyPieceHere = false;

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipatePlayerAttack = false;

                            allPiece.anticipatingPlayerAttackPawn = false;
                            allPiece.anticipatingPlayerAttackHorse = false;
                            allPiece.anticipatingPlayerAttackTower = false;
                            allPiece.anticipatingPlayerAttackBishop = false;
                            allPiece.anticipatingPlayerAttackQueen = false;
                        }

                    }
                    else
                    {
                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipatePlayerAttack = false;

                            allPiece.anticipatingPlayerAttackPawn = false;
                            allPiece.anticipatingPlayerAttackHorse = false;
                            allPiece.anticipatingPlayerAttackTower = false;
                            allPiece.anticipatingPlayerAttackBishop = false;
                            allPiece.anticipatingPlayerAttackQueen = false;
                        }
                    }

                    #endregion

                }
            }
        }

        #endregion

        #region Enemy Movments

        if (!playerTurn && enemyPieceHere)
        {
            playerTurn = true;

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

        #endregion
    }

    #region Mouse Over & Exit

    private void OnMouseOver()
    {
        if(playerSpawnGrid && !gameHasStarted && !playerPieceHere)
        {
            if (spawningPawnNow)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "Pawn")
                    {
                        child.gameObject.SetActive(true);
                    }

                }
            }

            if (spawningHorseNow)
            {
                foreach(Transform child in gameObject.transform)
                {
                    if(child.tag == "PlayerHorse")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            if (spawningTowerNow)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerTower")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            if (spawningBishopNow)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.tag == "PlayerBishop")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }

            if (spawningQueenNow)
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
        

        mouseOver = false;
    }

    #endregion

    public void CheckWhoDied()
    {
        if (playerPieceHere && enemyPieceHere)
        {

            playerPieceHere = false;        

        }
    }

}
