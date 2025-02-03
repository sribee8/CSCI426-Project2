using UnityEngine;
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Alien Prefab")]
    public GameObject Alien;

    [Header("Player Sprites")]
    [SerializeField] private Sprite aliveSprite;
    [SerializeField] private Sprite deadSprite;

    [Header("Spawn Settings")]
    [SerializeField] private float baseInterval = 3f;
    [SerializeField] private float minimumInterval = 0.5f;
    [SerializeField] private float intervalDecreaseRate = 0.1f;

    private float t;
    private float currentInterval;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        t = 0f;
        currentInterval = baseInterval;

        if (spriteRenderer != null && aliveSprite != null)
        {
            spriteRenderer.sprite = aliveSprite;
        }
    }

    void Update()
    {
        if (isDead || GameManager.Instance.isGameOver) return;

        t += Time.deltaTime;
        currentInterval = Mathf.Max(currentInterval - (Time.deltaTime * intervalDecreaseRate), minimumInterval);
        if (Input.GetKeyDown("space"))
        {
            rb.gravityScale *= -1;
            transform.Rotate(0, 0, 180);
        }
        if (t >= currentInterval)
        {
            SpawnAlien();
            t = 0f;
        }
    }

    void SpawnAlien()
    {
        float randomY = Random.Range(-3.5f, 3.5f);
        Vector3 spawnPosition = new Vector3(13.4f, randomY, 0);
        Instantiate(Alien, spawnPosition, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!isDead && col.gameObject.CompareTag("Alien"))
        {
            isDead = true;

            if (spriteRenderer != null && deadSprite != null)
            {
                spriteRenderer.sprite = deadSprite;
            }

            GameManager.Instance.GameOver();
        }
    }

    public void ResetForNewGame()
    {
        isDead = false;
        transform.position = new Vector3(-6.16f, -2.9f, 0);
        transform.rotation = Quaternion.identity;

        if (spriteRenderer != null && aliveSprite != null)
        {
            spriteRenderer.sprite = aliveSprite;
        }

        t = 0f;
        currentInterval = baseInterval;
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
    }
}