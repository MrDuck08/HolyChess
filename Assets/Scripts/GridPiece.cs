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
    bool pawnHere = false;

    public bool anticipateMovment;
    public bool anticipatePlayerAttack;

    public bool gameHasStarted = false;
    public bool spawningPawnNow = false;

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
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
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
            if(mouseOver)
            {
                if (pawnHere && gameHasStarted)
                {
                    controller.AnticipatePawnMovment(xPos, yPos, gameObject);
                }

                if (!pawnHere)
                {

                    #region Start Spawn

                    if (playerSpawnGrid && spawningPawnNow)
                    {
                        pawnHere = true;

                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.spawningPawnNow = false;
                        }
                    }

                    #endregion

                    if (anticipateMovment)
                    {
                        controller.movePiece(0);

                        pawnHere = true;

                        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                        foreach (GridPiece allPiece in gridPieces)
                        {
                            allPiece.anticipateMovment = false;
                        }
                    }
                }
            }
        }
    }

    #region Mouse Over & Exit

    private void OnMouseOver()
    {
        if(playerSpawnGrid && !pawnHere && spawningPawnNow)
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
        if (playerSpawnGrid && !pawnHere)
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
