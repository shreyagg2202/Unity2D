using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2048 : MonoBehaviour
{
    public Cell2048 right;
    public Cell2048 left;
    public Cell2048 down;
    public Cell2048 up;

    public Fill2048 fill;

    private void OnEnable()
    {
        GameController.slide += OnSlide; 
    }

    private void OnDisable()
    {
        GameController.slide -= OnSlide;
    }

    private void OnSlide(string whatWasSent)
    {
        Debug.Log(whatWasSent);
        if (whatWasSent == "w")
        {
            if (up != null)
                return;
            Cell2048 currentCell = this;
            SlideUp(currentCell);
        }
        if (whatWasSent == "d")
        {
            if (right != null)
                return;
            Cell2048 currentCell = this;
            SlideRight(currentCell);
        }
        if (whatWasSent == "s")
        {
            if (down != null)
                return;
            Cell2048 currentCell = this;
            SlideDown(currentCell);
        }
        if (whatWasSent == "a")
        {
            if (left != null)
                return;
            Cell2048 currentCell = this;
            SlideLeft(currentCell);
        }
    }

    void SlideUp(Cell2048 currentCell)
    {
        if (currentCell.down == null)
            return;

        Debug.Log(currentCell.gameObject);
        if (currentCell.fill != null)
        {
            Cell2048 nextCell = currentCell.down;
            while(nextCell.down != null && nextCell.fill == null)
            {
                nextCell = nextCell.down;
            }
            if (nextCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Doubled();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else
                {
                    Debug.Log("!doubled");
                    nextCell.fill.transform.parent = currentCell.down.transform;
                    currentCell.down.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cell2048 nextCell = currentCell.down;
            while (nextCell.down != null && nextCell.fill == null)
            {
                nextCell = nextCell.down;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideUp(currentCell);
                Debug.Log("Slide to Empty");
            }
        }

        if (currentCell.down == null)
            return;
        SlideUp(currentCell.down);
    }

    void SlideRight(Cell2048 currentCell)
    {
        if (currentCell.left == null)
            return;

        Debug.Log(currentCell.gameObject);
        if (currentCell.fill != null)
        {
            Cell2048 nextCell = currentCell.left;
            while(nextCell.left != null && nextCell.fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Doubled();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else
                {
                    Debug.Log("!doubled");
                    nextCell.fill.transform.parent = currentCell.left.transform;
                    currentCell.left.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cell2048 nextCell = currentCell.left;
            while (nextCell.left != null && nextCell.fill == null)
            {
                nextCell = nextCell.left;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideRight(currentCell);
                Debug.Log("Slide to Empty");
            }
        }

        if (currentCell.left == null)
            return;
        SlideRight(currentCell.left);
    }

    void SlideDown(Cell2048 currentCell)
    {
        if (currentCell.up == null)
            return;

        Debug.Log(currentCell.gameObject);
        if (currentCell.fill != null)
        {
            Cell2048 nextCell = currentCell.up;
            while (nextCell.up != null && nextCell.fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Doubled();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else
                {
                    Debug.Log("!doubled");
                    nextCell.fill.transform.parent = currentCell.up.transform;
                    currentCell.up.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cell2048 nextCell = currentCell.up;
            while (nextCell.up != null && nextCell.fill == null)
            {
                nextCell = nextCell.up;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideDown(currentCell);
                Debug.Log("Slide to Empty");
            }
        }

        if (currentCell.up == null)
            return;
        SlideDown(currentCell.up);
    }

    void SlideLeft(Cell2048 currentCell)
    {
        if (currentCell.right == null)
            return;

        Debug.Log(currentCell.gameObject);
        if (currentCell.fill != null)
        {
            Cell2048 nextCell = currentCell.right;
            while (nextCell.right != null && nextCell.fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell.fill != null)
            {
                if (currentCell.fill.value == nextCell.fill.value)
                {
                    nextCell.fill.Doubled();
                    nextCell.fill.transform.parent = currentCell.transform;
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                }
                else
                {
                    Debug.Log("!doubled");
                    nextCell.fill.transform.parent = currentCell.right.transform;
                    currentCell.right.fill = nextCell.fill;
                    nextCell.fill = null;
                }
            }
        }
        else
        {
            Cell2048 nextCell = currentCell.right;
            while (nextCell.right != null && nextCell.fill == null)
            {
                nextCell = nextCell.right;
            }
            if (nextCell.fill != null)
            {
                nextCell.fill.transform.parent = currentCell.transform;
                currentCell.fill = nextCell.fill;
                nextCell.fill = null;
                SlideLeft(currentCell);
                Debug.Log("Slide to Empty");
            }
        }

        if (currentCell.right == null)
            return;
        SlideLeft(currentCell.right);
    }
}


