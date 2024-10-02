using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy_EarthColumn : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float forwardSpeed = 5f;    
    [SerializeField] float raiseHeight = 5f;     
    [SerializeField] float raiseDuration = 2f;   
    [SerializeField] float waitAfterRaise = 1f;  

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(RaiseAndMove());
    }

    IEnumerator RaiseAndMove()
    {
        yield return StartCoroutine(SetColumnHeight());

        if (waitAfterRaise > 0)
        {
            yield return new WaitForSeconds(waitAfterRaise);
        }

        StartMovingForward();
    }

    IEnumerator SetColumnHeight()
    {
        Vector3 startScale = transform.localScale;
        Vector3 targetPosition = new Vector3(startScale.x, startScale.y + raiseHeight, startScale.z);
        float elapsedTime = 0f;

        // Smoothly raise the column to the target height over the given duration
        while (elapsedTime < raiseDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / raiseDuration;

            // Lerp the position towards the target height
            transform.localScale = Vector3.Lerp(startScale, targetPosition, t);

            // Wait until the next frame before continuing
            yield return null;
        }

        // Ensure final height is exactly set to the target after the loop
        transform.localScale = targetPosition;
    }

    // Function to start moving the column forward
    void StartMovingForward()
    {
        // Apply forward velocity to the column's rigidbody
        rigidbody.velocity = transform.forward * forwardSpeed;
    }
}
