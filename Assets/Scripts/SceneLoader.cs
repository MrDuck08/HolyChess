using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    GridPiece[] gridPieces;
    [SerializeField] List<GameObject> Shops = new List<GameObject>();

    Inventory inventory;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            int WhatShopToAcive = Random.Range(0, Shops.Count);

            Shops[WhatShopToAcive].SetActive(true);
        }
    }

    #region Start Match Game

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

    #endregion

    #region Change Scenes

    public void ChangeScene(int buildIndex)
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
