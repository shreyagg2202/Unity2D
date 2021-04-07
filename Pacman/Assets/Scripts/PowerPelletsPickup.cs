using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPelletsPickup : MonoBehaviour
{
    CircleCollider2D myBodyCollider;
    public bool isFrightened;
    [SerializeField] float frightenedTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        myBodyCollider = GetComponent<CircleCollider2D>();
        isFrightened = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Pacman")))
        {
            StartCoroutine(Frightened());
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        Debug.Log(isFrightened);
    }
    IEnumerator Frightened()
    {
        isFrightened = true;
        yield return new WaitForSeconds(frightenedTime);
    }
}
