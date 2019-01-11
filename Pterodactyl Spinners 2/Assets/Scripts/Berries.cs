using UnityEngine;

public class Berries : MonoBehaviour
{
    // Details of the direction the berries are going to fly after the interaction
    [SerializeField] Vector2 flight = new Vector2(-10f, 3f);

    private Rigidbody2D RigidBody;
    private CircleCollider2D Collider;

    // Use this for initialization
    void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        ThrowBerries();
    }

    // This logic determines if the berries themselves are going to be thrown if the needed conditions are met
    public void ThrowBerries()
    {
        if (Input.GetKey(KeyCode.E) && Collider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            RigidBody.velocity = flight;
            FindObjectOfType<GameSession>().ThrowBerries();
        }
    }
}
