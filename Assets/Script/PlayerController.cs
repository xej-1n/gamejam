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
        anim.SetBool("IsRunning", Mathf.Abs(moveInput.x) > 0.1f);

        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-3, 3, 1);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}