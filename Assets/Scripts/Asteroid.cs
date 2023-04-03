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
    GameObject gameOverMenu;
    TextMeshProUGUI scoreText;

    public AudioClip[] explodeSFX;
    public AudioClip[] dieSFX;
    AudioSource sfxAudio;
    public GameObject dieVFX;

    void Start()
    {
        objectPooler = ObjectPools.Instance;
        scoreText = GameObject
            .Find("score")
            .transform.GetChild(0)
            .gameObject.GetComponent<TextMeshProUGUI>();

        gameOverMenu = GameObject.Find("GameOverMenu").transform.GetChild(0).gameObject;

        sfxAudio = GameObject.Find("SFX Source").GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            HitBullet(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(KillPlayer(collision.gameObject));
        }
    }

    // When Bullet Hits
    void HitBullet(GameObject bullet)
    {
        bullet.SetActive(false);

        // Set Asteroid Spawn Amount and Score
        if (isBig)
        {
            SpawnAsteroid("AsteroidsMedium", Random.Range(1, 3));
            scoreText.text = (System.Convert.ToInt32(scoreText.text) + 25).ToString();
        }
        else if (isMedium)
        {
            SpawnAsteroid("AsteroidsSmall", Random.Range(2, 3));
            scoreText.text = (System.Convert.ToInt32(scoreText.text) + 10).ToString();
        }
        else
        {
            scoreText.text = (System.Convert.ToInt32(scoreText.text) + 5).ToString();
        }

        sfxAudio.PlayOneShot(explodeSFX[Random.Range(0, explodeSFX.Length)], 0.5f);

        gameObject.SetActive(false);
    }

    // When Hits Player
    IEnumerator KillPlayer(GameObject player)
    {
        sfxAudio.PlayOneShot(dieSFX[Random.Range(0, dieSFX.Length)], 1.5f);
        Instantiate(dieVFX, player.transform.position, player.transform.rotation);

        player.SetActive(false);
        yield return new WaitForSeconds(1);
        gameOverMenu.SetActive(true);
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
