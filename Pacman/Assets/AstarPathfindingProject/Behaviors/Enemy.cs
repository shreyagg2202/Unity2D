using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{ 
    public class Enemy : MonoBehaviour
    {
        [SerializeField] Transform[] waypoints;
        int waypointIndex = 0;

        [SerializeField] float blinkySpeed;
        [SerializeField] float timeTillScatter;

        [Header("State")]
        public bool isScattering;
        public bool isChasing;
        public bool isEaten;

        Vector3 prevPos;
        Vector3 moveDirection;

        [Header("Time Controller")]
        public float scatterTime = 0f;
        public float chaseTime = 0f;

        CircleCollider2D myBodyCollider;
        Animator myAnimator;

        // Start is called before the first frame update
        public void Start()
        {
            isScattering = false;
            isChasing = false;
            myBodyCollider = GetComponent<CircleCollider2D>();
            myAnimator = GetComponent<Animator>();
            transform.position = waypoints[waypointIndex].transform.position;
        }

        public void Update()
        {
            FindObjectOfType<AIPath>().maxSpeed = blinkySpeed;
            if (prevPos != transform.position)
            {
                moveDirection = (transform.position - prevPos).normalized;
                prevPos = transform.position;
            }
        }

        public void FixedUpdate()
        {
            if (FindObjectOfType<Pacman>().enemyFrightened == false && isEaten == false)
            {
                scatterTime += Time.deltaTime;
                if (scatterTime <= timeTillScatter && isChasing == false)
                {
                    ScatterMode();
                }

                else
                {
                    chaseTime += Time.deltaTime;
                    ChaseMode();
                }
            }
            else
            {
                waypointIndex = 0;
            }
            SpeedController();
        }

        public void ScatterMode()
        {
            myAnimator.SetBool("isFrightened", false);
            isScattering = true;
            isChasing = false;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, blinkySpeed * Time.deltaTime);
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
                if (waypointIndex >= 10)
                {
                    waypointIndex = 6;
                }
            }
        }

        public void ChaseMode()
        {
            myAnimator.SetBool("isFrightened", false);
            isScattering = false;
            isChasing = true;
            GetComponent<AIDestinationSetter>().enabled = true;
            waypointIndex = 0;
            
        }

        public void SpeedController()                                           //Controls Enemy Speed at different States
        {
            if (FindObjectOfType<Pacman>().enemyFrightened == true)
            {
                blinkySpeed = 4.5f;
            }
            else if (isEaten == true)
            {
                blinkySpeed = 12f;
            }
            else
            {
                blinkySpeed = 7f;
            }
        }                               
        
        public void OnCollisionEnter2D(Collision2D other)                        //Destroy Enemy When it touches Player
        {
            if (other.collider.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                if (isEaten == false)
                {
                    Destroy(gameObject);
                }
                else if (isEaten == true)
                {
                    FindObjectOfType<Pacman>().enemyFrightened = false;
                    myAnimator.SetBool("isEaten", true);
                    myAnimator.SetBool("isFrightened", false);
                    gameObject.layer = 14;                                      //Layer is changed to dead to prevent collisions
                }
            }
        }
    }
}
