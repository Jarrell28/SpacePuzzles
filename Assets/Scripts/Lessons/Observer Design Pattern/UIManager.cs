using TMPro;
using UnityEngine;


public class UIManager: MonoBehaviour
{
    public TMP_Text healthDisplay;
    public GameObject gameOverPanel;
    public HealthScript playerHealth;

    void Awake()
    {
        playerHealth.OnHealthChanged += UpdateHealthDisplay;
        playerHealth.OnDeath += ShowGameOver;
    }

    public void UpdateHealthDisplay(float currentHealth)
    {
        healthDisplay.text = "Health: " + currentHealth.ToString("F0");
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        GameManager.instance.ChangeState(GameManager.GameState.GameOver);
    }
}