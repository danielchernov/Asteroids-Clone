using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkForBounds : MonoBehaviour
{
    ScreenBounds screenBounds;

    void Start()
    {
        if (screenBounds == null)
            screenBounds = GameObject.Find("ScreenBounds").GetComponent<ScreenBounds>();
    }

    void Update()
    {
        // Teleport if Out Of Bounds
        if (screenBounds.isOutOfBounds(transform.position))
        {
            Vector2 newPosition = screenBounds.CalculateWrappedPosition(transform.position);
            transform.position = newPosition;
        }
    }
}
