using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Asteroid : MonoBehaviour
{
    ObjectPools objectPooler;

    public bool isBig = false;
    TextMeshProUGUI scoreText;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        scoreText = GameObject
            .Find("score")
            .transform.GetChild(0)
            .gameObject.GetComponent<TextMeshProUGUI>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.SetActive(false);

            int amountToSpawn;

            // Set Asteroid Spawn Amount and Score
            if (isBig)
            {
                amountToSpawn = Random.Range(2, 4);
                scoreText.text = (System.Convert.ToInt32(scoreText.text) + 100).ToString();
            }
            else
            {
                amountToSpawn = 0;
                scoreText.text = (System.Convert.ToInt32(scoreText.text) + 50).ToString();
            }

            // Spawn Small Asteroids
            for (int i = 0; i < amountToSpawn; i++)
            {
                GameObject asteroid = objectPooler.SpawnFromPool(
                    "AsteroidsSmall",
                    transform.position,
                    Quaternion.identity
                );

                Rigidbody2D asteroidRb = asteroid.GetComponent<Rigidbody2D>();

                Vector2 asteroidDirection = new Vector2(
                    Random.Range(-100f, 100f),
                    Random.Range(-100f, 100f)
                );

                asteroidRb.AddForce(
                    asteroidDirection.normalized * Random.Range(2, 10),
                    ForceMode2D.Impulse
                );

                asteroidRb.AddTorque(Random.Range(0, 50));
            }

            gameObject.SetActive(false);
        }
    }
}
