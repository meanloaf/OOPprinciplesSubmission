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
    public float moveSpeed { get; private set; }
    private Vector3 startPosition = new Vector3(-11f, 0f, 0f);
    private Rigidbody2D rb;
    private Animator animationControl;
    private SpriteRenderer characterSprite;
    private AudioSource attackSound;
    public int faceDirection { get; private set; }
    public GameController gameController;

    private GameObject hitbox;

    // Start is called before the first frame update
    void Start()
    {
        attackSound = GetComponent<AudioSource>();
        transform.position = startPosition;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animationControl = gameObject.GetComponent<Animator>();
        characterSprite = GetComponent<SpriteRenderer>();
        hitbox = transform.GetChild(0).gameObject;
        faceDirection = 1;
        moveSpeed = 20f;
        gameController = GameController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //Doesn't allow movement or interrupting attacks while attack animation is playing
        if (!animationControl.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !gameController.gameOver)
        {
            Movement();
            if (Input.GetKeyDown("space"))      //Starts attack method when space pressed
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
            faceDirection = -1;
            hitbox.transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        else if (xaxis > 0)
        {
            characterSprite.flipX = false;
            faceDirection = 1;
            hitbox.transform.localScale = new Vector3(faceDirection, 1, 1);
        }
    }

    void Attack()
    {
        attackSound.Play();
        animationControl.SetTrigger("Attack");  //Triggers attack animation.
    }
}
