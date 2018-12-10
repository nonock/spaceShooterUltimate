﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    private int hazardCount;
    private float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text victoryText;

    private bool gameOver;
    private bool restart;
    private int score;
    private bool victory;

    private int waves;
    private int maxScore;
    private int waveUpdate;

    void Start()
    {
        gameOver = false;
        restart = false;
        victory = false;
        restartText.text = "";
        gameOverText.text = "";
        victoryText.text = "";
        score = 0;
        waves = 0;
        waveUpdate = 0;
        maxScore = 1000;
        hazardCount = 10;
        spawnWait = 1.0f;
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }

            if (victory)
            {
                break;
            }
            waves = waves + 1;
            if(waves > waveUpdate)
            {
                waves = 0;
                hazardCount = hazardCount + 3;
                spawnWait = spawnWait - 0.2f;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        if(score > maxScore)
        {
            Victory();
        }
        else
        {
            UpdateScore();
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    public void Victory()
    {
        victoryText.text = "Victory !!!";
        victory = true;
    }
}