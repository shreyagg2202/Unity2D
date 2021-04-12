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
        [SerializeField] float timeTillChase;

        public bool isScattering;
        public bool isChasing;

        Vector3 prevPos;
        Vector3 moveDirection;

        public float scatterTime = 0f;
        public float chaseTime = 0f;

        CircleCollider2D myBodyCollider;

        // Start is called before the first frame update
        void Start()
        {
            isScattering = false;
            isChasing = false;
            myBodyCollider = GetComponent<CircleCollider2D>();
            transform.position = waypoints[waypointIndex].transform.position;

        }

        private void Update()
        {
            if (prevPos != transform.position)
            {
                moveDirection = (transform.position - prevPos).normalized;
                prevPos = transform.position;
            }
        }

        private void FixedUpdate()
        {
            scatterTime += Time.deltaTime;
            if (scatterTime <= timeTillScatter)
            {
                ScatterMode();
            }

            else
            {
                chaseTime += Time.deltaTime;
                ChaseMode();
            }
            DestroyOnCollision();
        }

        public void ScatterMode()
        {
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
            isScattering = false;
            isChasing = true;
            GetComponent<AIDestinationSetter>().enabled = true;
            waypointIndex = 0;
        }

        public void DestroyOnCollision()                                        //Destroy Enemy When it touches Player
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Pacman")))
            {
                Destroy(gameObject);
            }
        }
    }
}
