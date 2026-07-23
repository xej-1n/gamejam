using UnityEngine;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    [Header("1. 안 눌린 버튼 이미지 (PBP-V2_14)")]
    public Sprite idleSprite;

    [Header("2. 눌린 버튼 이미지 (PBP-V2_15)")]
    public Sprite pressedSprite;

    [Header("3. 동작시킬 벽 오브젝트")]
    public MovingWall targetWall;

    private SpriteRenderer spriteRenderer;
    private bool isPressed = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
   
        if (idleSprite != null)
        {
            spriteRenderer.sprite = idleSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (isPressed) return;

       
        if (collision.CompareTag("Rock"))
        {
            isPressed = true;

 
            if (pressedSprite != null)
            {
                spriteRenderer.sprite = pressedSprite;
            }

          
            if (targetWall != null)
            {
                targetWall.OpenWall();
            }
        }
    }
}