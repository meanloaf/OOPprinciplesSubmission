using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public int playerLives;
    public int score = 0;
    private Animator animator;
    
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Awake()
    {
        isDead = false;
        health = Mathf.FloorToInt(score/10) + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            MoveLeft();
            if (transform.position.x < -16)
            {
                --playerLives;
                Deactivate();
            }
            if (health <= 0)
            {
                isDead = true;
                animator.SetBool("Die", true);
            }
        }
        
    }

    //Manages getting hit by the player attack
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            --health;
        }
    }

    public void Deactivate()
    {
        animator.SetBool("Die", false);
        gameObject.SetActive(false);
    }
}
