using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [Header("내려갈 거리 (Y축으로 얼마나 내려갈지)")]
    public Vector3 moveOffset = new Vector3(0, -5f, 0);

    [Header("내려가는 속도")]
    public float moveSpeed = 2f;

    private Vector3 targetPosition;
    private bool isOpened = false;

    void Start()
    {
        targetPosition = transform.position + moveOffset;
    }

    void Update()
    {
        if (isOpened)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void OpenWall()
    {
        isOpened = true;
    }
}