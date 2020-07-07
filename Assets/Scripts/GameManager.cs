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
    public Text highScoreText;
    public static int score;
    public static int highscore;
    private bool gameOver = false;
    public bool gameStarted = false;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
        scoreText.gameObject.SetActive(false);
        playerController.playerGravityScale = 0;
        highScoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
    }

    public void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            gameStarted = false;
            audioSource.PlayOneShot(punchAudio, 1f);
            audioSource.PlayOneShot(endSong, 0.9f); 

            Time.timeScale = 0; // Stop time | Pause Game
        } 

        if (score > PlayerPrefs.GetInt("Highscore", 0)) {
            PlayerPrefs.SetInt("Highscore", score);
            highScoreText.text = score.ToString();
        }
        
    }

    public void StartGame() {
        playerController.playerGravityScale = 1f;
        audioSource.Stop();
        score = 0;
        scoreText.gameObject.SetActive(true);
        gameStarted = true;
        if (gameOver) {
            SceneManager.LoadScene("GameScene");
            Time.timeScale = 1;
            playerController.playerGravityScale = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + "";
    }
}
