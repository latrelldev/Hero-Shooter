using UnityEngine;

public class SpeedsterSkills : Skills
{
    public PlayerMove playerMove;
    public ShootPool shootPool;
    public float dashForca = 20f;
    public float dashDuracao = 0.2f;

    protected override void Skill1()
    {
        playerMove.Dash(dashForca, dashDuracao);
    }

    protected override void Skill2()
    {
        shootPool.fireRate = 0.25f;
        shootPool.damage = 15f;
        Invoke("FimFrenesi", 2.5f);
    }

    void FimFrenesi()
    {
        shootPool.fireRate = 0.5f;
        shootPool.damage = 20f;
    }
}