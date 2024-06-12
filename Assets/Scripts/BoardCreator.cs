using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{

    #region PlaceBoardTiles

    int gridX;
    int gridY;

    int maxXRight;
    int maxYUp;
    int maxXLeft;
    int maxYDown;

    int howManyUppPosetiveX = 1;
    int howManyDownPosetiveX = 0;

    int howManyUppNegativeX = 1;
    int howManyDownNegativeX = 0;

    int whiteOrBlackToSPawn = 1;

    [SerializeField] int maxRandomRange = 11;
    [SerializeField] int minRandomRange = 5;

    bool firstDone = false;
    bool secondDone = false;
    bool thirdDone = false;
    bool fourthDone = false;

    #endregion

    int boardHeight;
    int boardWidth;

    [SerializeField] GridPiece[] gridPieceObject;

    void Start()
    {
        //maxXRight = Random.Range(minRandomRange, maxRandomRange);
        //maxXLeft = Random.Range(-minRandomRange, -maxRandomRange);

        //boardWidth = maxXRight + -1 * maxXLeft + 1;
        //Debug.Log(boardWidth + " Width");

        //maxYUp = Random.Range(minRandomRange, maxRandomRange);
        //maxYDown = Random.Range(-minRandomRange, -maxRandomRange);

        //boardHeight = maxYUp + -1 * maxYDown - 1;
        //Debug.Log(boardHeight + " Height");

        maxXRight = 8;
        maxXLeft = -8;

        maxYDown = -8;
        maxYUp = 8;

        #region PlaceBoard

        while (true)
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

            gridX = maxXRight;
            while (true)
            {

                if (howManyUppPosetiveX >= maxYUp)
                {
                    firstDone = true;

                    break;
                }

                if(whiteOrBlackToSPawn == 0)
                {
                    GridPiece spawnedObject = Instantiate(gridPieceObject[0]);

                    spawnedObject.SpawnLocation(gridX, howManyUppPosetiveX);


                    whiteOrBlackToSPawn++;
                }
                else
                {
                    if(whiteOrBlackToSPawn == 1)
                    {
                        GridPiece spawnedObject = Instantiate(gridPieceObject[1]);

                        spawnedObject.SpawnLocation(gridX, howManyUppPosetiveX);


                        whiteOrBlackToSPawn--;
                    }
                }

                gridX--;

                if (gridX < 0)
                {
                    howManyUppPosetiveX++;
                    break;
                }
            }

            gridX = maxXRight;

            while (true)
            {
                if (howManyDownPosetiveX <= maxYDown)
                {
                    secondDone = true;

                    break;
                }

                if (whiteOrBlackToSPawn == 0)
                {
                    GridPiece spawnedObject = Instantiate(gridPieceObject[0]);

                    spawnedObject.SpawnLocation(gridX, howManyDownPosetiveX);


                    whiteOrBlackToSPawn++;
                }
                else
                {
                    if (whiteOrBlackToSPawn == 1)
                    {
                        GridPiece spawnedObject = Instantiate(gridPieceObject[1]);

                        spawnedObject.SpawnLocation(gridX, howManyDownPosetiveX);


                        whiteOrBlackToSPawn--;
                    }
                }

                gridX--;

                if (gridX < 0)
                {
                    howManyDownPosetiveX--;
                    break;
                }
            }

            gridX = maxXLeft;

            while (true)
            {
                if (howManyUppNegativeX >= maxYUp)
                {
                    thirdDone = true;

                    break;
                }


                if (whiteOrBlackToSPawn == 0)
                {
                    GridPiece spawnedObject = Instantiate(gridPieceObject[0]);

                    spawnedObject.SpawnLocation(gridX, howManyUppNegativeX);


                    whiteOrBlackToSPawn++;
                }
                else
                {
                    if (whiteOrBlackToSPawn == 1)
                    {
                        GridPiece spawnedObject = Instantiate(gridPieceObject[1]);

                        spawnedObject.SpawnLocation(gridX, howManyUppNegativeX);


                        whiteOrBlackToSPawn--;
                    }
                }

                gridX++;

                if (gridX >= 0)
                {
                    howManyUppNegativeX++;
                    break;
                }
            }

            gridX = maxXLeft;

            while (true)
            {
                if(howManyDownNegativeX <= maxYDown)
                {
                    fourthDone = true;

                    break;
                }


                if (whiteOrBlackToSPawn == 0)
                {
                    GridPiece spawnedObject = Instantiate(gridPieceObject[0]);

                    spawnedObject.SpawnLocation(gridX, howManyDownNegativeX);


                    whiteOrBlackToSPawn++;
                }
                else
                {
                    if (whiteOrBlackToSPawn == 1)
                    {
                        GridPiece spawnedObject = Instantiate(gridPieceObject[1]);

                        spawnedObject.SpawnLocation(gridX, howManyDownNegativeX);


                        whiteOrBlackToSPawn--;
                    }
                }

                gridX++;

                if (gridX >= 0)
                {
                    howManyDownNegativeX--;
                    break;
                }
            }

            if (firstDone && secondDone && thirdDone && fourthDone)
            {
                break;
            }
        }

        #endregion

    }

}
