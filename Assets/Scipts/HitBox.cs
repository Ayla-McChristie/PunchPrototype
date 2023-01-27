using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
    public float damage = 1;
    public float launchPower = 1;
    public Vector3 launchAngle = new Vector3(0,0,0);

    public float hitPauseDuration = .2f;

    private void Start()
    {
        Collider c = GetComponent<Collider>();
        c.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //do damage
        if (other.GetComponent(typeof(IDamageable)) != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
        //apply launch
        if (other.GetComponent(typeof(ILaunchable)) != null)
        {
            other.GetComponent<ILaunchable>().ApplyLaunchForce(
                (transform.forward * launchAngle.z) +
                (transform.right * launchAngle.x) +
                new Vector3(0, launchAngle.y, 0), launchPower);
        }
        //camera effects
        //CameraUtility.Instance.ShakeCam();
        CameraUtility.Instance.HitPause(hitPauseDuration);
    }
}
