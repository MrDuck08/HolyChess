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

    public bool playerPieceHere = false;
    public bool enemyHorsePieceHere = false;
    public bool enemyPieceHere = false;
    public bool anticipateMovment;
    public bool anticipatePlayerAttack;

    public bool gameHasStarted = false;
    public bool spawningPawnNow = false;
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

        if (!playerPieceHere)
        {
            playerPawnHere = false;
        }

        #region Anticipate Movment

        if (anticipateMovment)
        {
            anticipateMovemtVisualls.a = 144;
            gameObject.GetComponent<SpriteRenderer>().color = anticipateMovemtVisualls;
        }
        else
        {
            anticipateMovemtVisualls.a = 255;
            gameObject.GetComponent<SpriteRenderer>().color = anticipateMovemtVisualls;
        }

        if (anticipatePlayerAttack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }


        #endregion

        if (Input.GetMouseButton(0))
        {
            if(mouseOver && playerTurn)
            {

                #region Anticipate Movment

                if (playerPawnHere && gameHasStarted)
                {
                    controller.AnticipatePawnMovment(xPos, yPos, gameObject);
                    controller.AnticipatePawnAttack(xPos, yPos, gameObject);
                }

                #endregion

                if (!playerPieceHere)
                {

                    #region Start Spawn

                    if (playerSpawnGrid && spawningPawnNow && !gameHasStarted)
                    {
                        playerPawnHere = true;
                        playerPieceHere = true;

                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.spawningPawnNow = false;
                        }
                    }

                    #endregion

                    #region Movment

                    if (anticipateMovment)
                    {
                        controller.movePiece(0);

                        playerPieceHere = true;
                        playerPawnHere = true;

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
                        controller.AttackPiece(0);

                        playerPieceHere = true;
                        playerPawnHere = true;

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
        if(playerSpawnGrid && !playerPawnHere && spawningPawnNow && !gameHasStarted)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "Pawn")
                {
                    child.gameObject.SetActive(true);
                }

            }
        }

        mouseOver = true;
    }

    private void OnMouseExit()
    {
        if (playerSpawnGrid && !playerPawnHere && !gameHasStarted)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "Pawn")
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
            playerPawnHere = false;
            Debug.Log("KILL");

        }
    }

}
