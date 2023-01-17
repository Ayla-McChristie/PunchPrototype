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
        //may need to use custom physics for the desired bounces
        other.attachedRigidbody.AddForce(launchAngle * launchPower);
        //do damage

        //apply launch

        //camera effects
        CameraUtility.Instance.ShakeCam();
        CameraUtility.Instance.HitPause(hitPauseDuration);
    }
}
