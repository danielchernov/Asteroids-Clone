using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreManager : MonoBehaviour
{
    public TextMeshProUGUI bestText;

    // Sets Best Score on Start
    void Start()
    {
        bestText.text = PlayerPrefs.GetInt("bestScore").ToString();
    }
}
