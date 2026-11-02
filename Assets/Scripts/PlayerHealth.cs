using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    [HideInInspector] public float currentHealth;
    public Image healthBar;

    public GameObject escudo;
    public bool invencivel = false;

    void Start()
    {
        currentHealth = maxHealth;
        AtualizarBarra();
    }

    public void TakeDamage(float amount)
    {
        if (invencivel || currentHealth <= 0f) return;

        if (escudo != null && escudo.activeSelf)
        {
            escudo.SetActive(false);
            return;
        }

        currentHealth -= amount;
        AtualizarBarra();
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public bool Heal(float amount)
    {
        if (currentHealth >= maxHealth) return false;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        AtualizarBarra();
        return true;
    }

    void AtualizarBarra()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    void Die()
    {
        GameManager.instance.Derrota();
    }
}