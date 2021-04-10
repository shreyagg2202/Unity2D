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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Pacman")))
        {
            Destroy(gameObject);
        }
    }
}
