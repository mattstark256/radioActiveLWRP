using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionTester : MonoBehaviour
{
    [SerializeField]
    private CorruptionController corruption;

    [SerializeField]
    private Vector2 radiusRange;
    [SerializeField]
    private Vector3 centre;
    [SerializeField]
    private float duration = 5;

    [SerializeField]
    private Transform corruptionCentreTransform;

    private void Start()
    {
        //corruption.SetCentre(corruptionCentreTransform.position);

        StartCoroutine(TestCorruption());
    }

    //void Update()
    //{
    //    corruption.SetRadius(Mathf.Lerp(radiusRange.x, radiusRange.y, (Mathf.Sin(Time.time * Mathf.PI * 2 / duration) + 1) / 2));
    //}

    private IEnumerator TestCorruption()
    {
        yield return new WaitForSeconds(1);

        corruption.ExpandFromPoint(corruptionCentreTransform.position, 10, 20);

        yield return new WaitForSeconds(8);


        corruption.Dissappear(1);
        corruption.ExpandFromPoint(corruptionCentreTransform.position + Vector3.forward * 9, 10, 20);

    }
}
