using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionMaterialController : MonoBehaviour
{
    [SerializeField]
    private float borderFraction = 0.5f;
    [SerializeField]
    private float maxBorderWidth = 0.3f;
    [SerializeField]
    private Material corruptionMaterial;
    
    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

    private Vector3 corruptionCentre;
    private float corruptionRadius;


    private void Awake()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Corruptable"))
        {
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.material = corruptionMaterial;
                meshRenderers.Add(meshRenderer);

            }
        }

        Debug.Log(meshRenderers.Count + " corruption meshes found");
    }


    public void SetRadius(float newRadius)
    {
        corruptionRadius = newRadius;

        float borderWidth = newRadius * borderFraction;
        if (borderWidth > maxBorderWidth) borderWidth = maxBorderWidth;

        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.SetFloat("Vector1_37BCA140", newRadius);
            meshRenderer.material.SetFloat("Vector1_5FD0F504", borderWidth);
        }
    }


    public void SetCentre(Vector3 newCentre)
    {
        corruptionCentre = newCentre;
        foreach (MeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.material.SetVector("Vector3_84CC7E50", newCentre);
        }
    }


    public bool PointIsInsideCorruption(Vector3 point)
    {
        return Vector3.Distance(point, corruptionCentre) < corruptionRadius;
    }
}
