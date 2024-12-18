using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PieceVisual : MonoBehaviour
{

    int currenType;

    float speed = 3;

    bool startMoving = false;
    bool startMovingBack = false;
    bool isItPlayer;
    bool isPlayerAttacking;
    public bool animationIsPlaying = false;

    GameObject objectToMoveTo;
    GameObject objectToMoveFrom;
    GameObject objectToMoveToBack;

    Color32 unitVisuals;

    PlayerType playerType;
    EnemyType enemyType;

    GridController gridController;

    private void Update()
    {
        if (startMoving)
        {

            transform.position = Vector2.MoveTowards(transform.position, objectToMoveTo.transform.position, speed * Time.deltaTime);

            if(transform.position == objectToMoveTo.transform.position)
            {


                if (isItPlayer) // Spelare
                {

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


                    objectToMoveFrom.GetComponent<GridPiece>().playerPieceHere = false;
                    objectToMoveFrom.GetComponent<GridPiece>().currentPieceVisuals = null;

                    SpawnInfo((int)playerType, transform.position, true);

                }
                else // Fiende Som Rör På Sig
                {

                    gridController = FindObjectOfType<GridController>();

                    objectToMoveTo.GetComponent<GridPiece>().CheckWhoDied(objectToMoveFrom.GetComponent<GridPiece>());


                    if(startMovingBack == true) // Om den rör sig tillbacka ska den här koden köras
                    {

                        objectToMoveFrom.GetComponent<GridPiece>().enemyPieceHere = true;
                        objectToMoveFrom.GetComponent<GridPiece>().currentEnemyType = enemyType;
                        objectToMoveFrom.GetComponent<GridPiece>().currentPieceVisuals = gameObject;

                        objectToMoveToBack.GetComponent<GridPiece>().enemyPieceHere = false;

                        startMoving = false;
                        startMovingBack = false;
                        animationIsPlaying = false;
                        startMoving = false;

                        return;

                    }

                    if (gridController.reviveHappened && startMovingBack == false || objectToMoveTo.GetComponent<GridPiece>().playerPieceHere == false) // Om ingen spelare är vart man ska gå körs det här som vanligt men om det finns en spelare där kollas om spelaren har använt sin revive
                    {

                        objectToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;
                        objectToMoveTo.GetComponent<GridPiece>().currentEnemyType = enemyType;
                        Destroy(objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals);
                        objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals = gameObject;

                        objectToMoveFrom.GetComponent<GridPiece>().enemyPieceHere = false;
                        objectToMoveFrom.GetComponent<GridPiece>().currentPieceVisuals = null;

                    }

                    SpawnInfo((int)enemyType, transform.position, false);

                }

                startMoving = false;
                animationIsPlaying = false;
                startMoving = false;

            }

        }

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

    #region Movment

    public void MovePiece(GameObject objectToGoTo, bool playerMoving, bool isPlayerAttack, GameObject originalGameobject)
    {

        objectToMoveTo = objectToGoTo;
        objectToMoveFrom = originalGameobject;

        playerType = objectToMoveFrom.GetComponent<GridPiece>().currentPlayerType;
        enemyType = objectToMoveFrom.GetComponent<GridPiece>().currentEnemyType;

        startMoving = true;
        animationIsPlaying = true;
        isItPlayer = playerMoving;
        isPlayerAttacking = isPlayerAttack;

    }

    public void GoBack()
    {
        objectToMoveToBack = objectToMoveTo;
        objectToMoveTo = objectToMoveFrom;

        startMoving = true;
        startMovingBack = true;
        animationIsPlaying = true;

    }

    #endregion

    public void DentRevive()
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
