using UnityEngine;

public class AIFollow : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float stoppingDistance = 1f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Face the player
        Vector3 lookDirection = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        // Calculate movement direction towards the player
        Vector3 playerDirection = (player.position - transform.position).normalized;
        Vector3 movementDirection = new Vector3(playerDirection.x, 0f, playerDirection.z);

        // Move the AI
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            rb.velocity = movementDirection * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        // Ensure the AI stays on the ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            float distanceToGround = hit.distance;
            if (distanceToGround > 0.1f)
            {
                rb.velocity += Vector3.down * distanceToGround * 10f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Kill the player if the AI collides with them
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
