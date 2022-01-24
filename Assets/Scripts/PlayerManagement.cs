using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    Camera mainCamera;
    Vector3 relativeCameraPos;

    bool aiming;

    public static Vector3 playerPositionLastFrame;

    Rigidbody rb;
    Transform model;
    int runSpeed = 12;
    int aimSpeed = 9;
    int movementSpeed;
    Animator tanukiAnim;
    Animator backpackAnim;

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
        backpackAnim = model.Find("backpack").GetComponent<Animator>();

        currentHealth = maxHealth;
    }

    void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        if(dir != Vector3.zero)
        {
            AudioManager.Play("Running");
        }
        else if (AudioManager.IsPlaying("Running"))
        {
            AudioManager.Stop("Running");
        }

        if (!aiming && !dir.Equals(Vector3.zero))
        {
            tanukiAnim.SetFloat("horizontal", dir.x);
            tanukiAnim.SetFloat("vertical", dir.z);
            float lookAngle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
            model.eulerAngles = Vector3.up * (90 - lookAngle);
        }
        else if (aiming)
        {
            Vector3 lookDir = (target - Vector3.Scale(transform.position, (Vector3.one - Vector3.up))).normalized;
            float lookAngle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg;
            float angleDiff = Vector3.SignedAngle(lookDir, dir, Vector3.up);
            Vector3 relativeVector = Quaternion.AngleAxis(angleDiff, Vector3.up) * Vector3.forward;
            tanukiAnim.SetFloat("horizontal", relativeVector.x);
            tanukiAnim.SetFloat("vertical", relativeVector.z);
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
        aiming = Input.GetKey(KeyCode.Mouse1);
        backpackAnim.SetBool("rifle", aiming);

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

        playerPositionLastFrame = transform.position;
    }
}
