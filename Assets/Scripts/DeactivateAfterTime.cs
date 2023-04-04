using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    public float timeForDeactivate = 1.5f;

    void OnEnable()
    {
        StartCoroutine(DeactivateMe(timeForDeactivate));
    }

    IEnumerator DeactivateMe(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
