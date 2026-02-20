using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class IAPersonne : MonoBehaviour
{
    [SerializeField] GameObject particuleDeBase;
    NavMeshAgent agent;
    public Personne personne { get; private set; }
    string location;



    public void Creation()
    {
        personne = new Personne();
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Pathfinding());
    }

    private void Update()
    {
        if (personne.infecté)
        {
            // LOGIQUE MUST BE POPPED
            if (personne.niveauToux - personne.virus.niveauMin > Random.Range(20, 50))
            {
                Toux();
                personne.niveauToux = 0;
            }
            else
                personne.niveauToux += 25 * Time.deltaTime;

            Debug.DrawRay(transform.position, transform.forward * 5, Color.green, 0.1f);
        }
        
        transform.LookAt(transform.position + agent.velocity);
    }


    private IEnumerator Pathfinding()
    {
        Vector3 destination;
        if (location == "Bureau")
        {
            destination = new Vector3(Random.Range(-20, 20), -5, Random.Range(-20, 20));
            agent.SetDestination(destination);
            yield return new WaitUntil(() => new Vector2(transform.position.x, transform.position.z) == new Vector2(destination.x, destination.z));
            location = "Roam";
        }
        else
        {
            destination = personne.GénérerPosition();
            agent.SetDestination(destination);
            yield return new WaitUntil(() => new Vector2(transform.position.x, transform.position.z) == new Vector2(destination.x, destination.z));
            location = "Bureau";
        }

        yield return new WaitForSeconds(Random.Range(10, 30)/10);
        StartCoroutine(Pathfinding());
    }

    private void Toux()
    {
        Tousse(particuleDeBase);
    }

    private void Tousse(GameObject prefab)
    {
        GameObject instance;
        var pos = transform.position + 0.6f * transform.forward;
        for (int i = 0; i < 10; i++)
        {
            instance = GameObject.Instantiate(prefab, pos, transform.rotation);
            VirusParticule vir = instance.GetComponent<VirusParticule>();
            Debug.DrawRay(transform.position, transform.forward * 5, Color.blue, 2f);
            vir.Création(this.gameObject, transform.forward, personne.virus);
        }
    }
}
