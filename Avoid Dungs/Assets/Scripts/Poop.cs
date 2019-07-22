using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //if game is not over (Player did NOT get hit by poop)
            if (!GameManager.Instance.getStopTrigger())
            {
                GameManager.Instance.Score();
            }
            
            animator.SetTrigger("poop");
        }

        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.GameOver();
            animator.SetTrigger("poop");
        }
    }

}
