using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameManagerSr : MonoBehaviour
{
    #region Scripts

    GridPiece[] gridPieces;
    GridController gridController;
    Shops shops;

    static GameManagerSr instance;

    #endregion

    #region Enemy Spawn

    [Header("Enemy Spawn")]

    public int howManyEnemiesBought = 0;
    public List<int> whatTypeOfEnemyWasBought = new List<int>();

    #endregion

    #region Shop

    [Header("Shop")]

    public bool nextSceneShopTrue = false;
    int whatShopNumber;

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

    [Header("Upgrades")]

    [Header("Player Upgrades")]

    #region Player

    [Header("Pawn Upgrades")]

    #region Pawn

    public int howManyExtraSteepsPawn = 0;

    public bool pawmMoveAllDirections = false;
    public bool pawnMoveWhereAttack = false;

    #endregion

    [Header("Horse Upgrades")]

    #region Horse

    public bool horseCanMoveWhereHeJumpsOver = false;
    public bool horseCanAttackWhereHeJumpsOver = false;
    public bool horseAerialStrike = false;

    #endregion

    [Header("Tower Upgrades")]

    #region Tower

    public bool towerArtillery = false;
    public bool towerProtect = false;

    #endregion

    #region Bishop



    #endregion

    #region Queen



    #endregion

    #endregion

    [Header("Enemy Upgrades")]

    #region Enemy

    [Header("Pawn Upgrades")]

    #region Pawn

    public int enemyPawnHowManyExtraSteeps = 0; // Tier 1

    public bool enemyPawnMoveAllDirections = false; // Tier 2
    public bool enemyPawnMoveWhereAttack = false;  // Tier 1

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

    #region ShowInfo

    [Header("Show Info")]

    [SerializeField] GameObject infoObject;

    #endregion


    private void Awake()
    {

        if (!GameObject.Find("CoinUI"))
        {
            Instantiate(coinUi);
        }

        if (infoObject != null)
        {
            infoObject.SetActive(false);
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

    #region Upgrade Buttons

    public void BuyUpgrade(string whatUpgrade)
    {

        switch (whatUpgrade)
        {

            #region Pawn

            case "PawnExtraMovement":

                if(money >= 2)
                {

                    howManyExtraSteepsPawn++;

                    money -= 2;

                }

                break;

            case "PawnMoveAllDirections":

                if (money >= 4)
                {

                    pawmMoveAllDirections = true;

                    money -= 4;

                }

                break;

            case "PawnMoveWhereAttack":

                if (money >= 3)
                {

                    pawnMoveWhereAttack = true;

                    money -= 3;

                }

                break;

            #endregion

            #region Horse

            case "HorseMoveWhereHeJumpsOver":

                if (money >= 6)
                {

                    horseCanMoveWhereHeJumpsOver = true;

                    money -= 6;

                }

                break;

            case "HorseAttackWhereHeJumpsOver":

                if (money >= 4)
                {

                    horseCanAttackWhereHeJumpsOver = true;

                    money -= 4;

                }

                break;

            case "HorseAerialStrike":

                if (money >= 7)
                {

                    horseAerialStrike = true;

                    money -= 7;

                }

                break;

            #endregion

            #region Tower

            case "TowerArtillery":

                if(money >= 2)
                {

                    towerArtillery = true;

                    money -= 2;

                }

                break;

            case "TowerProtect":

                if (money >= 3)
                {

                    towerProtect = true;

                    money -= 3;

                }

                break;

                #endregion

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

        howManyPointsForEnemys -= numberOfPointsForUnits; // Tar bort antal poäng från pjäser och resten av poängen går till upgraderingar

        numberOfPointsForUnits++; // Så det altid finns minnst en fiende
        Debug.Log(numberOfPointsForUnits + " How Many Points");

        #region Bools

        bool pawnBought = false;
        bool towerBought = false;
        bool bishopBought = false;
        bool queenBought = false;
        bool horseBought = false;

        #endregion

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

                        switch (pawnOrHorse)
                        {

                            case 0:

                                pawnBought = true;

                                break;

                            case 1:

                                horseBought = true;

                                break;

                        }

                    }

                break;


                case 1:

                    if (numberOfPointsForUnits >= 2) //Buy Bishop Or Tower
                    {
                        int bishopOrTower = Random.Range(2, 4);

                        howManyEnemiesBought++;
                        whatTypeOfEnemyWasBought.Add(bishopOrTower);
                        numberOfPointsForUnits -= 2;

                        switch (bishopOrTower)
                        {

                            case 2:

                                towerBought = true;

                                break;

                            case 3:

                                bishopBought = true;

                                break;

                        }

                    }

                break;


                case 2:

                    if (numberOfPointsForUnits >= 3) //Buy Queen
                    {

                        howManyEnemiesBought++;
                        whatTypeOfEnemyWasBought.Add(4);
                        numberOfPointsForUnits -= 3;

                        queenBought = true;

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
            int toWhatUnitBuyUpgrade = Random.Range(0, 5);

            switch (randomWhaUpgradeToBuy)
            {
                case 0:

                    if (howManyPointsToBuyUpgrades >= 0.5f)
                    {

                        //Buy Tier 1 upgrade

                        switch (toWhatUnitBuyUpgrade)
                        {

                            case 0: // Pawn

                                enemyPawnHowManyExtraSteeps++;

                                howManyPointsToBuyUpgrades -= 0.5f;

                                break;

                        }

                        break;

                    }

                break;

                case 1:

                    if (howManyPointsToBuyUpgrades >= 1)
                    {

                        //Buy Tier 2 upgrade

                        switch (toWhatUnitBuyUpgrade)
                        {

                            case 0: // Pawn

                                int randomPawnUpgrade = Random.Range(0, 2);

                                switch (randomPawnUpgrade) // Random vilken pawn Upgradering
                                {

                                    case 0:

                                        if (!enemyPawnMoveAllDirections)
                                        {
                                            enemyPawnMoveAllDirections = true;

                                            howManyPointsToBuyUpgrades -= 1;
                                        }

                                        break;

                                    case 1:

                                        if (!enemyPawnMoveWhereAttack)
                                        {

                                            enemyPawnMoveWhereAttack = true;

                                            howManyPointsToBuyUpgrades -= 1;

                                        }

                                        break;

                                }


                                break;

                        }

                        break;

                    }

                break;

                case 2:

                    if (randomWhaUpgradeToBuy == 2 && howManyPointsToBuyUpgrades >= 1.5f)
                    {

                        //Buy Tier 3 upgrade

                    }

                break;
            }


            if (howManyPointsToBuyUpgrades <= 0)
            {
                break;
            }

        }

        gridController = FindAnyObjectByType<GridController>();

        gridController.ReciveEnemyUpgrades();

        #endregion

    }

    #endregion

    #region Show Info

    public void ShowInfo(GameObject whoCalled)
    {

        GridPiece piece = whoCalled.GetComponent<GridPiece>();

        if (infoObject == null)
        {

            infoObject = GameObject.Find("ShowInfoCollection");
            infoObject = infoObject.transform.GetChild(0).gameObject;

        }

        infoObject.transform.position = whoCalled.transform.position + new Vector3(5, 2);
        infoObject.SetActive(true);

        TextMeshProUGUI showcaseText = infoObject.GetComponentInChildren<TextMeshProUGUI>();

        int numberOfUpgrades = 0;

        if (piece.playerPieceHere == true) // Spelare
        {
            #region Player

            switch (piece.currentPlayerType)
            {

                case PlayerType.Pawn:

                    if (pawmMoveAllDirections)
                    {
                        showcaseText.text += "\nPawn move all directions";

                        numberOfUpgrades++;
                    }
                    if (pawnMoveWhereAttack)
                    {
                        showcaseText.text += "\nPawn move where attack";

                        numberOfUpgrades++;
                    }
                    if(howManyExtraSteepsPawn > 0)
                    {
                        showcaseText.text += "\n" + howManyExtraSteepsPawn.ToString() + " How many extra steps";

                        numberOfUpgrades++;
                    }



                    break;

                case PlayerType.Tower:

                    if (towerArtillery)
                    {
                        showcaseText.text += "\nTower artillery";

                        numberOfUpgrades++;
                    }

                    break;

                case PlayerType.Bishop:



                    break;

                case PlayerType.Queen:



                    break;

                case PlayerType.Horse:

                    if (horseAerialStrike)
                    {
                        showcaseText.text += "\nHorse aerial strike";

                        numberOfUpgrades++;
                    }
                    if (horseCanAttackWhereHeJumpsOver)
                    {
                        showcaseText.text += "\nHorse can attack where he jumps over";

                        numberOfUpgrades++;
                    }
                    if (horseCanMoveWhereHeJumpsOver)
                    {
                        showcaseText.text += "\nHorse can move where he jumps over";

                        numberOfUpgrades++;
                    }

                    break;

            }

            #endregion

        }
        else // Fiende
        {

            #region Enemy

            switch (piece.currentEnemyType)
            {

                case EnemyType.Pawn:


                    if (enemyPawnMoveAllDirections)
                    {
                        showcaseText.text += "\n-Pawn move all directions";

                        numberOfUpgrades++;
                    }
                    if (enemyPawnHowManyExtraSteeps > 0)
                    {
                        showcaseText.text += "\n-" + enemyPawnHowManyExtraSteeps.ToString() + " How many extra steps";

                        numberOfUpgrades++;
                    }

                    break;

                case EnemyType.Tower:



                    break;

                case EnemyType.Bishop:



                    break;

                case EnemyType.Queen:



                    break;

                case EnemyType.Horse:



                    break;

            }

            #endregion

        }

        if (numberOfUpgrades == 0)
        {

            showcaseText.text = " \nNo Upgrades";

        }

    }

    public void StopShowingInfo()
    {

        TextMeshProUGUI showcaseText = infoObject.GetComponentInChildren<TextMeshProUGUI>();

        showcaseText.text = "";

        infoObject.SetActive(false);

    }

    #endregion

}
