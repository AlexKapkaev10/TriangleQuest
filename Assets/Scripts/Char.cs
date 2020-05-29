using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Char : MonoBehaviour
{
    private Vector2 _direction = Vector2.zero;
    Vector3 startPosition = Vector3.zero;

    [SerializeField] private float _dirLeft;
    [SerializeField] private float _dirRight;
    [SerializeField] private float _stepSpeed;
    public int HP = 2;


    private bool _checkPoint;
    private bool _finish;
    private bool _isShield;

    private bool _isCrashed = false;

    public float speed;

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
        if (!_isCrashed)
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
        if (!_isCrashed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_finish )
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
        if (!_checkPoint)
        {
            transform.Translate(_direction * Time.deltaTime);
        }

        if (_checkPoint)
        {
            if (Input.GetKey(KeyCode.A))
            {
                _direction.x = -2;
                if (!_finish)
                    transform.Translate(_direction * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                _direction.x = 2;
                if (!_finish)
                    transform.Translate(_direction * Time.deltaTime);
            }
        }

#endif
#if UNITY_IOS && UNITY_ANDROID

        
#endif

    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<wall>() != null)
        {
            if (collision.gameObject.GetComponent<wall>().myType == wall.WallType.CheckPoint ||
                collision.gameObject.GetComponent<BonusSystem>())
                _checkPoint = true;
            else
                _checkPoint = false;

            if (collision.gameObject.GetComponent<wall>().myType == wall.WallType.MoveLeft)
                _direction.x = _dirLeft;

            if (collision.gameObject.GetComponent<wall>().myType == wall.WallType.MoveRight)
            {
                _direction.x = _dirRight;
            }

            if (collision.gameObject.GetComponent<wall>().myType == wall.WallType.Finish)
            {
                _finish = true;
                _direction.x = 0;
                transform.DOMove(new Vector3(0, 4.7f, 0), 0.3f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    GameManager.instance.DestroyLevel();
                    StartCoroutine(NextLevel());
                });

            }
        }

        else if (collision.gameObject.GetComponent<BonusSystem>() != null)
        {
            
            var bonus = collision.gameObject.GetComponent<BonusSystem>();
            
            if(bonus.MyType == BonusSystem.Type.live && HP != 3)
            {
                GameManager.instance.lives[HP].enabled = true;
                HP++;
                Debug.Log(HP);
                Destroy(collision.gameObject);
            }
            else if (bonus.MyType == BonusSystem.Type.immunity)
            {
                _isShield = true;
                colChar.isTrigger = true;
                Destroy(collision.gameObject);
            }
            GameManager.instance.isPosibleSpawnBonuse = false;
            Destroy(collision.gameObject);
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<wall>().myType == wall.WallType.Boarder && !_isShield)
        {
            _direction.x = 0;
            _isCrashed = true;
            
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
        _isCrashed = false;
        _finish = false;
        colChar.enabled = false;
            
        if(_isShield == true)
        {
            colChar.isTrigger = false;
            _isShield = false;
        }
        if(speed < 5)
        {
            _dirLeft -= _stepSpeed;
            _dirRight += _stepSpeed;
            speed = _dirRight;
        }

        transform.DOMove(startPosition, 0.5f).SetEase(Ease.Linear).OnComplete(()=> colChar.enabled = true);
        GameManager.instance.LevelUp();
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        _isCrashed = false;
        _finish = false;
        colChar.enabled = false;
        transform.DOMove(startPosition, 0.3f).SetEase(Ease.Linear).OnComplete(() => colChar.enabled = true);
    }

}
