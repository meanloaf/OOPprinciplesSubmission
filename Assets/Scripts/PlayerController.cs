using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float xaxis;
    [SerializeField]
    private float yaxis;
    [SerializeField]
    public float moveSpeed = 30f;
    private Vector3 startPosition = new Vector3(-11f, 0f, 0f);
    private Rigidbody2D rb;
    private Animator animationControl;
    private SpriteRenderer characterSprite;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animationControl = gameObject.GetComponent<Animator>();
        characterSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animationControl.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Movement();
            if (Input.GetKeyDown("space"))
            {
                Attack();
            }
        }
    }

    void Movement()
    {
        xaxis = Input.GetAxisRaw("Horizontal"); //Aquires unsmoothed horizontal input
        yaxis = Input.GetAxisRaw("Vertical");   //Aquires unsmoothed vertical input
        Vector3 moveDirection = new Vector3(xaxis, yaxis, 0);
        moveDirection.Normalize();              //Creates a overall movement direction with magnitude 1 for constant speed at diagonals
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        if (moveDirection.magnitude != 0)       //Controls animation for movement
        {
            animationControl.SetBool("Moving", true);
        }
        else
        {
            animationControl.SetBool("Moving", false);
        }
        if(xaxis < 0)                           //Flips sprite to face moving direction
        {
            characterSprite.flipX = true;
        }
        else if (xaxis > 0)
        {
            characterSprite.flipX = false;
        }
    }

    void Attack()
    {
        animationControl.SetTrigger("Attack");  //Triggers attack animation.
    }
}
