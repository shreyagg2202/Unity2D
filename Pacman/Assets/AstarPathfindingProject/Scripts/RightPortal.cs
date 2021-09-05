using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPortal : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform leftPortal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<LeftPortal>().playerTeleporting == false)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(PlayerTeleportation());
            }
            else if (FindObjectOfType<LeftPortal>().enemyTeleporting == false)
            {
                StartCoroutine(EnemyTeleportation());
            }
        }

        IEnumerator PlayerTeleportation()
        {
            FindObjectOfType<LeftPortal>().playerTeleporting = true;
            player.transform.position = leftPortal.transform.position;
            yield return new WaitForSeconds(0.3f);
            FindObjectOfType<LeftPortal>().playerTeleporting = false;
        }

        IEnumerator EnemyTeleportation()
        {
            FindObjectOfType<LeftPortal>().enemyTeleporting = true;
            other.transform.position = leftPortal.transform.position;
            yield return new WaitForSeconds(1f);
            FindObjectOfType<LeftPortal>().enemyTeleporting = false;
        }
    }
}
