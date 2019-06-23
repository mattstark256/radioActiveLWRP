using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    enum MoveMode
    {
        Speed,
        Time
    }

    [SerializeField] float restTime = 1.0f;
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float moveTime = 5.0f;
    [SerializeField] float tolerance = 0.01f;
    [SerializeField] MoveMode moveMode;

    bool hasReachedTarget;
    bool isActive;
    bool isResting;
    float startTime;
    float totalDistance;
    Transform currentTarget;
    Transform previousTarget;
    List<Transform> targets = new List<Transform>();

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
        targets.AddRange(GetComponentsInChildren<Transform>());
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
                switch(moveMode)
                {
                    case MoveMode.Speed:
                        hasReachedTarget = MoveBySpeed();
                        break;
                    case MoveMode.Time:
                        hasReachedTarget = MoveByTime();
                        break;
                }
            }
        }
    }
 
    bool MoveBySpeed()
    {
        float distanceCovered = (Time.time - startTime) * moveSpeed;
        if(distanceCovered == 0.0f)
            return false;

        float distanceFraction = distanceCovered / totalDistance;
        transform.position = Vector3.Lerp(previousTarget.position, currentTarget.position, distanceFraction);
        transform.rotation = Quaternion.Lerp(previousTarget.rotation, currentTarget.rotation, distanceFraction);

        return distanceFraction > 1.0f - tolerance;
    }   
 
    bool MoveByTime()
    {
        float elapsedTime = (Time.time - startTime);
        if(elapsedTime == 0.0f)
            return false;

        float timeFraction = elapsedTime / moveTime;
        transform.position = Vector3.Lerp(previousTarget.position, currentTarget.position, timeFraction);
        transform.rotation = Quaternion.Lerp(previousTarget.rotation, currentTarget.rotation, timeFraction);

        return timeFraction > 1.0f - tolerance;
    }   

    void GetNextTarget()
    {
        hasReachedTarget = false;
        previousTarget = currentTarget;
        int currentIndex = targets.IndexOf(currentTarget);
        currentIndex = currentIndex < targets.Count - 1 ? currentIndex + 1 : 0;
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
