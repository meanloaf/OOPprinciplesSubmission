using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 1.5f;                           //Mushroom unit is slower to balance health increase
        health = Mathf.FloorToInt(score / 10) + 2;
    }

    protected override void Awake()
    {
        isDead = false;
        //health = Mathf.FloorToInt(score / 5) + 2;   //Mushroom health is higher than other enemies and scales faster
    }

    //Looking at them, update method for all enemies could be mostly moved to base class. Keeping it here
    //allows for different score and lives values to be assigned easily. This is my excuse cause I am too lazy to move it all.
    private void Update()
    {
        if (!isDead)
        {
            MoveLeft();
            if (transform.position.x < -16)
            {
                isDead = true;
                gameController.ChangeLives(3);
                Deactivate();
            }
            if (health <= 0)
            {
                gameController.ChangeScore(3);
                isDead = true;
                animator.SetBool("Die", true);
            }
        }
        if (gameController.gameOver)
        {
            isDead = true;
            animator.SetBool("Die", true);
            animator.SetTrigger("hit");
        }
    }

    //Handles collision with player attack
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            --health;
            animator.SetTrigger("hit");
            hitSound.Play();
        }
        else if (collision.gameObject.CompareTag("GameOver"))
        {
            health = 0;
        }
    }
}