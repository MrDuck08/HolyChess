using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece : MonoBehaviour
{
    int xPos;
    int yPos;

    bool playerSpawnGrid = false;
    bool enemySpawnGrid = false;

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

    private void Update()
    {
        if (enemySpawnGrid)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }    
    }


    private void OnMouseOver()
    {
        if(playerSpawnGrid)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }


    }

    private void OnMouseExit()
    {
        if (playerSpawnGrid)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}
