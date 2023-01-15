using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    /*
     Input
     */
    [SerializeField]
    private InputActionReference movementVector, jump;

    /*
     Movement
     */
    public float speed = 12f;
    Vector3 activeMoveSpeed;
    public float acceleration = 2f;
    public float gravity = -10f;
    public float jumpGrav = -5f;
    public float jumpHeight = 2f;

    private CharacterController charController;

    /*
     Jumping
     */

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 vertVelocity;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        bool jumpPressed = false;

        var delta = movementVector.action.ReadValue<Vector2>();

        Vector3 moveInput = (
            transform.right * delta.x +
            transform.forward * delta.y
            ).normalized;

        activeMoveSpeed = Vector3.Lerp(activeMoveSpeed, moveInput * speed, acceleration * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log("grounded: " + isGrounded);

        if (isGrounded && vertVelocity.y < 0)
        {
            vertVelocity.y = -2;
        }

        //this is a silly quick fix but if we wanna change the control scheme this will need to be done manually
        if (vertVelocity.y > 0 && Keyboard.current.spaceKey.isPressed)
        {
            vertVelocity.y += jumpGrav * Time.deltaTime;
        }
        else
        {
            vertVelocity.y += gravity * Time.deltaTime;
        }


        charController.Move((activeMoveSpeed + vertVelocity) * Time.deltaTime);

    }
    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && gameObject.scene.IsValid())
        {
            if (isGrounded)
            {
                //vertVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                vertVelocity.y = jumpHeight;
            }
            //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else
        {
            Debug.Log("Cant jump");
        }
    }
}
