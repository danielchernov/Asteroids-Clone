using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidsManager : MonoBehaviour
{
    Camera mainCamera;

    private float screenWidth;
    private float screenHeight;

    ObjectPools objectPooler;

    public TextMeshProUGUI timerText;
    int countedTime = 0;

    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        screenWidth = mainCamera.aspect * mainCamera.orthographicSize;
        screenHeight = mainCamera.orthographicSize;

        objectPooler = ObjectPools.Instance;
        countedTime = 0;

        // Start AsteroidSpawner and Counter Coroutines
        StartCoroutine(SpawnAsteroids());
        StartCoroutine(CountSeconds());
    }

    // Calculate Spawn Position
    Vector2 CalculateRandomPosition()
    {
        Vector2 spawnPosition = Vector2.zero;

        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // top
                spawnPosition = new Vector2(Random.Range(-screenWidth, screenWidth), screenHeight);
                break;
            case 1: // right
                spawnPosition = new Vector2(screenWidth, Random.Range(-screenHeight, screenHeight));
                break;
            case 2: // bottom
                spawnPosition = new Vector2(Random.Range(-screenWidth, screenWidth), -screenHeight);
                break;
            case 3: // left
                spawnPosition = new Vector2(-screenWidth, Random.Range(-screenHeight, screenHeight));
                break;
        }

        return spawnPosition;
    }

    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(2f);

        // Spawn Asteroid
        Vector2 spawnHere = CalculateRandomPosition();

        GameObject asteroid = objectPooler.SpawnFromPool(
            "AsteroidsBig",
            spawnHere,
            Quaternion.identity
        );

        // Push Asteroid
        Rigidbody2D asteroidRb = asteroid.GetComponent<Rigidbody2D>();

        Vector2 lookToCenter = (Vector3.zero - asteroid.transform.position).normalized;

        Vector2 asteroidDirection = new Vector2(
            lookToCenter.x + Random.Range(-1f, 1f),
            lookToCenter.y + Random.Range(-1f, 1f)
        );

        asteroidRb.AddForce(asteroidDirection.normalized * Random.Range(1, 6), ForceMode2D.Impulse);

        asteroidRb.AddTorque(Random.Range(0, 100));

        StartCoroutine(SpawnAsteroids());
    }

    // Timer Function
    IEnumerator CountSeconds()
    {
        yield return new WaitForSeconds(1);
        countedTime++;
        timerText.text = countedTime.ToString();
        StartCoroutine(CountSeconds());
    }
}
