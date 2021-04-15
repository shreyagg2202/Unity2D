using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class PacDotsPickup : MonoBehaviour
    {
        [SerializeField] int pointsForPickup = 10;
        CircleCollider2D myBodyCollider;

        public void Start()
        {
            myBodyCollider = GetComponent<CircleCollider2D>();
        }
        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Pacman")))
            {
                FindObjectOfType<GameSession>().AddScore(pointsForPickup);
                Destroy(gameObject);
            }

            else { return; }

        }
    }
}
