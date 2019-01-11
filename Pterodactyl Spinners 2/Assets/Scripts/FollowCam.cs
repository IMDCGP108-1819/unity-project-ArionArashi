using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Component references
    public Player Player;
    public Camera Camera;
    public BoxCollider2D BoxColl;
    public Bounds Bounds;

    // Use this for initialization
    void Start()
    {
        Player = FindObjectOfType<Player>();
        Camera = FindObjectOfType<Camera>();
        Bounds = GameObject.Find("Background").GetComponent<SpriteRenderer>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        // Here we are getting the vertical and horizontal extents of the camera halved in two
        float camVertExtent = Camera.orthographicSize;
        float camHorzExtent = Camera.aspect * camVertExtent;

        // By knowing the limits of our background image and our halved camera dimentions,
        // we can figure out the limits of where the cameras center point has to be so it doesnt go off the background
        float leftBound = Bounds.min.x + camHorzExtent;
        float rightBound = Bounds.max.x - camHorzExtent;
        float bottomBound = Bounds.min.y + camVertExtent;
        float topBound = Bounds.max.y - camVertExtent;

        // Here we estabilish boundaries for the camera to not be able to cross in order to avoid it going off screen
        float camX = Mathf.Clamp(Player.transform.position.x, leftBound, rightBound);
        float camY = Mathf.Clamp(Player.transform.position.y, bottomBound, topBound);
        transform.position = new Vector3(camX, camY, transform.position.z);
    }
}

