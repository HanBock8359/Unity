 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer renderer;

    private float speed = 3;
    private float horizontal;

    private bool isAttacking;
    private bool swordAttack;
    private bool shieldAttack;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        isAttacking = false;
        swordAttack = false;
        shieldAttack = false;
    }

    // Update is called once per frame 
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        PlayerMove();   //moves the character
        PlayerAttack(); //attack animation
        ScreenCheck();  //checks if the player is out of bound to the camera
    }

    private void PlayerMove()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        //if moving to LEFT
        if(horizontal < 0)
        {
            renderer.flipX = true;
        }
        //else moving to RIGHT
        else if(horizontal > 0)
        {
            renderer.flipX = false;
        }

        rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y);
    }

    private void PlayerAttack()
    {
        if (!isAttacking)
        {
            //sword attack
            if (Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetBool("swordAttack", true);
                isAttacking = true;
            }
            //shield bash
            else if (Input.GetKeyDown(KeyCode.X))
            {
                animator.SetBool("shieldAttack", true);
                isAttacking = true;
            }
        }

    }

    private void EndAttack()
    {
        isAttacking = false;
        animator.SetBool("swordAttack", false);
        animator.SetBool("shieldAttack", false);
    }

    private void ScreenCheck()
    {
        Vector3 charPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (charPos.x < 0.05f) { charPos.x = 0.05f; }
        if (charPos.x > 0.95f) { charPos.x = 0.95f; }
        this.transform.position = Camera.main.ViewportToWorldPoint(charPos);
    }



}
