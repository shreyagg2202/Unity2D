using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementRestrictor : MonoBehaviour
{
    bool faceUp, faceDown, faceLeft, faceRight;
    Animator parentBodyAnimator;
    // Start is called before the first frame update
    void Start()
    {
        parentBodyAnimator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(faceUp);
        var rotationVector = transform.rotation.eulerAngles;
        EnemyFacing();
        if (faceUp == true)
        {
            transform.position = new Vector3(0, -1);
            rotationVector.z = 90;
        }
        else if (faceDown == true)
        {
            transform.position = new Vector3(0, 1);
            rotationVector.z = 90;
        }
        else if (faceLeft == true)
        {
            transform.position = new Vector3(1, 0);
            rotationVector.z = 0;
        }
        else if (faceRight == true)
        {
            transform.position = new Vector3(-1, 0);
            rotationVector.z = 0;
        }
    }

    private void EnemyFacing()
    {
        Debug.Log("hello");
        if (parentBodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Blinky_Up"))
        {
            Debug.Log("Byee");
            faceUp = true;

        }
        else if (parentBodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Blinky Down"))
        {
            faceDown = true;
        }
        else if (parentBodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Blinky Left"))
        {
            faceLeft = true;
        }
        else if (parentBodyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Blinky Right"))
        {
            faceRight = true;
        }
    }
}
