using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("How Much In Inventory")]

    public int pawnsInInventory = 0;
    public int horseInInvenory = 0;
    public int towersInInventory = 0;
    public int bishopInInventory = 0;
    public int queenInInventory = 0;

    [Header("Place Pieces Visual")]

    [SerializeField] GameObject addPawnButton;
    [SerializeField] GameObject addTowerButton;
    [SerializeField] GameObject addBishopButton;
    [SerializeField] GameObject addQueenButton;
    [SerializeField] GameObject addHorseButton;

    [Header("Game Has Started")]

    public bool gameHasStarted = false;

    GridPiece[] gridPieces;
    GameManagerSr GameManager;

    private void Start()
    {
        StartCoroutine(DelayStart());

    }

    IEnumerator DelayStart()
    {

        yield return new WaitForSeconds(0.1f);

        GameManager = FindAnyObjectByType<GameManagerSr>();

        pawnsInInventory = GameManager.pawnsInInventory;
        towersInInventory = GameManager.towersInInventory;
        bishopInInventory = GameManager.bishopInInventory;
        queenInInventory = GameManager.queenInInventory;
        horseInInvenory = GameManager.horseInInvenory;

    }

    private void Update()
    {

        #region Place Unit Visuals

        if (addPawnButton == null)
        {

            return;

        }

        if (!gameHasStarted)
        {

            if (pawnsInInventory == 0)
            {
                addPawnButton.SetActive(false);
            }
            else
            {
                addPawnButton.SetActive(true);
            }
            if (towersInInventory == 0)
            {
                addTowerButton.SetActive(false);
            }
            else
            {
                addTowerButton.SetActive(true);
            }
            if (bishopInInventory == 0)
            {
                addBishopButton.SetActive(false);
            }
            else
            {
                addBishopButton.SetActive(true);
            }
            if (queenInInventory == 0)
            {
                addQueenButton.SetActive(false);
            }
            else
            {
                addQueenButton.SetActive(true);
            }
            if (horseInInvenory == 0)
            {
                addHorseButton.SetActive(false);
            }
            else
            {
                addHorseButton.SetActive(true);
            }

            addPawnButton.GetComponentInChildren<TextMeshProUGUI>().text = pawnsInInventory.ToString();
            addTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = towersInInventory.ToString();
            addBishopButton.GetComponentInChildren<TextMeshProUGUI>().text = bishopInInventory.ToString();
            addQueenButton.GetComponentInChildren<TextMeshProUGUI>().text = queenInInventory.ToString();
            addHorseButton.GetComponentInChildren<TextMeshProUGUI>().text = horseInInvenory.ToString();

        }
        else
        {
            addPawnButton.SetActive(false);
            addTowerButton.SetActive(false);
            addBishopButton.SetActive(false);
            addQueenButton.SetActive(false);
            addHorseButton.SetActive(false);
        }

        #endregion

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

                towersInInventory--;
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

        GameManager = FindAnyObjectByType<GameManagerSr>();

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

    #region Buy Upgrade

    public void BuyUpgrade(string whatUpgrade)
    {

        GameManager = FindAnyObjectByType<GameManagerSr>();

        GameManager.BuyUpgrade(whatUpgrade);

    }

    #endregion
}
