using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PieceVisual : MonoBehaviour
{

    int currenType;

    float speed = 3;

    bool startMoving = false;
    bool startMovingBack = false;
    public bool isItPlayer;
    bool isPlayerAttacking;
    bool swithchingPlace;
    public bool animationIsPlaying = false;
    public bool waitUntilReviveDone = false;

    GameObject objectToMoveTo;
    GameObject objectToMoveFrom;
    GameObject objectToMoveToBack;

    Color32 unitVisuals;

    PlayerType playerType;
    EnemyType enemyType;

    GridController gridController;

    private void Update()
    {

        #region Move

        if (startMoving)
        {

            transform.position = Vector2.MoveTowards(transform.position, objectToMoveTo.transform.position, speed * Time.deltaTime);

            if(transform.position == objectToMoveTo.transform.position)
            {


                if (isItPlayer) // Spelare som r�r sig
                {

                    #region Player

                    if (isPlayerAttacking)
                    {

                        objectToMoveTo.GetComponent<GridPiece>().enemyPieceHere = false;
                        Destroy(objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals);

                        gridController = FindObjectOfType<GridController>();

                        gridController.numberOfEnemys--;

                    }

                    objectToMoveTo.GetComponent<GridPiece>().playerPieceHere = true;
                    objectToMoveTo.GetComponent<GridPiece>().currentPlayerType = playerType;
                    objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals = gameObject;

                    objectToMoveTo.GetComponent<GridPiece>().movedOnce = true;

                    if (!swithchingPlace) // Om den ska byta plats med n�gon d� ska den inte st�nga av n�gonting d�r den kom ifr�n
                    {

                        objectToMoveFrom.GetComponent<GridPiece>().playerPieceHere = false;
                        objectToMoveFrom.GetComponent<GridPiece>().currentPieceVisuals = null;

                    }

                    SpawnInfo((int)playerType, transform.position, true);

                    #endregion

                }
                else // Fiende som r�r sig
                {

                    #region Enemy

                    gridController = FindObjectOfType<GridController>();

                    objectToMoveTo.GetComponent<GridPiece>().CheckWhoDied(objectToMoveFrom.GetComponent<GridPiece>(), gameObject.GetComponent<PieceVisual>());


                    if(startMovingBack == true) // Om den r�r sig tillbacka ska den h�r koden k�ras
                    {

                        objectToMoveFrom.GetComponent<GridPiece>().enemyPieceHere = true;
                        objectToMoveFrom.GetComponent<GridPiece>().currentEnemyType = enemyType;
                        objectToMoveFrom.GetComponent<GridPiece>().currentPieceVisuals = gameObject;

                        objectToMoveToBack.GetComponent<GridPiece>().enemyPieceHere = false;

                        startMoving = false;
                        startMovingBack = false;
                        animationIsPlaying = false;
                        startMoving = false;

                        return; // Return s� den inte "r�r" sig till vart den igentligen skulle g�

                    }

                    if (gridController.reviveHappened && startMovingBack == false && waitUntilReviveDone == false || objectToMoveTo.GetComponent<GridPiece>().playerPieceHere == false) // Om ingen spelare �r vart man ska g� k�rs det h�r som vanligt men om det finns en spelare d�r kollas om spelaren har anv�nt sin revive
                    {

                        objectToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;
                        objectToMoveTo.GetComponent<GridPiece>().currentEnemyType = enemyType;
                        Destroy(objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals);
                        objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals = gameObject;

                        objectToMoveFrom.GetComponent<GridPiece>().enemyPieceHere = false;
                        objectToMoveFrom.GetComponent<GridPiece>().currentPieceVisuals = null;

                    }

                    SpawnInfo((int)enemyType, transform.position, false);

                    #endregion

                }

                startMoving = false;
                animationIsPlaying = false;
                startMoving = false;


            }

        }

        #endregion

    }

    #region Spawn

    public void SpawnInfo(int whatType, Vector2 pos, bool playerPiece)
    {

        currenType = whatType;

        transform.position = pos;

        Transform VisualsCollection = transform.Find("VisuallsCollection");

        switch ((int)currenType)
        {

            case (int)PlayerType.Pawn:

                foreach (Transform child in VisualsCollection)
                {
                    if (child.tag == "Pawn")
                    {
                        if (playerPiece)
                        {
                            unitVisuals.a = 144;
                        }
                        else
                        {
                            unitVisuals.a = 255;
                        }

                        child.gameObject.SetActive(true);

                        child.GetComponent<SpriteRenderer>().color = unitVisuals;

                    }
                    else
                    {

                        child.gameObject.SetActive(false);

                    }

                }

                break;

            case (int)PlayerType.Tower:

                foreach (Transform child in VisualsCollection)
                {
                    if (child.tag == "PlayerTower")
                    {
                        if (playerPiece)
                        {
                            unitVisuals.a = 144;
                        }
                        else
                        {
                            unitVisuals.a = 255;
                        }

                        child.gameObject.SetActive(true);

                        child.GetComponent<SpriteRenderer>().color = unitVisuals;

                    }
                    else
                    {

                        child.gameObject.SetActive(false);

                    }

                }

            break;

            case (int)PlayerType.Bishop:

                foreach (Transform child in VisualsCollection)
                {
                    if (child.tag == "PlayerBishop")
                    {
                        if (playerPiece)
                        {
                            unitVisuals.a = 144;
                        }
                        else
                        {
                            unitVisuals.a = 255;
                        }

                        child.gameObject.SetActive(true);

                        child.GetComponent<SpriteRenderer>().color = unitVisuals;

                    }
                    else
                    {

                        child.gameObject.SetActive(false);

                    }

                }

            break;

            case (int)PlayerType.Queen:

                foreach (Transform child in VisualsCollection)
                {
                    if (child.tag == "PlayerQueen")
                    {
                        if (playerPiece)
                        {
                            unitVisuals.a = 144;
                        }
                        else
                        {
                            unitVisuals.a = 255;
                        }

                        child.gameObject.SetActive(true);

                        child.GetComponent<SpriteRenderer>().color = unitVisuals;

                    }
                    else
                    {

                        child.gameObject.SetActive(false);

                    }

                }

            break;

            case (int)PlayerType.Horse:

                foreach (Transform child in VisualsCollection)
                {
                    if (child.tag == "PlayerHorse")
                    {
                        if (playerPiece)
                        {
                            unitVisuals.a = 144;
                        }
                        else
                        {
                            unitVisuals.a = 255;
                        }

                        child.gameObject.SetActive(true);

                        child.GetComponent<SpriteRenderer>().color = unitVisuals;

                    }
                    else
                    {

                        child.gameObject.SetActive(false);

                    }

                }

            break;

        }

    }

    #endregion

    #region Move info

    public void MovePiece(GameObject objectToGoTo, bool playerMoving, bool isPlayerAttack, GameObject originalGameobject, bool swithingPlace)
    {

        objectToMoveTo = objectToGoTo;
        objectToMoveFrom = originalGameobject;
       
        playerType = objectToMoveFrom.GetComponent<GridPiece>().currentPlayerType;
        enemyType = objectToMoveFrom.GetComponent<GridPiece>().currentEnemyType;

        isItPlayer = playerMoving;
        isPlayerAttacking = isPlayerAttack;
        animationIsPlaying = true;
        startMoving = true;
        swithchingPlace = swithingPlace;

    }

    public void GoBack()
    {
        objectToMoveToBack = objectToMoveTo;
        objectToMoveTo = objectToMoveFrom;

        animationIsPlaying = true;
        startMoving = true;
        startMovingBack = true;

    }

    #endregion

    public void DenyRevive()
    {

        objectToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;
        objectToMoveTo.GetComponent<GridPiece>().currentEnemyType = enemyType;
        Destroy(objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals);
        objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals = gameObject;

        objectToMoveTo.GetComponent<GridPiece>().playerPieceHere = false;

        objectToMoveFrom.GetComponent<GridPiece>().enemyPieceHere = false;
        objectToMoveFrom.GetComponent<GridPiece>().currentPieceVisuals = null;

    }
}
