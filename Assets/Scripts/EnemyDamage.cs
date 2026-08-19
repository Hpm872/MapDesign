using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 5;

    void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageObject = collision.gameObject.GetComponent<IDamageable>();

        if (damageObject != null && collision.gameObject.CompareTag("Player"))
        {
            damageObject.TakeDamage(damageAmount);
            Debug.Log("Enemy received damage");
        }
    }
}
