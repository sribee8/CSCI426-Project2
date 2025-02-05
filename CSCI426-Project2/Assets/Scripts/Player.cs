using UnityEngine;
using System.Collections;
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

    private Vector3 originalScale;
    public AudioSource eating;
    public AudioSource blip;
    public GameObject AlienEating;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        t = 0f;
        currentInterval = baseInterval;
        originalScale = transform.localScale;

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
            blip.Play();
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
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            if (rb.gravityScale < 0)
            {
                rb.gravityScale *= -1;
            }
            StartCoroutine(Shrink());
            Instantiate(AlienEating, transform.position, Quaternion.identity);
            eating.Play();
            GameManager.Instance.GameOver();
        }
    }

    public void ResetForNewGame()
    {
        StopAllCoroutines();
        isDead = false;
        transform.position = new Vector3(-6.16f, -2.9f, 0);
        transform.rotation = Quaternion.identity;
        transform.localScale = originalScale;

        if (spriteRenderer != null && aliveSprite != null)
        {
            spriteRenderer.sprite = aliveSprite;
        }

        t = 0f;
        currentInterval = baseInterval;
        rb.gravityScale = Mathf.Abs(rb.gravityScale);
    }

    private IEnumerator Shrink()
    {

        float timer = 0f;
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 0f, timer / 2);
            transform.localScale = originalScale * scale;
            yield return null;
        }
    }
}