using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public int movementSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Move()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        rb.velocity = input.normalized * movementSpeed;
        //transform.Translate(input.normalized * movementSpeed * Time.deltaTime, Space.World);
    }

    private void FixedUpdate()
    {
        Move();
    }
}
