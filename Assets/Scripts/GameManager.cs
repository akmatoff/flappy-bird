using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip punchAudio;
    public AudioClip endSong;

    public Text scoreText;
    public static int score;
    private bool gameOver = false;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            audioSource.PlayOneShot(punchAudio, 1f);
            audioSource.PlayOneShot(endSong, 0.9f);

            Time.timeScale = 0; // Stop time | Pause Game
        }
        
    }

    public void Restart()
    {
        Time.timeScale = 1; // Start time | Start Game
        audioSource.Stop();
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + "";
    }
}
