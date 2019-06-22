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
    [SerializeField]
    private PlayerController playerController;

    
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
                GameOver();
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


    private void GameOver()
    {
        playerController.SetInputEnabled(false);
        Debug.Log("game over!");
    }

}
