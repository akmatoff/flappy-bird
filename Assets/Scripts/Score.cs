using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.score++;
    }
}
