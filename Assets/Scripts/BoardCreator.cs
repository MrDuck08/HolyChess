using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    int gridX;
    int gridY;

    int maxXPosetive = 8;
    int maxYPosetive = 4;
    int maxXNegative;
    int maxYNegative;

    int howManyUppPosetiveX = 1;
    int howManyDownPosetiveX = 0;

    int howManyUppNegativeX = 1;
    int howManyDownNegativeX = 0;

    int test = 0;

    [SerializeField] GridPiece gridPieceObject;

    void Start()
    {
        //maxXPosetive = Random.Range(1, 4);
        //maxYPosetive = Random.Range(1, 4);
        maxXNegative = Random.Range(1, 8);
        maxYNegative = Random.Range(-1, -8);

        while (true)
        {
            gridX = maxXPosetive;
            while (true)
            {
                GridPiece spawnedObject = Instantiate(gridPieceObject);

                spawnedObject.SpawnLocation(gridX, howManyUppPosetiveX);

                gridX--;

                if (gridX < 0)
                {
                    howManyUppPosetiveX++;
                    break;
                }
            }

            gridX = maxXPosetive;

            while (true)
            {
                GridPiece spawnedObject = Instantiate(gridPieceObject);

                spawnedObject.SpawnLocation(gridX, howManyDownPosetiveX);

                gridX--;

                if (gridX < 0)
                {
                    howManyDownPosetiveX--;
                    break;
                }
            }

            gridX = -maxXPosetive;

            while (true)
            {
                if (howManyUppNegativeX >= maxXNegative)
                {
                    break;
                }

                GridPiece spawnedObject = Instantiate(gridPieceObject);

                spawnedObject.SpawnLocation(gridX, howManyUppNegativeX);

                gridX++;

                if (gridX >= 0)
                {
                    howManyUppNegativeX++;
                    break;
                }
            }

            gridX = -maxXPosetive;

            while (true)
            {
                if(howManyDownNegativeX <= maxYNegative)
                {
                    break;
                }
                GridPiece spawnedObject = Instantiate(gridPieceObject);

                spawnedObject.SpawnLocation(gridX, howManyDownNegativeX);

                gridX++;

                if (gridX >= 0)
                {
                    howManyDownNegativeX--;
                    break;
                }
            }

            test++;

            if (howManyUppPosetiveX >= maxYPosetive)
            {
                break;
            }
        }
    }

}
