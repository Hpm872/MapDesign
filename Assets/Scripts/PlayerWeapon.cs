using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int bumpDamage = 20;
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageableObj = collision.gameObject.GetComponent<IDamageable>();

        if (damageableObj != null && collision.gameObject.CompareTag("Enemy"))
        {
            damageableObj.TakeDamage(bumpDamage);
            Debug.Log("¡El jugador chocó y lastimó al enemigo!");
        }
    }
}