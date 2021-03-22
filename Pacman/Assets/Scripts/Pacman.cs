using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] float pacmanSpeed = 1f;

    Rigidbody2D myRigidBody;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PacmanXPosMove();
        PacmanYPosMove();
    }

    private void PacmanXPosMove()
    {
        float xControlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(xControlThrow * pacmanSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        animator.SetFloat("Horizontal", xControlThrow);
    }

    private void PacmanYPosMove()
    {
        float yControlThrow = Input.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, yControlThrow * pacmanSpeed);
        myRigidBody.velocity = playerVelocity;
        animator.SetFloat("Vertical", yControlThrow);
    }
}
