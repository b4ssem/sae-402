using System.Collections;
using UnityEngine;

// Changement du nom de la classe pour éviter le conflit avec l'autre fichier
public class FallingPlatformTest : MonoBehaviour
{
    [Tooltip("Delay before the platform starts to fall")]
    public float fallDelay = 1.5f;
    private float destroyDelay = 2.25f;
    private Vector2 startPosition = Vector2.zero;

    public Rigidbody2D rb;
    public Animator animator;
    public ParticleSystem particleEmitter;

    private bool isFalling = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnBecameVisible()
    {
        if (particleEmitter != null)
            particleEmitter.Play();
    }

    private void OnBecameInvisible()
    {
        if (particleEmitter != null)
            particleEmitter.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // MODIFICATION : On ne lie l'objet à la plateforme QUE si c'est le joueur
        // Cela empêche la Tilemap de disparaître avec la plateforme
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);

            if (collision.relativeVelocity.y <= 0.1f)
            {
                PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
                if (pm != null)
                {
                    pm.isOnFallingPlatform = true;
                }

                if (!isFalling)
                {
                    StartCoroutine(Fall());
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            
            PlayerMovement pm = collision.gameObject.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.isOnFallingPlatform = false;
            }
        }
    }

    private IEnumerator Fall()
    {
        float current = 0;
        float duration = 0.15f;
        isFalling = true;

        while (current <= 1)
        {
            rb.MovePosition(
                Vector2.Lerp(
                    startPosition, 
                    startPosition + Vector2.down * 0.3f, 
                    Mathf.PingPong(current, 0.5f)
                )
            );
            current += Time.fixedDeltaTime / duration;
            yield return null;
        }

        animator.speed = 0.5f;
        yield return new WaitForSeconds(fallDelay);

        if (particleEmitter != null)
            particleEmitter.Stop();

        animator.SetTrigger("Fall");
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(destroyDelay);
        
        animator.speed = 1;
        isFalling = false;
        
        gameObject.SetActive(false);
        
        animator.ResetTrigger("Fall");
        transform.position = startPosition;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.zero; 

        Invoke(nameof(Activate), destroyDelay);
    }

    void Activate()
    {
        gameObject.SetActive(true);
    }
}