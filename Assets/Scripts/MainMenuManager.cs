using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    public static MainMenuManager Instance;
    [SerializeField] private TextMeshProUGUI errorText;
    [SerializeField] private TextMeshProUGUI nameTooLong;
    public string playerName { get; private set; }
    private InfoCarrier infoCarrier;
    private void Start()
    {
        if (Instance != null)       //Ensures only one version of menu manager loaded
        {
            Destroy(gameObject);
        }
        Instance = this;
        infoCarrier = InfoCarrier.Instance;
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
        infoCarrier = InfoCarrier.Instance;
        playerName = inputField.text;
        infoCarrier.playerName = playerName;
    }

    public void DeleteHighscores()
    {
        string path = Application.persistentDataPath + "/save.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}