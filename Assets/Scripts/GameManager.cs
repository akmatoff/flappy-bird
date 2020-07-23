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
    public GameObject menu;
    public bool gameOver = false;
    public bool gameStarted = false;

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = FindObjectOfType<PlayerController>();
        scoreText.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        playerController.playerGravityScale = 0;
        highScoreText.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        Time.timeScale = 1;
    }

    public void GameOver()
    {

        gameOver = true;
        audioSource.PlayOneShot(punchAudio, 1f);
        audioSource.PlayOneShot(endSong, 0.9f); 
        menu.gameObject.SetActive(true);

        Time.timeScale = 0; // Stop time | Pause Game

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
    }

    public void RestartGame() {
        SceneManager.LoadSceneAsync("GameScene");
    }

    public void OpenLeaderboard() {
        SceneManager.LoadSceneAsync("LeaderboardScene");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score + "";
    }
}
