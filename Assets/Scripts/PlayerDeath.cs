using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public MonoBehaviour[] scriptsToDisable;

    private HealthSystem healthSystem;
    private bool isDead = false;

    void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    void OnEnable()
    {
        if (healthSystem != null) healthSystem.onDeath.AddListener(HandleDeath);
    }

    void OnDisable()
    {
        if (healthSystem != null) healthSystem.onDeath.RemoveListener(HandleDeath);
    }

    void HandleDeath()
    {
        if (isDead) return;
        isDead = true;

        foreach (var script in scriptsToDisable)
        {
            if (script != null) script.enabled = false;
        }

        if (UIManager.Instance != null) UIManager.Instance.ShowGameOver();
    }

    public void ResetState()
    {
        isDead = false;
    } 
}
