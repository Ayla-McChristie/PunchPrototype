using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [SerializeField]
    public float sensX, sensY;

    public Transform camera;

    float xRot, yRot;

    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        var lookDelta = Mouse.current.delta.ReadValue();

        //float mouseX = lookDelta.x * Time.deltaTime * sensX;
        //float mouseY = lookDelta.y * Time.deltaTime * sensY;

        yRot += lookDelta.x * Time.deltaTime * sensX;

        xRot -= lookDelta.y * Time.deltaTime * sensY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        this.transform.rotation = Quaternion.Euler(0, yRot, 0);
        camera.transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }
}
