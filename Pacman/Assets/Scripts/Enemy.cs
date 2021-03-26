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
        float scatterTime = 0f;
        float chaseTime = 0f;

        CircleCollider2D myBodyCollider;
        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AIDestinationSetter>().enabled = false;
            myBodyCollider = GetComponent<CircleCollider2D>();
            
            transform.position = waypoints[waypointIndex].transform.position;

        }

        // Update is called once per frame
        void Update()
        {
            scatterTime += Time.deltaTime;

            if (scatterTime <= timeTillScatter )
            {
                Scatter();
            }

            else
            {
                chaseTime += Time.deltaTime;
                Chase();
                if (chaseTime >= timeTillChase)
                {
                    scatterTime = -3f;
                    chaseTime = 0f;
                }
            }
            DestroyOnCollision();
        }

        public void Scatter()
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, blinkySpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }

            if (waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
        }

        public void Chase()
        {
            GetComponent<AIDestinationSetter>().enabled = true;
            waypointIndex = 0;
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
