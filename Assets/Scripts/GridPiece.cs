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

    public bool playerPawnHere = false;
    public bool playerHorseHere = false;
    public bool playerTowerHere = false;
    public bool playerPieceHere = false;

    public bool enemyHorsePieceHere = false;
    public bool enemyPieceHere = false;

    public bool anticipateMovment;
    public bool anticipatePlayerAttack;
    public bool anticipatingPlayerPawn;
    public bool anticipatingPlayerHorse;
    public bool movedOnce = false;

    public bool gameHasStarted = false;
    public bool spawningPawnNow = false;
    public bool spawningHorseNow = false;
    public bool spawningTowerNow = false;
    public bool placingDownAUNitNow = false;
    public bool playerTurn = true;

    #endregion

    Color32 anticipateMovemtVisualls;
    Color32 PieceVisualls;

    GridController controller;

    GridPiece[] gridPieces;

    #region Spawn info

    public void SpawnLocation(int x, int y, int spawnWho, Color32 whatVisualls)
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
            enemyHorsePieceHere = true;
            enemyPieceHere = true;
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
        }

        #endregion

        #region Enemy Pieces

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

        #endregion

        #region Check If Dead

        if (!playerPieceHere)
        {
            playerPawnHere = false;
            playerHorseHere = false;
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
                                allPiece.placingDownAUNitNow = false;
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
                                allPiece.placingDownAUNitNow = false;
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
                                allPiece.placingDownAUNitNow = false;
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

                        #endregion

                        playerPieceHere = true;

                        movedOnce = true;

                        enemyPieceHere = false;
                        enemyHorsePieceHere = false;

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
            }
        }
        

        mouseOver = false;
    }

    #endregion

    public void CheckIfEnemyAttackedPlayer()
    {
        if (playerPieceHere && enemyPieceHere)
        {

            playerPieceHere = false;        

        }
    }

}
