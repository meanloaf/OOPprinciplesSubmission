using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    public int playerLives;

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
            animator.SetTrigger("hit");
        }
    }
}
