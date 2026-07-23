using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("죽음 애니메이션 대기 시간")]
    public float deathDelay = 0.5f;

    [Header("감지 레이어 설정")]
    public LayerMask groundLayer; 
    public LayerMask dieLayer;   

    private Animator anim;
    private Collider2D col;
    private Rigidbody2D rb;
    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckDeath(collision.gameObject.layer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckDeath(collision.gameObject.layer);
    }

    private void CheckDeath(int targetLayer)
    {
        if (isDead) return;

       
        if (((1 << targetLayer) & groundLayer) != 0 || ((1 << targetLayer) & dieLayer) != 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

       
        if (anim != null)
        {
            anim.SetTrigger("Die");
        }


        if (col != null) col.enabled = false;
        if (rb != null) rb.simulated = false;

  
        Destroy(gameObject, deathDelay);
    }
}