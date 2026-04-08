using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 1500f;
    public float turnTorque = 400f;
    public float maxSpeed = 20f;

    [Header("Water Resistance")]
    public float waterDrag = 1.5f;
    public float waterAngularDrag = 3f;

    [Header("Bounds")]
    public float boundaryRadius = 200f;

    private Rigidbody rb;
    private Vector3 startPosition;

    public bool hasDocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        // Apply drag
        rb.drag = waterDrag;
        rb.angularDrag = waterAngularDrag;

        Debug.Log("BoatController started");
    }

    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");     // W/S
        float strafeInput = Input.GetAxis("Horizontal"); // A/D

        // Forward movement
        Vector3 forward = transform.forward * moveInput * 10f;

        // Side movement
        Vector3 sideways = transform.right * strafeInput * 10f;

        // Combine both
        Vector3 movement = forward + sideways;

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    void Update()
    {
        CheckBounds();
    }

    public float maxDistance = 500f;

    void CheckBounds()
    {
        float distanceFromStart = Vector3.Distance(transform.position, startPosition);

        if (!hasDocked && distanceFromStart > maxDistance)
        {
            ResetBoat();
        }
    }

    void ResetBoat()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit obstacle - resetting!");
            ResetBoat();
        }
    }
}
