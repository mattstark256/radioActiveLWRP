using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionMaterialTester : MonoBehaviour
{
    [SerializeField]
    private CorruptionMaterialController corruption;

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
        corruption.SetCentre(corruptionCentreTransform.position);
    }

    void Update()
    {
        corruption.SetRadius(Mathf.Lerp(radiusRange.x, radiusRange.y, (Mathf.Sin(Time.time * Mathf.PI * 2 / duration) + 1) / 2));
    }
}
