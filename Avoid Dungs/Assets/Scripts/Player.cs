 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer renderer;

    private float speed = 5;
    private float horizontal;

    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(768, 1024, false);

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        
        isAttacking = false;
    }

    // Update is called once per frame 
    void FixedUpdate()
    {
        //For Android Application
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x >= (Screen.width / 2))
                {
                    horizontal = 1;
                }
                else if (touch.position.x < (Screen.width / 2))
                {
                    horizontal = -1;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                horizontal = 0;
            }
        }
        else
        {
            horizontal = Input.GetAxis("Horizontal");
        }

        //Debug.Log(horizontal);
        

        if (!GameManager.Instance.getStopTrigger())
        {
            animator.SetTrigger("alive");
            PlayerMove();   //moves the character
            PlayerAttack(); //attack animation
        }

        if(GameManager.Instance.getStopTrigger())
        {
            animator.SetTrigger("dead");
        }
        
        ScreenCheck();  //checks if the player is out of bound to the camera
    }
  
    private void PlayerMove()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        //For Windows Application
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
                isAttacking = true;
                animator.SetTrigger("swordAttack");
            }
            //shield bash
            else if (Input.GetKeyDown(KeyCode.X))
            {
                isAttacking = true;
                animator.SetTrigger("shieldAttack");
            }
        }

    }

    private void EndAttack()
    {
        isAttacking = false;
    }

    private void ScreenCheck()
    {
        Vector3 charPos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (charPos.x < 0.05f) { charPos.x = 0.05f; }
        if (charPos.x > 0.95f) { charPos.x = 0.95f; }
        this.transform.position = Camera.main.ViewportToWorldPoint(charPos);
    }

    public void gotHit()
    {
        animator.SetTrigger("dead");
    }

}
