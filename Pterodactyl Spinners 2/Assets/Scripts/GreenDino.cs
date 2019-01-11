using UnityEngine;

public class GreenDino : MonoBehaviour
{
    // Details of the direction the dinosaur is going to move after the interaction
    [SerializeField] Vector2 flight = new Vector2(-2f, 0f);

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
        Run();
    }

    // This logic determines if the dinosaur is going to move if the needed conditions are met
    public void Run()
    {
        if (FindObjectOfType<GameSession>().BerriesThrown())
        {
            RigidBody.velocity = flight;
            Anim.SetBool("Walk", true);
        }
    }
}