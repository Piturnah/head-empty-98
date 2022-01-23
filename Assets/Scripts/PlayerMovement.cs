using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Transform model;
    public int movementSpeed;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        model = transform.Find("model");
        anim = model.GetComponent<Animator>();
    }

    void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            float lookAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            model.eulerAngles = Vector3.up * (90 - lookAngle);
        }
        rb.velocity = dir * movementSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        anim.SetBool("running", rb.velocity.magnitude != 0);
    }
}
