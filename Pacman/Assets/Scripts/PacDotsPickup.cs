using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDotsPickup : MonoBehaviour
{
    [SerializeField] int pointsForPickup = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddScore(pointsForPickup);
        Destroy(gameObject);
    }
}
