using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCarrier : MonoBehaviour
{
    public static InfoCarrier Instance;
    public string playerName;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
