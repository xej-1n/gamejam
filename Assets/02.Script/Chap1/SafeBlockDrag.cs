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
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;

        Collider2D[] results = new Collider2D[10];

        foreach (Collider2D col in colliders)
        {
            int count = col.Overlap(filter, results);
            for (int i = 0; i < count; i++)
            {
                if (results[i].CompareTag("Player") || results[i].CompareTag("Ground"))
                {
                    return true;
                }
            }
        }
        return false;
    }

}