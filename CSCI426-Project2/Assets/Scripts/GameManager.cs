using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float speed = 3f;
    public float totalTime = 0f;
    public TextMeshProUGUI totalTimeText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI highScoreText;
    public GameObject redOverlay;

    public bool isGameOver = false;
    private float record;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (redOverlay != null) redOverlay.SetActive(false);
        if (highScoreText != null) highScoreText.gameObject.SetActive(false);
        record = 0f;
    }

    void Update()
    {
        if (!isGameOver)
        {
            speed += Time.deltaTime;
            totalTime += Time.deltaTime;
            totalTimeText.text = "Time: " + totalTime.ToString("F2");
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }
        }
    }


    public void GameOver()
    {
        isGameOver = true;
        if (totalTime > record)
        {
            record = totalTime;
        }
        // Hide time text
        if (totalTimeText != null)
        {
            totalTimeText.gameObject.SetActive(false);
        }

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER\nSurvival Time: " + totalTime.ToString("F2") + "\nPress R to Restart";
        }
        if (highScoreText != null)
        {
            highScoreText.gameObject.SetActive(true);
            highScoreText.text = "High Score:\n" + record.ToString("F2");
        }

        if (redOverlay != null)
        {
            redOverlay.SetActive(true);
        }
    }

    public void RestartGame()
    {
        isGameOver = false;
        speed = 3f;
        totalTime = 0f;

        // Show time text again
        if (totalTimeText != null) totalTimeText.gameObject.SetActive(true);
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (redOverlay != null) redOverlay.SetActive(false);
        if (highScoreText != null) highScoreText.gameObject.SetActive(false);

        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach (GameObject alien in aliens)
        {
            Destroy(alien);
        }

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.ResetForNewGame();
        }
    }
}