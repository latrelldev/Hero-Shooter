using UnityEngine;

public class TankSkills : Skills
{
    public GameObject escudo;
    public PlayerHealth health;
    public Renderer corpo;
    public Color corInvencivel = Color.yellow;
    private Color corNormal;

    void Start()
    {
        corNormal = corpo.material.color;
        escudo.SetActive(false);
    }

    protected override bool PodeUsar(int skill)
    {
        if (skill == 0)
        {
            return !escudo.activeSelf;
        }
        return true;
    }

    protected override void Skill1()
    {
        escudo.SetActive(true);
    }

    protected override void Skill2()
    {
        health.invencivel = true;
        corpo.material.color = corInvencivel;
        Invoke("FimInvencivel", 3f);
    }

    void FimInvencivel()
    {
        health.invencivel = false;
        corpo.material.color = corNormal;
    }
}