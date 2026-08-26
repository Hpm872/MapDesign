using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int bumpDamage = 20;
   
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HealthSystem enemyHealth = collision.gameObject.GetComponent<HealthSystem>();
            if (enemyHealth == null) enemyHealth = collision.gameObject.GetComponentInParent<HealthSystem>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(bumpDamage);
                Debug.Log("Daño al enemigo");
            }
        }
    }
}