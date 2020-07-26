using UnityEngine;

public class Score : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.score++;
    }
}
