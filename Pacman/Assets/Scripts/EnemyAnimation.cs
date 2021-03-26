using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator myBodyAnimation;
    Rigidbody2D myRigidBody;

    Vector3 previousPosition;
    private bool moving = true;
    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        myBodyAnimation = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (previousPosition != transform.position)
        {
            moveDirection = (transform.position - previousPosition).normalized;
            previousPosition = transform.position;
        }

        if (moving)
        {
            myRigidBody.velocity = moveDirection;
            myBodyAnimation.SetBool("isWalking", true);
            myBodyAnimation.SetFloat("xMov", moveDirection.x);
            myBodyAnimation.SetFloat("yMov", moveDirection.y);
        }
    }
}
