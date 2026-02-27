using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] int nbPersonne;
    GameObject personne;
    List<EspaceDeTravail> espacesDeTravail;
    GameObject bureau;



    void Awake()
    {
        // Charge les prefabs à créer
        personne = Resources.Load<GameObject>("Prefabs/Personne");
        bureau = Resources.Load<GameObject>("Prefabs/Bureau");
        espacesDeTravail = new List<EspaceDeTravail>();
        // Crée un espace de travail par personne
        for (int i = 0; i < nbPersonne; i++) {
            EspaceDeTravail espace = new()
            {
                bureau = GameObject.Instantiate(bureau)
            };
            espace.RandomiserPositionBureau();
            espacesDeTravail.Add(espace);
            
            IAPersonne o = GameObject.Instantiate(personne).GetComponent<IAPersonne>();
            o.Creation(espace);
            // Infecte 1 personne sur 5
            if (i%5 == 0)
            {
                Virus virus = new Virus(transform);
                o.personne.Infecter(virus);
            }
                
        }

        GameObject.Find("NavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
