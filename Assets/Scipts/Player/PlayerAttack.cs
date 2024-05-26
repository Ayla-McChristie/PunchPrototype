using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;

    private Animator anim;

    bool rightOrLeft = true;

    /*
     * Charge attack
     */
    [SerializeField]
    private float chargeAttackRate = .1f;
    private float chargeAttackCurrentCharge = 0f;
    private bool isCharging;

    /*
     * Rapid attack
     */
    [SerializeField]
    private float rapidAttackWindow = .2f;
    [SerializeField]
    private int numOfClicks = 3;
    private float rapidAttackCooldown;
    private bool isRapidAttacking;

    /*
     * Debug 
     */
    public GameObject textObject;
    private TextMeshProUGUI debugText;
    public Image chargeWheel;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        InputAction action = new InputAction();
        debugText = textObject.GetComponent<TextMeshProUGUI>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }
    private void Update()
    {
        if (isCharging && chargeAttackCurrentCharge < 1)
        {
            Debug.Log("Charge is Increasing!!");
            chargeAttackCurrentCharge += chargeAttackRate*Time.deltaTime;
            Debug.Log("Charge Amount: " + chargeAttackCurrentCharge);

            anim.SetFloat("ChargeAmount", chargeAttackCurrentCharge);
        }

        if(chargeWheel != null)
        {
            chargeWheel.fillAmount = chargeAttackCurrentCharge;
        }
    }
    public void OnFire(InputAction.CallbackContext callbackContext)
    {
        
        if (callbackContext.performed)
        {
            if (callbackContext.interaction is HoldInteraction)
            {
                isCharging = true;
                anim.SetBool("IsCharging", true);

                if (debugText != null)
                {                    
                    debugText.text = "Attack State: Charge Punch Started";
                }
            }
            else
            {
                if (callbackContext.interaction is TapInteraction && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
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
            }
        }
      
        if (callbackContext.canceled)
        {
            if (callbackContext.interaction is HoldInteraction)
            {
                chargeAttackCurrentCharge = 0f;
                isCharging = false;
                anim.SetBool("IsCharging", false);

                if (debugText != null)
                {
                    debugText.text = "Attack State: Charge Punch Cancelled";
                }         
            }
        }
    }
}
