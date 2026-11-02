using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    public float[] cooldowns = { 3f, 10f };
    public Image[] skill_Image;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Keyboard.current[Key.Q].wasPressedThisFrame)
        {
            TentarUsar(0);
        }
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            TentarUsar(1);
        }

        for (int i = 0; i < skill_Image.Length; i++)
        {
            if (skill_Image[i].fillAmount < 1f)
            {
                skill_Image[i].fillAmount += Time.deltaTime / cooldowns[i];
            }
        }
    }

    void TentarUsar(int skill)
    {
        if (skill_Image[skill].fillAmount >= 1f && PodeUsar(skill))
        {
            skill_Image[skill].fillAmount = 0f;
            if (skill == 0)
            {
                Skill1();
            }
            else
            {
                Skill2();
            }
        }
    }

    protected virtual bool PodeUsar(int skill)
    {
        return true;
    }

    protected virtual void Skill1()
    {
    }

    protected virtual void Skill2()
    {
    }
}