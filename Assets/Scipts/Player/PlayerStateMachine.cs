using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public bool isGrounded;

    [HideInInspector]
    public bool isDashing;

    [HideInInspector]
    public PlayerState State = PlayerState.Walking;
    public enum PlayerState
    {
        Walking,
        Dashing,
        Airborne
    }

    private void HandleState()
    {
        if (isDashing)
        {
            State = PlayerState.Dashing;
        }

        //on the floor
        if (isGrounded)
        {
            State = PlayerState.Walking;
        }

        //in the air
        else
        {
            State = PlayerState.Airborne;
        }
    }

    public void Update()
    {
        HandleState();

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
}
