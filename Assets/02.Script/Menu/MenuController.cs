using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject menuPanel;

    [Header("Audio Sliders")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        if (menuPanel != null)
            menuPanel.SetActive(false);


    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (menuPanel.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void ResumeGame()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1f; 
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void GoToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}