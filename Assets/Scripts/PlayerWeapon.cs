using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int attackDamage = 15;

    void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageObject = collision.gameObject.GetComponent<IDamageable>();

        if (damageObject != null && collision.gameObject.CompareTag("Enemy"))
        {
            damageObject.TakeDamage(attackDamage);
            Debug.Log("Enemy received damage" + attackDamage);
        }
    }
}
