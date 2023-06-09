using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenBounds : MonoBehaviour
{
    public Camera mainCamera;
    BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        UpdateBoundsSize();
    }

    //Set Screen Collider Size
    public void UpdateBoundsSize()
    {
        float ySize = mainCamera.orthographicSize * 2;
        Vector2 colliderSize = new Vector2(ySize * mainCamera.aspect + 3, ySize + 3);
        boxCollider.size = colliderSize;
    }

    // Check if Out Of Bounds
    public bool isOutOfBounds(Vector3 worldPosition)
    {
        return Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x)
            || Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider.bounds.min.y);
    }

    // Calculate Wrapping Position
    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult = Mathf.Abs(worldPosition.x) > (Mathf.Abs(boxCollider.bounds.min.x));
        bool yBoundResult = Mathf.Abs(worldPosition.y) > (Mathf.Abs(boxCollider.bounds.min.y));

        Vector2 signWorldPosition = new Vector2(
            Mathf.Sign(worldPosition.x),
            Mathf.Sign(worldPosition.y)
        );

        if (xBoundResult && yBoundResult)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1)
                + Vector2.Scale(Vector2.zero, signWorldPosition);
        }
        else if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y) + Vector2.zero;
        }
        else if (yBoundResult)
        {
            return new Vector2(worldPosition.x, worldPosition.y * -1) + Vector2.zero;
        }
        else
        {
            return worldPosition;
        }
    }
}
