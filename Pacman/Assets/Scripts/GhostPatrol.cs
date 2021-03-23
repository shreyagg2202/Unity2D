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
    
    public float switchToChaseTimer = 0f;

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

        if(switchToChaseTimer == 20f)
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
        //cannot move down
        //cannot move up
        //cannot move left
        //cannot move right
        //cannot move down or up
        //cannot move down or left
        //cannot move down or right
        //cannot move up or left
        //cannot move up or right
        //cannot move left or right
        //can only move down
        //can only move up
        //can only move left
        //can only move right
    }
}
