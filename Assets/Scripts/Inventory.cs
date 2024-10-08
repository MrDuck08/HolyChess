using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int pawnsInInventory = 0;
    public int horseInInvenory = 0;
    public int towersInInventory = 0;
    public int bishopInInventory = 0;
    public int queenInInventory = 0;

    public bool gameHasStarted = false;

    GridPiece[] gridPieces;

    #region Place Pieces

    public void PlacePawn()
    {
        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
        foreach (GridPiece piece in gridPieces)
        {
            if (pawnsInInventory > 0 && !gameHasStarted && piece.placingDownAUnitNow == false)
            {

                foreach (GridPiece allPieces in gridPieces)
                {
                    allPieces.spawningPawnNow = true;
                    allPieces.placingDownAUnitNow = true;
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
            if (horseInInvenory > 0 && !gameHasStarted && piece.placingDownAUnitNow == false)
            {
                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                foreach (GridPiece allPieces in gridPieces)
                {
                    allPieces.spawningHorseNow = true;
                    allPieces.placingDownAUnitNow = true;
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
            if (towersInInventory > 0 && !gameHasStarted && piece.placingDownAUnitNow == false)
            {
                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                foreach (GridPiece allPieces in gridPieces)
                {
                    allPieces.spawningTowerNow = true;
                    allPieces.placingDownAUnitNow = true;
                }

                horseInInvenory--;
            }
        }
    }

    public void PlaceBishop()
    {
        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
        foreach (GridPiece piece in gridPieces)
        {
            if (bishopInInventory > 0 && !gameHasStarted && piece.placingDownAUnitNow == false)
            {
                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                foreach (GridPiece allPieces in gridPieces)
                {
                    allPieces.spawningBishopNow = true;
                    allPieces.placingDownAUnitNow = true;
                }

                bishopInInventory--;
            }
        }
    }

    public void PlaceQueen()
    {
        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];
        foreach (GridPiece piece in gridPieces)
        {
            if (queenInInventory > 0 && !gameHasStarted && piece.placingDownAUnitNow == false)
            {
                gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

                foreach (GridPiece allPieces in gridPieces)
                {
                    allPieces.spawningQueenNow = true;
                    allPieces.placingDownAUnitNow = true;
                }

                queenInInventory--;
            }
        }
    }

    #endregion
}
