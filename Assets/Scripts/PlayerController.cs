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
    public float force = 270f;

    private Rigidbody2D bird;
    public float birdGravityScale;
    public float fallMultiplier = 2.5f;

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0) {
            if (!gm.gameStarted) {
                gm.StartGame();
                bird.gravityScale = birdGravityScale;
            }
            if (!gameOver) {
                bird.velocity = UnityEngine.Vector2.zero;
                bird.AddForce(UnityEngine.Vector2.up * force);
                audioSource.Play();
            } else {
                gm.StartGame();
            }
        }

        if (gm.gameStarted) {
            // Better jumping
            bird.velocity += UnityEngine.Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            if (bird.velocity.y > 0) {
                transform.eulerAngles += UnityEngine.Vector3.forward * 22 * Time.deltaTime;
            } else {
                transform.eulerAngles += UnityEngine.Vector3.back * 22 * Time.deltaTime;
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
