using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Header("Reference")]
    public LevelData levelData;

    private bool isLevelActive = true;

    private void Start()
    {
        levelData.ResetData();
    }

    private void Update()
    {
        if (levelData.isActive)
        {
            levelData.timeElapsed += Time.deltaTime;
        }
    }

    public void StopTimer() 
    { 
        levelData.StopTimer();
    }
}