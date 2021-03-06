﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public float xMov;
    public float yMov;

    Animator myBodyAnimation;
    Rigidbody2D myRigidBody;

    Vector3 previousPosition;
    public Vector3 moveDirection;

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

        myRigidBody.velocity = moveDirection;

        xMov = moveDirection.x;
        yMov = moveDirection.y;
        
        myBodyAnimation.SetBool("isWalking", true);
        myBodyAnimation.SetFloat("xMov", xMov);
        myBodyAnimation.SetFloat("yMov", yMov);
    }
}
