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
    float gravite; // Force appliquée vers le bas
    float dureeVie; // Temps de vie avant de mourir
    float tempsVie; // Temps de vie depuis sa création
    bool premiereCollision;
    bool estCollée;


    ///Particules de la toux
    public void CréationVolatile(GameObject personne, Virus virus)
    {
        personneÉmettrice = personne;
        objetsCollisionnés = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        this.virus = virus;
        force = virus.force;
        gravite = virus.gravite;
        rb.linearDamping = Random.Range(virus.decceleration*0.25f, virus.decceleration*2f);
        dureeVie = virus.dureeVie/10;
        tempsVie = 0;
        premiereCollision = true;

        var forceVectorielle = personne.transform.forward * Random.Range(0, force) + new Vector3((float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10);
        rb.AddForce(forceVectorielle, ForceMode.Impulse);
    }

    ///Particules du vomis
    public void CréationSolide(GameObject personne, Virus virus)
    {
        personneÉmettrice = personne;
        objetsCollisionnés = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        this.virus = virus;
        force = (virus.force + 10) / 10;
        gravite = 9f;
        rb.linearDamping = Random.Range(virus.decceleration * 0.25f, virus.decceleration * 2f);
        dureeVie = virus.dureeVie/2;
        tempsVie = 0;
        premiereCollision = true;

        var forceVectorielle = personne.transform.forward * Random.Range(0, force) + new Vector3((float)Random.Range(-virus.maxSpread / 3, virus.maxSpread / 3) / 10, (float)Random.Range(-virus.maxSpread / 3, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10);
        rb.AddForce(forceVectorielle, ForceMode.Impulse);
    }



    private void FixedUpdate()
    {
        if (!estCollée)
        {
            rb.AddForce(0, -gravite, 0);
        }

        tempsVie += Time.fixedDeltaTime;
        if (tempsVie > dureeVie)
            GameObject.Destroy(gameObject);
    }


    // Lorsque la particule touche quelque chose
    private void OnCollisionEnter(Collision collision)
    {
        GameObject objet = collision.gameObject;

        //Si l'objet touché est une personne et n'est pas celle qui émet la particule, on l'infecte
        if (objet != personneÉmettrice && !objetsCollisionnés.Contains(collision.gameObject))
        {
            if (objet.tag == "Personne")
            {
                objet.GetComponent<IAPersonne>().DevientInfecte(virus);
                Destroy(gameObject);
            }
            else
            {
                if (premiereCollision)
                {
                    rb.isKinematic = true;
                    premiereCollision = false;
                    estCollée = true;
                    personneÉmettrice = objet;
                }
            }

            objetsCollisionnés.Add(collision.gameObject);
        }
    }
}
