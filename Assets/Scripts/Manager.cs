using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    [SerializeField] int nbPersonne;
    GameObject personne;
    List<EspaceDeTravail> espacesDeTravail;
    GameObject bureau;



    void Awake()
    {
        personne = Resources.Load<GameObject>("Personne");
        bureau = Resources.Load<GameObject>("Bureau");
        espacesDeTravail = new List<EspaceDeTravail>();
        for (int i = 0; i < nbPersonne; i++) {
            EspaceDeTravail espace = new()
            {
                bureau = GameObject.Instantiate(bureau)
            };
            espace.RandomiserPositionBureau();
            espacesDeTravail.Add(espace);
            
            var o = GameObject.Instantiate(personne);
            o.GetComponent<IAPersonne>().Creation();
            o.GetComponent<IAPersonne>().personne.espaceDeTravail = espace;
            if (i%5 == 0)
                o.GetComponent<IAPersonne>().personne.Infecter(new Virus(transform, Random.Range(100, 160) / 10, 15, Random.Range(55, 85), 1f, 0.2f, 10));
        }
    }
}
