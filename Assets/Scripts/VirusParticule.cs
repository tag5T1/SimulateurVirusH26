using UnityEngine;

public class VirusParticule : MonoBehaviour
{
    Rigidbody rb;
    Virus virus;
    private float maxSpread = 20;
    private float force;
    private float gravité;
    private float duréeVie;
    private float tempsVie;

    public void Creation(Vector3 directionEmission, Virus virus)
    {
        rb = GetComponent<Rigidbody>();
        force = virus.force;
        gravité = virus.gravité;
        duréeVie = virus.duréeVie;
        tempsVie = 0;

        var forceVectorielle = directionEmission * Random.Range(0, force) + new Vector3((float)Random.Range(0, maxSpread) / 10, (float)Random.Range(0, maxSpread) / 10, (float)Random.Range(0, maxSpread) / 10);
        rb.AddForce(forceVectorielle, ForceMode.Impulse);
        transform.LookAt(transform.position + forceVectorielle);
        Debug.DrawRay(transform.position, forceVectorielle, Color.red, 1);
    }

    private void FixedUpdate()
    {
        rb.AddForce(0, -gravité, 0);
        tempsVie += Time.fixedDeltaTime;
        if (tempsVie > duréeVie)
        {
            GameObject.Destroy(gameObject);
        }
    }



    public void setVirus(Virus virus)
    {
        this.virus = virus;
    }
    public void setGravité(float gravité)
    {
        this.gravité = gravité;
    }
    public void setForce(float force)
    {
        this.force = force;
    }
    public void setDuréeVie(float duréeVie)
    {
        this.duréeVie = duréeVie;
    }
}
