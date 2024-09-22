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

    [Header("Upgrades")]

    [SerializeField] List<Transform> itemsInShopPosition = new List<Transform>();
    [SerializeField] List<GameObject> upgradesObjectsTotal = new List<GameObject>();

    [Header("Types Of Upgrades")]

    #region General

    [SerializeField] List<GameObject> generalUpgradeObjects = new List<GameObject>();

    #endregion

    #region Pawn

    [SerializeField] List<GameObject> pawnUpgradeObjects = new List<GameObject>();

    #endregion

    #region Horse

    [SerializeField] List<GameObject> horseUpgradeObjects = new List<GameObject>();

    #endregion

    #region Tower

    [SerializeField] List<GameObject> towerUpgradeObjects = new List<GameObject>();

    #endregion

    #region Bishop

    [SerializeField] List<GameObject> bishopUpgradeObjects = new List<GameObject>();

    #endregion

    #region Queen

    [SerializeField] List<GameObject> queenUpgradeObjects = new List<GameObject>();

    #endregion

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        upgradesObjectsTotal.AddRange(generalUpgradeObjects);
        upgradesObjectsTotal.AddRange(pawnUpgradeObjects);
        upgradesObjectsTotal.AddRange(horseUpgradeObjects);
        upgradesObjectsTotal.AddRange(towerUpgradeObjects);
        upgradesObjectsTotal.AddRange(bishopUpgradeObjects);
        upgradesObjectsTotal.AddRange(queenUpgradeObjects);

        for (int i = 0; i < itemsInShopPosition.Count; i++)
        {

            if(i <= 1)
            {
                Debug.Log("Basic Upgrade");

                int whatUpgradeToChoce = Random.Range(0, upgradesObjectsTotal.Count);

                upgradesObjectsTotal[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                upgradesObjectsTotal[whatUpgradeToChoce].SetActive(true);
            }
            else
            {
                Debug.Log("Special Upgrade");

                #region Who To Upgrade

                if (iAmBasicShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, upgradesObjectsTotal.Count);

                    upgradesObjectsTotal[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    upgradesObjectsTotal[whatUpgradeToChoce].SetActive(true);
                }

                if (iAmPawnShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, pawnUpgradeObjects.Count);

                    pawnUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    pawnUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                }

                if (iAmTowerShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, towerUpgradeObjects.Count);

                    towerUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    towerUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                }

                if (iAmBishopShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, bishopUpgradeObjects.Count);

                    bishopUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    bishopUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                }

                if (iAmQueenShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, queenUpgradeObjects.Count);

                    queenUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    queenUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                }

                if (iAmHorseShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, horseUpgradeObjects.Count);

                    horseUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    horseUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                }

                #endregion
            }


        }

    }

}
