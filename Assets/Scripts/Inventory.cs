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
    GameManagerSr GameManager;

    private void Start()
    {
        GameManager = FindAnyObjectByType<GameManagerSr>();

        pawnsInInventory = GameManager.pawnsInInventory;
        towersInInventory = GameManager.towersInInventory;
        bishopInInventory = GameManager.bishopInInventory;
        queenInInventory = GameManager.queenInInventory;
        horseInInvenory = GameManager.horseInInvenory;
    }

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
                    allPieces.currentSpawnType = SpawnType.Pawn;
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
                    allPieces.currentSpawnType = SpawnType.Horse;
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
                    allPieces.currentSpawnType = SpawnType.Tower;
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
                    allPieces.currentSpawnType = SpawnType.Bishop;
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
                    allPieces.currentSpawnType = SpawnType.Queen;
                    allPieces.placingDownAUnitNow = true;
                }

                queenInInventory--;
            }
        }
    }

    #endregion

    #region Add Pieces

    public void buyWhatUnit(int whatToBuy)
    {

        switch (whatToBuy)
        {

            case 0:

                if(GameManager.money >= 2 && pawnsInInventory < 5)
                {

                    pawnsInInventory++;
                    GameManager.pawnsInInventory++;

                    GameManager.money -= 2;
                    Debug.Log(pawnsInInventory);

                }

                break;

            case 1:

                if (GameManager.money >= 5 && towersInInventory < 5)
                {

                    towersInInventory++;
                    GameManager.towersInInventory++;

                    GameManager.money -= 5;

                }

                break;

            case 2:

                if (GameManager.money >= 4 && bishopInInventory < 5)
                {

                    bishopInInventory++;
                    GameManager.bishopInInventory++;

                    GameManager.money -= 4;

                }

                break;

            case 3:

                if (GameManager.money >= 7 && queenInInventory < 5)
                {

                    queenInInventory++;
                    GameManager.queenInInventory++;

                    GameManager.money -= 7;

                }

                break;

            case 4:

                if (GameManager.money >= 3 && horseInInvenory < 5)
                {

                    horseInInvenory++;
                    GameManager.horseInInvenory++;

                    GameManager.money -= 3;

                }

                break;
        }

    }

    #endregion
}
