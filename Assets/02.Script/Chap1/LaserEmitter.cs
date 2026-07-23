using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    [Header("레이저 설정")]
    public float maxDistance = 50f;
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
    }

    void ShootLaser()
    {
        if (lineRenderer == null) return;

        lineRenderer.SetPosition(0, transform.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, maxDistance, obstacleLayer);

        if (hit.collider != null)
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            Vector3 endPosition = transform.position + (Vector3.down * maxDistance);
            lineRenderer.SetPosition(1, endPosition);
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, maxDistance, obstacleLayer);

        Gizmos.color = Color.magenta;
        if (hit.collider != null)
        {
            Gizmos.DrawLine(transform.position, hit.point);
        }
        else
        {
            Gizmos.DrawRay(transform.position, Vector3.down * maxDistance);
        }
    }
}