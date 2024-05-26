using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLaunchable : BasicHitable, ILaunchable
{
    /*
     * ILaunchable
     */
    [SerializeField]
    float gravity = -30;
    [SerializeField]
    float launchForceFallOff = .2f;
    [SerializeField]
    public float Mass { get; private set; }   
    public bool IsLaunched { get; private set; }
    public Vector3 ActiveVelocity { get; private set; }

    float launchBounceSpeedReduction = .3f;

    /*
     * Launch Stun
     */
    private bool isLaunchStunned;
    private float launchStun = 1f;
    private float launchStunTimeStamp;

    /*
     * Ground check
     */
    bool isGrounded = false;
    public Transform groundCheck;
    float groundDistance;
    [SerializeField]
    LayerMask groundMask;

    /*
     * Visual Feedback
     */
    [SerializeField]
    public Material launchMat;
    private Material defaultMat;
    private Renderer renderer;

    /*
     * Launch Rotation
     */
    private float rotationScale = 60.0f;

    public CharacterController CharCont => GetComponent<CharacterController>();
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        defaultMat = renderer.material;
        groundDistance = (GetComponent<CapsuleCollider>().height) + .1f;
    }

    public void Update()
    {
        //funny spin effect
        ApplyLaunchSpin();

        //only check grounded if not in launch stun
        if (!isLaunchStunned)
        {
            GroundCheck();
        }

        //checks to see if launch stunn has passed
        if (Time.time > launchStunTimeStamp && isLaunchStunned)
        {
            isLaunchStunned = false;
        }

        CharCont.Move((ActiveVelocity) * Time.deltaTime);
        HandleVelocityReduction();       

        if (isGrounded && renderer.material != defaultMat)
        {
            renderer.material = defaultMat;
        }
    }
    //Launch bounce calculations
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (IsLaunched)
        {
            float velocityMagnitude =ActiveVelocity.magnitude -  (ActiveVelocity.magnitude*launchBounceSpeedReduction);
            Vector3 collisionPerpendicular = Vector3.Reflect(ActiveVelocity.normalized,hit.normal);
            ActiveVelocity = collisionPerpendicular*velocityMagnitude;
        }

    }
    private void ApplyGravity(bool isOnGround)
    {
        if (isOnGround)
        {
            ActiveVelocity += new Vector3(0, -2*Time.deltaTime, 0);
        }
        else
        {
            ActiveVelocity += new Vector3(0, gravity*Time.deltaTime, 0);
        }
    } 
    
    private void ApplyLaunchSpin()
    {
        if (IsLaunched)
        {
            Debug.Log("Active Velocity: " + ActiveVelocity.magnitude);
            Debug.Log("Active Velocity: " + ActiveVelocity.ToString());
            //if (ActiveVelocity.magnitude < 15)
            //{
                this.gameObject.transform.Rotate(new Vector3(rotationScale * ActiveVelocity.magnitude * Time.deltaTime, 0, 0), Space.Self);
            //}
            //else
            //{
            //    this.gameObject.transform.Rotate(new Vector3(0, rotationScale * ActiveVelocity.magnitude * Time.deltaTime, 0), Space.Self);
            //}
                
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y, 0));
        }
    }

    //reduces horizontal launch forces and applies gravity
    private void HandleVelocityReduction()
    {
        float velX = ActiveVelocity.x;
        float velZ = ActiveVelocity.z;

        if (velX > 0)
        {
            velX -= velX * launchForceFallOff * Time.deltaTime;
        }
        else
        {
            velX -= velX * launchForceFallOff * Time.deltaTime;
        }

        if (velZ > 0)
        {
            velZ -= velZ * launchForceFallOff * Time.deltaTime;
        }
        else
        {
            velZ -= velZ * launchForceFallOff * Time.deltaTime;
        }

        ActiveVelocity = new Vector3(velX, ActiveVelocity.y, velZ);

        ApplyGravity(isGrounded);
    }

    public void UpdateLaunchDirection()
    {
        throw new System.NotImplementedException();
    }

    public void ApplyLaunchForce(Vector3 angle, float magnitude)
    {
        IsLaunched = true;
        isLaunchStunned = true;
        launchStunTimeStamp = Time.time + launchStun;
        isGrounded = false;
        ActiveVelocity = angle.normalized * magnitude;

        Debug.Log("Launched!");

        if (launchMat != null)
        {
            renderer.material = launchMat;
        }
    }
    private bool GroundCheck()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * groundDistance, Color.red,.1f,true);
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundDistance, groundMask))
        {
            isGrounded = true;
            IsLaunched = false;
        }
        else
        {
            isGrounded = false;
        }
        //Debug.Log("is grounded: " + isGrounded);
        return isGrounded;
    }


}
