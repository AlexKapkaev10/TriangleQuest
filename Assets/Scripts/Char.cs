using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Char : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] private int dirLeft;
    [SerializeField] private int dirRight;
    private bool checkPoint, finish;

    Vector3 startPosition;
    bool isCrashed = false;
    //public Rigidbody2D rg;
    //public int speed;

    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    private void Update()
    {
        MoveChar();
        if (!isCrashed)
        {
            MoveCharCheckPoint();
        }
    }

    void MoveChar()
    {
        if (!isCrashed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!finish)
                    transform.position += new Vector3(0, 1, 0 * Time.deltaTime);
            }
        }
    }

    void MoveCharCheckPoint()
    {
#if UNITY_EDITOR

#endif
#if UNITY_IOS && UNITY_ANDROID
        
#endif
        if (!checkPoint)
        {
            transform.Translate(direction * Time.deltaTime);
        }

        if (checkPoint)
        {
            if (Input.GetKey(KeyCode.A))
            {
                direction.x = -2;
                if (!finish)
                    transform.Translate(direction * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction.x = 2;
                if (!finish)
                    transform.Translate(direction * Time.deltaTime);
            }
        }
    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "checkPoint")
        {
            checkPoint = true;
        }
        else
        {
            checkPoint = false;
        }

        if(collision.gameObject.tag == "left")
        {
            direction.x = dirLeft;
            Debug.Log("yes");
            
        }
        if (collision.gameObject.tag == "right")
        {
            direction.x = dirRight;
        }

        if (collision.gameObject.tag == "finish")
        {
            finish = true;
            direction.x = 0;
            transform.position = new Vector3(0, 4.7f, 0 * Time.deltaTime);
            GameManager.instance.DestroyLevel();
            StartCoroutine(NextLevel());
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            direction.x = 0;
            isCrashed = true;
            StartCoroutine(Restart());

        }
    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2);
        isCrashed = false;
        finish = false;
        transform.position = startPosition;
        GameManager.instance.levelCount++;
        GameManager.instance.curLevel++;
        GameManager.instance.LevelGeneric();
        GameManager.instance.UpdateResourse();
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(2);
        isCrashed = false;
        finish = false;
        transform.position = startPosition;
    }

}
