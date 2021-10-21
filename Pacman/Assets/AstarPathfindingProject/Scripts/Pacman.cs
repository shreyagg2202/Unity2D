using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pacman : MonoBehaviour
    {
        [Header("Pacman Settings")]
        [SerializeField] float pacmanSpeed;
        bool isAlive = true;

        [Header("Time Controller")]
        [SerializeField] float frightenedTime = 7f;
        [SerializeField] float timeElapsed;

        int numberOfPowerPelletsEaten;

        [Header("Cache")]
        Rigidbody2D myRigidBody;
        Animator myAnimator;
        CircleCollider2D myBodyCollider;

        // Start is called before the first frame update
        public void Start()
        {
            Enemy.isFrightened = false;
            myRigidBody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
            myBodyCollider = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        public void Update()
        {
            if (Enemy.isFrightened == true)
            {
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0f;
            }
            if (isAlive == true)
            {
                PacmanXPosMove();
                PacmanYPosMove();
            }
            LastMovement();
        }

        private void PacmanXPosMove()                               // X moving direction of character
        {
            float xControlThrow = Input.GetAxis("Horizontal");
            Vector2 playerVelocity = new Vector2(xControlThrow * pacmanSpeed, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;

            myAnimator.SetFloat("SpeedX", xControlThrow);
        }

        private void PacmanYPosMove()                               // Y moving direction of character
        {
            float yControlThrow = Input.GetAxis("Vertical");
            Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, yControlThrow * pacmanSpeed);
            myRigidBody.velocity = playerVelocity;

            myAnimator.SetFloat("SpeedY", yControlThrow);
        }
        
        private void LastMovement()                                 // Holds the last moving direction of the character
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

        public void OnCollisionEnter2D(Collision2D other)            // Pacman Death
        {
            if (other.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))                 
            {
                if (Enemy.isFrightened == true)
                {
                    FindObjectOfType<Enemy>().isEaten = true;
                }
                else if (FindObjectOfType<Enemy>().isEaten == false)
                {
                    isAlive = false;
                    myAnimator.SetTrigger("Dead");                      //Death Animation
                    FindObjectOfType<GameSession>().PacmanDeath();
                    myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                    myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
                }
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)          // Checks if Pacman has eaten The Power Pellets
        {
            if (collision.CompareTag("Power Pellet"))               
            {
                numberOfPowerPelletsEaten += 1;
                if (FindObjectOfType<Enemy>().isEaten == false)
                {
                    StartCoroutine(Frightened());
                }
                else
                {
                    numberOfPowerPelletsEaten = 0;
                }
            }
        }

        IEnumerator Frightened()
        {
            Enemy.isFrightened = true;
            FindObjectOfType<Enemy>().isScattering = false;             // Ememy Script Disabled
            FindObjectOfType<Enemy>().isChasing = true;                 // AI Script Enabled for random movement
            if (numberOfPowerPelletsEaten > 1)                          // if more than one pellet eaten at a time then frightened time is increased
            {
                StopCoroutine(Frightened());
                StartCoroutine(TwoPelletsEaten());
            }

            yield return new WaitForSeconds(frightenedTime);

            if (numberOfPowerPelletsEaten <= 1)                         // if only one pellet is eaten
            {
                numberOfPowerPelletsEaten = 0;                          // reset number of pellets eaten
                Enemy.isFrightened = false;
                FindObjectOfType<Enemy>().isScattering = false;         // starts chasing player
                FindObjectOfType<Enemy>().isChasing = true;
                FindObjectOfType<Enemy>().scatterTime = 0f;
                AIDestinationSetter.frightenedEndTime = 0f;
            }
        }

        IEnumerator TwoPelletsEaten()
        {

            AIDestinationSetter.frightenedEndTime = 0f;
            frightenedTime *= 2;                                          // frightened time increased
            frightenedTime -= timeElapsed;
            timeElapsed = 0f;

            yield return new WaitForSeconds(frightenedTime);
            
            frightenedTime = 7f;                                          // frightened time reset for next pellet
            numberOfPowerPelletsEaten = 0;
            Enemy.isFrightened = false;
            FindObjectOfType<Enemy>().isScattering = false;                // starts chasing
            FindObjectOfType<Enemy>().isChasing = true;
            FindObjectOfType<Enemy>().scatterTime = 0f;
            AIDestinationSetter.frightenedEndTime = 0f;
        }
    }
}
