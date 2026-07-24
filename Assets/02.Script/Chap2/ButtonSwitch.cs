using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    [Header("버튼이 눌리면 켜질 길(길 오브젝트들)")]
    public GameObject[] pathsToEnable;

    private bool isPressed = false;

    void Start()
    {
        foreach (GameObject path in pathsToEnable)
        {
            if (path != null) path.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPressed) return;

        if (other.CompareTag("Box") || other.CompareTag("Player"))
        {
            isPressed = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.5f, transform.localScale.z);

            foreach (GameObject path in pathsToEnable)
            {
                if (path != null) path.SetActive(true);
            }

            if (other.CompareTag("Box"))
            {
                MonoBehaviour dragScript = other.GetComponent<SafeBlockDrag>();
                if (dragScript == null)
                {
                    dragScript = other.GetComponent("DraggableBlock") as MonoBehaviour;
                }
                if (dragScript != null)
                {
                    Destroy(dragScript);
                }
                Collider2D[] boxColliders = other.GetComponentsInChildren<Collider2D>();
                foreach (Collider2D col in boxColliders)
                {
                    col.isTrigger = false;
                }
                other.transform.position = new Vector3(transform.position.x, other.transform.position.y, other.transform.position.z);
            }
        }
    }
}