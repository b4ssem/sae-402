using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sr;
    public PlayerInvulnerable playerInvulnerable;

    [Tooltip("Please uncheck it on production")]
    public bool needResetHP = true;

    [Header("ScriptableObjects")]
    public PlayerData playerData;

    [Header("Debug")]
    public VoidEventChannel onDebugDeathEvent;

    [Header("Broadcast event channels")]
    public VoidEventChannel onPlayerDeath;
    public Slider slider;

    [Header("Collectible channels")]
    public IntEventChannel onPickUpHeal;

    [Header("Camera Shake")]
    public CameraShakeEventChannel cameraShake;
    public ShakeTypeVariable damageShakeInfo;
    public ShakeTypeVariable deathShakeInfo;

    private void Awake()
    {
        if (needResetHP || playerData.currentHealth <= 0)
        {
            playerData.currentHealth = playerData.maxHealth;
        }
    }

    private void OnEnable()
    {
        onDebugDeathEvent.OnEventRaised += Die;
        onPickUpHeal.OnEventRaised += Heal;
    }

    private void Start()
    {
        if (slider == null) { Debug.LogError("Slider non assigné !"); return; }
        slider.maxValue = playerData.maxHealth;
        slider.value = playerData.currentHealth;
    }

    public void TakeDamage(float damage)
    {
        if (playerInvulnerable.isInvulnerable && damage < float.MaxValue) return;

        playerData.currentHealth -= damage;
        slider.value = playerData.currentHealth;

        if (playerData.currentHealth <= 0)
        {
            Die();
        }
        else
        {
            if (cameraShake != null && damageShakeInfo != null)
            {
                cameraShake.Raise(damageShakeInfo);
            }
            StartCoroutine(playerInvulnerable.Invulnerable());
        }
    }

    public void Heal(int amount)
    {
        float healAmount = playerData.maxHealth * (amount / 100f);
        playerData.currentHealth = Mathf.Min(
            playerData.currentHealth + healAmount,
            playerData.maxHealth
        );
        slider.value = playerData.currentHealth;
    }

    private void Die()
    {
        if (cameraShake != null)
        {
            cameraShake.Raise(deathShakeInfo != null ? deathShakeInfo : damageShakeInfo);
        }

        onPlayerDeath?.Raise();
        GetComponent<Rigidbody2D>().simulated = false;
        transform.Rotate(0f, 0f, 45f);
        animator.SetTrigger("Death");
    }

    public void OnPlayerDeathAnimationCallback()
    {
        sr.enabled = false;
    }

    private void OnDisable()
    {
        onDebugDeathEvent.OnEventRaised -= Die;
        onPickUpHeal.OnEventRaised -= Heal;
    }
}