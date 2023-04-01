using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 100;
    public float turnSpeed = 100;
    public float bulletForce = 100;
    float moveInput;
    float turnInput;

    Rigidbody2D playerBody;

    ObjectPools objectPooler;

    public Transform firePoint;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();

        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        // Get Movement Input
        moveInput = Input.GetAxis("Vertical");

        turnInput = Input.GetAxis("Horizontal");

        // Fire Bullets
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    void FixedUpdate()
    {
        // Move and Turn Player
        if (moveInput != 0)
        {
            playerBody.AddForce(transform.up * moveInput * moveSpeed * Time.deltaTime);
        }

        if (turnInput != 0)
        {
            transform.Rotate(Vector3.forward, -turnInput * turnSpeed * Time.deltaTime);
        }
    }

    // Bullet Fire Function
    void FireBullet()
    {
        GameObject bullet = objectPooler.SpawnFromPool(
            "Bullets",
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        Vector2 bulletDirection = firePoint.up;

        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
