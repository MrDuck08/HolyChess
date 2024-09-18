using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    GridPiece[] gridPieces;

    Inventory inventory;

    public void StartMatch(GameObject button)
    {
        inventory = FindObjectOfType<Inventory>();
        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            allPieces.gameHasStarted = true;
        }

        inventory.gameHasStarted = true;
        button.SetActive(false);

    }
}
