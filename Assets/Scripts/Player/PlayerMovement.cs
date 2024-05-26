using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    private BoxCollider2D touchingCol;
    private Rigidbody2D body;
    private Animator anim;
    [SerializeField] private LayerMask wallLayer;

    // Handle check is ground?
    private ContactFilter2D castFilter;
    private float groundDistance = 0.05f;
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];

    [Header("SFX")]
    AudioManager audioManager;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        touchingCol = GetComponent<BoxCollider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        float horizonInput = Input.GetAxis("Horizontal");

        // Flip player
        if (horizonInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizonInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        // Animation 
        anim.SetBool("run", horizonInput != 0);
        anim.SetBool("grounded", isGrounded());
        anim.SetFloat("yVelocity", body.velocity.y);

        // Key input to jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // Fix when face wall
        if (!onWall())
        {
            body.velocity = new Vector2(horizonInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime;
                jumpCounter = extraJumps;
            }
            else
                coyoteCounter -= Time.deltaTime;
        }       
    }

    private void Jump()
    {
        // If coyote counter is 0 or less and not on the wall and don't have any extra jumps don't do anything
        if (coyoteCounter <= 0 && jumpCounter <= 0 && !onWall()) return;

        audioManager.PlaySound(audioManager.jump);

        // On ground
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else // On air
        {
            if (coyoteCounter > 0)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            } 
            else
            {
                if (jumpCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
            }
           
        }
        coyoteCounter = 0;
    }

    private bool isGrounded()
    {
        return touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }

    private bool onWall()
    {
        RaycastHit2D rayCast = Physics2D.BoxCast(touchingCol.bounds.center, touchingCol.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);

        return rayCast.collider != null;
    }

}
