using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : MonoBehaviour
{
    Rigidbody rigid;
    float speed = 15;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"),
                                  0,
                                  Input.GetAxisRaw("Vertical"));

        rigid.AddForce(vec * speed);

    }
}
