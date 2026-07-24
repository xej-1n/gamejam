using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingManager : MonoBehaviour
{
    [Header("UI 설정")]
    public CanvasGroup endingCanvasGroup; 
    public float fadeDuration = 2.0f;    

    [Header("씬 설정")]
    public string titleSceneName = "Title"; 

    private bool isEndingStarted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.CompareTag("Player") && !isEndingStarted)
        {
            isEndingStarted = true;

        
            var character = collision.GetComponent<EndingCharacterController>();
            if (character != null)
            {
                character.enabled = false;
                var rb = collision.GetComponent<Rigidbody2D>();
                if (rb != null) rb.linearVelocity = Vector2.zero; 
            }


            StartCoroutine(ShowEndingRoutine());
        }
    }

    private IEnumerator ShowEndingRoutine()
    {

        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            endingCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }
        endingCanvasGroup.alpha = 1;

        yield return new WaitForSeconds(5.0f);


        SceneManager.LoadScene(titleSceneName);
    }
}