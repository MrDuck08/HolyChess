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
    bool playerPawnHere = false;

    public bool playerPieceHere = false;
    public bool enemyPieceHere = false;
    public bool anticipateMovment;
    public bool anticipatePlayerAttack;

    public bool gameHasStarted = false;
    public bool spawningPawnNow = false;
    public bool yourTurn = true;

    #endregion

    Color32 visualls;

    GridController controller;

    GridPiece[] gridPieces;

    #region Spawn info

    public void SpawnLocation(int x, int y, int spawnWho, Color32 whatVisualls)
    {
        xPos = x;
        yPos = y;
        visualls = whatVisualls;

        if(spawnWho == 0)
        {
            playerSpawnGrid = true;
        }
        if(spawnWho == 1)
        {
            enemySpawnGrid = true;
            enemyPieceHere = true;
        }

        gameObject.GetComponent<SpriteRenderer>().color = visualls;

        transform.position = new Vector2 (xPos, yPos);
    }

    #endregion

    private void Start()
    {
        controller = FindObjectOfType<GridController>();
    }

    private void Update()
    {
        if (enemySpawnGrid)
        {
            foreach (Transform child in gameObject.transform)
            {
                if (child.tag == "EnemyHorse")
                {
                    child.gameObject.SetActive(true);
                }

            }
        }

        if (!playerPieceHere)
        {
            playerPawnHere = false;
        }

        #region Anticipate Movment

        if (anticipateMovment)
        {
            visualls.a = 144;
            gameObject.GetComponent<SpriteRenderer>().color = visualls;
        }
        else
        {
            visualls.a = 255;
            gameObject.GetComponent<SpriteRenderer>().color = visualls;
        }

        #endregion

        if (Input.GetMouseButton(0))
        {
            if(mouseOver && yourTurn)
            {

                #region Anticipate Movment

                if (playerPawnHere && gameHasStarted)
                {
                    controller.AnticipatePawnMovment(xPos, yPos, gameObject);
                }

                #endregion

                if (!playerPawnHere)
                {

                    #region Start Spawn

                    if (playerSpawnGrid && spawningPawnNow)
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

                }
            }
        }

        if (!yourTurn && enemyPieceHere)
        {
            controller.EnemyHorseMovment(xPos, yPos);

            yourTurn = true;
        }
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

}
