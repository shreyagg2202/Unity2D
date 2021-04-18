using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPortal : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform[] enemies;
    [SerializeField] Transform leftPortal;
    float timeToTeleportation = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<LeftPortal>().teleporting == false)
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        FindObjectOfType<LeftPortal>().teleporting = true;
        player.transform.position = leftPortal.transform.position;
        yield return new WaitForSeconds(timeToTeleportation);
        FindObjectOfType<LeftPortal>().teleporting = false;
    }
}
