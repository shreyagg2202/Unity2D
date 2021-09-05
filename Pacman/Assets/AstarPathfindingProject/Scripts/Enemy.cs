using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{ 
    public class Enemy : MonoBehaviour
    {
        [SerializeField] Transform[] waypoints;
        int waypointIndex = 0;

        public float enemiesEaten;
        float scoreTime;

        public float originalEnemySpeed;
        public float EnemySpeed;
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
            enemiesEaten = 0;
            isScattering = false;
            isChasing = false;
            myBodyCollider = GetComponent<CircleCollider2D>();
            myAnimator = GetComponent<Animator>();
            transform.position = waypoints[waypointIndex].transform.position;
        }

        public void Update()
        {
            FindObjectOfType<AIPath>().maxSpeed = EnemySpeed;
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
            enemiesEaten = 0;
            myAnimator.SetBool("isFrightened", false);
            isScattering = true;
            isChasing = false;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, EnemySpeed * Time.deltaTime);
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
            enemiesEaten = 0;
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
                EnemySpeed = 4.5f;
            }
            else if (isEaten == true)
            {
                EnemySpeed = 12f;
            }
            else
            {
                EnemySpeed = originalEnemySpeed;
            }
        }                               
        
        public void OnCollisionEnter2D(Collision2D other)                        //Destroy Enemy When it touches Player
        {
            if (other.collider.gameObject.layer == LayerMask.NameToLayer("Pacman"))
            {
                if (isEaten == true)
                {
                    enemiesEaten += 1;
                    FindObjectOfType<Pacman>().enemyFrightened = false;
                    myAnimator.SetBool("isEaten", true);
                    myAnimator.SetBool("isFrightened", false);
                    StartCoroutine(FreezeTime());
                    gameObject.layer = 14;                                      //Layer is changed to dead to prevent collisions
                }
            }
        }
        IEnumerator FreezeTime()
        { 
            myAnimator.SetFloat("enemiesEaten", enemiesEaten);
            Time.timeScale = 0;
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
