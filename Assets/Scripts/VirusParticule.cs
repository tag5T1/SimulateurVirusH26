using UnityEngine;

public class VirusParticule : MonoBehaviour
{
    Rigidbody rb;
    private float maxSpread = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Creation(Vector3 directionEmission, float force)
    {
        rb = GetComponent<Rigidbody>();
        var forceVectorielle = directionEmission * force + new Vector3((float)Random.Range(0, maxSpread) / 10, (float)Random.Range(0, maxSpread) / 10, (float)Random.Range(0, maxSpread) / 10);
        rb.AddForce(forceVectorielle, ForceMode.Impulse);
        transform.LookAt(transform.position + forceVectorielle);
        Debug.DrawRay(transform.position, forceVectorielle, Color.red, 1);
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.back * 1);
    }
}
