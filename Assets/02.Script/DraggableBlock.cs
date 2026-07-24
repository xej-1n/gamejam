using UnityEngine;

public class DraggableBlock : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Collider2D[] colliders;
    private int currentAngle = 0;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        offset = transform.position - mousePos;
        SetTriggers(true);

        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.SetHoldingBlock(true);
    }

    void OnMouseUp()
    {
        isDragging = false;
        SetTriggers(false);

        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.SetHoldingBlock(false);
    }

    private void SetTriggers(bool state)
    {
        if (colliders != null)
        {
            foreach (Collider2D col in colliders)
            {
                if (col != null)
                {
                    col.isTrigger = state;
                }
            }
        }   
    }
}