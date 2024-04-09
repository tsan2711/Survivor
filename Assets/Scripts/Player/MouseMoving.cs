using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMoving : MonoBehaviour
{
    [SerializeField] private float mouseSensitive = 400f;
    private float xRotate;
    private float yRotate;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitive;
            float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitive;


            xRotate -= mouseY;
            xRotate = Mathf.Clamp(xRotate, -75f, 75f);

            yRotate += mouseX;

            transform.localRotation = Quaternion.Euler(xRotate, yRotate, 0f);


        }



    }
}
