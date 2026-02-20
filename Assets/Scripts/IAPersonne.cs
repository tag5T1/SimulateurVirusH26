using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class IAPersonne : MonoBehaviour
{
    [SerializeField] GameObject particuleDeBase;
    NavMeshAgent agent;
    public Personne personne { get; private set; }


    public void Creation()
    {
        personne = new Personne();
        personne.virus = new Virus(transform, Random.Range(50, 80) / 10, 10, Random.Range(55, 85), 0.5f, 0.1f);
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Pathfinding());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            personne.niveauToux = 999;

        if (personne.niveauToux - personne.virus.niveauMin > Random.Range(20, 50))
        {
            Toux();
            personne.niveauToux = 0;
        }
        else
            personne.niveauToux += 25 * Time.deltaTime;

            Debug.DrawRay(transform.position, transform.forward * 5, Color.green, 0.1f);
        transform.LookAt(transform.position + agent.velocity);
    }


    private IEnumerator Pathfinding()
    {
        Vector3 destination = personne.GénérerPosition();
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
            Debug.DrawRay(transform.position, transform.forward * 5, Color.blue, 2f);
            vir.Creation(transform.forward, personne.virus);
        }
    }
}
