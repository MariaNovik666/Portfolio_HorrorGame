using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseState();
        }
    }

    public void ChangePauseState()
    {
        if (settingsPanel.active == false)
        {
            if (pauseMenu.active == false)
            {
                pauseMenu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
            }
            else
            {
                pauseMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
            }
        }
    }

    public void ShowSettings()
    {
        if (settingsPanel.active == false)
        {
            settingsPanel.SetActive(true);
            pauseMenu.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
}
