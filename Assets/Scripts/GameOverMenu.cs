using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    int bestScore = 0;
    int currentScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestText;
    public GameObject scoreMenu;
    public GameObject newBestScore;
    public TextMeshProUGUI yourScore;
    public TextMeshProUGUI countdown;

    void OnEnable()
    {
        Time.timeScale = 0;

        currentScore = System.Convert.ToInt32(scoreText.text);
        bestScore = PlayerPrefs.GetInt("bestScore");

        scoreMenu.SetActive(false);

        // If score is bigger than best change it
        if (currentScore > bestScore)
        {
            newBestScore.SetActive(true);
            PlayerPrefs.SetInt("bestScore", currentScore);

            yourScore.text = scoreText.text;
        }
        else if (currentScore <= bestScore)
        {
            yourScore.text = scoreText.text;
        }

        StartCoroutine(RestartGame());
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }

    // Restart the level
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Restart in: 8";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Restart in: 7";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Restart in: 6";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Restart in: 5";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Restart in: 4";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Restart in: 3";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Restart in: 2";
        yield return new WaitForSecondsRealtime(1);
        countdown.text = "Restart in: 1";
        yield return new WaitForSecondsRealtime(1);

        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
