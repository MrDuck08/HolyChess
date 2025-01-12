using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonInfo : MonoBehaviour
{

    GameObject infoObjectRemembered;
    [SerializeField] GameObject whereToGoToObject;

    public void ShowInfo(GameObject infoObject)
    {
        infoObjectRemembered = infoObject;

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {

            infoObjectRemembered.transform.position = whereToGoToObject.transform.position;

        }

        infoObjectRemembered.SetActive(true);

    }

    public void StopShovingInfo()
    {

        infoObjectRemembered.SetActive(false);

    }

}
