using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        this.progressBar.value = 0;
        this.levelText.text = "Level " + (ChunkManager.instance.GetLevels() + 1).ToString();

        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    private void GameStateChangedCallBack(GameManager.GameState gamestate)
    {
        if(gamestate == GameManager.GameState.GameOver)
            this.ShowGameOverPanel();
        else if (gamestate == GameManager.GameState.LevelComplete)
            this.ShowLevelCompletePanel();
    }

    private void Update()
    {
        this.UpdateProgressBar();   
    }

    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(0);
    }  
    
    public void ShowGameOverPanel()
    {
        this.gamePanel.SetActive(false);
        this.gameOverPanel.SetActive(true);
    }

    public void ShowLevelCompletePanel()
    {
        this.gamePanel.SetActive(false);
        this.levelCompletePanel.SetActive(true);
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);
        this.menuPanel.SetActive(false);
        this.gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState()) return;
        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinish();
        this.progressBar.value = progress;
    }
}
