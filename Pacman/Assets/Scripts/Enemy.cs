using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pathfinding
{

    public class Enemy : MonoBehaviour
    {
        [SerializeField] Transform[] waypoints;
        int waypointIndex = 0;

        [SerializeField] float blinkySpeed = 2f;
        [SerializeField] float timeTillScatter;
        [SerializeField] float timeTillChase;
       
        Vector3 prevPos;
        Vector3 moveDirection;

        float scatterTime = 0f;
        float chaseTime = 0f;

        CircleCollider2D myBodyCollider;

        // Start is called before the first frame update
        void Start()
        {
            myBodyCollider = GetComponent<CircleCollider2D>();

            GetComponent<AIDestinationSetter>().enabled = false;
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
            if (scatterTime <= timeTillScatter )
            {
                ScatterMode();
            }

            else
            {
                chaseTime += Time.deltaTime;
                ChaseMode();
                if (chaseTime >= timeTillChase)
                {
                    scatterTime = -3f;
                    chaseTime = 0f;
                }
            }
            DestroyOnCollision();
        }

        public void ScatterMode()
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, blinkySpeed * Time.deltaTime);
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }

        public void ChaseMode()
        {
            GetComponent<AIDestinationSetter>().enabled = true;
            waypointIndex = 0;
        }

        public void FrightenedMode(bool active)
        {
            transform.position = new Vector3(-moveDirection.x, -moveDirection.y);       
        }

        public void DestroyOnCollision()
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Pacman")))
            {
                Destroy(gameObject);
            }
        }
    }
}
