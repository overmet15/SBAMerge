using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve curve;
    private Vector3 originalPosition;
    [SerializeField] private bool isCamera;
    private Rigidbody2D rb;

    void Start()
    {
        originalPosition = transform.position;
        if (!isCamera) rb = GetComponent<Rigidbody2D>();
    }

    public void GoShake(bool isCam = false)
    {
        if (isCam == isCamera) StartCoroutine(DoShake());
    }

    IEnumerator DoShake()
    {
        float elapsedTime = 0;
        Vector3 targetPosition = originalPosition;
        while (elapsedTime < duration)
        {
            float strength = curve.Evaluate(elapsedTime / duration);
            Vector3 randomOffset = Random.insideUnitSphere * strength;
            targetPosition = originalPosition + randomOffset;

            if (isCamera) transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f); // Adjust the speed as needed
            else rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
}
