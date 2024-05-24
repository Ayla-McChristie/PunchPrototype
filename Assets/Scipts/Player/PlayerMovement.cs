using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    PlayerStateMachine ps;
    /*
     Input
     */
    [SerializeField]
    private InputActionReference movementVector, jump;

    private CharacterController charController;
    private Vector3 moveInput;
    private Vector3 activeDashSpeed;
    /*
     Movement
     */
    [Header("Movement")]
    public float speed = 12f;
    Vector3 activeMoveSpeed;
    public Vector3 ActiveMoveSpeed { get { return activeMoveSpeed; } }
    public float acceleration = 2f;

    [Header("Dashing")]
    public float dashDistance = 2f;
    public float dashDuration = .5f;
    public float dashFalloff = 1;
    private bool canDash;
    public float dashCooldown = 2;
    private float dashCooldownTimestamp;

    /*
     Jumping
     */
    [Header("Jumping")]
    public float gravity = -10f;
    public float jumpGrav = -5f;
    public float jumpHeight = 2f;

    private Vector3 vertVelocity;

    /*
     * State
     */

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
        ps = GetComponent<PlayerStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        var delta = movementVector.action.ReadValue<Vector2>();

        moveInput = (
            transform.right * delta.x +
            transform.forward * delta.y
            ).normalized;

        activeMoveSpeed = Vector3.Lerp(activeMoveSpeed, moveInput * speed, acceleration * Time.deltaTime);
     

        if (ps.isGrounded && vertVelocity.y < 0)
        {
            vertVelocity.y = -2;
        }

        if (!ps.isDashing)
        {
            //this is a silly quick fix but if we wanna change the control scheme this will need to be done manually
            if (vertVelocity.y > 0 && Keyboard.current.spaceKey.isPressed)
            {
                vertVelocity.y += jumpGrav * Time.deltaTime;
            }
            else
            {
                vertVelocity.y += gravity * Time.deltaTime;
            }
        }

        //move player by velocities
        charController.Move((activeMoveSpeed + activeDashSpeed + vertVelocity) * Time.deltaTime);

        //move the dash speed to 0
        activeDashSpeed = Vector3.Lerp(activeDashSpeed, Vector3.zero, dashFalloff * Time.deltaTime);

        if (Time.time > dashCooldownTimestamp && !canDash)
        {
            canDash = true;
        }
    }
    public void OnJump(InputAction.CallbackContext callbackContext)
    {     
        if (callbackContext.performed && gameObject.scene.IsValid())
        {
            if (ps.isGrounded)
            {
                vertVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                //vertVelocity.y = jumpHeight;
            }
            //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            Debug.Log("Cant jump");
        }
    }
    public void OnDash(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && canDash)
        {
            if (moveInput != Vector3.zero)
            {
                activeMoveSpeed += moveInput * (dashDistance/dashDuration);
            }
            else
            {
                activeMoveSpeed += transform.forward * (dashDistance/dashDuration);
            }

            vertVelocity = Vector3.zero;
            ps.isDashing = true;
            canDash = false;
            dashCooldownTimestamp = Time.time + dashCooldown;

            /*
             * TODO: View Bob
             */

            Invoke(nameof(ResetDash),dashDuration);
        }
    }

    private void ResetDash()
    {
        ps.isDashing = false;
    }
}
