using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SpawnBonuses spawnBonus;

    public int levelCount;

    public float curLevel = 1;

    public Char charScript;

    public GameObject currentLevel;

    public GameObject[] levels;
    Vector3 pointLevels;

    public GameObject[] lines;
    private int lineCount;

    public Image[] lives;
    public int liveCount;

    public Text levelTxt;

    public GameObject GameOver;

    private Vector2 _bonusPos = Vector2.zero;

    [SerializeField] private float _minSpawnBonusX;
    [SerializeField] private float _maxSpawnBonusX;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        LevelGeneric();
        UpdateResourse();
        LinesActiv();
    }

    public void UpdateResourse()
    {
        levelTxt.text = "Level: " + curLevel.ToString();
    }

    public void LevelGeneric()
    {
        levelCount = Random.Range(1, GameManager.instance.levels.Length);
        currentLevel = Instantiate(levels[levelCount], pointLevels, Quaternion.identity);
        _bonusPos = new Vector2(Random.Range(_minSpawnBonusX, _maxSpawnBonusX), 0);
        spawnBonus.SpawnBonus(_bonusPos);
    }

    public void LinesActiv()
    {
        lines[lineCount].gameObject.SetActive(false);
        lineCount = Random.Range(0, lines.Length);
        lines[lineCount].gameObject.SetActive(true);
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

}
