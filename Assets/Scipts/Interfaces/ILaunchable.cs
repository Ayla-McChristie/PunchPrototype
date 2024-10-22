using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILaunchable
{
    public CharacterController CharCont { get; }
    public float Mass { get; }
    public bool IsLaunched { get; }
    public Vector3 ActiveVelocity { get; }
    public void UpdateLaunchDirection();
    public void ApplyLaunchForce(Vector3 angle, float magnitude);
}
