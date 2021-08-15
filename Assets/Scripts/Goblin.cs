using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    private int playerLives;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 3f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            --health;
            animator.SetTrigger("hit");
        }
    }
}
