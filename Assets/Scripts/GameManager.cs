using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public Text gameOverText;
    public Text scoreText;
    public Text livesText;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public static int highScore { get; private set; }
    public int lives { get; private set; }

    public void Start()
    {
        Cursor.visible = false;
        NewGame();
    }

    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        highScore = PlayerPrefs.GetInt("HighScore");
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        SoundManager.PlaySound("gameBackground");
        this.gameOverText.enabled = false;

        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    private void ResetState()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }

        this.pacman.ResetState();
    }

    private void GameOver()
    {
        this.gameOverText.enabled = true;

        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        this.pacman.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        this.livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        this.scoreText.text = score.ToString().PadLeft(2, '0');
    }

    public void PacmanEaten()
    {
        SoundManager.PlaySound("pacmanDeath");

        this.pacman.DeathSequence();

        SetLives(this.lives - 1);

        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            SoundManager.BackgroundStop();
            SoundManager.PlaySound("gameOver");
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        SoundManager.PlaySound("ghostEaten");
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + points);

        this.ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        SoundManager.PlaySound("pelletEaten");
        pellet.gameObject.SetActive(false);

        SetScore(this.score + pellet.points);

        if (!HasRemainingPellets())
        {
            SoundManager.BackgroundStop();
            SoundManager.PlaySound("newRound");
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        SoundManager.PlaySound("powerPelletEaten");
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        this.ghostMultiplier = 1;
    }

}
