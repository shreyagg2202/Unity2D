using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacman : MonoBehaviour
{
    [SerializeField] float pacmanSpeed = 1f;
    bool isFacingRight;
    bool isFacingUp;

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
        LastMovement();
    }

    private void PacmanXPosMove()                               //X moving direction of character
    {
        float xControlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(xControlThrow * pacmanSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        animator.SetFloat("SpeedX", xControlThrow);
    }

    private void PacmanYPosMove()                               //Y moving direction of character
    {
        float yControlThrow = Input.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, yControlThrow * pacmanSpeed);
        myRigidBody.velocity = playerVelocity;

        animator.SetFloat("SpeedY", yControlThrow);
    }

    private void LastMovement()                                   //Holds the last moving direction of the character
    {
        float lastInputX = Input.GetAxis("Horizontal");
        float lastInputY = Input.GetAxis("Vertical");

        if (lastInputX != 0 || lastInputY != 0)
        {
            if (lastInputX > 0)
            {
                animator.SetFloat("LastMoveX", 1f);
            }
            else if (lastInputX < 0)
            {
                animator.SetFloat("LastMoveX", -1f);
            }
            else
            {
                animator.SetFloat("LastMoveX", 0f);
            }

            if (lastInputY > 0)
            {
                animator.SetFloat("LastMoveY", 1f);
            }
            else if (lastInputY < 0)
            {
                animator.SetFloat("LastMoveY", -1f);
            }
            else
            {
                animator.SetFloat("LastMoveY", 0f);
            }
        }

    }
}
