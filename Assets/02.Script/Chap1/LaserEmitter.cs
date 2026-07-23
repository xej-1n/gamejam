using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserEmitter : MonoBehaviour
{
    [Header("레이저 설정")]
    public float maxDistance = 100f;
    public LayerMask obstacleLayer;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
        }
    }

    void Update()
    {
        ShootLaser();
        Debug.DrawRay(transform.position, Vector3.down * maxDistance, Color.red);
    }

    void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, maxDistance, obstacleLayer);

        Vector3 endPoint;

        if (hit.collider != null)
        {
            endPoint = hit.point;
            if (hit.collider.CompareTag("Player") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Die"))
            {
                RestartScene();
            }
        }
        else
        {
            endPoint = transform.position + (Vector3.down * maxDistance);
        }

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPoint);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, Vector3.down * maxDistance);
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