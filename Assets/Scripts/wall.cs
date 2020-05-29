using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public enum WallType
    {
        MoveLeft,
        MoveRight,
        Boarder,
        Finish,
        CheckPoint
    }

    public WallType myType;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (myType != WallType.Boarder)
            return;

        if (collision.gameObject.GetComponent<Char>())
        {
            Destroy(collision.gameObject);
        }
    }
}
