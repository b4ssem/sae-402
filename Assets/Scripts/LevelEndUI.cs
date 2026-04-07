using UnityEngine;
using TMPro;

public class LevelEndUI : MonoBehaviour
{
    [Header("References")]
    public LevelData levelData; 
    public LevelTimer levelTimer; 
    
    [Header("Événement pour changer de scène")]
    public StringEventChannel onLevelEnded;

    [Header("Text UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI jumpsText;

    private string levelToLoad;

    public void ShowEndScreen(string nextLevelName)
    {
        levelToLoad = nextLevelName;

        if (levelTimer != null) levelTimer.StopTimer();

        timeText.text = "Temps : " + levelData.GetFormattedTime();
        enemiesText.text = "Ennemis vaincus : " + levelData.enemiesKilled;
        jumpsText.text = "Sauts effectués : " + levelData.jumpCount;

        Time.timeScale = 0f; 

        gameObject.SetActive(true);
    }

    public void OnClickNextLevel()
    {
        Time.timeScale = 1f;
        onLevelEnded.Raise(levelToLoad);
    }
}