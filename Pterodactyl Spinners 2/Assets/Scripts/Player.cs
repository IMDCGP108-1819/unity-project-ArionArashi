using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration before starting
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 10f);
    [SerializeField] AudioClip walkingSound;
    [SerializeField] AudioClip interactionSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip jumpSound;

    // State of the game object
    bool canInteract = true;
    bool isAirborne = false;

    // Component references
    private SpriteRenderer SpriteRen;
    private Animator PlayerAnim;
    private Rigidbody2D RigidBody;
    private Collider2D Collider;
    private AudioSource MainAudioSource;
    private AudioSource AltAudioSource;

    // Use this for initialization
    void Start()
    {
        SpriteRen = GetComponent<SpriteRenderer>();
        PlayerAnim = GetComponent<Animator>();
        RigidBody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CapsuleCollider2D>();
        var audioSources = GetComponents<AudioSource>();
        MainAudioSource = audioSources[0];
        AltAudioSource = audioSources[1];
    }

    // Update is called once per frame
    void Update()
    {
        // If the player dies or wins we stop them from moving
        if (!canInteract)
            return;

        // All logic is separated into their specific method for readability
        Jumping();
        Walking();
        Interacting();
        Dying();
        Winning();
    }

    private void Jumping()
    {
        // We restrict the player to only jump while on ground level as to prevent infinite jumping
        if (Input.GetKeyDown(KeyCode.Space) && Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isAirborne = true;
            PlayerAnim.SetBool("Spinning", true);
            RigidBody.velocity += new Vector2(0f, jumpSpeed);

            MainAudioSource.Stop();
            if (!AltAudioSource.isPlaying)
            {
                AltAudioSource.clip = jumpSound;
                AltAudioSource.Play();
            }
        }

        // In order to stop the "Spinning" animation we have to give it enough time to execute
        // by figuring out a proper time to stop the animation
        if (!isAirborne && Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            PlayerAnim.SetBool("Spinning", false);
        }

        if (isAirborne && !Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isAirborne = false;
        }
    }

    private void Interacting()
    {
        // The user can interact with objects in the world only when they are touching the ground layer
        if (Input.GetKeyDown(KeyCode.E) && Collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            PlayerAnim.SetBool("Interact", true);

            if (!MainAudioSource.isPlaying)
            {
                MainAudioSource.clip = interactionSound;
                MainAudioSource.timeSamples = 1;
                MainAudioSource.Play();
            }
        }
        else
        {
            PlayerAnim.SetBool("Interact", false);
        }
    }

    private void Walking()
    {
        // This logic handles how the player moves on the screen and plays the respective animation and sound
        // whilst also keeping track of the direction they are facing
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * playerSpeed * Time.deltaTime;
            SpriteRen.flipX = true;
            PlayerAnim.SetBool("Walking", true);

            if (!MainAudioSource.isPlaying)
            {
                MainAudioSource.clip = walkingSound;
                MainAudioSource.Play();
            }

            //AudioSource.PlayClipAtPoint(walkingSound, Camera.main.transform.position);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * playerSpeed * Time.deltaTime;
            SpriteRen.flipX = false;
            PlayerAnim.SetBool("Walking", true);

            if (!MainAudioSource.isPlaying)
            {
                MainAudioSource.clip = walkingSound;
                MainAudioSource.Play();
            }
        }
        else
        {
            PlayerAnim.SetBool("Walking", false);
        }
    }

    private void Dying()
    {
        // The death sequence stops the player from moving while playing the death animation and sound
        // and subtracting from their lives from the shared game session that keeps track of them
        if (Collider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            canInteract = false;
            PlayerAnim.SetTrigger("Death");
            RigidBody.velocity = deathKick;
            FindObjectOfType<GameSession>().TakeLife();
            MainAudioSource.Stop();
            if (!AltAudioSource.isPlaying)
            {
                AltAudioSource.clip = deathSound;
                AltAudioSource.Play();
            }
        }
    }

    private void Winning()
    {
        // When a player reaches the end of the game, the winning sequence is executed
        if (Collider.IsTouchingLayers(LayerMask.GetMask("Mother")))
        {
            canInteract = false;
            FindObjectOfType<GameSession>().WinGame();
        }
    }
}
