using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2D;

    [SerializeField] private AudioSource deathSoundEffect;


    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>(); // Stores Animator Component as a variable in order to improve performance and to prevent repeated callbacks
        rb2D = GetComponent<Rigidbody2D>(); // Stores Rigidbody2D Component as a variable in order to improve performance and to prevent repeated callbacks
    }

    private void OnCollisionEnter2D(Collision2D collision) // Starts when you collide with another object
    {
        if (collision.gameObject.CompareTag("Trap")) // Checks if a trap was hit
        {
            Die();


        }
    }

    private void Die()
    {
        deathSoundEffect.Play();
        rb2D.bodyType = RigidbodyType2D.Static;  // Stops player from moving
        animator.SetTrigger("death");  // Sets the Death animation
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Restarts the level
    }

}
