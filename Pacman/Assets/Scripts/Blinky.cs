using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pathfinding 
{

    public class Blinky : MonoBehaviour
    {
        [SerializeField] float blinkySpeed = 2f;
        [SerializeField] Transform[] waypoints;
        int waypointIndex = 0;
        [SerializeField] float scatterTime;
        [SerializeField] float chaseTime;

        GameObject Enemy = GameObject.Find("Blinky");


        // Start is called before the first frame update
        void Start()
        {
            GetComponent<AIDestinationSetter>().enabled = false;
            
            transform.position = waypoints[waypointIndex].transform.position;

            scatterTime = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            scatterTime += Time.deltaTime;
            Scatter();
            if (scatterTime >= 7)
            {
                chaseTime += Time.deltaTime;
                Chase();
                if (chaseTime >= 20)
                {
                    scatterTime = 0f;
                }
            }
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
        }
    }
}
