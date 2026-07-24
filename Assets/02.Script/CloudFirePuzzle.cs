using System.Collections;
using UnityEngine;

public class CloudFirePuzzle : MonoBehaviour
{
    [Header("연출 설정")]
    public GameObject exclamationMark;     
    public ParticleSystem rainParticles;   

    private bool isSolving = false;
    private DraggableBlock draggableScript;

    void Start()
    {
        draggableScript = GetComponent<DraggableBlock>();


        if (exclamationMark != null) exclamationMark.SetActive(false);
        if (rainParticles != null) rainParticles.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire") && !isSolving)
        {
            StartCoroutine(ExtinguishFireRoutine(collision.gameObject));
        }
    }

    private IEnumerator ExtinguishFireRoutine(GameObject fireObject)
    {
        isSolving = true;

       
        if (draggableScript != null) draggableScript.enabled = false;
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null) player.SetHoldingBlock(true);

      
        if (exclamationMark != null) exclamationMark.SetActive(true);
        yield return new WaitForSeconds(1f);
        if (exclamationMark != null) exclamationMark.SetActive(false);

     
        if (rainParticles != null) rainParticles.Play();
        yield return new WaitForSeconds(3f);

  
        if (rainParticles != null) rainParticles.Stop();

        Animator fireAnim = fireObject.GetComponent<Animator>();
        if (fireAnim != null)
        {
            fireAnim.SetTrigger("Extinguish");
            yield return new WaitForSeconds(0.5f); 
        }

    
        Destroy(fireObject);
        if (player != null) player.SetHoldingBlock(false);

   
        isSolving = false;
    }
}