using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pathfinding
{
    public class PinkyTargetPosition : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            float lastInputX = Input.GetAxis("Horizontal");
            float lastInputY = Input.GetAxis("Vertical");

            if (lastInputX != 0 || lastInputY != 0)
            {
                if (lastInputX > 0)
                {
                    transform.localPosition = new Vector3(4.5f, 0f, 0f);
                }
                else if (lastInputX < 0)
                {
                    transform.localPosition = new Vector3(-4.5f, 0f, 0f);
                }

                if (lastInputY > 0)
                {
                    transform.localPosition = new Vector3(0f, 4.5f, 0f);
                }
                else if (lastInputY < 0)
                {
                    transform.localPosition = new Vector3(0f, -4.5f, 0f);
                }
            }
        }
    }
}
