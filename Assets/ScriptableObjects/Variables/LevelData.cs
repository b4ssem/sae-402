using UnityEngine;

[CreateAssetMenu(fileName = "New LevelData", menuName = "ScriptableObjects/Variable/LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Statistiques")]
    public float timeElapsed;
    public int enemiesKilled;
    public int jumpCount;

    public void ResetData()
    {
        timeElapsed = 0f;
        enemiesKilled = 0;
        jumpCount = 0;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(timeElapsed - minutes * 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}