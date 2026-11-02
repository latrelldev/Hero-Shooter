using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject tabPanel;
    public TextMeshProUGUI fragText;
    public int FragScore = 0;
    private int inimigosVivos;

    public GameObject victoryPanel;
    public GameObject defeatPanel;
    [HideInInspector] public bool jogoAcabou = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        inimigosVivos = FindObjectsByType<EnemyHealth>().Length;
    }

    void Update()
    {
        ScorePanel();
    }

    void ScorePanel()
    {
        tabPanel.SetActive(Keyboard.current[Key.Tab].isPressed);
        fragText.text = "Frags: " + FragScore + "\nInimigos restantes: " + inimigosVivos;
    }

    public void InimigoMorreu()
    {
        FragScore++;
        inimigosVivos--;
        if (inimigosVivos <= 0)
        {
            FimDeJogo(victoryPanel);
        }
    }

    public void Derrota()
    {
        FimDeJogo(defeatPanel);
    }

    void FimDeJogo(GameObject painel)
    {
        if (jogoAcabou) return;

        jogoAcabou = true;
        painel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}