using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class GameController : MonoBehaviour
{
    public int score;
    public int playerLives;
    public string playerName;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public bool gameOver;
    public GameObject[] enemies = new GameObject[3];
    public float GobProb = 0.3f;        //Controls goblin spawn probability
    public float MushProb = 0.1f;       //Controls mushroom spawn probability
    public GameObject gameOverContainer;
    private List<string> names;
    private List<int> highscores;
    private SaveData prevLeader;
    private TextMeshProUGUI leaderboardText;
    public InfoCarrier carriedInfo;
    public static GameController Instance;  //Utilised to ensure only one version of game manager active
    private bool writtenScore;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        
        carriedInfo = InfoCarrier.Instance;
        playerName = carriedInfo.playerName;
        score = 0;
        playerLives = 10;
        gameOver = false;
        writtenScore = false;
        scoreText.text = $"Score: {score}";
        livesText.text = $"Lives: {playerLives}";
        InvokeRepeating("SpawnEnemies", 3f, 2f);
        //Loads high score data into lists
        prevLeader = ReadLeaderboard();
        if (prevLeader != null)
        {
            highscores = new List<int>();
                highscores.Add(prevLeader.score1);
                highscores.Add(prevLeader.score2);
                highscores.Add(prevLeader.score3);
            names = new List<string>();
                names.Add(prevLeader.name1);
                names.Add(prevLeader.name2);
                names.Add(prevLeader.name3);
        }
        else
        {
            //Creates a blank leaderboard if one doesn't exist
            highscores = new List<int>();
                highscores.Add(0);
                highscores.Add(0);
                highscores.Add(0);
            names = new List<string>();
                names.Add("---");
                names.Add("---");
                names.Add("---");
        }
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
        if (playerLives < 0)
        {
            playerLives = 0;
        }
        if (playerLives == 0)
        {
            gameOver = true;
            SetGameOver();
        }
    }

    //Handles all enemy spawning. Spawn position and enemy type
    void SpawnEnemies()
    {
        int pool = Mathf.FloorToInt(score/5);
        if (pool > 2)
        {
            pool = 2;
        }
        int toSpawn = Mathf.FloorToInt(score/30) + 1;
        if (toSpawn > 3)
        {
            toSpawn = 3;
        }
        for (int i = 0; i < toSpawn; i++)
        {
            float ypos = Random.Range(-5f, 6.25f);
            Vector3 spawnPos = new Vector3(14f, ypos, 0f);
            float index = Random.Range(0, 1f);
            int spawnIndex = 0;
            if (pool >= 2 && index < MushProb)
            {
                spawnIndex = 2;
            }
            else if (pool >= 1 && index < GobProb)
            {
                spawnIndex = 1;
            }
            Instantiate(enemies[spawnIndex], spawnPos, enemies[spawnIndex].transform.rotation);
        }
    }

    void SetGameOver()
    {
        carriedInfo = InfoCarrier.Instance;     //I need this for some reason or it loses reference when going back to menu
        CancelInvoke();                         //Stops enemy spawning
        gameOverContainer.SetActive(true);      //Enables game over UI
        CheckLeaderboard();                     //Checks if score is worthy of leaderboard position
        SaveLeaderboard();                      //Saves leaderboard to file
        leaderboardText = gameOverContainer.transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
        leaderboardText.text = $"{names[0]}: {highscores[0]} \r\n {names[1]}: {highscores[1]} \r\n {names[2]}: {highscores[2]}";
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    [System.Serializable]
    public class SaveData
    {
        public string name1;
        public string name2;
        public string name3;
        public int score1;
        public int score2;
        public int score3;
    }

    private void CheckLeaderboard()
    {
        if (score > highscores[0] && !writtenScore)
        {
            highscores.Insert(0, score);
            names.Insert(0, playerName);
            writtenScore = true;
            return;
        }
        else if (score > highscores[1] && !writtenScore)
        {
            highscores.Insert(1, score);
            names.Insert(1, playerName);
            writtenScore = true;
            return;
        }
        else if (score > highscores[2] && !writtenScore)
        {
            highscores.Insert(2, score);
            names.Insert(2, playerName);
            writtenScore = true;
            return;
        }
    }

    private SaveData ReadLeaderboard()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData prevLeader = JsonUtility.FromJson<SaveData>(json);
            return prevLeader;
        }
        else
        {
            return null;
        }
    }

    private void SaveLeaderboard()
    {
        string path = Application.persistentDataPath + "/save.json";
        SaveData newLeader = new SaveData();
        newLeader.name1 = names[0];
        newLeader.name2 = names[1];
        newLeader.name3 = names[2];
        newLeader.score1 = highscores[0];
        newLeader.score2 = highscores[1];
        newLeader.score3 = highscores[2];
        string json = JsonUtility.ToJson(newLeader);
        File.WriteAllText(path, json);
    }
}
