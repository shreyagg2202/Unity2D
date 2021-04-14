using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPelletsPickup : MonoBehaviour
{
    CircleCollider2D myBodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        myBodyCollider = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision other)
    {
        if (other.collider.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            Destroy(gameObject);
        }
    }
}
