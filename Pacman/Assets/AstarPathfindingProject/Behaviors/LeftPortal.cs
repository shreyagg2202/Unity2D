using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPortal : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform rightPortal;
    public bool teleporting = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && teleporting == false)
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        teleporting = true;
        player.transform.position = rightPortal.transform.position;
        yield return new WaitForSeconds(1);
        teleporting = false;
    }
}
