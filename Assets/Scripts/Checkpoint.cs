using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public BoxCollider2D bc2d;
    // Animator pour gérer les animations du checkpoint
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerSpawn playerSpawn = collision.GetComponent<PlayerSpawn>();
            if (playerSpawn != null)
            {
                // On met a jour la position de respawn du joueur
                playerSpawn.currentSpawnPosition = transform.position;
                // On désactive le collider pour ne plus déclencher le checkpoint
                bc2d.enabled = false;
                // On déclenche l'animation d'activation du drapeau
                animator.SetTrigger("Activate");
            }
        }
    }
}