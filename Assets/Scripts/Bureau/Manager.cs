using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.U2D;
using XCharts.Runtime;
using System;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class Manager : MonoBehaviour
{
    [SerializeField] int nbPersonne;
    public bool modeOfficeBuilderActivé;
    GameObject personne;
    List<EspaceDeTravail> espacesDeTravail;
    GameObject bureau;
    GameObject distributrice;
    public GameObject[] personnes;
    public GameObject[] distributrices;
    public GameObject[] pickUpObjets;
    public GameObject[] poubelles;

    [SerializeField] LineChart graphInfecte;



    void Awake()
    {
        // Charge les prefabs à créer
        personne = Resources.Load<GameObject>("Prefabs/Personne");
        bureau = Resources.Load<GameObject>("Prefabs/Bureau");
        distributrice = Resources.Load<GameObject>("Prefabs/Distributrice");

        espacesDeTravail = new List<EspaceDeTravail>();

        // Crée un espace de travail par personne
        personnes = new GameObject[nbPersonne];
        for (int i = 0; i < nbPersonne; i++) {
            EspaceDeTravail espace = new()
            {
                bureau = GameObject.Instantiate(bureau)
            };
            espace.RandomiserPositionBureau();
            espacesDeTravail.Add(espace);

            var p = GameObject.Instantiate(personne);
            p.GetComponent<IAPersonne>().Création(espace);

            personnes[i] = p;
        }


        //for (int i = 0; i < 5; i++)
        //{
        //    GameObject.Instantiate(distributrice);
        //    distributrice.transform.position = new Vector3(Random.Range(-20, 20), -3, -25);
        //}

        FindDistributrices();
        FindPickups();
        FindPoubelles();

        BuildNavMesh();
    }

    private void Start()
    {
        for(int i = 0; i < personnes.Length;i++) {
            var p = personnes[i];
            IAPersonne o = p.GetComponent<IAPersonne>();
            // Infecte 1 personne sur 5
            if (i % 5 == 0)
            {
                o.DevientInfecté(new Virus(o.gameObject));
            }
        }
    }
    private void Update()
    {

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
        return distrMoinsOccupée.ToArray()[UnityEngine.Random.Range(0, distrMoinsOccupée.Count)].GetComponent<Distributrice>();
    }


    public PickUpObjet GetPickUpObjet()
    {
        if (VérifierPickupObjetAccessible())
            return pickUpObjets[Random.Range(0, pickUpObjets.Length)].GetComponent<PickUpObjet>();
        else return null;

    }
    public bool VérifierPickupObjetAccessible() {
        if (pickUpObjets.Length > 0)
        {
            foreach (var o in pickUpObjets)
            {
                var x = o.GetComponent<PickUpObjet>();
                if (x != null && !x.utilisé)
                    return true;
            }
        }
        return false;
    }


    public GameObject GetPoubelleLaPlusProche(Vector3 positionPersonne)
    {
        var poubelleProche = poubelles[0];
        var distanceProche = CalculerLongueurPath(positionPersonne, poubelleProche.transform.position);

        foreach (GameObject go in poubelles)
        {
            var distance = CalculerLongueurPath(positionPersonne, go.transform.position);
            if (distance < distanceProche)
            {
                poubelleProche = go;
                distanceProche = distance;
            }
        }

        return poubelleProche;
    }
    public bool VérifierSiPoubelleAccessible() {
        if (poubelles.Length > 0)
            return true;
        else
            return false;
    }

    public float CalculerLongueurPath(Vector3 départ, Vector3 arrivée)
    {
        NavMeshHit hitDépart, hitArrivée;

        if (!NavMesh.SamplePosition(départ, out hitDépart, 2f, NavMesh.AllAreas) || !NavMesh.SamplePosition(arrivée, out hitArrivée, 2f, NavMesh.AllAreas))
            return 0;

        NavMeshPath path = new NavMeshPath();

        if (!NavMesh.CalculatePath(hitDépart.position, hitArrivée.position, NavMesh.AllAreas, path) || path.status != NavMeshPathStatus.PathComplete)
            return 0;

        float distance = 0;
        var corners = path.corners;

        for (int i = 0; i < corners.Length - 1; i++)
            distance += Vector3.Distance(corners[i], corners[i + 1]);

        return distance;
    }

    public void FindDistributrices()
    {
        distributrices = GameObject.FindGameObjectsWithTag("Distributrice");
    }
    public void FindPickups()
    {
        pickUpObjets = GameObject.FindGameObjectsWithTag("PickUpObjet");
    }
    public void FindPoubelles()
    {
        poubelles = GameObject.FindGameObjectsWithTag("Poubelle");
    }

    public void BuildNavMesh()
    {
        GameObject.Find("NavMesh").GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}
