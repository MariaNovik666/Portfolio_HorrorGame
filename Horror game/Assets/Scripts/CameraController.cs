using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static float mouseSensitiviti = 100f;

    public Transform Player;

    private float xRotation = 0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update()
    {
        if (GameStates.inventoryIsOpen)
            return;


        float mouseX = Input.GetAxis("Mouse X") * mouseSensitiviti * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitiviti * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}
