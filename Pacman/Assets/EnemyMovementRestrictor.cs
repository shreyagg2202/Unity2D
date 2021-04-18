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
        //Get animation from blend tree with the help pf the float values managing it
        Debug.Log("hello");
        if (FindObjectOfType<EnemyAnimation>().yMov >= 0)
        {
            Debug.Log("Byee");
            faceUp = true;

        }
        else if (FindObjectOfType<EnemyAnimation>().yMov <= 0)
        {
            faceDown = true;
        }
        else if (FindObjectOfType<EnemyAnimation>().xMov >= 0)
        {
            faceLeft = true;
        }
        else if (FindObjectOfType<EnemyAnimation>().xMov <= 0)
        {
            faceRight = true;
        }
    }
}
