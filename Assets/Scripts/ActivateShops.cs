using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShops : MonoBehaviour
{
    GameManagerSr[] gameManager;

    void Awake()
    {

        gameManager = FindObjectsOfType<GameManagerSr>();

        for (int i = 0; i < gameManager.Length; i++)
        {

            if (gameManager[i].nextSceneShopTrue == true)
            {
                gameManager[i].ActivateShop();
            }

        }


        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }

}
