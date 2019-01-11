using UnityEngine;

public class Butterflies : MonoBehaviour
{
    // Details of the direction the butterflies are going to fly after the interaction
    [SerializeField] Vector2 flight = new Vector2(-10f, 0f);

    private Rigidbody2D RigidBody;
    private CircleCollider2D Collider;
    private Animator Anim;

    // Use this for initialization
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CircleCollider2D>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ScareButterflies();
    }

    // This logic determines if the butterflies themselves are going to fly if the needed conditions are met
    public void ScareButterflies()
    {
        if (Input.GetKey(KeyCode.E) && Collider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            RigidBody.velocity = flight;
            FindObjectOfType<GameSession>().SpookButterflies();
            Anim.SetBool("Fly", true);
        }
    }
}
