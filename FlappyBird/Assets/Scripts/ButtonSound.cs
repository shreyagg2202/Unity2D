using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource mySounds;
    public AudioClip buttonHoverSound;
    public AudioClip buttoClickSound;

    public void HoverSound()
    {
        mySounds.PlayOneShot(buttonHoverSound);
    }

    public void ClickSound()
    {
        mySounds.PlayOneShot(buttoClickSound);
    }
}
