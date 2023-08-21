using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCursor();
        Swim();
        UseHands();

        RotateBody();
    }

    [SerializeField] float RotateSpeed = 15f;
    Vector3 lookdirection;

    void LookAtCursor()
    {
        if (rb.velocity.magnitude == 0)
        {
        }

    }

    float SpeedMultiplier = 2;
    float acceleration = 0.5f;
    float decceleration = 0.8f;

    Vector3 InitialMousePos;

    Vector3 mouseVelocity;

    float speed;

    bool SwimStarted;


    [SerializeField] Transform LeftArmTarget, RightArmTarget;
    void Swim()
    {
        Camera.main.transform.position = transform.position - Vector3.forward * 5;

        if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
        {
            SwimStarted = true;
            InitialMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookdirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            speed = SpeedMultiplier;
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            SwimStarted = false;
        }

        if (SwimStarted)
        {
            if (Input.GetMouseButton(0) && Input.GetMouseButton(1))
            {
                if ((InitialMousePos - Camera.main.ScreenToWorldPoint(Input.mousePosition)).magnitude < 5f)
                {
                    mouseVelocity = (InitialMousePos - Camera.main.ScreenToWorldPoint(Input.mousePosition)) / Time.deltaTime;

                    LeftArmTarget.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.right * 0.2f ;
                    RightArmTarget.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + transform.right * 0.2f;
                }
            }
        }
        else
        {
            if (speed > 0)
            {
                speed -= decceleration * Time.deltaTime;
            }
            else if (speed < 0)
            {
                speed = 0;
            }
        }

        rb.velocity = mouseVelocity * speed * Time.deltaTime;
    }

    float zrot;

    Quaternion rot;

    void RotateBody()
    {
        

        if (!Input.GetMouseButton(0) || !Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.A))
            {
                zrot += RotateSpeed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 50 * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.D))
            {
                zrot -= RotateSpeed * Time.deltaTime;
                transform.rotation = Quaternion.EulerRotation(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + (zrot * Mathf.PI / 180)));
            }
        }
        else
        {

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, lookdirection), RotateSpeed * Time.deltaTime);
        }
    }

    void UseHands()
    {
        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            LeftArmTarget.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.right * 0.2f;
        }

        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            RightArmTarget.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + transform.right * 0.2f;
        }
    }
}
