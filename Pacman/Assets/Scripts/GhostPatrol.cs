using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPatrol : MonoBehaviour
{
    Rigidbody2D enemyRb;
    private float enemySpeed = 5f;
    private int moveDir;
    private bool canChangeDir;
    private int curDir;
    
    float switchToChaseTimer = 0f;

    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        moveDir = Random.Range(1, 5);             //1=down  2=left  3=right  4=up
        if (moveDir < 1 || moveDir > 4)
        {
            while (moveDir < 1 || moveDir > 4)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        canChangeDir = true;   //Allows the player to chose a random direction to move
    }

    // Update is called once per frame
    void Update()
    {
        switchToChaseTimer += Time.deltaTime;            //time after which the ghost goes to chase stance
        Debug.Log(switchToChaseTimer);

        if (switchToChaseTimer == 20f)
        {
            MovementHandler();
            switchToChaseTimer = 0f;
        }

        else
        {
            //keep patrolling
        }

    }

    private void FixedUpdate()
    {
        enemyRb.MovePosition(enemyRb.position + (moveDirection * enemySpeed) * Time.fixedDeltaTime);
    }

    void ChangeDirection()
    {
        //can move in any direction
        if (WallDetection.pathOpenDown == true || WallDetection.pathOpenLeft == true || WallDetection.pathOpenRight == true || WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);             //1=down  2=left  3=right  4=up
            if (moveDir < 1 || moveDir > 4)
            {
                while (moveDir < 1 || moveDir > 4)
                {
                    moveDir = Random.Range(1, 5);
                    if (moveDir == curDir)
                    {
                        moveDir = Random.Range(1, 5);
                    }
                }
            }
        }

        //cannot move down              // can move left, right or up
        else if (WallDetection.pathOpenDown == false || WallDetection.pathOpenLeft == true || WallDetection.pathOpenRight == true || WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir >4 || moveDir == 1)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move up
        else if (WallDetection.pathOpenDown == true || WallDetection.pathOpenLeft == true || WallDetection.pathOpenRight == true || WallDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 4)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move left              
        else if (WallDetection.pathOpenDown == true || WallDetection.pathOpenLeft == false || WallDetection.pathOpenRight == true || WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 2)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move right
        else if (WallDetection.pathOpenDown == true || WallDetection.pathOpenLeft == true || WallDetection.pathOpenRight == false || WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move down or up
        else if (WallDetection.pathOpenDown == false || WallDetection.pathOpenLeft == true || WallDetection.pathOpenRight == true || WallDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 1 || moveDir == 4)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move down or left      // can only move up or right
        else if (WallDetection.pathOpenDown == false || WallDetection.pathOpenLeft == false || WallDetection.pathOpenRight == true || WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 1 || moveDir == 2)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move down or right     // can only move left ot up
        else if (WallDetection.pathOpenDown == false || WallDetection.pathOpenLeft == true || WallDetection.pathOpenRight == false || WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 1 || moveDir == 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move up or left        
        else if (WallDetection.pathOpenDown == true || WallDetection.pathOpenLeft == false || WallDetection.pathOpenRight == true || WallDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 2 || moveDir == 4)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move up or right       
        else if (WallDetection.pathOpenDown == true || WallDetection.pathOpenLeft == true || WallDetection.pathOpenRight == false || WallDetection.pathOpenUp == false)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 3 || moveDir == 4)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //cannot move left or right     
        else if (WallDetection.pathOpenDown == true || WallDetection.pathOpenLeft == false || WallDetection.pathOpenRight == false || WallDetection.pathOpenUp == true)
        {
            moveDir = Random.Range(1, 5);
            if (moveDir < 1 || moveDir > 4 || moveDir == 2 || moveDir == 3)
            {
                moveDir = Random.Range(1, 5);
            }
        }

        //can only move down            
        else if (WallDetection.pathOpenDown == true || WallDetection.pathOpenLeft == false || WallDetection.pathOpenRight == false || WallDetection.pathOpenUp == false)
        {
            moveDir = 1;
        }

        //can only move up              
        else if (WallDetection.pathOpenDown == false || WallDetection.pathOpenLeft == false || WallDetection.pathOpenRight == false || WallDetection.pathOpenUp == true)
        {
            moveDir = 4;
        }

        //can only move left            
        else if (WallDetection.pathOpenDown == false || WallDetection.pathOpenLeft == true || WallDetection.pathOpenRight == false || WallDetection.pathOpenUp == false)
        {
            moveDir = 2;
        }

        //can only move right           
        else if (WallDetection.pathOpenDown == false || WallDetection.pathOpenLeft == false || WallDetection.pathOpenRight == true || WallDetection.pathOpenUp == false)
        {
            moveDir = 3;
        }
    }

    void MoveDown()
    {
        curDir = 1;
        //Set animation here
        moveDirection = Vector2.down;
    }

    void MoveLeft()
    {
        curDir = 2;
        //Set animation here
        moveDirection = Vector2.left;
    }

    void MoveRight()
    {
        curDir = 3;
        //Set animation here
        moveDirection = Vector2.right;
    }

    void MoveUp()
    {
        curDir = 4;
        //Set animation here
        moveDirection = Vector2.up;
    }
    void MovementHandler()
    {
        if (moveDir == 1)
        {
            if (WallDetection.pathOpenDown == true)
            {
                MoveDown();
            }
            else
            {
                ChangeDirection();
            }
        }
        else if (moveDir == 2)
        {
            if (WallDetection.pathOpenLeft == true)
            {
                MoveLeft();
            }
            else
            {
                ChangeDirection();
            }
        }
        else if (moveDir == 3)
        {
            if (WallDetection.pathOpenRight == true)
            {
                MoveRight();
            }
            else
            {
                ChangeDirection();
            }
        }
        else if (moveDir == 4)
        {
            if (WallDetection.pathOpenUp == true)
            {
                MoveUp();
            }
            else
            {
                ChangeDirection();
            }
        }

        if (canChangeDir == true)
        {
            StartCoroutine(RandomMovement());
        }
    }

    IEnumerator RandomMovement()
    {
        canChangeDir = false;
        float timer = 0.1f;
        yield return new WaitForSeconds(timer);
        ChangeDirection();
        yield return new WaitForSeconds(timer);
        canChangeDir = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }
}
