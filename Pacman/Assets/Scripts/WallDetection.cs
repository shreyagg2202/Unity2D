using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetection : MonoBehaviour
{
    public Transform originPointDown, endPointDown, originPointUp, endPointUp, originPointLeft, endPointLeft, originPointRight, endPointRight;

    public bool wallDetectDown, wallDetectUp, wallDetectLeft, wallDetectRight;

    public static bool pathOpenDown, pathOpenUp, pathOpenLeft, pathOpenRight;
    private int wallLayer = 1 << 9;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WallDetector();
    }

    void WallDetector()
    {
        Debug.DrawLine(originPointDown.position, endPointDown.position, Color.green);
        Debug.DrawLine(originPointUp.position, endPointUp.position, Color.green);
        Debug.DrawLine(originPointLeft.position, endPointLeft.position, Color.green);
        Debug.DrawLine(originPointRight.position, endPointRight.position, Color.green);
        wallDetectDown = Physics2D.Linecast(originPointDown.position, endPointDown.position, wallLayer);
        wallDetectDown = Physics2D.Linecast(originPointUp.position, endPointUp.position, wallLayer);
        wallDetectDown = Physics2D.Linecast(originPointLeft.position, endPointLeft.position, wallLayer);
        wallDetectDown = Physics2D.Linecast(originPointRight.position, endPointRight.position, wallLayer);
        CheckResults();
    }

    private void CheckResults()
    {
        if (wallDetectDown == true)
        {
            pathOpenDown = false;
        }
        else { pathOpenDown = true; }

        if (wallDetectUp == true)
        {
            pathOpenUp = false;
        }
        else { pathOpenUp = true; }

        if (wallDetectLeft == true)
        {
            pathOpenLeft = false;
        }
        else { pathOpenLeft = true; }

        if (wallDetectRight == true)
        {
            pathOpenRight = false;
        }
        else { pathOpenRight = true; }
    }
}
