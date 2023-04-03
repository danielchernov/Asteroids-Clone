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

    public AudioClip[] shootSFX;
    public AudioSource propulsorAudio;
    AudioSource playerAudio;

    public ParticleSystem smokeVFX;

    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerAudio = GetComponent<AudioSource>();

        objectPooler = ObjectPools.Instance;
    }

    void Update()
    {
        // Get Movement Input
        moveInput = Input.GetAxisRaw("Vertical");

        turnInput = Input.GetAxisRaw("Horizontal");

        // Fire Bullets
        if (Input.GetButtonDown("Fire1"))
        {
            FireBullet();
        }
    }

    void FixedUpdate()
    {
        // Move and Turn Player
        if (moveInput == 1)
        {
            playerBody.AddForce(transform.up * moveInput * moveSpeed * Time.deltaTime);

            if (!propulsorAudio.isPlaying)
                propulsorAudio.Play();

            if (smokeVFX.isStopped)
                smokeVFX.Play();
        }
        else
        {
            if (propulsorAudio.isPlaying)
                propulsorAudio.Stop();

            if (smokeVFX.isPlaying)
                smokeVFX.Stop(true, ParticleSystemStopBehavior.StopEmitting);
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

        bulletRb.velocity = Vector2.zero;
        Vector2 bulletDirection = firePoint.up;

        bulletRb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        playerAudio.PlayOneShot(shootSFX[Random.Range(0, shootSFX.Length)], 0.5f);
    }
}
