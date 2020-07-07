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

    private Rigidbody2D player;
    public float playerGravityScale;
    public float fallMultiplier = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        player = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        player.gravityScale = playerGravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // On press 'Space'
        if (Input.touchCount == 1) {
            if (!gm.gameStarted) {
                gm.StartGame();
                player.gravityScale = playerGravityScale;
            }
            if (!gameOver) {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began) {
                    player.velocity = UnityEngine.Vector2.zero;
                    player.AddForce(UnityEngine.Vector2.up * force);
                    audioSource.Play();
                }
                
            } else {
                if (!gm.gameStarted) {
                    gm.StartGame();
                }
                
            }
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            if (!gm.gameStarted) {
                gm.StartGame();
                player.gravityScale = playerGravityScale;
            }
            if (!gameOver) {
                player.velocity = UnityEngine.Vector2.zero;
                player.AddForce(UnityEngine.Vector2.up * force);
                audioSource.Play();
            } else {
                if (!gm.gameStarted) {
                    gm.StartGame();
                }
            }
        }

        if (gm.gameStarted) {
            // Better jumping
            player.velocity += UnityEngine.Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

            // Rotate the player
            if (player.velocity.y > 0) {
                transform.eulerAngles += UnityEngine.Vector3.forward * 23 * Time.deltaTime;
            } else {
                transform.eulerAngles += UnityEngine.Vector3.back * 23 * Time.deltaTime;
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
