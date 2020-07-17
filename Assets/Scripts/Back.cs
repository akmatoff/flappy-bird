using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public void BackFromLeaderboard() {
        SceneManager.LoadSceneAsync("GameScene");
        Time.timeScale = 1;
    }
}
