using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip pelletEaten, pacmanDeath, ghostEaten, intro, powerPelletEaten, gameOver, newRound;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        pelletEaten = Resources.Load<AudioClip>("credit");
        pacmanDeath = Resources.Load<AudioClip>("death");
        ghostEaten = Resources.Load<AudioClip>("eat_ghost");
        intro = Resources.Load<AudioClip>("game_start");
        powerPelletEaten = Resources.Load<AudioClip>("power_pellet");
        gameOver = Resources.Load<AudioClip>("game_over");
        newRound = Resources.Load<AudioClip>("new_round");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "pelletEaten":
                audioSource.PlayOneShot(pelletEaten);
                audioSource.volume = 0.3f;
                break;
            case "pacmanDeath":
                audioSource.PlayOneShot(pacmanDeath);
                break;
            case "ghostEaten":
                audioSource.PlayOneShot(ghostEaten);
                break;
            case "intro":
                audioSource.PlayOneShot(intro);
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
}
