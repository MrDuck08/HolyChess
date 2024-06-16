using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{

    #region PlaceBoardTiles

    int gridX;

    int maxXRight;
    int maxYUp;
    int maxXLeft;
    int maxYDown;

    int whiteOrBlackToSPawn = 1;

    [SerializeField] int maxRandomRange = 11;
    [SerializeField] int minRandomRange = 5;

    bool firstDone = false;

    int boardHeight;
    float boardWidth;

    int playerSpawnLocation;
    int enemySpawnLocation;
    int playerOrEnemySpawnPiece;

    #endregion


    [SerializeField] GridPiece gridPieceObject;

    bool IsThisInteger(float myFloat)
    {
        return Mathf.Approximately(myFloat, Mathf.RoundToInt(myFloat));
    }

    void Start()
    {

        #region Set board Size

        maxXRight = Random.Range(minRandomRange, maxRandomRange);
        maxXLeft = Random.Range(-minRandomRange, -maxRandomRange);


        boardWidth = maxXRight + -1 * maxXLeft + 1;

        Debug.Log(boardWidth + " Width");





        maxYUp = Random.Range(minRandomRange, maxRandomRange);
        maxYDown = Random.Range(-minRandomRange, -maxRandomRange);

        boardHeight = maxYUp + -1 * maxYDown - 1;
        Debug.Log(boardHeight + " Height");

        playerSpawnLocation = boardHeight/3;
        enemySpawnLocation = boardHeight - playerSpawnLocation;


        Debug.Log(playerSpawnLocation);

        #endregion

        #region PlaceBoard

        while (true)
        {
            if (IsThisInteger(boardWidth/2))
            {

                if (whiteOrBlackToSPawn == 0)
                {

                    whiteOrBlackToSPawn++;

                }
                else
                {
                    if (whiteOrBlackToSPawn == 1)
                    {

                        whiteOrBlackToSPawn--;

                    }
                }
            }

            gridX = maxXRight;
            while (true)
            {

                if (maxYDown >= maxYUp)
                {
                    firstDone = true;

                    break;
                }

                if(whiteOrBlackToSPawn == 0)
                {
                    if(playerSpawnLocation > 0)
                    {
                        playerOrEnemySpawnPiece = 0;
                    }
                    else
                    {
                        playerOrEnemySpawnPiece = 1337;
                    }
                    if(enemySpawnLocation < 0)
                    {
                        playerOrEnemySpawnPiece = 1;
                    }


                    GridPiece spawnedObject = Instantiate(gridPieceObject);

                    spawnedObject.SpawnLocation(gridX, maxYDown, playerOrEnemySpawnPiece, new Color32(255, 255, 255, 255));


                    whiteOrBlackToSPawn++;
                }
                else
                {
                    if(whiteOrBlackToSPawn == 1)
                    {

                        #region Enemy or player spawntile

                        if (playerSpawnLocation > 0)
                        {
                            playerOrEnemySpawnPiece = 0;
                        }
                        else
                        {
                            playerOrEnemySpawnPiece = 1337;
                        }
                        if (enemySpawnLocation < 0)
                        {
                            playerOrEnemySpawnPiece = 1;
                        }

                        #endregion


                        GridPiece spawnedObject = Instantiate(gridPieceObject);

                        spawnedObject.SpawnLocation(gridX, maxYDown, playerOrEnemySpawnPiece, new Color32(0, 142, 99, 255));


                        whiteOrBlackToSPawn--;
                    }
                }

                gridX--;


                if (gridX < maxXLeft)
                {
                    maxYDown++;
                    break;
                }
            }


            playerSpawnLocation--;
            enemySpawnLocation--;

            if (firstDone)
            {
                break;
            }
        }

        #endregion

    }

}
