using System.Collections;
using UnityEngine;

public class GameManagerSr : MonoBehaviour
{
    #region Scripts

    GridPiece[] gridPieces;
    Shops shops;

    static GameManagerSr instance;

    #endregion

    public bool nextSceneShopTrue = false;
    int whatShopNumber;

    [SerializeField] GameObject aktivateShopObject;


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
        //Debug.Log(nextSceneShop + " nextSceneShop True Or False");

    }

    IEnumerator DelayStart()
    {
        Debug.Log("DS1");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("DS2");

    }

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

    
}
