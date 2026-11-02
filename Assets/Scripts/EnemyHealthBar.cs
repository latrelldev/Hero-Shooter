using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fill;

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
    }

    public void Atualizar(float vida, float vidaMaxima)
    {
        fill.fillAmount = vida / vidaMaxima;

        if (vida > 50f)
        {
            fill.color = Color.green;
        }
        else if (vida > 20f)
        {
            fill.color = Color.yellow;
        }
        else
        {
            fill.color = Color.red;
        }
    }
}