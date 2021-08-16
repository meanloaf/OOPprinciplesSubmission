using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int score;
    public int playerLives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public bool gameOver;
    public GameObject[] enemies = new GameObject[3];
    public GameObject gameOverContainer;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        playerLives = 10;
        gameOver = false;
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {playerLives}";
        InvokeRepeating("SpawnEnemies", 5, 2.5f);
    }

    public void ChangeScore(int value)
    {
        score += value;
        scoreText.text = $"Score: {score}";
    }

    public void ChangeLives(int value)
    {
        playerLives -= value;
        livesText.text = $"Lives: {playerLives}";
    }

    public void Update()
    {
        if (playerLives <= 0)
        {
            gameOver = true;
            SetGameOver();
        }
    }

    void SpawnEnemies()
    {
        int pool = Mathf.FloorToInt(score/5);
        if (pool > 2)
        {
            pool = 2;
        }
        int toSpawn = Mathf.FloorToInt(score/20) + 1;
        if (toSpawn > 5)
        {
            toSpawn = 5;
        }
        for (int i = 0; i < toSpawn; i++)
        {
            float ypos = Random.Range(-5f, 6.25f);
            Vector3 spawnPos = new Vector3(14f, ypos, 0f);
            int spawnIndex = Random.Range(0, pool+1);
            Instantiate(enemies[spawnIndex], spawnPos, enemies[spawnIndex].transform.rotation);
        }
    }

    void SetGameOver()
    {
        CancelInvoke();
        gameOverContainer.SetActive(true);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
