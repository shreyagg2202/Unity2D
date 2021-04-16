using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPortal : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform leftPortal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && FindObjectOfType<LeftPortal>().teleporting == false)
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        FindObjectOfType<LeftPortal>().teleporting = true;
        player.transform.position = leftPortal.transform.position;
        yield return new WaitForSeconds(1);
        FindObjectOfType<LeftPortal>().teleporting = false;
    }
}
