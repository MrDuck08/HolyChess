using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    public int xPos;
    public int yPos;

    bool playerSpawnGrid = false;
    bool enemySpawnGrid = false;
    bool mouseOver = false;
    bool pawnOnThisPiece = false;

    GridController controller;

    #region Spawn info

    public void SpawnLocation(int x, int y, int spawnWho)
    {
        xPos = x;
        yPos = y;

        if(spawnWho == 0)
        {
            playerSpawnGrid = true;
        }
        if(spawnWho == 1)
        {
            enemySpawnGrid = true;
        }

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

        if(Input.GetMouseButton(0))
        {
            if(mouseOver)
            {
                if (pawnOnThisPiece)
                {
                    controller.MovePawn(xPos, yPos);
                }

                pawnOnThisPiece = true;
            }
        }
    }


    private void OnMouseOver()
    {
        if(playerSpawnGrid && !pawnOnThisPiece)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        mouseOver = true;
    }

    private void OnMouseExit()
    {
        if (playerSpawnGrid && !pawnOnThisPiece)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        

        mouseOver = false;
    }
}
