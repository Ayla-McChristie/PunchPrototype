using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;

    bool rightOrLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OnFire(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
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
    }
}
