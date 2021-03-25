using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator myBodyAnimation;
    float currentPosX;
    float currentPosY;
    float newPosX;
    float newPosY;

    // Start is called before the first frame update
    void Start()
    {
        myBodyAnimation = GetComponent<Animator>();
        currentPosX = transform.position.x;
        currentPosY = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        newPosX = transform.position.x;
        newPosY = transform.position.y;
    }

    public void MovingUp()
    {
        if ()
    }
}
