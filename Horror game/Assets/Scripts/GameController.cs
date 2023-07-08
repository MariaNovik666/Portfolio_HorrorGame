using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Slider sensivitySlider;
    public Slider volumeSlider;
    public AudioSource audioSource;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        audioSource.volume = volumeSlider.value;
        sensivitySlider.value = PlayerPrefs.GetFloat("Sensivity");
        CameraController.mouseSensitiviti = sensivitySlider.value;

    }
    public void StartNewGame()
    {
        ChangeScene("Game");
    }

    public void ExitToMainMenu()
    {
        ChangeScene("Menu");
    }

    private void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1;
    }


    public void SetSensitive()
    {
        CameraController.mouseSensitiviti = sensivitySlider.value;
        PlayerPrefs.SetFloat("Sensivity", sensivitySlider.value);
    }

    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void QuitGame()
    {
        Application.Quit();

        UnityEditor.EditorApplication.isPlaying = false;
    }
}
