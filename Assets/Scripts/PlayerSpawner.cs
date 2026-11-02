using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] personagens;
    public GameObject[] paineisSkills;
    public MouseOrbit cameraOrbit;
    public MinimapCamera minimapCamera;

    void Awake()
    {
        int escolhido = GameData.personagemEscolhido;

        for (int i = 0; i < personagens.Length; i++)
        {
            personagens[i].SetActive(i == escolhido);
            paineisSkills[i].SetActive(i == escolhido);
        }

        cameraOrbit.player = personagens[escolhido].transform;
        minimapCamera.player = personagens[escolhido].transform;
    }
}