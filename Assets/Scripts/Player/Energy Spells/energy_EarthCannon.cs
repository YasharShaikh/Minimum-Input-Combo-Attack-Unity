using System.Collections;
using UnityEngine;

public class energy_EarthCannon : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float forwardSpeed;
    [SerializeField] float raiseHeight;
    [SerializeField] float raiseDuration;
    [SerializeField] float waitAfterRaise = 1f;

    Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(RaiseToPosition());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }




    IEnumerator RaiseToPosition()
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
            transform.position = Vector3.Lerp(startScale, targetPosition, t);

            // Wait until the next frame before continuing
            yield return null;
        }
        coroutine = null;
        // Ensure final height is exactly set to the target after the loop
        transform.position = targetPosition;
    }
}
