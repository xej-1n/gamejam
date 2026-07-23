using UnityEngine;

public class RockRespawn : MonoBehaviour
{
    [Header("이 좌표들을 벗어나면 처음 위치로 돌아감")]
    public float bottomLimitY = -10f; 
    public float topLimitY = 15f;    
    public float leftLimitX = -20f;  
    public float rightLimitX = 20f;   

    private Vector3 startPosition;
    private Rigidbody2D rb;

    void Start()
    {

        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
   
        if (transform.position.y < bottomLimitY ||
            transform.position.y > topLimitY ||
            transform.position.x < leftLimitX ||
            transform.position.x > rightLimitX)
        {
            RespawnRock();
        }
    }

    public void RespawnRock()
    {
   
        transform.position = startPosition;

        
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}