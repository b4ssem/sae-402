using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneManager : MonoBehaviour
{
    public bool isDebugConsoleOpened = false;

    [Header("Listen to events")]
    public StringEventChannel onLevelEnded;
    public BoolEventChannel onDebugConsoleOpenEvent;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1f;
    }

    private void Update() {
        #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartLevel();
            }
        #endif
    }

    private void OnEnable()
    {
        onLevelEnded.OnEventRaised += LoadScene;
        onDebugConsoleOpenEvent.OnEventRaised += OnDebugConsoleOpen;
    }

    public void LoadScene(string sceneName)
    {
        if (UtilsScene.DoesSceneExist(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log($"Unknown scene named {sceneName}. Please add the scene to the build settings.");
        }
    }

    public void LoadScene(int sceneIndex)
    {
        if (UtilsScene.DoesSceneExist(sceneIndex))
        {
            SceneManager.LoadScene(sceneIndex);
        }
        else
        {
            Debug.Log($"Unknown scene with index {sceneIndex}. Please add the scene to the build settings.");
        }
    }   

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void RestartLastCheckpoint()
    {
        Debug.Log("RestartLastCheckpoint");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = true;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.playerData.currentHealth = playerHealth.playerData.maxHealth;
            playerHealth.slider.value = playerHealth.playerData.currentHealth;
        }

        PlayerSpawn playerSpawn = player.GetComponent<PlayerSpawn>();
        if (playerSpawn != null)
        {
            player.transform.position = playerSpawn.currentSpawnPosition;
        }
        
        GameObject gameOverMenu = GameObject.Find("Game Over Menu");
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(false);
        }

        player.transform.rotation = Quaternion.Euler(0f, player.transform.rotation.eulerAngles.y, 0f);

        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.ToggleState(false); 
            playerMovement.isStunned = false;
        }
        SpriteRenderer sr = player.GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = true;
        }
        
        PauseManager pauseManager = FindFirstObjectByType<PauseManager>();
        if (pauseManager != null)
        {
            pauseManager.Resume();
        }

    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnDisable()
    {
        onLevelEnded.OnEventRaised -= LoadScene;
        onDebugConsoleOpenEvent.OnEventRaised -= OnDebugConsoleOpen;
    }

    private void OnDebugConsoleOpen(bool debugConsoleOpened)
    {
        isDebugConsoleOpened = debugConsoleOpened;
    }
}
