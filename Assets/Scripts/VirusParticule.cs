using UnityEngine;

public class VirusParticule : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Creation(Vector3 directionEmission)
    {

    }
}
