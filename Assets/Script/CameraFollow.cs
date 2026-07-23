using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 1.5f, -10f);
    public float smoothSpeed = 5f;

    [Header("카메라 튕김(미리보기) 설정")]
    public float leadDistance = 3f;
    public float leadSpeed = 3f;
    private float currentLead = 0f;

    void LateUpdate()
    {

        float inputX = Input.GetAxisRaw("Horizontal");


        float targetLead = inputX * leadDistance;


        currentLead = Mathf.Lerp(currentLead, targetLead, leadSpeed * Time.deltaTime);


        Vector3 desiredPosition = target.position + offset + new Vector3(currentLead, 0, 0);


        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}