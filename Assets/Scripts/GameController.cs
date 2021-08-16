using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int score;
    public int playerLives;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        playerLives = 10;
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

    public void ResetText()
    {
        score = 0;
        playerLives = 10;
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {playerLives}";
    }

    void SpawnEnemies()
    {

    }
}
