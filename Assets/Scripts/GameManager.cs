﻿using UnityEngine;
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

    public Text levelTxt;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
        LevelGeneric();
        UpdateResourse();
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
        currentLevel = Instantiate(levels[levelCount], pointLevels, Quaternion.identity);
    }

    public void DestroyLevel()
    {
        Destroy(currentLevel);
    } 

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}