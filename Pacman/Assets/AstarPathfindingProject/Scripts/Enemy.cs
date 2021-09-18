using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{ 
    public class Enemy : MonoBehaviour
    {
        [Header("Wayppints")]
        [SerializeField] Transform[] waypoints;
        int waypointIndex = 0;
        [SerializeField] int loopWaypointIndex;

        public float enemiesEaten;
        float scoreTime;
        public float originalEnemySpeed;
        public float EnemySpeed;
        [SerializeField] float timeTillScatter;

        [Header("State")]
        public bool isScattering;
        public bool isChasing;
        public bool isFrightened;
        public bool isEaten;

        [Header("Movement")]
        Vector3 prevPos;
        Vector3 moveDirection;

        [Header("Time Controller")]
        public float scatterTime = 0f;
        public float chaseTime = 0f;

        [Header("Cache")]
        CircleCollider2D myBodyCollider;
        Animator myAnimator;

        // Start is called before the first frame update
        public void Start()
        {
            enemiesEaten = 0;
            isScattering = false;
            isChasing = false;
            myBodyCollider = GetComponent<CircleCollider2D>();
            myAnimator = GetComponent<Animator>();
            transform.position = waypoints[waypointIndex].transform.position;                           // Initital position
        }

        public void Update()
        {
            GetComponent<AIPath>().maxSpeed = EnemySpeed;
            if (prevPos != transform.position)
            {
                moveDirection = (transform.position - prevPos).normalized;                              // Determine the move direction
                prevPos = transform.position;
            }
        }

        public void FixedUpdate()
        {
            if (isFrightened == false && isEaten == false)                // Check if scatter or chase
            {
                scatterTime += Time.deltaTime;
                if (scatterTime <= timeTillScatter && isChasing == false)                               // Scatter condition
                {
                    ScatterMode();
                }

                else                                                                                    // Else start chasing
                {
                    chaseTime += Time.deltaTime;
                    ChaseMode();
                }
            }
            else
            {
                waypointIndex = 0;                                                                      // After every chase return back to initial position
            }
            SpeedController();
        }

       public void ScatterMode()                                                                        // Controls the scatter state                        
        {
            enemiesEaten = 0;
            myAnimator.SetBool("isFrightened", false);
            isScattering = true;
            isChasing = false;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, EnemySpeed * Time.deltaTime);
            if (transform.position == waypoints[waypointIndex].transform.position)                      // Follow the given waypoint path
            {
                waypointIndex += 1;
                if (waypointIndex >= waypoints.Length - 1)
                {
                    waypointIndex = loopWaypointIndex;
                }
            }
        }

        public void ChaseMode()                                                                         // Controls the chase state
        {
            enemiesEaten = 0;
            myAnimator.SetBool("isFrightened", false);
            isScattering = false;
            isChasing = true;
            GetComponent<AIDestinationSetter>().enabled = true;                                         // Chase is handled by AIDestinationSetter script
            waypointIndex = 0;
            
        }

        public void SpeedController()                                           // Controls enemy speed in different states
        {
            if (isFrightened == true)             // Frightened speed
            {
                EnemySpeed = 4.5f;
            }
            else if (isEaten == true)                                           // Eaten speed
            {
                EnemySpeed = 12f;
            }
            else                                                                // Scatter and Chase speed
            {
                EnemySpeed = originalEnemySpeed;
            }
        }                               
        
        public void OnCollisionEnter2D(Collision2D other)                           
        {
            if (other.collider.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                if (isEaten == true)                                            // Enemy is eaten
                {
                    enemiesEaten++;
                    isFrightened = false;
                    myAnimator.SetBool("isEaten", true);
                    myAnimator.SetBool("isFrightened", false);
                    StartCoroutine(FreezeTime());
                    gameObject.layer = 14;                                      // Layer is changed to dead to prevent collisions
                }
            }
        }

        IEnumerator FreezeTime()                                                // Freeze for 1 second before resuming the game after enemy is eaten
        {
            Time.timeScale = 0;
            switch (enemiesEaten)
            {
                case 1:
                    myAnimator.SetFloat("enemiesEaten", enemiesEaten);
                    FindObjectOfType<GameSession>().AddScore(200);
                    break;
                case 2:
                    myAnimator.SetFloat("enemiesEaten", enemiesEaten);
                    FindObjectOfType<GameSession>().AddScore(400);
                    break;
                case 3:
                    myAnimator.SetFloat("enemiesEaten", enemiesEaten);
                    FindObjectOfType<GameSession>().AddScore(800);
                    break;
                case 4:
                    myAnimator.SetFloat("enemiesEaten", enemiesEaten);
                    FindObjectOfType<GameSession>().AddScore(1600);
                    break;
                default:
                    enemiesEaten = 0;
                    myAnimator.SetFloat("enemiesEaten", enemiesEaten);
                    break;
            }
            yield return new WaitForSecondsRealtime(1);
            Time.timeScale = 1;
            if (enemiesEaten > 1)
            {
                StopCoroutine(FreezeTime());
                StartCoroutine(FreezeTime());
                enemiesEaten = 0;
            }
            else
            {
                enemiesEaten = 0;
            }
            myAnimator.SetFloat("enemiesEaten", enemiesEaten);
        }
    }
}
