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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(myType == WallType.slide)
        {
            animationComponent.clip = slideClip;
            animationComponent.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "char" )
        {
            Destroy(collision.gameObject);
        }
    }
}
