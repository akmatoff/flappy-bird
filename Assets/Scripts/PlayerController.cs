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

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        bird = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // On press 'Space'
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (gameOver == false) {
                bird.velocity = UnityEngine.Vector2.zero;
                bird.AddForce(new UnityEngine.Vector2(0, force)); // Add Force to Y axis
                audioSource.Play();
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
