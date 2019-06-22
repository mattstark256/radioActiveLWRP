using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private CorruptionController corruptionController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Corruption tutorialCorruption;
    [SerializeField]
    private Corruption corruptions;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private PopupWindow tutorialPanelPrefab;
    [SerializeField]
    private GameObject gameOverPanelPrefab;


    void Start()
    {
        StartCoroutine(GameCoroutine());
    }   


    private IEnumerator GameCoroutine()
    {
        playerController.SetInputEnabled(false);
        PopupWindow tutorialPanel = Instantiate(tutorialPanelPrefab, canvas.transform);
        while (tutorialPanel != null) { yield return null; }
        playerController.SetInputEnabled(true);
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(1);

        tutorialCorruption.ActivateCorruption();
        corruptionController.ExpandFromPoint(tutorialCorruption.transform.position, 20, 30);


        // Wait until game over or radio delivered
        while (true)
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
        Instantiate(gameOverPanelPrefab, canvas.transform);
        Debug.Log("game over!");
    }

}
