using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip punchAudio;
    public AudioClip endSong;

    PlayerController playerController;

    public Text scoreText;
    public static int score;
    private bool gameOver = false;
    public bool gameStarted = false;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
        scoreText.gameObject.SetActive(false);
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

    public void StartGame() {
        playerController.birdGravityScale = 1f;
        audioSource.Stop();
        score = 0;
        scoreText.gameObject.SetActive(true);
        gameStarted = true;
        if (gameOver) {
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + "";
    }
}
