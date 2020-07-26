using UnityEngine;

public class PipeController : MonoBehaviour
{

    public float movespeed = 8f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-movespeed, 0);
        if (this.transform.position.x < -15f)
        {
            Destroy(this.gameObject);
        }
    }
}
