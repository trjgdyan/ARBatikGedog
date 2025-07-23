using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float zoomSpeed = 2f;
    public float rotationSpeed = 50f;

    void Update()
    {
        Vector3 move = Vector3.zero;

        // WASD movement 
        if (Input.GetKey(KeyCode.W))
            move += transform.forward * zoomSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            move -= transform.forward * zoomSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
            move -= transform.right * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            move += transform.right * moveSpeed * Time.deltaTime;

        // Vertical movement 
        if (Input.GetKey(KeyCode.PageUp))
            move += transform.up * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.PageDown))
            move -= transform.up * moveSpeed * Time.deltaTime;

        transform.position += move;

        // Mouse rotation
        if (Input.GetMouseButton(0)) // button kiri mouse
        {
            float rotationX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotationY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.up, rotationX, Space.World);
            transform.Rotate(Vector3.left, rotationY, Space.Self);
        }

    }
}
