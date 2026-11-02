using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float consumo = 10f;
    public float recarga = 10f;
    public Image staminaBar;

    private float stamina;
    private bool correndo;

    void Start()
    {
        stamina = maxStamina;
    }

    void Update()
    {
        if (correndo)
        {
            stamina -= consumo * Time.deltaTime;
        }
        else
        {
            stamina += recarga * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        staminaBar.fillAmount = stamina / maxStamina;
        correndo = false;
    }

    public bool Correr()
    {
        if (stamina <= 0f) return false;

        correndo = true;
        return true;
    }
}