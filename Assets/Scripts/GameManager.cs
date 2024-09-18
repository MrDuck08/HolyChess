using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GridPiece[] gridPieces;

    #region Upgrades

    #region Player

    #region Pawn

    int howManyExtraSteeps = 0;



    #endregion

    #region Horse



    #endregion

    #region Tower



    #endregion

    #region Bishop



    #endregion

    #region Queen



    #endregion

    #endregion

    #endregion

    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        DelayStart();
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(0.1f);


    }

    public void EndRound()
    {

        gridPieces = FindObjectsOfType(typeof(GridPiece)) as GridPiece[];

        foreach (GridPiece allPieces in gridPieces)
        {
            allPieces.playerTurn = false;

            allPieces.movedOnce = false;
        }

    }
    
}
