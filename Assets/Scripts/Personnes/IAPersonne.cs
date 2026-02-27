using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class IAPersonne : MonoBehaviour
{
    [SerializeField] GameObject particuleDeBase;
    NavMeshAgent agent;
    public Personne personne { get; private set; }
    public string actionEnCours;
    public Tâche tâcheEnCours;
    public Vector2 position2D { get; private set; }



    public void Creation(EspaceDeTravail espace)
    {
        personne = new Personne(espace);
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Pathfinding());
    }

    private void Update()
    {
        position2D = new Vector2(transform.position.x, transform.position.z);

        if (personne.infecté)
        {
            // LOGIQUE MUST BE POPPED
            if (personne.niveauToux - personne.virus.niveauMin > Random.Range(20, 50))
            {
                Symptomes();
                personne.niveauToux = 0;
            }
            else
                personne.niveauToux += 25 * Time.deltaTime;
        }
        
        transform.LookAt(transform.position + agent.velocity);
    }


    private IEnumerator Pathfinding()
    {
        // CHOIX DE TÂCHE [À DÉPLACER DANS SÉLECTEUR DE TÂCHE]
        if (actionEnCours == "AuBureau")
        {
            tâcheEnCours = new Roam(this);
        }
        else
        {
            tâcheEnCours = new AllerAuBureau(this);
        }
        FaireTâche(tâcheEnCours);

        yield return new WaitUntil(() => tâcheEnCours.status == StatusTâche.TERMINÉ);
        // Attente entre les tâches [À DÉPLACER DANS LES TÂCHES]
        yield return new WaitForSeconds(Random.Range(20, 60)/10);
        StartCoroutine(Pathfinding());
    }

    private void Symptomes()
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
            vir.Création(gameObject, transform.forward, personne.virus);
        }
    }


    public void FaireTâche(Tâche tâche)
    {
        StartCoroutine(tâche.FaireTâche());
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public void SetActionEnCours(string action)
    {
        actionEnCours = action;
    }
}
