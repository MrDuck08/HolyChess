using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject[] whatShopButton;

    GridPiece[] gridPieces;
    PieceVisual[] pieceVisuals;
    Inventory inventory;
    GameManagerSr gameManager;
    GridController gridController;

    int whatShopToActivate;

    bool animationsAreDone;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManagerSr>();

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {

            while (true)
            {
                whatShopToActivate = Random.Range(0, (int)Shops.ShopType.total);

                if(whatShopToActivate != (int)gameManager.forbidenShop)
                {
                    break;
                }
            }

            if(gameManager.guaranteedShop != Shops.ShopType.none)
            {

                whatShopToActivate = (int)gameManager.guaranteedShop;
                gameManager.guaranteedShop = Shops.ShopType.none;

            }


            // 0 = General
            // 1 = Pawn
            // 2 = Tower
            // 3 = Bishop
            // 4 = Queen
            // 5 = Horse

            whatShopButton[whatShopToActivate].SetActive(true);

            gameManager.NextSceneWhatShop(whatShopToActivate);

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
        pieceVisuals = FindObjectsOfType(typeof(PieceVisual)) as PieceVisual[];
        gridController = FindObjectOfType<GridController>();

        animationsAreDone = true;

        foreach(PieceVisual piece in pieceVisuals)
        {

            if (piece.animationIsPlaying)
            {

                return;

            }

        }

        gridController.firstRoundDone = true;

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
            gameManager = FindAnyObjectByType<GameManagerSr>();
            // Gameplay Scene
            gameManager.nextSceneShopTrue = false;
            gameManager.money += 10;

            SceneManager.LoadScene(1);

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
