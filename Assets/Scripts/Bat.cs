using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    protected override void Start()
    {
        base.Start();
        moveSpeed += Random.Range(-0.25f, 0.25f);   //Bat slightly randomises movement speed
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            MoveLeft();
            //Despawn at left of screen and lose lives
            if (transform.position.x < -16)
            {
                isDead = true;
                gameController.ChangeLives(2);
                Deactivate();
            }
            //Despawn if health hits 0
            if (health <= 0)
            {
                gameController.ChangeScore(1);
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

    //Manages getting hit by the player attack
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            --health;
            animator.SetTrigger("hit");
        }
        else if (collision.gameObject.CompareTag("GameOver"))
        {
            health = 0;
        }
    }
}
