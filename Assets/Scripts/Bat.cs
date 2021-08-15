using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            MoveLeft();
            if (transform.position.x < -16)
            {
                isDead = true;
                gameController.ChangeLives(2);
                Deactivate();
            }
            if (health <= 0)
            {
                gameController.ChangeScore(1);
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
            animator.SetTrigger("hit");
        }
    }
}
