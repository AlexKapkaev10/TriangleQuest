using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Char charScript;

    public SpawnBonuses spawnBonus;

    public int levelCount;

    public float curLevel = 1;

    public GameObject currentLevel;

    public GameObject[] easyLevels;
    public GameObject[] mediumLevels;
    private Vector3 _pointLevels = Vector3.zero;

    public GameObject[] lines;
    private int lineCount;

    public Image[] lives;
    public int liveCount;

    public Text levelTxt;
    public Text speedTxt;

    public GameObject GameOver;

    private Vector2 _bonusPos = Vector2.zero;

    public bool isPosibleSpawnBonuse = true;

    [SerializeField] private float _minSpawnBonusX;
    [SerializeField] private float _maxSpawnBonusX;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        LevelGeneric();
        UpdateResourse();
        LinesActiv();
    }

    private void Start()
    {

    }

    public void UpdateResourse()
    {
        levelTxt.text = "Level: " + curLevel.ToString();
        speedTxt.text = charScript.speed.ToString();
    }

    public void LevelGeneric()
    {
        levelCount = Random.Range(1, GameManager.instance.easyLevels.Length);
        if (curLevel < 5)
        {
            currentLevel = Instantiate(easyLevels[levelCount], _pointLevels, Quaternion.identity);
        }
        else
        {
            currentLevel = Instantiate(mediumLevels[levelCount], _pointLevels, Quaternion.identity);
        }

        _bonusPos = new Vector2(Random.Range(_minSpawnBonusX, _maxSpawnBonusX), 0);

        spawnBonus.SpawnBonus(_bonusPos, isPosibleSpawnBonuse);
    }

    public void LinesActiv()
    {
        lines[lineCount].gameObject.SetActive(false);
        lineCount = Random.Range(0, lines.Length);
        lines[lineCount].gameObject.SetActive(true);
    }

    public void LevelUp()
    {
        StartCoroutine(DelayLevelUp());
    }

    public void DestroyLevel()
    {
        Destroy(currentLevel);
    } 

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ActivGameOver()
    {
        GameOver.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator DelayLevelUp()
    {
        yield return new WaitForSeconds(0.5f);
        curLevel++;
        LevelGeneric();
        UpdateResourse();
        LinesActiv();
    }

}
