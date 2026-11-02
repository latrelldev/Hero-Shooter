using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject mapPanel;
    public GameObject characterPanel;
    public GameObject tutorialPanel;

    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (GameData.abrirSelecaoMapa)
        {
            MostrarPainel(mapPanel);
        }
        else
        {
            MostrarPainel(mainPanel);
        }
        GameData.abrirSelecaoMapa = false;
    }

    public void Jogar()
    {
        MostrarPainel(mapPanel);
    }

    public void Tutorial()
    {
        MostrarPainel(tutorialPanel);
    }

    public void Voltar()
    {
        MostrarPainel(mainPanel);
    }

    public void EscolherMapa(string nomeCena)
    {
        GameData.mapaEscolhido = nomeCena;
        MostrarPainel(characterPanel);
    }

    public void EscolherPersonagem(int personagem)
    {
        GameData.personagemEscolhido = personagem;
        SceneManager.LoadScene(GameData.mapaEscolhido);
    }

    void MostrarPainel(GameObject painel)
    {
        mainPanel.SetActive(false);
        mapPanel.SetActive(false);
        characterPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        painel.SetActive(true);
    }
}