using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CorruptionMaterial))]
public class CorruptionController : MonoBehaviour
{
    private Vector3 corruptionCentre;
    private float corruptionRadius;

    private bool dissappearing = false;
    private bool gameOver = false;

    private CorruptionMaterial corruptionMaterial;


    private void Awake()
    {
        corruptionMaterial = GetComponent<CorruptionMaterial>();
    }


    public void SetRadius(float newRadius)
    {
        corruptionRadius = newRadius;
        corruptionMaterial.SetRadius(newRadius);
    }


    public void SetCentre(Vector3 newCentre)
    {
        corruptionCentre = newCentre;
        corruptionMaterial.SetCentre(newCentre);
    }


    public bool PointIsInsideCorruption(Vector3 point)
    {
        return Vector3.Distance(point, corruptionCentre) < corruptionRadius;
    }


    // Returns whether the corruption has grown so large that the game should end
    public bool GameIsOver()
    {
        return gameOver;
    }


    // Make a new corruption area start expanding from the specified point.
    // It will expand to a radius of radiusAtGameOver over durationUntilGameover seconds, at which point gameOver is set to true.
    public void ExpandFromPoint(Vector3 point, float durationUntilGameover, float radiusAtGameOver)
    {
        StartCoroutine(ExpandFromPointCoroutine(point, durationUntilGameover, radiusAtGameOver));
    }
    private IEnumerator ExpandFromPointCoroutine(Vector3 point, float durationUntilGameover, float radiusAtGameOver)
    {
        // Wait until the previous corruption area has finished dissappearing
        while (dissappearing) { yield return null; }

        SetCentre(point);
        SetRadius(0);

        float f = 0;
        while (f < 1)
        {
            f += Time.deltaTime / durationUntilGameover;
            if (f > 1) f = 1;

            float f2 = f / (1 - f) - f;

            SetRadius(radiusAtGameOver * (f + f2 * 0.1f));

            yield return null;
        }

        gameOver = true;
    }



    // Make the corruption area stop expanding and shrink down to nothing over the specified duration
    public void Dissappear(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(DissappearCoroutine(duration));
    }
    private IEnumerator DissappearCoroutine(float duration)
    {
        dissappearing = true;
        float initialRadius = corruptionRadius;

        float f = 0;
        while (f < 1)
        {
            f += Time.deltaTime / duration;
            if (f > 1) f = 1;

            SetRadius(Mathf.Lerp(initialRadius, 0, f));

            yield return null;
        }

        dissappearing = false;
    }
}
