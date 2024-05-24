using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class CameraUtility : MonoBehaviour
{
    public static CameraUtility Instance { get; private set; }
    CinemachineVirtualCamera vcam;
    /*
     * Hit Pause
     */
    bool waiting;

    /*
     * Camera Shake
     */
    float shakeTimer;
    float shakeTimerTotal;
    float startingIntesity;

    /*
     * ViewBob
     */
    float viewBobRate = 1f;
    private bool bobbingToTarget = false;
    private Vector3 bobbingTarget;
    private Vector3 cameraRoot;

    private void Awake()
    {
        Instance = this;
        vcam = GetComponent<CinemachineVirtualCamera>();
        cameraRoot = vcam.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //ResetScreenShake();
        UpdateViewBob();
    }
    /*
     * Hit Pause
     */
    public void HitPause()
    {
        HitPause(.2f);
    }
    public void HitPause(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
    /*
     * Camera Shake
     */
    public void ShakeCam()
    {
        ShakeCam(1, 1);
    }
    public void ShakeCam(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlin.m_AmplitudeGain = intensity;
        startingIntesity = intensity;

        shakeTimer = time;
        shakeTimerTotal = time;
    }
    private void ResetScreenShake()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.unscaledDeltaTime;
            CinemachineBasicMultiChannelPerlin perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            if (shakeTimer <= 0)
            {
                perlin.m_AmplitudeGain = 0f;
            }

            //perlin.m_AmplitudeGain = Mathf.Lerp(startingIntesity, 0f, (1 -(shakeTimer / shakeTimerTotal)));
        }
        else if (vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain > 0)
        {
            vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        }
    }
    /*
     * ViewBob UNSTABLE!!!!!
     */

    public void ViewBob()
    {
        ViewBob();
    }
    public void ViewBob(float depth)
    {
        bobbingToTarget = true;
        bobbingTarget= this.transform.position+new Vector3 (0f, depth, 0f);
        bobbingToTarget = true;
    }

    private void UpdateViewBob()
    {
        if (bobbingToTarget)
        {
            vcam.transform.localPosition = Vector3.Lerp(vcam.transform.position, bobbingTarget, viewBobRate); 
        }
        else
        {
            vcam.transform.localPosition = Vector3.Lerp(vcam.transform.position, cameraRoot, viewBobRate);
        }

        if (Vector3.Distance(cameraRoot, vcam.transform.position)<.05 && bobbingToTarget == true)
        {
            bobbingToTarget= false;
        }
    }
}