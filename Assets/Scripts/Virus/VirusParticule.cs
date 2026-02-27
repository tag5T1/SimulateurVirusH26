using UnityEngine;

public class VirusParticule : MonoBehaviour
{
    GameObject personneÉmettrice;
    Rigidbody rb;
    Virus virus;
    float force; // Force à laquelle la particule est projetée
    float gravité; // Force appliquée vers le bas
    float duréeVie; // Tremps de vie avant de mourir
    float tempsVie; // Temps de vie depuis sa création

    public void Création(GameObject personne, Vector3 directionEmission, Virus virus)
    {
        personneÉmettrice = personne;
        rb = GetComponent<Rigidbody>();
        this.virus = virus;
        force = virus.force;
        gravité = virus.gravité;
        rb.linearDamping = Random.Range(virus.decceleration*500, virus.decceleration*1500)/1000;
        duréeVie = virus.duréeVie;
        tempsVie = 0;

        var forceVectorielle = directionEmission * Random.Range(0, force) + new Vector3((float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10);
        rb.AddForce(forceVectorielle, ForceMode.Impulse);
        //transform.LookAt(transform.position + forceVectorielle);
    }

    private void FixedUpdate()
    {
        rb.AddForce(0, -gravité, 0);
        tempsVie += Time.fixedDeltaTime;
        if (tempsVie > duréeVie)
            GameObject.Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Personne" && collision.gameObject != personneÉmettrice)
        {
            collision.gameObject.GetComponent<IAPersonne>().personne.Infecter(virus);
        }
    }
}
