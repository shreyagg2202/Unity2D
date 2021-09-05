using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPelletsPickup : MonoBehaviour
{
    CircleCollider2D myBodyCollider;

    // Start is called before the first frame update
    public void Start()
    {
        myBodyCollider = GetComponent<CircleCollider2D>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
