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
        EnemyFacing();
        if (faceUp == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector3(0, -0.9f, 0);
        }
        else if (faceDown == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector3(0, 0.9f, 0);
        }
        else if (faceLeft == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.localPosition = new Vector3(0.9f, 0);
        }
        else if (faceRight == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            transform.localPosition = new Vector3(-0.9f, 0);
        }
    }

    private void EnemyFacing()
    {
        //Get animation from blend tree with the help pf the float values managing it
        if (FindObjectOfType<EnemyAnimation>().yMov >= 0.1)
        {
            Debug.Log("face Up");
            faceUp = true;
            faceDown = false;
            faceLeft = false;
            faceRight = false;

        }
        else if (FindObjectOfType<EnemyAnimation>().yMov <= -0.1)
        {
            Debug.Log("face Down");
            faceUp = false;
            faceDown = true;
            faceLeft = false;
            faceRight = false;
        }
        else if (FindObjectOfType<EnemyAnimation>().xMov >= 0.1)
        {
            Debug.Log("face Right");
            faceUp = false;
            faceDown = false;
            faceLeft = false;
            faceRight = true;
        }
        else if (FindObjectOfType<EnemyAnimation>().xMov <= -0.1)
        {
            Debug.Log("face Left");
            faceUp = false;
            faceDown = false;
            faceLeft = true;
            faceRight = false;
        }
    }
}
