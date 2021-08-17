using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public static MainMenuManager Instance;
    public TextMeshProUGUI errorText;
    public TextMeshProUGUI nameTooLong;
    private string playerName;
    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(inputField.text) || string.IsNullOrWhiteSpace(inputField.text))
        {
            errorText.gameObject.SetActive(true);
            nameTooLong.gameObject.SetActive(false);
        }
        else if (inputField.text.Length > 12)
        {
            nameTooLong.gameObject.SetActive(true);
            errorText.gameObject.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void UpdateName()
    {
        playerName = inputField.text;
    }
}
