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
    public GameObject button;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Time.timeScale = 0;
        DontDestroyOnLoad(GetComponent<GameManager>());
        if (gameOver) {
          StartGame();  
        }  
    }

    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            audioSource.PlayOneShot(punchAudio, 1f);
            audioSource.PlayOneShot(endSong, 0.9f);

            Time.timeScale = 0; // Stop time | Pause Game
            button.SetActive(true);
        } 
    }

    public void StartGame() {
        audioSource.Stop();
        Time.timeScale = 1;
        score = 0;
        button.SetActive(false);
        if (gameOver) {
            SceneManager.LoadScene("GameScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + "";
    }
}
