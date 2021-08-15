using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int score;
    public int playerLives;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        playerLives = 10;
    }

    public void ChangeScore(int value)
    {
        score += value;
    }

    public void ChangeLives(int value)
    {
        playerLives -= value;
    }

    void SpawnEnemies()
    {

    }
}
