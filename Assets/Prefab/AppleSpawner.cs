using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;

    float timer = 0f;
    void Start()
    {
        InvokeRepeating("SpawnApple", 1f, 1f); // 1•b‚¨‚«
    }

    void SpawnApple()
    {
        float x = Random.Range(-4f, 4f);
        Vector3 pos = new Vector3(x, transform.position.y, 0);
        Instantiate(applePrefab, pos, Quaternion.identity);
    }

    void Update()
    {
        if (GameManager.instance.isGameOver) return;

        timer += Time.deltaTime;

        if (timer >= GameManager.instance.spawnInterval)
        {
            timer = 0f;
            SpawnApple();
        }
    }
}
