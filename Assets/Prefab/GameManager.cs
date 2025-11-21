using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float appleSpeed = 3f;
    public float spawnInterval = 1f;

    int missCount = 0;
    public int maxMiss = 3;
    public bool isGameOver = false;

    public TextMeshProUGUI gameOverText;


    private void Awake()
    {
        instance = this;
    }

    public void Miss()
    {
        missCount++;

        Debug.Log("Miss! " + missCount);

        if (missCount >= maxMiss)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER");

        gameOverText.gameObject.SetActive(true); // Å© UIÇï\é¶
    }


    void Update()
    {
        if (!isGameOver)
        {
            appleSpeed += 0.2f * Time.deltaTime;      // èôÅXÇ…è„è∏
            spawnInterval = Mathf.Max(0.3f, spawnInterval - 0.1f * Time.deltaTime);
        }
    }
}
