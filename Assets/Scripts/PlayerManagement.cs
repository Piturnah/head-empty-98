using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    Rigidbody rb;
    Transform model;
    int runSpeed = 12;
    int aimSpeed = 9;
    int movementSpeed;
    Animator tanukiAnim;

    public int maxHealth = 3;
    int currentHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        model = transform.Find("models");
        tanukiAnim = model.Find("model").GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        tanukiAnim.SetFloat("horizontal", dir.x);
        tanukiAnim.SetFloat("vertical", dir.z);
        if (!Input.GetKey(KeyCode.Mouse1))
        {
            float lookAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            model.eulerAngles = Vector3.up * (90 - lookAngle);
        }
        rb.velocity = dir * movementSpeed;
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        movementSpeed = (Input.GetKey(KeyCode.Mouse1) ? aimSpeed : runSpeed);
        tanukiAnim.SetFloat("velocity", rb.velocity.magnitude);
        tanukiAnim.SetBool("aiming", Input.GetKey(KeyCode.Mouse1));
        if(currentHealth <= 0)
        {
            Controller.GameLost();
        }
    }
}
