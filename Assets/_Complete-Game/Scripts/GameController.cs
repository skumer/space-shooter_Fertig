using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    private GameObject spawnAsteroid;
    public GameObject enemy;
    public Vector3 spawnValues;
    public int hazardCount;
    private int waveCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        waveCount = 1;
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
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (Random.Range(1, 4) == 1)
                {
                    Instantiate(enemy, spawnPosition, spawnRotation);
                }
                else
                {
                    int j = Random.Range(1, 3);
                    if (j == 1)
                    {
                        spawnAsteroid = asteroid1;
                    }
                    else if (j == 2)
                    {
                        spawnAsteroid = asteroid2;
                    }
                    else
                    {
                        spawnAsteroid = asteroid3;
                    }
                    Instantiate(spawnAsteroid, spawnPosition, spawnRotation);
                }
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait / 2);
            if (!gameOver)
            {
                gameOverText.text = "Wave " + waveCount + " done";
                hazardCount++;
                waveCount++;
                yield return new WaitForSeconds(waveWait / 2);
                gameOverText.text = "";
            }
            else
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
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
}