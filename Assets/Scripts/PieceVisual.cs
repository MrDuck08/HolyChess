using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceVisual : MonoBehaviour
{

    PlayerType currenType;

     Color32 unitVisuals;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WhoAmI(PlayerType whatType, Transform pos, bool playerPiece)
    {

        currenType = whatType;

        transform.position = pos.position;

        Transform VisualsCollection = transform.Find("VisuallsCollection");

        switch (currenType)
        {

            case PlayerType.Pawn:

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
                        child.GetComponent<SpriteRenderer>().color = unitVisuals;

                    }

                }

                break;

            case PlayerType.Tower:

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
                        child.GetComponent<SpriteRenderer>().color = unitVisuals;

                    }

                }

                break;

        }

    }
}
