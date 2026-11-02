using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void VoltarMenu()
    {
        Time.timeScale = 1f;
        GameData.abrirSelecaoMapa = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void TrocarMapa()
    {
        Time.timeScale = 1f;
        GameData.abrirSelecaoMapa = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void Sair()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}