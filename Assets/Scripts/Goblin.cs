using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Enemy
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 3f;     //Goblin unit starts faster than other enemies
    }

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = Mathf.FloorToInt(score / 10) / 2 + 3;   //Speed increases with score
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
