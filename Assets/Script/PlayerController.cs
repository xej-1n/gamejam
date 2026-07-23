using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("이동 속도")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. 애니메이터 파라미터 업데이트 (달리기만 남김)
        anim.SetBool("IsRunning", Mathf.Abs(moveInput.x) > 0.1f);

        // 2. 캐릭터 좌우 반전 (Scale 사용)
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(2.5f, 2.5f, 1); // 오른쪽
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-2.5f, 2.5f, 1); // 왼쪽 (X축 반전)
        }
    }

    void FixedUpdate()
    {
        // 3. 물리 이동
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    // --- 인풋 시스템 입력 ---
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}