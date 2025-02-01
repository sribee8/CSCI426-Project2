using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Alien1;
    public GameObject Alien2;
    private float t;
    private float tt;
    public float interval = 3f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        t = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (Input.GetKeyDown("space"))
        {
            rb.gravityScale *= -1;
        }
        //spawning the aliens after a certain interval
        while (t >= interval)
        {
            SpawnAlien();
            t -= interval;
            interval -= 0.5f;
        }
    }

    void SpawnAlien()
    {
        int val = Random.Range(0, 2);
        if (val == 0)
        {
            Instantiate(Alien1, new Vector3(13.4f, 2.7f, 0), Quaternion.identity);
        }
        else
        {
            {
                Instantiate(Alien2, new Vector3(13.4f, -2.8f, 0), Quaternion.identity);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Alien")
        {
            transform.position = new Vector3(0, -2.9f, 0);
            GameManager.Instance.speed = 3f;
            GameManager.Instance.totalTime = 0f;
        }
    }
}
