using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
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
    private int wave;
    private bool victory;

    private int waves;
    private int maxScore;
    private int waveUpdate;
    Scene m_Scene;

	public Texture2D fadeTexture;
    private float alpha = 1;


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
        wave = 0;
        waveUpdate = 0;
        maxScore = 1000;
        hazardCount = 10;
        spawnWait = 1.0f;
        UpdateScore();

        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if(alpha>0)
            alpha-=0.07f;

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
            for(int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                
                Vector3 rotationVector = new Vector3(90, 0, 0);

                Quaternion spawnRotation = Quaternion.Euler(rotationVector);
                
                Instantiate(hazard, spawnPosition, spawnRotation);

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

            m_Scene = SceneManager.GetActiveScene();

            waves = waves + 1;
            if(waves > waveUpdate)
            {
                waves = 0;
                hazardCount = hazardCount + 3;
                spawnWait = spawnWait - 0.2f;
            }
            if(m_Scene.name == "niv1")
            {
                UpdateWave();
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

    public void UpdateWave()
    {
        wave = wave + 1;

        UpdateScoreWave();

        if (wave == 5)
        {
            Victory();
        }
    }

    void UpdateScoreWave()
    {
        scoreText.text = "Wave: " + wave;
    }

    void OnGUI()
	{
	GUI.color = new Color(1,1,1, alpha);
	GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), fadeTexture);
	}
}