using System.Collections.Generic;
using UnityEngine;

public class MonsterGroundBlock : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private int currentAngle = 0;

    private bool isPlayerStandingOnTop = false;
    private List<Transform> ridingObjects = new List<Transform>();

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = transform.position.z;

            Vector3 targetPos = mousePos + offset;
            Vector3 deltaPosition = targetPos - transform.position;

            for (int i = ridingObjects.Count - 1; i >= 0; i--)
            {
                if (ridingObjects[i] != null)
                {
                    if (ridingObjects[i].position.y > transform.position.y)
                    {
                        ridingObjects[i].position += deltaPosition;
                    }
                }
                else
                {
                    ridingObjects.RemoveAt(i);
                }
            }

            transform.position = targetPos;

            if (Input.GetKeyDown(KeyCode.R))
            {
                currentAngle = (currentAngle + 90) % 360;
                transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            }
        }
    }

    void OnMouseDown()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();

 
        if (isPlayerStandingOnTop)
        {
            return;
        }

        isDragging = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = transform.position.z;
        offset = transform.position - mousePos;

        if (player != null) player.SetHoldingBlock(true);
    }

    void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;

        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.SetHoldingBlock(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
    
            bool onTop = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y < -0.5f)
                {
                    onTop = true;
                    break;
                }
            }
            isPlayerStandingOnTop = onTop;

            if (isDragging)
            {
                isDragging = false;
                player.SetHoldingBlock(false);
            }
        }

        if (collision.gameObject.GetComponent<Monster>() != null || player != null)
        {
            if (!ridingObjects.Contains(collision.transform))
            {
                ridingObjects.Add(collision.transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            isPlayerStandingOnTop = false;
        }

        if (collision.gameObject.GetComponent<Monster>() != null || player != null)
        {
            if (ridingObjects.Contains(collision.transform))
            {
                ridingObjects.Remove(collision.transform);
            }
        }
    }
}