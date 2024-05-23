using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ParticleSystem))]
public class BasicHitable : MonoBehaviour, IDamageable
{
    /*
     * IDamagable
     */
    ParticleSystem ps;
    public float Health { get; private set; }

    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }
    public void TakeDamage()
    {
        TakeDamage(1f);
    }

    public void TakeDamage(float damageAmount)
    {
        //Health = Health-damageAmount;
        PlayHitEffect();
    }

    void PlayHitEffect()
    {
        ps.Clear();
        ps.Stop();
        ps.Play();
    }
}
