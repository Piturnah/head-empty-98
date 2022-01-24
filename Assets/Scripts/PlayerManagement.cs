using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    Camera mainCamera;
    Vector3 relativeCameraPos;

    bool aiming;
    bool aimingPrevFrame;

    Rigidbody rb;
    Transform model;
    int runSpeed = 12;
    int aimSpeed = 9;
    int movementSpeed;
    Animator tanukiAnim;

    public int maxHealth = 3;
    int currentHealth;

    Vector3 target;

    private void Start()
    {
        mainCamera = Camera.main;
        relativeCameraPos = mainCamera.transform.localPosition;

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
            rb.velocity = dir * movementSpeed;
        } else
        {
            Vector3 lookDir = (target - Vector3.Scale(transform.position, (Vector3.one - Vector3.up))).normalized;
            float lookAngle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg;     
            model.eulerAngles = Vector3.up * (90 - lookAngle);
            rb.velocity = (model.transform.forward * dir.z + model.transform.right * dir.x) * movementSpeed;
        }
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
        aimingPrevFrame = aiming;
        aiming = Input.GetKey(KeyCode.Mouse1);

        if (aimingPrevFrame != aiming)
        {
            if (aiming)
            {
                mainCamera.transform.parent = null;
            }
            else
            {
                mainCamera.transform.parent = transform;
                mainCamera.transform.localPosition = relativeCameraPos;
            }
        }

        movementSpeed = (aiming ? aimSpeed : runSpeed);
        tanukiAnim.SetFloat("velocity", rb.velocity.magnitude);
        tanukiAnim.SetBool("aiming", aiming);
        if(currentHealth <= 0)
        {
            Controller.GameLost();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Plane floorPlane = new Plane(Vector3.up, 0);

            float dist;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (floorPlane.Raycast(ray, out dist))
            {
                target = Vector3.Scale(ray.GetPoint(dist), (Vector3.one - Vector3.up));
            }
        }
    }
}
