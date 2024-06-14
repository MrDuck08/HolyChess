using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridController : MonoBehaviour
{
    GridPiece[] gridPiece;

    public void MovePawn(int currentY, int currentX)
    {
        foreach (var piece in gridPiece)
        {
            //https://discussions.unity.com/t/how-to-find-a-certain-gameobject-using-a-variable-in-a-common-script-from-an-array-of-gameobjects/132458/2
        }
    }
}
