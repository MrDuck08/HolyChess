using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerSr : MonoBehaviour
{
    #region Scripts

    GridPiece[] gridPieces;
    Shops shops;

    static GameManagerSr instance;

    #endregion

    #region Shop

    [Header("Shop")]

    public bool nextSceneShopTrue = false;
    int whatShopNumber;
    public int howManyEnemiesBought = 0;
    public List<int> whatTypeOfEnemyWasBought = new List<int>();

    public Shops.ShopType forbidenShop = Shops.ShopType.none;
    public Shops.ShopType guaranteedShop = Shops.ShopType.none;

    [SerializeField] GameObject aktivateShopObject;

    #endregion

    #region Inventory

    [Header("Inventory")]

    public int pawnsInInventory = 0;
    public int horseInInvenory = 0;
    public int towersInInventory = 0;
    public int bishopInInventory = 0;
    public int queenInInventory = 0;

    #endregion

    #region Money

    [Header("Money")]

    [SerializeField] GameObject coinUi;
    public int money = 0;

    #endregion

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

    #region Enemy Difficulty

    [Header("Scaling")]

    public int howManyPointsForEnemys = 10;
    public int whatRound = 0;

    #endregion


    private void Awake()
    {

        if (!GameObject.Find("CoinUI"))
        {
            Instantiate(coinUi);
        }

        int numberOfGameManagers = FindObjectsOfType<GameManagerSr>().Length;

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Instantiate(aktivateShopObject);
            Destroy(gameObject);

        }

    }

    private void Update()
    {

        coinUi = GameObject.Find("CoinUI(Clone)"); // Clone För att den är instansiatad

        foreach(Transform text in coinUi.transform)
        {
            text.gameObject.GetComponent<TextMeshProUGUI>().text = money.ToString("0");
        }

    }

    #region Shop

    public void NextSceneWhatShop(int whatShop)
    {
        whatShopNumber = whatShop;

        nextSceneShopTrue = true;

    }

    public void ActivateShop()
    {
        if (nextSceneShopTrue)
        {
            shops = FindAnyObjectByType<Shops>();

            switch (whatShopNumber)
            {
                case 0:

                    shops.typeOfShop = Shops.ShopType.BasicShop;

                    break;
                case 1:

                    shops.typeOfShop = Shops.ShopType.PawnShop;

                    break;
                case 2:

                    shops.typeOfShop = Shops.ShopType.TowerShop;

                    break;
                case 3:

                    shops.typeOfShop = Shops.ShopType.BishopShop;

                    break;
                case 4:

                    shops.typeOfShop = Shops.ShopType.QueenShop;

                    break;
                case 5:

                    shops.typeOfShop = Shops.ShopType.HorseShop;

                    break;
                default:
                    Debug.Log("How the fuck did you go out of range to here!?!?!?!?");
                    break;

            }

                nextSceneShopTrue = false;
            
        }
    }

    #endregion

    #region Enemy Geeting Harder

    // Varje Nivå har olika antal poäng att spendera på olika fiender och upgraderingar
    // 3 Tiers av fiender, Tier 1 (1p): Bonde Och Häst, Tier 2 (2p): Löpare Och Torn, Tier 3 (3p): Dam
    // 3 Tiers av upgraderingar, Tier 1 (0.5p), Tier 2 (1p), Tier 3 (1.5p)

    public void DistributePoints()
    {
        int numberOfPointsForUnits = Random.Range(0, howManyPointsForEnemys); // Hur mycket poäng till pjäser
        Debug.Log(numberOfPointsForUnits + " How Many Points");

        howManyPointsForEnemys -= numberOfPointsForUnits; // Tar bort antal poäng från pjäser och resten av poängen går till upgraderingar

        numberOfPointsForUnits++; // Så det altid finns minnst en fiende

        #region Unit Buy

        while (true)
        {
            int randomWhatUnitToBuy = Random.Range(0, 3);

            switch (randomWhatUnitToBuy)
            {
                case 0:

                    if (numberOfPointsForUnits >= 1) //Buy Pawn Or Horse
                    {
                        int pawnOrHorse = Random.Range(0, 2);

                        howManyEnemiesBought++;
                        whatTypeOfEnemyWasBought.Add(pawnOrHorse);
                        numberOfPointsForUnits--;

                    }

                break;


                case 1:

                    if (numberOfPointsForUnits >= 2) //Buy Bishop Or Tower
                    {
                        int bishopOrTower = Random.Range(2, 4);

                        howManyEnemiesBought++;
                        whatTypeOfEnemyWasBought.Add(bishopOrTower);
                        numberOfPointsForUnits -= 2;

                    }

                break;


                case 2:

                    if (numberOfPointsForUnits >= 3) //Buy Queen
                    {

                        howManyEnemiesBought++;
                        whatTypeOfEnemyWasBought.Add(4);
                        numberOfPointsForUnits -= 3;

                    }

                break;
            }

            if (numberOfPointsForUnits <= 0)
            {
                break;
            }
        }

        #endregion

        float howManyPointsToBuyUpgrades = howManyPointsForEnemys;

        #region Upgrade Buy

        while (true)
        {
            int randomWhaUpgradeToBuy = Random.Range(0, 3);

            switch (randomWhaUpgradeToBuy)
            {
                case 0:

                    if (howManyPointsToBuyUpgrades >= 0.5f)
                    {

                        //Buy Pawn Or Horse

                        howManyPointsToBuyUpgrades -= 0.5f;
                    }

                break;

                case 1:

                    if (howManyPointsToBuyUpgrades >= 1)
                    {

                        //Buy Bishop Or Tower

                        howManyPointsToBuyUpgrades -= 1;
                    }

                break;

                case 2:

                    if (randomWhaUpgradeToBuy == 2 && howManyPointsToBuyUpgrades >= 1.5f)
                    {

                        //Buy Pawn Or Horse

                        howManyPointsToBuyUpgrades -= 1.5f;
                    }

                break;
            }


            if (howManyPointsToBuyUpgrades <= 0)
            {
                break;
            }

        }

        #endregion

    }

    #endregion

}
