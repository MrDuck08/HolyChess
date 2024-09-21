using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shops : MonoBehaviour
{

    #region Who Am I

    [Header("Who Am I?")]

    [SerializeField] bool iAmBasicShop = false;
    [SerializeField] bool iAmPawnShop = false;
    [SerializeField] bool iAmTowerShop = false;
    [SerializeField] bool iAmBishopShop = false;
    [SerializeField] bool iAmQueenShop = false;
    [SerializeField] bool iAmHorseShop = false;

    #endregion

    #region Upgrades

    [SerializeField] List<GameObject> itemsInShop = new List<GameObject>();

    #region Pawn

    int howManyUpgradesPawn;

    #endregion

    #region Horse

    int howManyUpgradesHorse;

    #endregion

    #region Tower

    int howManyUpgradesTower;

    #endregion

    #region Bishop

    int howManyUpgradesBishop;

    #endregion

    #region Queen

    int howManyUpgradesQueen;

    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < itemsInShop.Count; i++)
        {



        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
