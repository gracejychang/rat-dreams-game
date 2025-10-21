using UnityEngine;

public class GameStateUI : MonoBehaviour
{
    public static GameStateUI Instance;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ShowPanel(GameState.Start);
    }

    public void ShowPanel(GameState state)
    {
        startPanel.SetActive(state == GameState.Start);
        pausePanel.SetActive(state == GameState.Pause);
        gameOverPanel.SetActive(state == GameState.GameOver);
    }

    public void HideAllPanels()
    {
        startPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }
}
