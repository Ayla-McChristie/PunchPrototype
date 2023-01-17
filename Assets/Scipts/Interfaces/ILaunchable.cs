using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaunchable
{
    public Rigidbody Rigidbody { get; }
    public float Mass { get; }
    public bool IsBeingLaunched { get; }
    public void UpdateLaunchDirection();
    public void ApplyLaunchForce();
}
