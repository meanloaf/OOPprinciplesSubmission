using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = gameObject.transform.Find("ScoreText").gameObject.GetComponent<TextMeshProUGUI>();
        livesText = gameObject.transform.Find("PlayerLivesText").gameObject.GetComponent<TextMeshProUGUI>();
    }
}
