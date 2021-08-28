using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip pelletEaten, pacmanDeath, ghostEaten, powerPelletEaten, gameOver, newRound, gameBackground, buttonClick;
    static AudioSource audioSource, smallAudioSource, loopAudioSource, perfectAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        pelletEaten = Resources.Load<AudioClip>("credit");
        pacmanDeath = Resources.Load<AudioClip>("death");
        ghostEaten = Resources.Load<AudioClip>("eat_ghost");
        powerPelletEaten = Resources.Load<AudioClip>("power_pellet");
        gameOver = Resources.Load<AudioClip>("game_over");
        newRound = Resources.Load<AudioClip>("new_round");
        gameBackground = Resources.Load<AudioClip>("game_background");
        buttonClick = Resources.Load<AudioClip>("button_click");
        audioSource = GetComponent<AudioSource>();
        smallAudioSource = GetComponent<AudioSource>();
        loopAudioSource = GetComponent<AudioSource>();
        perfectAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "buttonClick":
                perfectAudioSource.PlayOneShot(buttonClick);
                break;
            case "gameBackground":
                loopAudioSource.clip = gameBackground;
                loopAudioSource.Play(0);
                break;
            case "pelletEaten":
                smallAudioSource.volume = 0.3f;
                smallAudioSource.PlayOneShot(pelletEaten);
                break;
            case "pacmanDeath":
                audioSource.PlayOneShot(pacmanDeath);
                break;
            case "ghostEaten":
                audioSource.PlayOneShot(ghostEaten);
                break;
            case "powerPelletEaten":
                audioSource.PlayOneShot(powerPelletEaten);
                break;
            case "gameOver":
                audioSource.PlayOneShot(gameOver);
                break;
            case "newRound":
                audioSource.PlayOneShot(newRound);
                break;
        }
    }
    public static void StopSound()
    {
        audioSource.Pause();
        smallAudioSource.Pause();
        loopAudioSource.Pause();
    }
    public static void ResumeSound()
    {
        audioSource.UnPause();
        smallAudioSource.UnPause();
        loopAudioSource.UnPause();
    }
    public static void BackgroundStop()
    {
        loopAudioSource.Stop();
    }

}
