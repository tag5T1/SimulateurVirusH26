using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Personne : MonoBehaviour
{
    [SerializeField] GameObject particuleDeBase;
    NavMeshAgent agent;
    [SerializeField] Virus virus;
    bool infecté = false;
    [SerializeField] int rayonPathfinding = 10;
    [SerializeField] float niveauToux;

    private void Start()
    {
        if (name == "Personne")
            virus = new Virus(transform, 6, 10, 70, 0.5f, 0.2f, 200);
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Pathfinding());
    }

    private void Update()
    {
        if (virus != null)
        {
            infecté = true;
        }

        if (infecté) { 
            if (Input.GetKeyDown(KeyCode.Space))
                niveauToux = 999;

            if (niveauToux - virus.niveauMin > Random.Range(20, 50))
            {
                Toux();
                niveauToux = 0;
            }
            else
                niveauToux += 25 * Time.deltaTime;
            Debug.DrawRay(transform.position, transform.forward * 5, Color.green, 0.1f);
        }
            
        transform.LookAt(transform.position + agent.velocity);
    }


    private IEnumerator Pathfinding()
    {
        Vector3 destination = new Vector3(Random.Range(-rayonPathfinding, rayonPathfinding), -5, Random.Range(-rayonPathfinding, rayonPathfinding));
        agent.SetDestination(destination);
        yield return new WaitForSeconds(Random.Range(3, 10));
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
            Debug.DrawRay(transform.position, transform.forward*5, Color.blue, 2f);
            vir.Creation(this.gameObject, transform.forward, virus);
        }
    }

    public void Infecter(Virus virus)
    {
        this.virus = virus.Muter();
    }
}
