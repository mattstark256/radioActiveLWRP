using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Corruption tutorialCorruption;
    [SerializeField]
    private Corruption corruptions;

    [SerializeField]
    private CorruptionController corruptionController;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(GameCoroutine());
    }
    

    private IEnumerator GameCoroutine()
    {
        yield return new WaitForSeconds(1);

        tutorialCorruption.ActivateCorruption();
        corruptionController.ExpandFromPoint(tutorialCorruption.transform.position, 20, 30);


        // Wait until game over or radio delivered
        while(true)
        {
            if (corruptionController.GameIsOver())
            {
                tutorialCorruption.MakeUnstoppable();
                Debug.Log("game over!");
                yield break;
            }
            else if (!tutorialCorruption.IsActivated())
            {
                corruptionController.Dissappear(2);
                yield break;
            }

            yield return null;
        }
    }

}
