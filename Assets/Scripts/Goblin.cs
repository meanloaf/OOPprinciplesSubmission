using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 3f;     //Goblin unit starts faster than other enemies
        if (health > 2)
        {
            health = 2;     //Goblin health capped at 2
        }
        moveSpeed = Mathf.FloorToInt(score / 10) / 2 + 3;   //Speed increases with score
    }

    protected override void Awake()
    {
        base.Awake();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            MoveLeft();
            if (transform.position.x < -16)
            {
                isDead = true;
                gameController.ChangeLives(1);
                Deactivate();
            }
            if (health <= 0)
            {
                gameController.ChangeScore(2);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            --health;
            animator.SetTrigger("hit");
            hitSound.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver"))
        {
            health = 0;
        }
    }
}
