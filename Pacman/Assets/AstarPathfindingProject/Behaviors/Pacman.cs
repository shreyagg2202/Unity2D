using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Pacman : MonoBehaviour
    {
        [SerializeField] float pacmanSpeed;

        bool isAlive = true;
        public bool enemyFrightened;
        public float frightenedTime = 6f;

        Rigidbody2D myRigidBody;
        Animator myAnimator;
        CircleCollider2D myBodyCollider;


        // Start is called before the first frame update
        void Start()
        {
            enemyFrightened = false;
            myRigidBody = GetComponent<Rigidbody2D>();
            myAnimator = GetComponent<Animator>();
            myBodyCollider = GetComponent<CircleCollider2D>();
        }

        // Update is called once per frame
        void Update()
        {
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
                myAnimator.SetTrigger("Dead");
                FindObjectOfType<GameSession>().PacmanDeath();
                myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
                myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }

        public void OnTriggerEnter2D(Collider2D other)              //Checks if Pacman has eaten The Power Pellets
        {
            if (other.CompareTag("Power Pellet"))
            {
                StartCoroutine(Frightened());
            }
        }

        IEnumerator Frightened()
        {
            enemyFrightened = true;
            FindObjectOfType<Enemy>().enabled = false;              //Ememy Script Disabled
            FindObjectOfType<AIDestinationSetter>().enabled = true; //AI Script Enabled
            yield return new WaitForSeconds(frightenedTime);
            enemyFrightened = false;
            FindObjectOfType<Enemy>().enabled = true;               //Enemy Script Enabled
        }
    }
}
