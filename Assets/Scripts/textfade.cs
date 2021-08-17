using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textfade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI error;
    // Start is called before the first frame update
    void Start()
    {
        error = GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        error.CrossFadeAlpha(0.2f, 2f, false);
        gameObject.SetActive(false);
    }
}
