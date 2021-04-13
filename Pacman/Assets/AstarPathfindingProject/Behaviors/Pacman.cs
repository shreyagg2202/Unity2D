﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pacman : MonoBehaviour
    {
        [SerializeField] float pacmanSpeed;
        bool isAlive = true;

        public bool enemyFrightened;
        public float frightenedTime = 7f;
        int numberOfPowerPelletsEaten;
        public float timeElapsed;
        
        Rigidbody2D myRigidBody;
        Animator myAnimator;
        CircleCollider2D myBodyCollider;

        // Start is called before the first frame update
        public void Start()
        {
            enemyFrightened = false;
            myRigidBody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
            myBodyCollider = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        public void Update()
        {
            Debug.Log(numberOfPowerPelletsEaten);
            if (enemyFrightened == true)
            {
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0f;
            }
            if (isAlive)
            {
                PacmanXPosMove();
                PacmanYPosMove();
            }
            LastMovement();
            Debug.Log(enemyFrightened);
            Die();
        }

        private void PacmanXPosMove()                               //X moving direction of character
        {
            float xControlThrow = Input.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(xControlThrow * pacmanSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;

            myAnimator.SetFloat("SpeedX", xControlThrow);
        }

        private void PacmanYPosMove()                               //Y moving direction of character
        {
            float yControlThrow = Input.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, yControlThrow * pacmanSpeed);
            myRigidBody.velocity = playerVelocity;

            myAnimator.SetFloat("SpeedY", yControlThrow);
        }

        private void LastMovement()                                 //Holds the last moving direction of the character
        {
            float lastInputX = Input.GetAxis("Horizontal");
            float lastInputY = Input.GetAxis("Vertical");

            if (lastInputX != 0 || lastInputY != 0)
            {
                if (lastInputX > 0)
                {
                    myAnimator.SetFloat("LastMoveX", 1f);
                }
                else if (lastInputX < 0)
                {
                    myAnimator.SetFloat("LastMoveX", -1f);
                }
                else
                {
                    myAnimator.SetFloat("LastMoveX", 0f);
                }

                if (lastInputY > 0)
                {
                    myAnimator.SetFloat("LastMoveY", 1f);
                }
                else if (lastInputY < 0)
                {
                    myAnimator.SetFloat("LastMoveY", -1f);
                }
                else
                {
                    myAnimator.SetFloat("LastMoveY", 0f);
                }
            }
        }

        public void Die()                                           //Pacman Death
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
            {
                isAlive = false;
                myAnimator.SetTrigger("Dead");                      //Death Animation
                FindObjectOfType<GameSession>().PacmanDeath();
                myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)              //Checks if Pacman has eaten The Power Pellets
        {
            if (other.CompareTag("Power Pellet"))
            {
                numberOfPowerPelletsEaten += 1;
                StartCoroutine(Frightened());
            }
        }

        IEnumerator Frightened()
        {
            enemyFrightened = true;
            FindObjectOfType<Enemy>().isScattering = false;             //Ememy Script Disabled
            FindObjectOfType<Enemy>().isChasing = true;                 //AI Script Enabled
            if (numberOfPowerPelletsEaten > 1)                          //if more than one pellet eaten at a time then frightened time is increased
            {
                StopCoroutine(Frightened());
                StartCoroutine(TwoPelletsEaten());
            }
            yield return new WaitForSeconds(frightenedTime);
            if (numberOfPowerPelletsEaten <= 1)                         //if only one pellet is eaten
            {
                numberOfPowerPelletsEaten = 0;                          //reset number of pellets eaten
                enemyFrightened = false;
                FindObjectOfType<Enemy>().isScattering = true;          //Enemy Script Enabled
                FindObjectOfType<Enemy>().isChasing = false;
                FindObjectOfType<AIDestinationSetter>().frightenedEndTime = 0f;
            }
        }

        IEnumerator TwoPelletsEaten()
        {
            FindObjectOfType<AIDestinationSetter>().frightenedEndTime = 0f;
            frightenedTime *= 2;                                          //frightened time increased
            frightenedTime -= timeElapsed;
            timeElapsed = 0f;

            yield return new WaitForSeconds(frightenedTime);
            
            frightenedTime = 7f;                                          //frightened time reset for next pellet
            numberOfPowerPelletsEaten = 0;
            enemyFrightened = false;
            FindObjectOfType<Enemy>().isScattering = true;                //Enemy Script Enabled
            FindObjectOfType<Enemy>().isChasing = false;
            FindObjectOfType<AIDestinationSetter>().frightenedEndTime = 0f;
        }
    }
}
