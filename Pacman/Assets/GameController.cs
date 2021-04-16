using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Transform portalRight;
    [SerializeField] Transform portalLeft;
    [SerializeField] Transform player;
    [SerializeField] Transform[] Enemies;

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (player.transform.position == portalRight.transform.position)
        {
            player.transform.position = portalLeft.transform.position;
        }
        else if (player.transform.position == portalLeft.transform.position)
        {
            player.transform.position = portalRight.transform.position;
        }
    }
}
