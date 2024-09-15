using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    public int xPos;
    public int yPos;



    #region bools

    bool playerSpawnGrid = false;
    bool enemySpawnGrid = false;

    bool mouseOver = false;

    #region Player Pieces

    public bool playerPawnHere = false;
    public bool playerHorseHere = false;
    public bool playerTowerHere = false;
    public bool playerBishopHere = false;
    public bool playerQueenHere = false;
    public bool playerPieceHere = false;

    #endregion

    #region Enemy Pieces

    public bool enemyHorsePieceHere = false;
    public bool enemyTowerPieceHere = false;
    public bool enemyBishopPieceHere = false;
    public bool enemyQueenPieceHere = false;
    public bool enemyPawnPieceHere = false;
    public bool enemyPieceHere = false;

    #endregion

    #region Player Movment

    public bool anticipateMovment;
    public bool anticipatePlayerAttack;
    public bool anticipatingPlayerPawn;
    public bool anticipatingPlayerHorse;
    public bool anticipatingPlayerTower;
    public bool anticipatingPlayerBishop;
    public bool anticipatingPlayerQueen;
    public bool movedOnce = false;

    #endregion

    #region Spawning & Turns

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

    #region Spawn info

    public void SpawnLocation(int x, int y, int spawnWho, Color32 whatVisualls, int whatEnemyToSpawn)
    {
        xPos = x;
        yPos = y;
        anticipateMovemtVisualls = whatVisualls;
        PieceVisualls = whatVisualls;

        if(spawnWho == 0)
        {
            playerSpawnGrid = true;
        }
        if(spawnWho == 1)
        {
            enemySpawnGrid = true;


            //Change Later
            if(whatEnemyToSpawn == 0)
            {
                enemyPieceHere = true;
                enemyPawnPieceHere = true;
            }

        }

        gameObject.GetComponent<SpriteRenderer>().color = anticipateMovemtVisualls;

        transform.position = new Vector2 (xPos, yPos);
    }

    #endregion

    private void Start()
    {
        controller = FindObjectOfType<GridController>();
    }

    private void Update()
    {

        #region Pieces Visuals

        #region Player Pieces

        if (gameHasStarted)
        {

            #region Pawn

            if (playerPawnHere)
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

            if (playerTowerHere)
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

            if (playerHorseHere)
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

            if (playerBishopHere)
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

            if (playerQueenHere)
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

        if (enemyHorsePieceHere)
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

        if (enemyTowerPieceHere)
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

        if (enemyBishopPieceHere)
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

        if (enemyQueenPieceHere)
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

        if (enemyPawnPieceHere)
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
            playerPawnHere = false;
            playerHorseHere = false;
            playerTowerHere = false;
            playerBishopHere = false;
            playerQueenHere = false;
        }

        if (!enemyPieceHere)
        {
            enemyBishopPieceHere = false;
            enemyPawnPieceHere = false;
            enemyHorsePieceHere = false;
            enemyTowerPieceHere = false;
            enemyQueenPieceHere = false;
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

                        if (playerPawnHere)
                        {
                            controller.AnticipatePawnMovment(xPos, yPos, gameObject);
                            controller.AnticipatePawnAttack(xPos, yPos, gameObject);
                        }

                        if (playerHorseHere)
                        {
                            controller.AnticipateHorseMovment(xPos, yPos, gameObject);
                        }

                        if (playerTowerHere)
                        {
                            controller.AnticipateTowerMovment(xPos, yPos, gameObject);
                        }

                        if (playerBishopHere)
                        {
                            controller.AnticipateBishopMovment(xPos, yPos, gameObject);
                        }

                        if (playerQueenHere)
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
                            playerPawnHere = true;
                            playerPieceHere = true;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningPawnNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }

                        if (spawningHorseNow)
                        {
                            playerHorseHere = true;
                            playerPieceHere = true;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningHorseNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }

                        if (spawningTowerNow)
                        {
                            playerTowerHere = true;
                            playerPieceHere = true;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningTowerNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }

                        if (spawningBishopNow)
                        {
                            playerBishopHere = true;
                            playerPieceHere = true;

                            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                            foreach (GridPiece allPiece in gridPieces)
                            {
                                allPiece.spawningBishopNow = false;
                                allPiece.placingDownAUnitNow = false;
                            }
                        }

                        if (spawningQueenNow)
                        {
                            playerQueenHere = true;
                            playerPieceHere = true;

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
                            playerPawnHere = true;
                            controller.movePiece(0);
                        }

                        if (anticipatingPlayerHorse)
                        {
                            playerHorseHere = true;
                            controller.movePiece(1);
                        }


                        if (anticipatingPlayerTower)
                        {
                            playerTowerHere = true;
                            controller.movePiece(2);
                        }

                        if (anticipatingPlayerBishop)
                        {
                            playerBishopHere = true;
                            controller.movePiece(3);
                        }

                        if (anticipatingPlayerQueen)
                        {
                            playerQueenHere = true;
                            controller.movePiece(4);
                        }

                        #endregion

                        playerPieceHere = true;

                        movedOnce = true;

                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipateMovment = false;
                        }

                    }
                    else
                    {
                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipateMovment = false;
                        }
                    }
                    #endregion

                    #region Attacking

                    if (anticipatePlayerAttack)
                    {

                        #region Who Is Attacking

                        if (anticipatingPlayerPawn)
                        {
                            playerPawnHere = true;
                            controller.AttackPiece(0);
                        }

                        if (anticipatingPlayerHorse)
                        {
                            playerHorseHere = true;
                            controller.AttackPiece(1);
                        }

                        if (anticipatingPlayerTower)
                        {
                            playerTowerHere = true;
                            controller.AttackPiece(2);
                        }

                        if (anticipatingPlayerBishop)
                        {
                            playerBishopHere = true;
                            controller.AttackPiece(3);
                        }

                        if (anticipatingPlayerQueen)
                        {
                            playerQueenHere = true;
                            controller.AttackPiece(4);
                        }

                        #endregion

                        playerPieceHere = true;

                        movedOnce = true;

                        enemyPieceHere = false;

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipatePlayerAttack = false;
                        }

                    }
                    else
                    {
                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipatePlayerAttack = false;
                        }
                    }

                    #endregion

                }
            }
        }

        #endregion

        #region Enemy Movments

        if (!playerTurn)
        {

            if (enemyHorsePieceHere)
            {

                controller.EnemyHorseMovmentCall(xPos, yPos, gameObject);

            }

            if (enemyTowerPieceHere)
            {

                controller.EnemyTowerMovmentCall(xPos, yPos, gameObject);

            }

            if (enemyBishopPieceHere)
            {

                controller.EnemyBishopMovmentCall(xPos, yPos, gameObject);

            }

            if (enemyQueenPieceHere)
            {

                controller.EnemyQueenMovmentCall(xPos, yPos, gameObject);

            }

            if (enemyPawnPieceHere)
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
        if (playerPieceHere && enemyPieceHere && !playerTurn)
        {

            playerPieceHere = false;        

        }
    }

}
