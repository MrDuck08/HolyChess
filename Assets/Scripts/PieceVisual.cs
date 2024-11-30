using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceVisual : MonoBehaviour
{

    int currenType;

     Color32 unitVisuals;

    float speed = 2;

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

    public void MovePiece(Vector2 posToMoveTo)
    {



    }

    #endregion
}
