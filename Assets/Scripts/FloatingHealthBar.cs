using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Image healthFill;
    private int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthSystem enemyHealthSystem = GetComponentInParent<HealthSystem>();

        if (enemyHealthSystem != null)
        {
            maxHealth = enemyHealthSystem.maxHealth;
        }
    }

    public void UpdateBar(int currentHealth)
    {
        if (healthFill != null && maxHealth > 0)
        {
            healthFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
