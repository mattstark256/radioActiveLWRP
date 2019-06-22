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
    private AudioController audioController;

    [SerializeField]
    private List<Corruption> corruptions;
    private List<Corruption> shuffledCorruptions = new List<Corruption>();
    [SerializeField]
    private List<Transform> corruptionLocations;
    private List<Transform> shuffledCorruptionLocations = new List<Transform>();

    [SerializeField]
    private Transform popupParent;
    [SerializeField]
    private GameObject tutorialPanelPrefab;
    [SerializeField]
    private GameObject gameOverPanelPrefab;
    [SerializeField]
    private GameObject winPanelPrefab;
    [SerializeField]
    private GameObject menuPrefab;
    private GameObject menu;
    private bool menuOpen = false;
    private bool inputLockedBeforeMenuOpened;

    private bool inputLocked = false;


    void Start()
    {
        ShuffleLists();

        StartCoroutine(GameCoroutine());
    }


    private void Update()
    {
        if (menuOpen && menu == null)
        {
            SetInputLocked(inputLockedBeforeMenuOpened);
            menuOpen = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu == null)
            {
                inputLockedBeforeMenuOpened = inputLocked;
                SetInputLocked(true);
                menu = Instantiate(menuPrefab, popupParent);
                menuOpen = true;
            }
            else
            {
                SetInputLocked(inputLockedBeforeMenuOpened);
                Destroy(menu);
                menuOpen = false;
            }
        }
    }


    private IEnumerator GameCoroutine()
    {
        SetInputLocked(true);
        GameObject tutorialPanel = Instantiate(tutorialPanelPrefab, popupParent);
        while (tutorialPanel != null) { yield return null; }
        SetInputLocked(false);

        yield return new WaitForSeconds(1);

        audioController.PlayVoiceOver();

        yield return new WaitForSeconds(4);

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
                corruptionController.Dissappear(2);
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
                    corruptionController.Dissappear(2);
                    break;
                }

                yield return null;
            }
        }

        Win();
    }


    private void GameOver()
    {
        SetInputLocked(true);
        Instantiate(gameOverPanelPrefab, popupParent);
    }


    private void Win()
    {
        SetInputLocked(true);
        Instantiate(winPanelPrefab, popupParent);
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


    private void SetInputLocked(bool locked)
    {
        inputLocked = locked;
        Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
        playerController.SetInputEnabled(!locked);
    }
}
