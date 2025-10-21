using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetState(GameState.Start);
    }

    private void Update()
    {
        switch(CurrentState) {
            case GameState.Start:
                InventoryUI.Instance?.HideInventoryPanel();
                handleGameStart();
                break;
            case GameState.Pause:
                InventoryUI.Instance?.HideInventoryPanel();
                handleGamePause();
                break;
            case GameState.GameOver:
                InventoryUI.Instance?.HideInventoryPanel();
                handleGameOver();
                break;
            case GameState.Playing:
            default:
                InventoryUI.Instance?.InitializeUI();
                handleGamePlaying();
                break;
        }
        GameStateUI.Instance?.ShowPanel(CurrentState);
    }

    // handle key updates
    private void handleGameStart()
    {
        // User presses 'N' to start a new game
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            StartNewGame();
        }

        // User presses 'L' to load an existing game
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadGame();
        }
    }

    private void handleGamePause()
    {
        // Press 'R' to resume
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResumeGame();
        }

        // Press 'S' to save & quit
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            SaveAndQuit();
        }
    }

    private void handleGameOver()
    {
        // Press 'R' to restart the game
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

        private void handleGamePlaying()
    {
        // Gameplay logic, e.g. press 'P' to pause
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            PauseGame();
        }
    }


    // load/start/resume/quit game
    public void SetState(GameState newState)
    {
        CurrentState = newState;
        Time.timeScale = (newState == GameState.Playing) ? 1f : 0f;
    }

    public void StartNewGame()
    {
        InventoryManager.Instance.ResetInventory();
        SetState(GameState.Playing);
    }

    public void LoadGame()
    {
        InventoryManager.Instance.LoadFromFile();
        SetState(GameState.Playing);
    }

    public void PauseGame()
    {   
        SetState(GameState.Pause);
    }

    public void ResumeGame()
    {   
        SetState(GameState.Playing);
    }

    public void SaveAndQuit()
    {
        InventoryManager.Instance.SaveToFile();
        SetState(GameState.Start);
        DestroyAllGameplayObjects();
    }

    public void GameOver()
    {
        InventoryManager.Instance.ResetInventory();
        SetState(GameState.GameOver);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        InventoryManager.Instance.ResetInventory();
        SetState(GameState.Playing);
    }

    private void DestroyAllGameplayObjects()
    {
        string[] tagsToClear = { "Food", "Obstacle", "Attack" };

        foreach (string tag in tagsToClear)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objects)
            {
                if (obj.activeInHierarchy)
                {
                    Destroy(obj);
                }
            }
        }
    }

}
