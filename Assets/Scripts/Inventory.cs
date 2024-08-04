using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int pawnsInInventory = 0;
    public int horseInInvenory = 0;
    public int towersInInventory = 0;

    public bool gameHasStarted = false;

    GridPiece[] gridPieces;

    public void PlacePawn()
    {
        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
        foreach (GridPiece piece in gridPieces)
        {
            if (pawnsInInventory > 0 && !gameHasStarted && piece.placingDownAUNitNow == false)
            {

                foreach (GridPiece allPieces in gridPieces)
                {
                    allPieces.spawningPawnNow = true;
                    allPieces.placingDownAUNitNow = true;
                }

                pawnsInInventory--;
            }
        }

    }

    public void PlaceHorse()
    {
        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
        foreach (GridPiece piece in gridPieces)
        {
            if (horseInInvenory > 0 && !gameHasStarted && piece.placingDownAUNitNow == false)
            {
                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                foreach (GridPiece allPieces in gridPieces)
                {
                    allPieces.spawningHorseNow = true;
                    allPieces.placingDownAUNitNow = true;
                }

                horseInInvenory--;
            }
        }
    }

    public void PlaceTower()
    {
        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
        foreach (GridPiece piece in gridPieces)
        {
            if (towersInInventory > 0 && !gameHasStarted && piece.placingDownAUNitNow == false)
            {
                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                foreach (GridPiece allPieces in gridPieces)
                {
                    allPieces.spawningTowerNow = true;
                    allPieces.placingDownAUNitNow = true;
                }

                horseInInvenory--;
            }
        }
    }
}
