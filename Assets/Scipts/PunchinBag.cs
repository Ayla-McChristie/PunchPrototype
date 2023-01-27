using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchinBag : MonoBehaviour, IDamageable, ILaunchable
{
    bool isGrounded = false;
    public Transform jawnCheck;
    Vector3 groundCheckSize = new Vector3(.2f, .2f, .2f);
    [SerializeField]
    LayerMask groundMask;

    /*
     * IDamagable
     */
    ParticleSystem ps;
    public float Health => 10;

    /*
     * ILaunchable
     */
    [SerializeField]
    float gravity = 2;
    [SerializeField]
    float launchForceFallOff = 1;
    [SerializeField]
    public float Mass { get; private set; }   
    public bool IsBeingLaunched { get; private set; }
    public Vector3 ActiveVelocity { get; private set; }

    public CharacterController CharCont => GetComponent<CharacterController>();

    public void Update()
    {
        isGrounded = CharCont.isGrounded;

        ApplyGravity(isGrounded);

        CharCont.Move((ActiveVelocity) * Time.deltaTime);

        ActiveVelocity = Vector3.Lerp(ActiveVelocity, Vector3.zero, launchForceFallOff * Time.deltaTime);
    }
    private void ApplyGravity(bool isOnGround)
    {
        if (isOnGround)
        {
            ActiveVelocity += new Vector3(0, -2, 0);
        }
        else
        {
            ActiveVelocity +=new Vector3(0, gravity * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerHitBox")
        {
            //TakeDamage();
        }
    }

    public void TakeDamage()
    {
        TakeDamage(1f);
    }

    public void TakeDamage(float damageAmount)
    {
        PlayHitEffect();
    }
    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void PlayHitEffect()
    {
        ps.Clear();
        ps.Stop();
        ps.Play();
    }

    public void UpdateLaunchDirection()
    {
        throw new System.NotImplementedException();
    }

    public void ApplyLaunchForce(Vector3 angle, float magnitude)
    {
        ActiveVelocity = angle.normalized * magnitude;
    }
}
