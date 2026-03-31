using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("Listen to event channels")]
    public VoidEventChannel onPlayerDeath;

    [SerializeField]
    private GameObject GameOverMenu;

    void Awake()
    {
        GameOverMenu.SetActive(false);
    }

    private void OnEnable()
    {
        onPlayerDeath.OnEventRaised += OnGameOver;
    }

    public void OnGameOver()
    {
        Debug.Log("<size=15><color=#FF0000><b>GameOver!</b></color></size>");
        GameOverMenu.SetActive(true);
    }

    private void OnDisable()
    {
        onPlayerDeath.OnEventRaised -= OnGameOver;
    }
}
