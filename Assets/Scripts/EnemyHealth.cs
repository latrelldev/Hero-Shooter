using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;
    public EnemyNavMesh ai;
    public EnemyHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.Atualizar(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0f) return;

        currentHealth -= amount;
        healthBar.Atualizar(currentHealth, maxHealth);
        ai.Alertar();
        ai.ChamarReforcos();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public bool Heal(float amount)
    {
        if (currentHealth >= maxHealth) return false;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar.Atualizar(currentHealth, maxHealth);
        return true;
    }

    void Die()
    {
        GameManager.instance.InimigoMorreu();
        gameObject.SetActive(false);
    }
}