using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0;
    public float turnSpeed = 0;

    float moveInput;
    float turnInput;

    Rigidbody2D playerBody;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");

        if (moveInput != 0)
        {
            playerBody.AddForce(transform.up * moveInput * moveSpeed * Time.deltaTime);
        }

        turnInput = Input.GetAxis("Horizontal");

        if (turnInput != 0)
        {
            transform.Rotate(Vector3.forward, -turnInput * turnSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        return;
    }
}
