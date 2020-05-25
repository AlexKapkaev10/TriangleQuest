using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour
{
    public Animator anim;

    public AnimationClip slideClip;

    public Animation animationComponent;

    public enum WallType
    {
        border,
        obstacle,
        slide
    }

    public WallType myType;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "char" )
        {
            Destroy(collision.gameObject);
        }
    }
}
