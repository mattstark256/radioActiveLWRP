using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private float timeBeforeFirstCorruption = 5f;


    [SerializeField]
    private CorruptionController corruptionController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private Corruption tutorialCorruption;

    [SerializeField]
    private List<Corruption> corruptions;
    private List<Corruption> shuffledCorruptions = new List<Corruption>();
    [SerializeField]
    private List<Transform> corruptionLocations;
    private List<Transform> shuffledCorruptionLocations = new List<Transform>();

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private PopupWindow tutorialPanelPrefab;
    [SerializeField]
    private GameObject gameOverPanelPrefab;


    void Start()
    {
        ShuffleLists();

        StartCoroutine(GameCoroutine());
    }   


    private IEnumerator GameCoroutine()
    {
        playerController.SetInputEnabled(false);
        PopupWindow tutorialPanel = Instantiate(tutorialPanelPrefab, canvas.transform);
        while (tutorialPanel != null) { yield return null; }
        playerController.SetInputEnabled(true);
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(timeBeforeFirstCorruption);

        tutorialCorruption.ActivateCorruption();
        corruptionController.ExpandFromPoint(tutorialCorruption.transform.position, 60, 50);


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
                corruptionController.Dissappear(3);
                break;
            }

            yield return null;
        }


        for (int i = 0; i < shuffledCorruptions.Count; i++)
        {
            Corruption corruption = shuffledCorruptions[i];
            corruption.transform.position = shuffledCorruptionLocations[i].position;
            corruption.ActivateCorruption();
            corruptionController.ExpandFromPoint(corruption.transform.position, 120, 100);

            // Wait until game over or radio delivered
            while (true)
            {
                if (corruptionController.GameIsOver())
                {
                    corruption.MakeUnstoppable();
                    GameOver();
                    yield break;
                }
                else if (!corruption.IsActivated())
                {
                    corruptionController.Dissappear(3);
                    break;
                }

                yield return null;
            }
        }

        Debug.Log("you win!");
    }


    private void GameOver()
    {
        playerController.SetInputEnabled(false);
        Instantiate(gameOverPanelPrefab, canvas.transform);
        Debug.Log("game over!");
    }


    private void ShuffleLists()
    {
        while (corruptions.Count>0)
        {
            int i = Random.Range(0, corruptions.Count);
            shuffledCorruptions.Add(corruptions[i]);
            corruptions.RemoveAt(i);
        }

        for (int i = 0; i < shuffledCorruptions.Count; i++)
        {
            int j = Random.Range(0, corruptionLocations.Count);
            shuffledCorruptionLocations.Add(corruptionLocations[j]);
            corruptionLocations.RemoveAt(j);
        }
    }
}
