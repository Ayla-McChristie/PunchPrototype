using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerAttack : MonoBehaviour
{
    public GameObject textObject;
    private TextMeshProUGUI debugText;
    private Animator anim;

    bool rightOrLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InputAction action = new InputAction();
        debugText = textObject.GetComponent<TextMeshProUGUI>();
    }
    public void OnFire(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (debugText != null)
            {
                debugText.text = "Attack State: Regular Punch";
            }
            Debug.Log("punched!!");
            if (rightOrLeft)
            {
                anim.Play("RightPunch");          
            }
            else
            {
                anim.Play("LeftPunch");
            }
            rightOrLeft = !rightOrLeft;
        }
        if (callbackContext.performed)
        {
            if (callbackContext.interaction is HoldInteraction)
            {
                if (debugText != null)
                {
                    debugText.text = "Attack State: Charge Punch Started";
                }
            }
            else if (callbackContext.interaction is MultiTapInteraction)
            {
                if (debugText != null)
                {
                    debugText.text = "Attack State: Ora Ora!!!";
                }
            }
        }
      
        if (callbackContext.canceled)
        {
            if (debugText != null)
            {
                debugText.text = "Attack State: Charge Punch Cancelled";
            }
        }
    }
}
