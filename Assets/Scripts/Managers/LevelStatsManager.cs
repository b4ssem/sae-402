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
        if (isLevelActive)
        {
            levelData.timeElapsed += Time.deltaTime;
        }
    }

    public void StopTimer() 
    { 
        isLevelActive = false; 
    }
}