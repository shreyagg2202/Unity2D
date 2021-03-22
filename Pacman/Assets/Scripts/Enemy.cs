using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float enemySpeed = 5f;
    Vector2 currentPos;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentPos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = new Vector2(transform.position.x, transform.position.y);
        if (transform.position.x >= 1)
        {
            anim.SetFloat("MoveX", 1f);
        }
        else if (transform.position.x <= -1)
        {
            anim.SetFloat("MoveX", -1f);
        }
        else
        {
            anim.SetFloat("MoveX", 0f);
        }

        if (transform.position.y >= 1)
        {
            anim.SetFloat("MoveY", 1f);
        }
        else if (transform.position.y <= -1)
        {
            anim.SetFloat("MoveY", -1f);
        }
        else
        {
            anim.SetFloat("MoveY", 0f);
        }
        currentPos = newPos;
    }
}
