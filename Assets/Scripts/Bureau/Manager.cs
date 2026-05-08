using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    [SerializeField] int nbPersonne;
    List<EspaceDeTravail> espacesDeTravail;
    public List<GameObject> personnes;
    public GameObject[] distributrices;
    public GameObject[] pickUpObjets;
    public GameObject[] poubelles;



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate Manager in \"" + gameObject.name + "\"");
            GameObject.Destroy(this.gameObject);
            return;
        }
        Instance = this;

        // Charge les prefabs à créer
        var personne = Resources.Load<GameObject>("Prefabs/Personne");
        var bureau = Resources.Load<GameObject>("Prefabs/Bureau");

        espacesDeTravail = new List<EspaceDeTravail>();

        // Crée un espace de travail par personne
        personnes = new();
        for (int i = 0; i < nbPersonne; i++) {
            CreerEspaceDeTravail(GameObject.Instantiate(bureau));

            var p = GameObject.Instantiate(personne);
            p.GetComponent<IAPersonne>().Creation(espacesDeTravail[^1]);
            espacesDeTravail[^1].occupé = false;

            personnes.Add(p);
        }

        // REMOVE IN FULL BUILD
        foreach (EspaceDeTravail espace in espacesDeTravail)
        {
            espace.RandomiserPositionBureau();
        }


        FindDistributrices();
        FindPickups();
        FindPoubelles();

        BuildNavMesh();
    }

    private void Start()
    {
        for(int i = 0; i < personnes.Count; i++) {
            var p = personnes[i];
            IAPersonne o = p.GetComponent<IAPersonne>();
            // Infecte 1 personne sur 5
            if (i % 5 == 0)
            {
                o.DevientInfecte(new Virus(o.gameObject));
            }
        }
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
        if (VerifierPickupObjetAccessible())
            return pickUpObjets[UnityEngine.Random.Range(0, pickUpObjets.Length)].GetComponent<PickUpObjet>();
        else return null;

    }
    public bool VerifierPickupObjetAccessible() {
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
    public bool VerifierSiPoubelleAccessible() {
        if (poubelles.Length > 0)
            return true;
        else
            return false;
    }
    public EspaceDeTravail TrouverEspaceDeTravailLibre()
    {
        foreach (EspaceDeTravail espace in espacesDeTravail)
        {
            if (!espace.occupé)
                return espace;
        }
        return null;
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

    public void CreerEspaceDeTravail(GameObject bureau)
    {
        EspaceDeTravail espace = new()
        {
            bureau = bureau
        };
        
        espacesDeTravail.Add(espace);
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
