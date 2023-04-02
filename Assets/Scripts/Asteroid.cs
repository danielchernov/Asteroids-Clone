using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Asteroid : MonoBehaviour
{
    ObjectPools objectPooler;

    public bool isBig = false;
    public bool isMedium = false;

    GameObject thingToSpawn;
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

            // Set Asteroid Spawn Amount and Score
            if (isBig)
            {
                SpawnAsteroid("AsteroidsMedium", Random.Range(1, 3));
                scoreText.text = (System.Convert.ToInt32(scoreText.text) + 100).ToString();
            }
            else if (isMedium)
            {
                SpawnAsteroid("AsteroidsSmall", Random.Range(2, 3));
                scoreText.text = (System.Convert.ToInt32(scoreText.text) + 50).ToString();
            }
            else
            {
                scoreText.text = (System.Convert.ToInt32(scoreText.text) + 25).ToString();
            }

            gameObject.SetActive(false);
        }
    }

    // Spawn Smaller Asteroids
    void SpawnAsteroid(string thingToSpawn, int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject asteroid = objectPooler.SpawnFromPool(
                thingToSpawn,
                transform.position,
                Quaternion.identity
            );

            Rigidbody2D asteroidRb = asteroid.GetComponent<Rigidbody2D>();

            Vector2 asteroidDirection = new Vector2(
                Random.Range(-100f, 100f),
                Random.Range(-100f, 100f)
            );

            asteroidRb.AddForce(
                asteroidDirection.normalized * Random.Range(1, 6),
                ForceMode2D.Impulse
            );

            asteroidRb.AddTorque(Random.Range(0, 20));
        }
    }
}
