using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float Health { get; }
    void TakeDamage();
    void TakeDamage(float damageAmount);
}
