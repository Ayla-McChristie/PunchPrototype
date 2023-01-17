using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepHandsWithCamRot : MonoBehaviour
{
    private Camera mainCam;
    public float rate = 2f;
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, mainCam.transform.rotation, rate * Time.deltaTime);
    }
}
