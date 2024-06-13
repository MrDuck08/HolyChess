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

    #endregion


    [SerializeField] GridPiece[] gridPieceObject;

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

        boardWidth /= 2;



        maxYUp = Random.Range(minRandomRange, maxRandomRange);
        maxYDown = Random.Range(-minRandomRange, -maxRandomRange);

        boardHeight = maxYUp + -1 * maxYDown - 1;
        Debug.Log(boardHeight + " Height");

        #endregion

        #region PlaceBoard

        while (true)
        {
            if (IsThisInteger(boardWidth))
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
                    GridPiece spawnedObject = Instantiate(gridPieceObject[0]);

                    spawnedObject.SpawnLocation(gridX, maxYDown);


                    whiteOrBlackToSPawn++;
                }
                else
                {
                    if(whiteOrBlackToSPawn == 1)
                    {
                        GridPiece spawnedObject = Instantiate(gridPieceObject[1]);

                        spawnedObject.SpawnLocation(gridX, maxYDown);


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

            if (firstDone)
            {
                break;
            }
        }

        #endregion

    }

}
