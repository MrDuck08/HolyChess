using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shops : MonoBehaviour
{

    #region Who Am I

    public enum ShopType
    {
        BasicShop,
        PawnShop,
        TowerShop,
        BishopShop,
        QueenShop,
        HorseShop
    }

    [Header("Who Am I?")]

    public ShopType typeOfShop;

    #endregion

    #region Upgrades

    [Header("Upgrades")]

    [SerializeField] List<Transform> itemsInShopPosition = new List<Transform>();
    List<GameObject> upgradesObjectsTotal = new List<GameObject>();

    [Header("Types Of Upgrades")]

    #region General

    List<GameObject> allGeneralUpgradeObjects = new List<GameObject>();
    List<GameObject> generalUpgradeObjects = new List<GameObject>();

    #endregion

    #region Pawn

    List<GameObject> allPawnUpgradeObjects = new List<GameObject>();
    List<GameObject> pawnUpgradeObjects = new List<GameObject>();

    #endregion

    #region Horse

    List<GameObject> allHorseUpgradeObjects = new List<GameObject>();
    List<GameObject> horseUpgradeObjects = new List<GameObject>();

    #endregion

    #region Tower

    List<GameObject> allTowerUpgradeObjects = new List<GameObject>();
    List<GameObject> towerUpgradeObjects = new List<GameObject>();

    #endregion

    #region Bishop

    List<GameObject> allBishopUpgradeObjects = new List<GameObject>();
    List<GameObject> bishopUpgradeObjects = new List<GameObject>();

    #endregion

    #region Queen

    List<GameObject> allQueenUpgradeObjects = new List<GameObject>();
    List<GameObject> queenUpgradeObjects = new List<GameObject>();

    #endregion

    #endregion

    void Start()
    {

        #region Add Upgrades

        Transform Upgrades = transform.Find("Upgrades");
        Transform generalUpgradesObject = Upgrades.Find("GeneralUpgrades");
        foreach (Transform child in generalUpgradesObject.transform)
        {
            if (child.tag == "GeneralUpgrades")
            {
                generalUpgradeObjects.Add(child.gameObject);

            }

        }

        Transform pawnUpgradesObject = Upgrades.Find("PawnUpgrades");
        foreach (Transform child in pawnUpgradesObject.transform)
        {
            if (child.tag == "Pawn")
            {
                pawnUpgradeObjects.Add(child.gameObject);

            }

        }

        Transform HorseUpgradesObject = Upgrades.Find("HorseUpgrades");
        foreach (Transform child in HorseUpgradesObject.transform)
        {
            if (child.tag == "PlayerHorse")
            {
                horseUpgradeObjects.Add(child.gameObject);

            }

        }

        Transform TowerUpgradesObject = Upgrades.Find("TowerUpgrades");
        foreach (Transform child in TowerUpgradesObject.transform)
        {
            if (child.tag == "PlayerTower")
            {
                towerUpgradeObjects.Add(child.gameObject);

            }

        }

        Transform BishopUpgradesObject = Upgrades.Find("BishopUpgrades");
        foreach (Transform child in BishopUpgradesObject.transform)
        {
            if (child.tag == "PlayerBishop")
            {
                bishopUpgradeObjects.Add(child.gameObject);

            }

        }

        Transform QueenUpgradesObject = Upgrades.Find("QueenUpgrades");
        foreach (Transform child in QueenUpgradesObject.transform)
        {
            if (child.tag == "PlayerQueen")
            {
                queenUpgradeObjects.Add(child.gameObject);

            }

        }

        #endregion

        if (typeOfShop == ShopType.BasicShop)
        {
            upgradesObjectsTotal.AddRange(generalUpgradeObjects);
            upgradesObjectsTotal.AddRange(pawnUpgradeObjects);
            upgradesObjectsTotal.AddRange(horseUpgradeObjects);
            upgradesObjectsTotal.AddRange(towerUpgradeObjects);
            upgradesObjectsTotal.AddRange(bishopUpgradeObjects);
            upgradesObjectsTotal.AddRange(queenUpgradeObjects);
        }

        for (int i = 0; i < itemsInShopPosition.Count; i++)
        {

            if(i <= 2)
            {

                #region Who To Upgrade

                if (typeOfShop == ShopType.BasicShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, upgradesObjectsTotal.Count);

                    upgradesObjectsTotal[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    upgradesObjectsTotal[whatUpgradeToChoce].SetActive(true);
                    upgradesObjectsTotal.Remove(upgradesObjectsTotal[whatUpgradeToChoce]);
                }

                if (typeOfShop == ShopType.PawnShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, pawnUpgradeObjects.Count);

                    pawnUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    pawnUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                    pawnUpgradeObjects.Remove(pawnUpgradeObjects[whatUpgradeToChoce]);
                }

                if (typeOfShop == ShopType.TowerShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, towerUpgradeObjects.Count);

                    towerUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    towerUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                    towerUpgradeObjects.Remove(towerUpgradeObjects[whatUpgradeToChoce]);
                }

                if (typeOfShop == ShopType.BishopShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, bishopUpgradeObjects.Count);

                    bishopUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    bishopUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                    bishopUpgradeObjects.Remove(bishopUpgradeObjects[whatUpgradeToChoce]);
                }

                if (typeOfShop == ShopType.QueenShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, queenUpgradeObjects.Count);

                    queenUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    queenUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                    queenUpgradeObjects.Remove(queenUpgradeObjects[whatUpgradeToChoce]);
                }

                if (typeOfShop == ShopType.HorseShop)
                {
                    int whatUpgradeToChoce = Random.Range(0, horseUpgradeObjects.Count);

                    horseUpgradeObjects[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                    horseUpgradeObjects[whatUpgradeToChoce].SetActive(true);
                    horseUpgradeObjects.Remove(horseUpgradeObjects[whatUpgradeToChoce]);
                }

                #endregion

            }
            else
            {

                if (typeOfShop != ShopType.BasicShop)
                {
                    upgradesObjectsTotal.AddRange(generalUpgradeObjects);
                    upgradesObjectsTotal.AddRange(pawnUpgradeObjects);
                    upgradesObjectsTotal.AddRange(horseUpgradeObjects);
                    upgradesObjectsTotal.AddRange(towerUpgradeObjects);
                    upgradesObjectsTotal.AddRange(bishopUpgradeObjects);
                    upgradesObjectsTotal.AddRange(queenUpgradeObjects);
                }

                int whatUpgradeToChoce = Random.Range(0, upgradesObjectsTotal.Count);

                upgradesObjectsTotal[whatUpgradeToChoce].transform.position = itemsInShopPosition[i].transform.position;
                upgradesObjectsTotal[whatUpgradeToChoce].SetActive(true);
                upgradesObjectsTotal.Remove(upgradesObjectsTotal[whatUpgradeToChoce]);

            }


        }

    }

}
