using UnityEngine;

public class DraggableBlock : MonoBehaviour
{
    [Header("회전 속도 (R키를 누르고 있는 동안 도는 속도)")]
    public float rotationSpeed = 150f;

    private bool isDragging = false;
    private Vector3 offset;
    private Collider2D[] colliders;

    void Start()
    {
        colliders = GetComponentsInChildren<Collider2D>();
        SetTriggers(true);
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;
            transform.position = mousePos + offset;

            if (Input.GetKey(KeyCode.R))
            {
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
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
    }

    void OnMouseUp()
    {
        isDragging = false;
        SetTriggers(false);
    }

    private void SetTriggers(bool state)
    {
        foreach (Collider2D col in colliders)
        {
            col.isTrigger = state;
        }
    }
}