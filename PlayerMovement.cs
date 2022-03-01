using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private SpriteRenderer sr;
    private BoxCollider2D bc2D;

    [SerializeField] private LayerMask jumpableGround;

    private float directionX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState {idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // Stores Rigidbody2D Component as a variable in order to improve performance and to prevent repeated callbacks
        animator = GetComponent<Animator>(); // Stores Animator Component as a variable in order to improve performance and to prevent repeated callbacks
        sr = GetComponent<SpriteRenderer>(); // Stores SpriteRenderer Component as a variable in order to improve performance and to prevent repeated callbacks
        bc2D = GetComponent<BoxCollider2D>(); // Stores BoxCollider2D Component as a variable in order to improve performance and to prevent repeated callbacks
    }

    // Update is called once per frame
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal"); // Stores the Horizontal Axis input using a floating decimal | Positive numbers make the character go to the right, whilst negatives make it go to the left
        rb2D.velocity = new Vector2(directionX * moveSpeed, rb2D.velocity.y); // Makes the character move depending on how much the key is pressed

        if (Input.GetButtonDown("Jump") && IsGrounded())// Checks if space key is pressed using Unity's native Input Manager and if the player is on the ground
        {
            jumpSoundEffect.Play();
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce); // Gets Rigidbody2d Component (responsible for physics) and changes the value of velocity using a Vector2
        }

        UpdateAnimationState(); // Runs the UpdateAnimationUpdate section of code
    }

    private void UpdateAnimationState() // Separated from void Update() in order to clean up code
    {
        MovementState state;

        if (directionX > 0f) // Checks if the character is moving to the right
        {
            state = MovementState.running; // Sets the Running animation
            sr.flipX = false; // Makes the character look to the right
        }
        else if (directionX < 0f) // Checks if the character is moving to the left
        {
            state = MovementState.running; // Sets the Running animation
            sr.flipX = true; // Makes the character look to the left
        }
        else
        {
            state = MovementState.idle; // Sets the Idle animation
        }

        if (rb2D.velocity.y > .1f)  // Checks if the character is moving up
        {
            state = MovementState.jumping; // Sets the Jumping animation
        }

        else if (rb2D.velocity.y < -.1f)  // Checks if the character is moving down
        {
            state = MovementState.falling;  // Sets the Falling animation
        }

        animator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(bc2D.bounds.center, bc2D.bounds.size, 0f, Vector2.down, .1f, jumpableGround); // Creates a box around the character with the same shape as the box collider that will be used to detect if the character is in contact with the ground or not
    }
        
}
