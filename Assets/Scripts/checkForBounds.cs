using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkForBounds : MonoBehaviour
{
    ScreenBounds screenBounds;

    bool waitedASecond = true;

    void Start()
    {
        if (screenBounds == null)
            screenBounds = GameObject.Find("ScreenBounds").GetComponent<ScreenBounds>();
    }

    void Update()
    {
        // Teleport if Out Of Bounds
        if (waitedASecond && screenBounds.isOutOfBounds(transform.position))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(transform.position);
            transform.position = newPosition;

            waitedASecond = false;
            StartCoroutine(WaitAndUnlock());
        }
    }

    // Teleport limiter - Workaround for Asteroid Glitch
    IEnumerator WaitAndUnlock()
    {
        yield return new WaitForSeconds(0.5f);
        waitedASecond = true;
    }
}
