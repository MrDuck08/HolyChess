using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] AudioSource clickSound;
    [SerializeField] AudioSource pawnClick;
    [SerializeField] AudioSource horseClick;


    public void PlaySound(string whatSoundString)
    {

        switch (whatSoundString)
        {

            case "Click":

                clickSound.Play();

                StartCoroutine(DestroyGameobjet(clickSound.clip.length));

                break;

            case "Pawn":

                pawnClick.Play();

                StartCoroutine(DestroyGameobjet(pawnClick.clip.length));

                break;

            case "Horse":

                horseClick.Play();

                StartCoroutine(DestroyGameobjet(horseClick.clip.length));   

                break;

        }

    }

    IEnumerator DestroyGameobjet(float timeToDestroy)
    {


        yield return new WaitForSeconds(timeToDestroy);

        Destroy(gameObject);

    }

}
