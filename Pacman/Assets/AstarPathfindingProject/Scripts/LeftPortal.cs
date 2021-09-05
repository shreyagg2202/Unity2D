using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPortal : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform rightPortal;
    public bool playerTeleporting = false;
    public bool enemyTeleporting = false;

    private void Update()
    {
        //Debug.Log(enemyTeleporting);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Add Enemy Teleportation
        if (playerTeleporting == false)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(PlayerTeleportation());
            }
            else if (enemyTeleporting == false)
            {
                StartCoroutine(EnemyTeleportation());
            }
        }

        IEnumerator PlayerTeleportation()
        {
            playerTeleporting = true;
            player.transform.position = rightPortal.transform.position;
            yield return new WaitForSeconds(0.3f);
            playerTeleporting = false;
        }

        IEnumerator EnemyTeleportation()
        {
            enemyTeleporting = true;
            other.transform.position = rightPortal.transform.position;
            yield return new WaitForSeconds(1f);
            enemyTeleporting = false;
        }
    }
}