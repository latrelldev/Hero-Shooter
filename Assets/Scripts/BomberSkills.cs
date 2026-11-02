using System.Collections;
using UnityEngine;

public class BomberSkills : Skills
{
    public ShootPool shootPool;
    public Grenade granada;
    public Collider playerCollider;
    public float forcaGranada = 15f;

    protected override bool PodeUsar(int skill)
    {
        if (skill == 0)
        {
            return !granada.gameObject.activeSelf;
        }
        return true;
    }

    protected override void Skill1()
    {
        Vector3 direcao = Camera.main.transform.forward + Vector3.up * 0.3f;
        granada.Lancar(shootPool.firePoint.position, direcao.normalized * forcaGranada, playerCollider);
    }

    protected override void Skill2()
    {
        StartCoroutine(Tiroteio());
    }

    IEnumerator Tiroteio()
    {
        Vector3 frente = transform.forward;

        for (int i = 0; i < 16; i++)
        {
            Vector3 direcao = Quaternion.Euler(0f, i * 45f, 0f) * frente;
            shootPool.Disparar(transform.position, direcao);
            yield return new WaitForSeconds(0.125f);
        }
    }
}