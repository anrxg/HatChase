using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletForce = 20f;
    [SerializeField] private float maxDistance = 15f;
    private Vector3 startPosition;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector3 direction)
    {
        rb.AddForce(direction * bulletForce, ForceMode.Impulse);
    }
}
