using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdMotion : MonoBehaviour
{

    // Flap force
    public float force = 300f;

    private Rigidbody2D bird;

    // Start is called before the first frame update
    void Start()
    {
        bird = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            bird.velocity = UnityEngine.Vector2.zero;
            bird.AddForce(new UnityEngine.Vector2(0, force));

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
