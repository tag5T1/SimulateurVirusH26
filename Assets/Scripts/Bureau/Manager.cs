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
    GameObject distributrice;
    public GameObject[] distributrices;



    void Awake()
    {
        // Charge les prefabs à créer
        personne = Resources.Load<GameObject>("Prefabs/Personne");
        bureau = Resources.Load<GameObject>("Prefabs/Bureau");
        distributrice = Resources.Load<GameObject>("Prefabs/Distributrice");

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
            o.Création(this, espace);
            // Infecte 1 personne sur 5
            if (i%5 == 0)
            {
                Virus virus = new Virus(transform);
                o.personne.Infecter(virus);
            }
                
        }

        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject.Instantiate(distributrice);
        //    distributrice.transform.position = new Vector3(Random.Range(-20, 20), -3, -25);
        //}

        distributrices = GameObject.FindGameObjectsWithTag("Distributrice");

        GameObject.Find("NavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
    }


    public Distributrice GetDistributrice()
    {
        List<Distributrice> distrMoinsOccupée = new()
        {
            distributrices[0].GetComponent<Distributrice>()
        };

        foreach (GameObject go in distributrices)
        {
            // Reset la liste si une distributrice avec moins de personnes est trouvée
            if (go.GetComponent<Distributrice>().fileDattente.Count < distrMoinsOccupée.ToArray()[0].fileDattente.Count)
            {
                distrMoinsOccupée = new()
                {
                    go.GetComponent<Distributrice>()
                };
            }
            else if (go.GetComponent<Distributrice>().fileDattente.Count == distrMoinsOccupée.ToArray()[0].fileDattente.Count)
                distrMoinsOccupée.Add(go.GetComponent<Distributrice>());
        }
        return distrMoinsOccupée.ToArray()[Random.Range(0, distrMoinsOccupée.Count)].GetComponent<Distributrice>();
    }
}
