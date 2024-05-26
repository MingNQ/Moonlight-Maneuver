using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject settingMenuScreen;
    [SerializeField] private GameObject gameOverScreen;
    private bool statusPause = false;
    private bool statusGameOver = false;
    AudioManager audioManager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) menuScreen.SetActive(false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Screen
        statusPause = menuScreen.activeInHierarchy;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (statusPause)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if (!statusPause && !statusGameOver)
        {
            menuScreen.SetActive(!statusPause);
            Time.timeScale = 0;
        }
    }

    public void Continue()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Press");
        }
        if (statusPause)
        {
            menuScreen.SetActive(!statusPause);
            Time.timeScale = 1;
        }
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void SettingMenu()
    {
        menuScreen.SetActive(false);
        settingMenuScreen.SetActive(true);
    }

    public void Back()
    {
        menuScreen.SetActive(true);
        settingMenuScreen.SetActive(false);
    }

    public void Restart()
    {
        statusGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        statusGameOver = true;
        gameOverScreen.SetActive(true);
        audioManager.MuteMusic(true);
    }

    public void PressSFX()
    {
        audioManager.PlaySound(audioManager.press);
    }

}
