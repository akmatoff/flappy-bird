using UnityEngine;

public class PipeSpawner : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject pipes;
    public float elapsedTime;
    public float spawnDelay;
    private float variation;
    
    public void spawnPipes()
    {
        variation = Random.Range(-2.2f, 2.2f);

        Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y + variation, 1.0f);
        Quaternion q = this.transform.rotation;

        Instantiate(pipes, position, q);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > spawnDelay && gameManager.gameStarted)
        {
            spawnPipes();
            elapsedTime = 0.0f;
        }
    }
}
                    