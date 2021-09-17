using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding {
    public class BaseMovement : MonoBehaviour
    {
        bool isSpawned = false;
        float spawnTimer = 0f;
        [SerializeField] Transform[] waypoints;
        int waypointIndex = 0;
        float EnemySpeed = 7f;

        private void Awake()
        {
            DisableScripts();
            transform.position = waypoints[waypointIndex].transform.position;
        }
        // Update is called once per frame
        void Update()
        {
            spawnTimer += Time.deltaTime;
            Debug.Log(spawnTimer);
            if (spawnTimer >= 15f)
            {
                isSpawned = true;
                this.enabled = false;
            }

            if (isSpawned == true)
            {
                EnableScripts();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, EnemySpeed * Time.deltaTime);   
                if (transform.position == waypoints[0].transform.position)
                {
                    waypointIndex = 1;
                }
                else if (transform.position == waypoints[1].transform.position)
                {
                    waypointIndex = 0;
                }
            }
        }
        void DisableScripts()
        {
            GetComponent<Enemy>().enabled = false;
        }

        void EnableScripts()
        {
            GetComponent<Enemy>().enabled = true;
        }
    }
}
