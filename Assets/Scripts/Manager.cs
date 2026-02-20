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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
