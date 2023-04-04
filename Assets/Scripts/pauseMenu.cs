using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public AudioSource[] backgroundAudio;
    public GameObject pauseMenuObject;

    public PlayerController playerController;
    public GameObject GameOverScreen;

    void Update()
    {
        // If ESC is pressed menu is opened and time and music stopped
        if (Input.GetButtonDown("Cancel") && !GameOverScreen.activeSelf)
        {
            if (!pauseMenuObject.activeSelf)
            {
                playerController.isInputLocked = true;
                Time.timeScale = 0;

                pauseMenuObject.SetActive(true);
                for (int i = 0; i < backgroundAudio.Length; i++)
                    backgroundAudio[i].Pause();
            }
            else
            {
                playerController.isInputLocked = false;
                Time.timeScale = 1;

                pauseMenuObject.SetActive(false);
                for (int i = 0; i < backgroundAudio.Length; i++)
                    backgroundAudio[i].UnPause();
            }
        }
    }

    public void Retry()
    {
        playerController.isInputLocked = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {
        playerController.isInputLocked = false;
        Time.timeScale = 1;

        pauseMenuObject.SetActive(false);
        for (int i = 0; i < backgroundAudio.Length; i++)
            backgroundAudio[i].UnPause();
    }

    public void QuitToMenu()
    {
        playerController.isInputLocked = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
