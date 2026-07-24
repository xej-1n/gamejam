using UnityEngine;

public class SafeBlockDrag : MonoBehaviour
{
    [Header("투명도 설정")]
    [Range(0f, 1f)]
    public float dragAlpha = 0.5f;

    private bool isDragging = false;
    private Vector3 offset;
    private Collider2D[] colliders;
    private SpriteRenderer[] spriteRenderers;
    private int currentAngle = 0;

    private Vector3 originalPosition;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        SetTriggers(false);
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            transform.position = mousePos + offset;
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentAngle = (currentAngle + 90) % 360;
                transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            }
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        originalPosition = transform.position;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        offset = transform.position - mousePos;

        SetTriggers(true);
        SetAlpha(dragAlpha);

        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.SetHoldingBlock(true);
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (IsOverlappingWithOther())
        {
            transform.position = originalPosition;
        }

        SetTriggers(false);
        SetAlpha(1.0f);

        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.SetHoldingBlock(false);
    }

    private void SetTriggers(bool state)
    {
        foreach (Collider2D col in colliders)
        {
            col.isTrigger = state;
        }
    }

    private void SetAlpha(float alpha)
    {
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            Color color = sr.color;
            color.a = alpha;
            sr.color = color;
        }
    }
    private bool IsOverlappingWithOther()
    {
        foreach (Collider2D col in colliders)
        {
            Bounds bounds = col.bounds;

            Vector2 checkSize = new Vector2(bounds.size.x * 0.8f, bounds.size.y * 0.8f);

            Collider2D[] results = Physics2D.OverlapBoxAll(bounds.center, checkSize, 0f);

            foreach (Collider2D hit in results)
            {
                if (hit.gameObject == gameObject || hit.transform.IsChildOf(transform)) continue;

                if (hit.CompareTag("Player") || hit.CompareTag("Ground"))
                {
                    return true;
                }
            }
        }
        return false;
    }

}