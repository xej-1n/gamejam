using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserEmitter : MonoBehaviour
{
    [Header("레이저 설정")]
    public float maxDistance = 50f;
    public LayerMask obstacleLayer;

    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
        }

        if (boxCollider != null)
        {
            boxCollider.isTrigger = true;
        }
    }

    void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, maxDistance, obstacleLayer);

        float currentDistance = maxDistance;
        Vector3 endPoint = transform.position + (Vector3.down * maxDistance);

        if (hit.collider != null)
        {
            currentDistance = hit.distance;
            endPoint = hit.point;
        }

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPoint);
        }
        if (boxCollider != null)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, currentDistance);

            boxCollider.offset = new Vector2(0, -currentDistance / 2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}