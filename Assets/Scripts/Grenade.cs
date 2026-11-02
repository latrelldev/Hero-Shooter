using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public Rigidbody rb;
    public Collider col;
    public float damage = 70f;
    public float radius = 4f;

    public void Lancar(Vector3 posicao, Vector3 velocidade, Collider dono)
    {
        transform.position = posicao;
        gameObject.SetActive(true);
        Physics.IgnoreCollision(col, dono);
        rb.linearVelocity = velocidade;
        rb.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        Explodir();
    }

    void Explodir()
    {
        Collider[] atingidos = Physics.OverlapSphere(transform.position, radius);
        List<EnemyHealth> jaAtingidos = new List<EnemyHealth>();

        foreach (Collider atingido in atingidos)
        {
            EnemyHealth enemy = atingido.GetComponentInParent<EnemyHealth>();

            if (enemy != null && !jaAtingidos.Contains(enemy))
            {
                jaAtingidos.Add(enemy);
                enemy.TakeDamage(damage);
            }
        }
        gameObject.SetActive(false);
    }
}