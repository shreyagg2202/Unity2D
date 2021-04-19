using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementRestrictor : MonoBehaviour
{
    bool faceUp, faceDown, faceLeft, faceRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(faceUp);
        EnemyFacing();
        if (faceUp == true)
        {
            transform.localPosition = new Vector3(0, -1);
        }
        else if (faceDown == true)
        {
            transform.localPosition = new Vector3(0, 1);
        }
        else if (faceLeft == true)
        {
            transform.localPosition = new Vector3(1, 0);
        }
        else if (faceRight == true)
        {
            transform.localPosition = new Vector3(-1, 0);
        }
    }

    private void EnemyFacing()
    {
        //Get animation from blend tree with the help pf the float values managing it
        if (FindObjectOfType<EnemyAnimation>().yMov >= 0)
        {
            faceUp = true;
            faceDown = false;
            faceLeft = false;
            faceRight = false;

        }
        else if (FindObjectOfType<EnemyAnimation>().yMov <= 0)
        {
            faceUp = false;
            faceDown = true;
            faceLeft = false;
            faceRight = false;
        }
        else if (FindObjectOfType<EnemyAnimation>().xMov >= 0)
        {
            faceUp = false;
            faceDown = false;
            faceLeft = true;
            faceRight = false;
        }
        else if (FindObjectOfType<EnemyAnimation>().xMov <= 0)
        {
            faceUp = false;
            faceDown = false;
            faceLeft = false;
            faceRight = true;
        }
    }
}
