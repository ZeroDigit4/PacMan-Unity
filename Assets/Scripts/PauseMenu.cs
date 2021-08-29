using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject firstButton;
    public Text recordText;
    public int record;
    public static bool gameIsPaused = false, isRunning = false;
    public GameObject pauseMenuUI;

    GameObject lastselect;
    void Start()
    {
        lastselect = new GameObject();
    }
    // Update is called once per frame

    // Update is called once per frame
    void Update()
    {
        record = GameManager.highScore;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.recordText.enabled = false;
            Cursor.visible = true;
            SoundManager.StopSound();
            if (gameIsPaused)
            {
                Back();
            }
            else
            {
                Pause();
            }
        }
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }
    public void Back()
    {
        Cursor.visible = false;
        SoundManager.ResumeSound();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void Resume()
    {
        Cursor.visible = false;
        SoundManager.ResumeSound();
        SoundManager.PlaySound("buttonClick");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
    public void QuitGame()
    {
        StartCoroutine(QuitAfterDelay(1f));
    }
    private IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(1);
        Application.Quit();
        PlayerPrefs.DeleteAll();
    }
    public void Restart()
    {
        Cursor.visible = false;
        SoundManager.ResumeSound();
        SoundManager.PlaySound("buttonClick");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Record()
    {
        if (isRunning == false)
        {
            StartCoroutine(TextDelay());
        }
    }
    private IEnumerator TextDelay()
    {
        isRunning = true;
        this.recordText.enabled = true;
        recordText.text = record.ToString();
        yield return new WaitForSecondsRealtime(0.5f);
        this.recordText.enabled = false;
        isRunning = false;
    }
}
