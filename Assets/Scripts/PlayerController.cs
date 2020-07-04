using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameManager gm;
    AudioSource audioSource;
    private bool gameOver = false;

    // Flap force
    public float force = 250f;

    private Rigidbody2D bird;
    public float birdGravityScale;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        bird = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        bird.gravityScale = birdGravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // On press 'Space'
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gm.gameStarted) {
                gm.StartGame();
                bird.gravityScale = birdGravityScale;
            }
            if (!gameOver) {
                bird.velocity = UnityEngine.Vector2.zero;
                bird.AddForce(new UnityEngine.Vector2(0, force));
                audioSource.Play();
            } else {
                gm.StartGame();
            }
        }
    }

    // Start the scene from the beginning if collides
    void OnCollisionEnter2D(Collision2D collision)
    {
        gm.GameOver();
        gameOver = true;
    }
}
