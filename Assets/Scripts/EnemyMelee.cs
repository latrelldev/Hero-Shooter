using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float damage = 20f;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;

    public void Atacar(PlayerHealth alvo)
    {
        if (Time.time < nextAttackTime) return;

        alvo.TakeDamage(damage);
        nextAttackTime = Time.time + attackRate;
    }
}