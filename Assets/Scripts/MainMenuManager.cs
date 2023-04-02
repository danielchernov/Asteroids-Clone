using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;
    public GameObject creditsMenu;

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        creditsMenu.SetActive(true);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void ControlsButton()
    {
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
