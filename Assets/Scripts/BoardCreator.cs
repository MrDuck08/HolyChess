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
    public int topPosY;
    public int bottomPosY;

    int whiteOrBlackToSPawn = 1;

    [SerializeField] int maxRandomRange = 10;
    [SerializeField] int minRandomRange = 5;

    bool firstDone = false;

    int boardHeight;
    float boardWidth;

    int playerSpawnLocation;
    int enemySpawnLocation;
    int playerOrEnemySpawnPiece;

    #endregion


    [SerializeField] GridPiece gridPieceObject;

    List<GridPiece> enemyGridPieceList = new List<GridPiece>();

    GameManagerSr gameManagerSr;

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

        //Debug.Log(boardWidth + " Width");





        maxYUp = Random.Range(minRandomRange, maxRandomRange);
        maxYDown = Random.Range(-minRandomRange, -maxRandomRange);
        bottomPosY = maxYDown;
        topPosY = maxYUp;

        boardHeight = maxYUp + -1 * maxYDown - 1;
        //Debug.Log(boardHeight + " Height");

        playerSpawnLocation = boardHeight/3;
        enemySpawnLocation = boardHeight - playerSpawnLocation;


        #endregion

        #region PlaceBoard

        while (true)
        {
            if (IsThisInteger(boardWidth/2)) // Måste Ha Det Här För Annars Blir Det Inte Var Annan Svart Vitt När Man Bytter Rad
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

                    #region Enemy or player spawntile

                    GridPiece spawnedObject = Instantiate(gridPieceObject);

                    if (playerSpawnLocation > 0)
                    {
                        playerOrEnemySpawnPiece = 0;
                        // 0 = Player
                    }
                    else
                    {
                        playerOrEnemySpawnPiece = 1337;
                    }
                    if(enemySpawnLocation < 0)
                    {
                        playerOrEnemySpawnPiece = 1;

                        enemyGridPieceList.Add(spawnedObject);

                        // 1 = Enemy
                    }

                    #endregion

                    spawnedObject.SpawnLocation(gridX, maxYDown, playerOrEnemySpawnPiece, new Color32(255, 255, 255, 255));

                    whiteOrBlackToSPawn++;
                }
                else
                {
                    if(whiteOrBlackToSPawn == 1)
                    {

                        #region Enemy or player spawntile

                        GridPiece spawnedObject = Instantiate(gridPieceObject);

                        if (playerSpawnLocation > 0)
                        {
                            playerOrEnemySpawnPiece = 0;
                            // 0 = Player
                        }
                        else
                        {
                            playerOrEnemySpawnPiece = 1337;
                        }
                        if (enemySpawnLocation < 0)
                        {
                            playerOrEnemySpawnPiece = 1;

                            enemyGridPieceList.Add(spawnedObject);
                            // 1 = Enemy
                        }

                        #endregion

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
                SpawnEnemyPieces();
                break;
            }
        }

        #endregion

    }

    void SpawnEnemyPieces()
    {

        gameManagerSr = FindObjectOfType<GameManagerSr>();

        gameManagerSr.DistributePoints();


        while (true)
        {
            gameManagerSr.howManyEnemiesBought--;

            if (gameManagerSr.howManyEnemiesBought >= 0)
            {

                int whatGridPieceToPlaceEnemyOn = Random.Range(0, enemyGridPieceList.Count);

                enemyGridPieceList[whatGridPieceToPlaceEnemyOn].SpawnEnemy(gameManagerSr.whatTypeOfEnemyWasBought[gameManagerSr.howManyEnemiesBought]);

                enemyGridPieceList.RemoveAt(whatGridPieceToPlaceEnemyOn);

            }
            else
            {

                break;
            }

        }

    }

}
