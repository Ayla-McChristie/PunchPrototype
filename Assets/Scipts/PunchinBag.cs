using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchinBag : MonoBehaviour, IDamageable
{
    ParticleSystem ps;
    public float Health => 10;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerHitBox")
        {
            TakeDamage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "PlayerHitBox")
        {
            TakeDamage();
        }
    }
}
