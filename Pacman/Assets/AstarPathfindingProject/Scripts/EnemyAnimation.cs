using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [Header("Movement and Position")]
    public float xMov;
    public float yMov;
    Vector3 previousPosition;
    public Vector3 moveDirection;

    [Header("Cache")]
    Animator myBodyAnimation;
    Rigidbody2D myRigidBody;

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
            moveDirection = (transform.position - previousPosition).normalized;                     // Calculating the move direction
            previousPosition = transform.position;
        }

        myRigidBody.velocity = moveDirection;

        xMov = moveDirection.x;
        yMov = moveDirection.y;
        
        myBodyAnimation.SetBool("isWalking", true);
        myBodyAnimation.SetFloat("xMov", xMov);
        myBodyAnimation.SetFloat("yMov", yMov);
    }
}
