using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class VirusParticule : MonoBehaviour
{
    GameObject personneÉmettrice;
    List<GameObject> objetsCollisionnés; // Objets que la particule a touchés
    Rigidbody rb;
    Virus virus;
    float force; // Force avec laquelle la particule est projetée
    float gravité; // Force appliquée vers le bas
    float duréeVie; // Temps de vie avant de mourir
    float tempsVie; // Temps de vie depuis sa création
    bool premièreCollision;
    bool estCollée;


    public void Création(GameObject personne, Virus virus)
    {
        personneÉmettrice = personne;
        objetsCollisionnés = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        this.virus = virus;
        force = virus.force;
        gravité = virus.gravité;
        rb.linearDamping = Random.Range(virus.décceleration*0.25f, virus.décceleration*2f);
        duréeVie = virus.duréeVie;
        tempsVie = 0;
        premièreCollision = true;

        var forceVectorielle = personne.transform.forward * Random.Range(0, force) + new Vector3((float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10);
        rb.AddForce(forceVectorielle, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (!estCollée)
        {
            rb.AddForce(0, -gravité, 0);
        }

        tempsVie += Time.fixedDeltaTime;
        if (tempsVie > duréeVie)
            GameObject.Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        GameObject objet = collision.gameObject;

        if (objet != personneÉmettrice && !objetsCollisionnés.Contains(collision.gameObject))
        {
            if (objet.tag == "Personne")
            {
                objet.GetComponent<IAPersonne>().DevientInfecté(virus);
                Destroy(gameObject);
            }
            else
            {
                if (premièreCollision)
                {
                    rb.isKinematic = true;
                    premièreCollision = false;
                    estCollée = true;
                    personneÉmettrice = objet;
                }
            }

            objetsCollisionnés.Add(collision.gameObject);
        }
    }
}
