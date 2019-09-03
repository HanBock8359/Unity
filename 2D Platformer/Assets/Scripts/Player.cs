using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer renderer;

    public float speed;
    public float jumpPower;
    private float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        //speed = 5.0f;
        //jumpPower = 5.0f;
        //horizontal = 0.0f;
    }

    void FixedUpdate()
    {
        //Landing Platform
        if (rigidbody.velocity.y < 0)
        {
            Debug.DrawRay(rigidbody.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            //if Player collides to a platform
            if (rayHit.collider != null)
            {
                animator.SetBool("isJumping", false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        //Horizontal movement
        horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        //if moving to LEFT
        if (horizontal < 0)
        {
            renderer.flipX = true;
        }
        //else moving to RIGHT
        else if (horizontal > 0)
        {
            renderer.flipX = false;
        }

        rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y);

        //Walk
        //if movement speed is less than 0.3
        if(Mathf.Abs(rigidbody.velocity.x) < 0.3)
        {
            animator.SetBool("isWalking", false);   //animation will be Idle
        }
        else
        {
            animator.SetBool("isWalking", true);    //animation will be walking
        }

        //Vertical movement

        //Jump

        //Allows only a SINGLE Jump
        if (Input.GetButtonDown("Jump") && !animator.GetBool("isJumping"))
        {
            rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
 ;          animator.SetBool("isJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Got hit!");
        }
    }
}
