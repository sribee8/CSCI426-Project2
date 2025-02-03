using UnityEngine;

public class Alien : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(-GameManager.Instance.speed, 0, 0) * Time.deltaTime;
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
