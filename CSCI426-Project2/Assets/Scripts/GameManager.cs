using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float speed = 3f;
    public float totalTime = 0f;
    public TextMeshProUGUI totalTimeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed += Time.deltaTime;
        totalTime += Time.deltaTime;
        totalTimeText.text = "Time: " + totalTime.ToString("F2");
    }
}
