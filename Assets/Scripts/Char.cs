using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Char : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    Vector3 startPosition = Vector3.zero;

    [SerializeField] private float dirLeft;
    [SerializeField] private float dirRight;
    public int HP = 2;


    private bool checkPoint, finish;
    private bool isCrashed = false;

    [SerializeField] private Transform finishPos;
    [SerializeField] private float speed = 1;

    private delegate void OnMove();
    private event OnMove OnMoved;

    private CircleCollider2D colChar;

    public GameObject effect;

    public Transform effectPoint = null;

    void Start()
    {
        colChar = GetComponent<CircleCollider2D>();
        startPosition = gameObject.transform.position;
        OnMoved += MoveUp;
    }

    private void Update()
    {
        MoveChar();
        if (!isCrashed)
        {
            MoveCharCheckPoint();
        }
    }

    void MoveUp()
    {
        transform.DOMoveY(1, 0.1f).SetRelative(true).SetEase(Ease.Linear);
        GameObject thisEffect = Instantiate(effect, effectPoint);
        Destroy(thisEffect, 1);
    }

    void MoveChar()
    {
#if UNITY_EDITOR
        if (!isCrashed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!finish )
                    OnMoved();
            }
        }

#endif

#if UNITY_IOS && UNITY_ANDROID
        if (!isCrashed)
        {
            if (Input.touchCount > 0)
            {
                if (!finish)
                    transform.Translate(Vector2.up);
            }
        }
        
#endif
    }

    void MoveCharCheckPoint()
    {
#if UNITY_EDITOR
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

#endif
#if UNITY_IOS && UNITY_ANDROID

        
#endif

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
            transform.DOMove(new Vector3(0, 4.7f, 0), 0.3f).SetEase(Ease.Linear);
            transform.position = new Vector3(0, 4.7f, 0 * Time.deltaTime);
            GameManager.instance.DestroyLevel();
            StartCoroutine(NextLevel());
        }
        if (collision.gameObject.GetComponent<BonusSystem>() != null)
        {
            Destroy(collision.gameObject);
            if(HP != 3)
            {
                GameManager.instance.lives[HP].enabled = true;
                HP++;
                Debug.Log(HP);
                
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "death")
        {
            direction.x = 0;
            isCrashed = true;
            
            HP--;
            if (HP != 0)
            {
                GameManager.instance.lives[HP].enabled = false;
            }
            else
                GameManager.instance.ActivGameOver();

            StartCoroutine(Restart());

        }
    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);
        isCrashed = false;
        finish = false;
        colChar.enabled = false;
        dirLeft -= 0.3f;
        dirRight += 0.3f;
        transform.DOMove(startPosition, 0.1f).SetEase(Ease.Linear).OnComplete(()=> colChar.enabled = true);
        GameManager.instance.curLevel++;
        GameManager.instance.LevelGeneric();
        GameManager.instance.UpdateResourse();
        GameManager.instance.LinesActiv();
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        isCrashed = false;
        finish = false;
        colChar.enabled = false;
        transform.DOMove(startPosition, 0.3f).SetEase(Ease.Linear).OnComplete(() => colChar.enabled = true);
    }

}
