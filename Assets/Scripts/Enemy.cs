using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //Enemy base class carries variables for health, movespeed, isDead and an attached animator
    //Carries basic movement and deactivate methods that all enemies will use
    [SerializeField]
    public float moveSpeed = 2f;   //base movement speed of enemies
    public bool isDead = false;     //if enemy is dead or not. Used as a trigger for death animation
    public int health;              //how many hits required to kill enemy
    public Animator animator;       //attached animator component. communicates
    public int score;
    public SpriteRenderer sprite;
    protected GameController gameController;
    protected AudioSource hitSound;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        hitSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        gameController = GameController.Instance;
        score = gameController.score;
        health = Mathf.FloorToInt(score / 20) + 1;
    }

    protected virtual void Awake()
    {
        isDead = false;
        //health = Mathf.FloorToInt(score / 10) + 1;
    }

    // Moves enemy left proportional to movespeed variable
    public void MoveLeft()
    {
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }

    //Deactivates the object
    public void Deactivate()
    {
        animator.SetBool("Die", false);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        Color tmp = sprite.color;
        while(sprite.color.a > .4)
        {
            tmp.a -= .2f;
            sprite.color = tmp;
            yield return new WaitForSeconds(.1f);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        if (gameController.gameOver)
        {
            isDead = true;
        }
    }
}
