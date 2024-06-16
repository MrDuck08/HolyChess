using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int pawnsInInventory = 0;

    public bool gameHasStarted = false;

    GridPiece[] gridPieces;

    public void PlacePawn()
    {
        if(pawnsInInventory > 0 && !gameHasStarted)
        {
            gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

            foreach (GridPiece allPieces in gridPieces)
            {
                allPieces.spawningPawnNow = true;
            }

            pawnsInInventory--;
        }
    }
}
