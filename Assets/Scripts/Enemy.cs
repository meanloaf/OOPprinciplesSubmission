using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;
    public bool isDead = false;
    public int health;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    public void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }
}
