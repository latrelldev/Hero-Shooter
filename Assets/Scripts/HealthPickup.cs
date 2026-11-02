using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public static List<HealthPickup> todas = new List<HealthPickup>();

    public float cura = 30f;
    public float cooldown = 10f;
    public GameObject visual;
    public bool disponivel = true;

    void OnEnable()
    {
        todas.Add(this);
    }

    void OnDisable()
    {
        todas.Remove(this);
    }

    void OnTriggerStay(Collider other)
    {
        if (!disponivel) return;

        bool curou = false;
        PlayerHealth player = other.GetComponentInParent<PlayerHealth>();
        EnemyHealth enemy = other.GetComponentInParent<EnemyHealth>();

        if (player != null)
        {
            curou = player.Heal(cura);
        }
        else if (enemy != null)
        {
            curou = enemy.Heal(cura);
        }

        if (curou)
        {
            disponivel = false;
            visual.SetActive(false);
            Invoke("Reativar", cooldown);
        }
    }

    void Reativar()
    {
        disponivel = true;
        visual.SetActive(true);
    }
}