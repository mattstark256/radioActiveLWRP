using NaughtyAttributes;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float restTime = 1.0f;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float tolerance = 0.01f;

    bool hasReachedTarget;
    bool isActive;
    bool isResting;
    float startTime;
    float totalDistance;
    Transform currentTarget;
    Transform previousTarget;
    Transform[] targets;

    [Button]
    public void Activate()
    {
        isActive = true;
        GetNextTarget();
        startTime = Time.time;
    }

    [Button]
    public void Deactivate()
    {
        isActive = false;
    }

    [Button]
    public void GoToNextPosition()
    {
        Activate();
        Deactivate();
    }

    void Awake()
    {
        GameObject targetParent = new GameObject(gameObject.name + " targets");
        targets = GetComponentsInChildren<Transform>();
        targets[0] = new GameObject("StartTarget").transform;
        targets[0].position = transform.position;
        targets[0].rotation = transform.rotation;
        currentTarget = targets[0];
        previousTarget = targets[0];
        startTime = Time.time;
        totalDistance = Vector3.Distance(currentTarget.position, previousTarget.position);
        foreach(Transform target in targets)
        {
            target.parent = targetParent.transform;
        }
    }

    void Update()
    {
        if(!isResting)
        {
            if(isActive && hasReachedTarget)
            {
                GetNextTarget();
                StartCoroutine(Rest());
            }
            else
            {
                hasReachedTarget = MoveTowardsTarget();
            }
        }
    }
 
    bool MoveTowardsTarget()
    {
        float distanceCovered = (Time.time - startTime) * speed;
        if(distanceCovered == 0.0f)
            return false;

        float distanceFraction = distanceCovered / totalDistance;
        transform.position = Vector3.Lerp(previousTarget.position, currentTarget.position, distanceFraction);
        transform.rotation = Quaternion.Lerp(previousTarget.rotation, currentTarget.rotation, distanceFraction);

        return distanceFraction > 1.0f - tolerance;
    }   

    void GetNextTarget()
    {
        hasReachedTarget = false;
        previousTarget = currentTarget;
        int currentIndex = ArrayUtility.IndexOf(targets, currentTarget);
        currentIndex = currentIndex < targets.Length - 1 ? currentIndex + 1 : 0;
        currentTarget = targets[currentIndex];
        totalDistance = Vector3.Distance(currentTarget.position, previousTarget.position);
    }

    IEnumerator Rest()
    {
        isResting = true;
        yield return new WaitForSeconds(restTime);
        isResting = false;
        startTime = Time.time;
    }
}
