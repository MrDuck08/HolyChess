using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject[] whatShopButton;

    GridPiece[] gridPieces;
    Shops shopsScript;
    Inventory inventory;
    GameManagerSr gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManagerSr>();

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            shopsScript = FindAnyObjectByType<Shops>();

            int WhatShopToAcive = Random.Range(0, 6);

            // 0 = General
            // 1 = Pawn
            // 2 = Tower
            // 3 = Bishop
            // 4 = Queen
            // 5 = Horse

            whatShopButton[WhatShopToAcive].SetActive(true);

            gameManager.NextSceneWhatShop(WhatShopToAcive);

        }
    }

    #region Start Match Game And End Round

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

    public void EndRound()
    {

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            if (allPieces.enemyPieceHere)
            {
                allPieces.playerTurn = false;
            }

            allPieces.movedOnce = false;
        }

    }

    #endregion

    #region Change Scenes

    public void ChangeScene(int buildIndex)
    {

        if (buildIndex == 1337) // Skip Shop Button
        {

            // Gameplay Scene
            SceneManager.LoadScene(1);

            gameManager.nextSceneShopTrue = false;

            // Add Coins
        }
        else
        {
            SceneManager.LoadScene(buildIndex);
        }
    }

    public void ChangeSceneToShop(int buildIndex)
    {
        
        SceneManager.LoadScene(buildIndex);

    }

    public void ReloadScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void QuitGame()
    {
        Debug.Log("Left game");
        Application.Quit();
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = LoopBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(nextSceneIndex);
    }

    private int LoopBuildIndex(int buildIndex)
    {
        if (buildIndex >= SceneManager.sceneCountInBuildSettings)
        {
            buildIndex = 0;
        }

        return buildIndex;
    }

    #endregion
}
