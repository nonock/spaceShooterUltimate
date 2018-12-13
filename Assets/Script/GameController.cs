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
	public string level2load;

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
	bool tr = false;


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
        spawnWait = 0.5f;

        m_Scene = SceneManager.GetActiveScene();

        if(m_Scene.name == "niv5")
        {
            SpawnBoss();
        }
        else
        {
            StartCoroutine(SpawnWaves());
            //SpawnSpecialWave1();
            //SpawnSpecialWave2();
            //SpawnSpecialWave3();
        }
    }

    void Update()
    {
        if(tr){
			alpha+= 0.07F;
		}
		else {
			if(alpha > 0)
				alpha-= 0.07F;
		}

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    void SpawnBoss()
    {
        GameObject Boss = hazards[0];

        Vector3 spawnPosition = new Vector3(0, spawnValues.y, 12);

        Vector3 rotationVector = new Vector3(90, 180, 0);

        Quaternion spawnRotation = Quaternion.Euler(rotationVector);

        Instantiate(Boss, spawnPosition, spawnRotation);
    }

    void SpawnSpecialWave1()
    {
        GameObject hazard = hazards[0];
        for(float i = -spawnValues.x; i < spawnValues.x + 1; i++)
        {
            Vector3 spawnPosition = new Vector3(i, spawnValues.y, spawnValues.z);

            Vector3 rotationVector = new Vector3(90, 0, 0);

            Quaternion spawnRotation = Quaternion.Euler(rotationVector);

            Instantiate(hazard, spawnPosition, spawnRotation);
        }
    }

    void SpawnSpecialWave2()
    {
        GameObject hazard = hazards[1];
        
        Vector3 rotationVector = new Vector3(90, 0, 0);

        Quaternion spawnRotation = Quaternion.Euler(rotationVector);

        for (float a = 0; a < spawnValues.x + 1; a++)
        {
            Vector3 spawnPosition2 = new Vector3(0 + a, spawnValues.y, spawnValues.z + a);
            Vector3 spawnPosition3 = new Vector3(0 - a, spawnValues.y, spawnValues.z + a);
           
            Instantiate(hazard, spawnPosition2, spawnRotation);
            Instantiate(hazard, spawnPosition3, spawnRotation);
        }
    }

    void SpawnSpecialWave3()
    {
        GameObject hazard = hazards[1];

        Vector3 rotationVector = new Vector3(90, 0, 0);

        Quaternion spawnRotation = Quaternion.Euler(rotationVector);
        
        for (float a = 0; a < (spawnValues.x/2) + 1; a++)
        {
            Vector3 spawnPosition2 = new Vector3(0 + a, spawnValues.y, spawnValues.z + a);
            Vector3 spawnPosition3 = new Vector3(0 - a, spawnValues.y, spawnValues.z + a);

            Instantiate(hazard, spawnPosition2, spawnRotation);
            Instantiate(hazard, spawnPosition3, spawnRotation);
        }
        for (float a = (spawnValues.x/2) + 1; a > 0; a--)
        {
            Vector3 spawnPosition2 = new Vector3(a, spawnValues.y, spawnValues.z - a);
            Vector3 spawnPosition3 = new Vector3(-a, spawnValues.y, spawnValues.z - a);

            Instantiate(hazard, spawnPosition2, spawnRotation);
            Instantiate(hazard, spawnPosition3, spawnRotation);
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
            if (waves > waveUpdate)
            {
                waves = 0;
                hazardCount = hazardCount + 3;
            }
            UpdateWave();
        }
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
        StartCoroutine(transition());

    }

	IEnumerator transition()
    {
		yield return new WaitForSeconds(2); 
		tr = true;
		StartCoroutine(loadLevel());

	 }

	IEnumerator loadLevel()
    {
		yield return new WaitForSeconds(1); 
		SceneManager.LoadScene(level2load);

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
        GUI.color = new Color(1, 1, 1, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }
}