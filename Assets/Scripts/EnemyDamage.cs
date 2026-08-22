using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 5; 
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageableObj = collision.gameObject.GetComponent<IDamageable>();

        if (damageableObj != null && collision.gameObject.CompareTag("Player"))
        {
            damageableObj.TakeDamage(damageAmount);
            Debug.Log("¡El enemigo lastimó al jugador!");
        }
    }
}