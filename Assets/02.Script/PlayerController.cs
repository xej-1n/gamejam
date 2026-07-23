using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("이동 속도")]
    public float moveSpeed = 5f;

    [Header("죽음 애니메이션 대기 시간")]
    public float deathDelay = 1f;

    [Header("바닥 감지 설정")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float checkRadius = 0.2f;

    [Header("낙하 가속도 조절")]
    public float fallMultiplier = 2.5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveInput;
    private bool isDead = false;
    private int dieLayer;

    private bool isGrounded;
    private bool isHoldingBlock = false;
    private bool isSteepSlope = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dieLayer = LayerMask.NameToLayer("Die");
    }

    void Update()
    {
        if (isDead) return;

        bool rayGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        isGrounded = rayGrounded && !isSteepSlope;

        anim.SetBool("IsRunning", !isHoldingBlock && Mathf.Abs(moveInput.x) > 0.1f && isGrounded);
        anim.SetBool("IsGrounded", isGrounded);

        if (!isHoldingBlock && isGrounded)
        {
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(3, 3, 1);
            }
            else if (moveInput.x < 0)
            {
                transform.localScale = new Vector3(-3, 3, 1);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                float slopeAngle = Vector2.Angle(contact.normal, Vector2.up);
                if (slopeAngle > 30f)
                {
                    isSteepSlope = true;
                    return;
                }
            }
        }
        isSteepSlope = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isSteepSlope = false;
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        if (isHoldingBlock)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }


        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }

        else if (isSteepSlope)
        {
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
            }
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }

        else
        {
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.fixedDeltaTime;
            }
            rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        }
    }

    public void OnMove(InputValue value)
    {
        if (isHoldingBlock)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = value.Get<Vector2>();
    }

    public void SetHoldingBlock(bool holding)
    {
        isHoldingBlock = holding;
        if (holding)
        {
            moveInput = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == dieLayer && !isDead)
        {
            StartCoroutine(DieAndRespawn());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == dieLayer && !isDead)
        {
            StartCoroutine(DieAndRespawn());
        }
    }

    private IEnumerator DieAndRespawn()
    {
        isDead = true;

        moveInput = Vector2.zero;
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsGrounded", true);
        rb.linearVelocity = Vector2.zero;

        anim.SetTrigger("Die");

        yield return new WaitForSeconds(deathDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}