using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player Healthbar")]
    public Image playerHealthFill;
    public int playerMaxHealth = 100;

    [Header("Boss Healthbar")]
    public Image bossHealthFill;
    public int bossMaxHealth = 200;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void UpdatePlayerHealth(int currentHealth)
    {
        if (playerHealthFill != null)
        {
            playerHealthFill.fillAmount = (float)currentHealth / playerMaxHealth;
        }
    }

    public void UpdateBossHealth(int currentHealth)
    {
        if (bossHealthFill != null)
        {
            bossHealthFill.fillAmount = (float)currentHealth / bossMaxHealth;
        }
    }

    /*
    public void ShowGameOver() 
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void ShowVictory()
    {
        if (victoryPanel != null) victoryPanel.SetActive(true);
    } 
    */
}
