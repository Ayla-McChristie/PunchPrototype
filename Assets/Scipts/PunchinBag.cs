using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchinBag : MonoBehaviour, IDamageable, ILaunchable
{
    /*
     * IDamagable
     */
    ParticleSystem ps;
    public float Health => 10;

    /*
     * ILaunchable
     */
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();

    [SerializeField]
    public float Mass { get; private set; }   

    public bool IsBeingLaunched => throw new System.NotImplementedException();

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

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == "PlayerHitBox")
    //    {
    //        TakeDamage();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "PlayerHitBox")
        {
            TakeDamage();
        }
    }

    public void UpdateLaunchDirection()
    {
        throw new System.NotImplementedException();
    }

    public void ApplyLaunchForce()
    {
        throw new System.NotImplementedException();
    }
}
