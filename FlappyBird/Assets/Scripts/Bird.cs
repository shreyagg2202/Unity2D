using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class Bird : MonoBehaviour
{
    private const float JUMP_AMOUNT = 100f;

    private static Bird instance;

    public static Bird Getinstance()
    {
        return instance;
    }
    public event EventHandler OnDied;
    public event EventHandler OnStartedPlaying;

    private Rigidbody2D birdRigidBody2D;
    private State state;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead,
    }
    private void Awake()
    {
        instance = this;
        birdRigidBody2D = GetComponent<Rigidbody2D>();
        birdRigidBody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    state = State.Playing;
                    birdRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    if (OnStartedPlaying != null)
                    {
                        OnStartedPlaying(this, EventArgs.Empty);
                    }
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                break;
            case State.Dead:
                break;
        }
    }

    private void Jump()
    {
        birdRigidBody2D.velocity = Vector2.up * JUMP_AMOUNT;
        SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        birdRigidBody2D.bodyType = RigidbodyType2D.Static;
        SoundManager.PlaySound(SoundManager.Sound.Lose);  
        if (OnDied != null)
        {
            OnDied(this, EventArgs.Empty);
        }
    }
}
