using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveDistance = 5f;
    public float speed = 2f;

    private Vector3 startPosition;
    private bool isMoving = true;

    private Rigidbody2D playerRb;
    private RigidbodyType2D originalBodyType;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.Translate(Vector3.right * speed * Time.deltaTime);

        float currentOffset = transform.position.x - startPosition.x;

        if (currentOffset >= moveDistance)
        {
            StopPlatformAndReleasePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isMoving)
            {
                playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                if (playerRb != null)
                {
                    originalBodyType = playerRb.bodyType;
                    playerRb.bodyType = RigidbodyType2D.Static;
                }
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ReleasePlayer();
        }
    }

    private void StopPlatformAndReleasePlayer()
    {
        isMoving = false;
        ReleasePlayer();
    }

    private void ReleasePlayer()
    {
        if (playerRb != null)
        {
            playerRb.bodyType = originalBodyType;

            if (gameObject.activeInHierarchy && playerRb.gameObject.activeInHierarchy)
            {
                playerRb.transform.SetParent(null);
            }

            playerRb = null;
        }
    }
}