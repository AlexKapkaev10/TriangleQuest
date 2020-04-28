using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        LevelGeneric();
        UpdateResourse();
        LinesActiv();
    }

    void Update()
    {

    }

    public void UpdateResourse()
    {
        levelTxt.text = "Level: " + curLevel.ToString();
    }

    public void LevelGeneric()
    {
        levelCount = Random.Range(1, GameManager.instance.levels.Length);
        currentLevel = Instantiate(levels[levelCount], pointLevels, Quaternion.identity);

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
