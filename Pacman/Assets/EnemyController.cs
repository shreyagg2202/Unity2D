using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] Transform leftWaypoint;
        [SerializeField] Transform rightWaypoint;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (FindObjectOfType<LeftPortal>().teleporting == true)
                {
                    FindObjectOfType<Enemy>().EnemySpeed = 4;
                }
                else
                {
                    FindObjectOfType<Enemy>().EnemySpeed = FindObjectOfType<Enemy>().originalEnemySpeed;
                }
            }
        }
    }
}
