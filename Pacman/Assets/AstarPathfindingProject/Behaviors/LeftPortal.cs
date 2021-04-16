using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPortal : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform rightPortal;
    public bool teleporting = false;
    float timeToTeleportation = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (teleporting == false)
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        teleporting = true;
        player.transform.position = rightPortal.transform.position;
        yield return new WaitForSeconds(timeToTeleportation);
        teleporting = false;
    }
}
