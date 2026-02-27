using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class VirusParticule : MonoBehaviour
{
    GameObject personne�mettrice;
    Rigidbody rb;
    Virus virus;
    float force; // Force � laquelle la particule est projet�e
    float gravit�; // Force appliqu�e vers le bas
    float dur�eVie; // Tremps de vie avant de mourir
    float tempsVie; // Temps de vie depuis sa cr�ation
    List<GameObject> objetsCollisionn�s; // Objets que la particule � touch�
    bool premi�reCollision;
    bool estEnCollision = false;


    public void Cr�ation(GameObject personne, Vector3 directionEmission, Virus virus)
    {
        personne�mettrice = personne;
        premi�reCollision = true;
        objetsCollisionn�s = new List<GameObject>();
        rb = GetComponent<Rigidbody>();
        this.virus = virus;
        force = virus.force;
        gravit� = virus.gravit�;
        rb.linearDamping = Random.Range(virus.decceleration*500, virus.decceleration*1500)/1000;
        dur�eVie = virus.dur�eVie;
        tempsVie = 0;

        var forceVectorielle = directionEmission * Random.Range(0, force) + new Vector3((float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10, (float)Random.Range(-virus.maxSpread, virus.maxSpread) / 10);
        rb.AddForce(forceVectorielle, ForceMode.Impulse);
        //transform.LookAt(transform.position + forceVectorielle);
    }

    private void FixedUpdate()
    {
        if (!estEnCollision)
        {
            rb.AddForce(0, -gravit�, 0);
        }
        tempsVie += Time.fixedDeltaTime;
        if (tempsVie > dur�eVie)
            GameObject.Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != personne�mettrice && !objetsCollisionn�s.Contains(collision.gameObject))
        {
            if (collision.gameObject.tag == "Personne")
            {
                collision.gameObject.GetComponent<IAPersonne>().personne.Infecter(virus);
            }
            else
            {
                if (premi�reCollision)
                {
                    Debug.Log("stick");
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    gameObject.GetComponent<VirusParticule>().personne�mettrice = collision.gameObject;
                    premi�reCollision = false;
                    estEnCollision = true;
                }
                else
                {
                    Debug.Log("Copie");
                    GameObject instance;
                    instance = Instantiate(gameObject, collision.GetContact(0).point, Quaternion.identity);
                }

            }

            objetsCollisionn�s.Add(collision.gameObject);
        }
    }
}
