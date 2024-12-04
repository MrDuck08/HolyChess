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
    bool isPlayerMoving;
    public bool animationIsPlaying = false;

    GameObject objectToMoveTo;

    Color32 unitVisuals;

    PlayerType playerType;
    EnemyType enemyType;

    private void Update()
    {
        if (startMoving)
        {

            transform.position = Vector2.MoveTowards(transform.position, objectToMoveTo.transform.position, speed * Time.deltaTime);

            if(transform.position == objectToMoveTo.transform.position)
            {


                if (isPlayerMoving)
                {

                    objectToMoveTo.GetComponent<GridPiece>().playerPieceHere = true;
                    objectToMoveTo.GetComponent<GridPiece>().currentPlayerType = playerType;
                    objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals = gameObject;

                    objectToMoveTo.GetComponent<GridPiece>().movedOnce = true;

                }
                else
                {

                    objectToMoveTo.GetComponent<GridPiece>().enemyPieceHere = true;
                    objectToMoveTo.GetComponent<GridPiece>().currentEnemyType = enemyType;
                    objectToMoveTo.GetComponent<GridPiece>().currentPieceVisuals = gameObject;

                }
             
                startMoving = false;
                animationIsPlaying = false;

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

                }

            break;

        }

    }

    #endregion

    #region Movment

    public void MovePiece(GameObject objectToGoTo, PlayerType typePlayer, bool playerMoving, EnemyType typeEnemy)
    {

        objectToMoveTo = objectToGoTo;

        playerType = typePlayer;
        enemyType = typeEnemy;

        startMoving = true;
        animationIsPlaying = true;
        isPlayerMoving = playerMoving; 

    }

    #endregion
}
