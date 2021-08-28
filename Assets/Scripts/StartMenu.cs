using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public GameObject startMenuUI;
    GameObject lastselect;
    void Start()
    {
        lastselect = new GameObject();
    }
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void QuitGame()
    {
        SoundManager.PlaySound("buttonClick");
        StartCoroutine(QuitAfterDelay(1f));
    }
    private IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
    public void Play()
    {
        SoundManager.PlaySound("buttonClick");
        SceneManager.LoadScene("Pacman", LoadSceneMode.Single);
    }
}
