using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
    public float damage = 1;
    public float launchPower = 1;
    public Vector3 launchAngle = new Vector3(0,0,0);
    public bool ExtendJump = false;
    public bool applyLaunch;

    public float hitPauseDuration = .2f;

    private void Start()
    {
        Collider c = GetComponent<Collider>();
        c.isTrigger = true;
    }
    private void OnHit(Collider other)
    {
        //do damage
        IDamageable damagable = (IDamageable)other.GetComponent(typeof(IDamageable));
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
        //apply launch
        ILaunchable launchable = (ILaunchable)other.GetComponent(typeof(ILaunchable));
        if (launchable != null && applyLaunch == true)
        {
            launchable.ApplyLaunchForce(
                (transform.forward * launchAngle.z) +
                (transform.right * launchAngle.x) +
                new Vector3(0, launchAngle.y, 0), 
                launchPower);
        }

        //make object hit face toward the collision
        other.gameObject.transform.LookAt(this.gameObject.transform);
        //camera effects
        //CameraUtility.Instance.ShakeCam();
        CameraUtility.Instance.HitPause(hitPauseDuration);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent(typeof(IDamageable)) != null)
        {
            OnHit(other);
        }
    }
}
