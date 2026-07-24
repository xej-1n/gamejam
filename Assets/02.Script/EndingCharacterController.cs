using System.Collections;
using UnityEngine;

public class EndingCharacterController : MonoBehaviour
{
    [Header("설정")]
    public float delayBeforeRun = 1.0f; 
    public float runSpeed = 5.0f;    


    private Animator animator;
    private Rigidbody2D rb;


    private bool shouldRun = false;

    void Start()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();


        StartCoroutine(WaitThenRunRoutine());
    }

    void FixedUpdate()
    {

        if (shouldRun)
        {
         
            rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);
        }
    }

    
    private IEnumerator WaitThenRunRoutine()
    {
        // 처음엔 멈춰있는 애니메이션 상태
        if (animator != null) animator.SetBool("isRunning", false);

      
        yield return new WaitForSeconds(delayBeforeRun);

        // 기다린 뒤에 달리기 애니메이션으로 변경
        if (animator != null) animator.SetBool("isRunning", true);

        shouldRun = true;
    }
}