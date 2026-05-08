using System;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class IAPersonne : MonoBehaviour
{
    [SerializeField] GameObject particuleDeBase;
    public NavMeshAgent agent;
    public Manager manager;
    public float vitesseDeDeplacementDeBase { get; private set; }
    public Personne personne { get; private set; }
    SelecteurTache selecteur;
    public NomTache nomTache;
    public Tache tacheEnCours;
    public Vector2 position2D { get; private set; }
    private float tempsInfecte;



    public void Creation(EspaceDeTravail espace)
    {
        personne = new Personne(espace, gameObject);
        selecteur = new SelecteurTache(this);
        agent = GetComponent<NavMeshAgent>();
        manager = Manager.Instance;
        vitesseDeDeplacementDeBase = agent.speed;
        tempsInfecte = 0;
        FaireTache();
    }

    private void Update()
    {
        tempsInfecte += Time.deltaTime;
        UpdatePosition2D();

        if (personne.espaceDeTravail == null)
        {
            var o = manager.TrouverEspaceDeTravailLibre();
            if (o != null)
                personne.espaceDeTravail = o;
            else
                Debug.LogWarning("Pas d'espace de travail disponible");
        }

        if (tacheEnCours != null && tacheEnCours.status == StatusTache.TERMINE)
        {
            FaireTache();
        }

        if (personne.estInfecte)
        {
            personne.virus.EffectuerSymptomes();

            if (tempsInfecte >= personne.virus.dureeVie)
            {
                Debug.Log("Gueri");
                DevientGueri();
                tempsInfecte = 0;
            }
        }
        
        transform.LookAt(transform.position + agent.velocity);
    }



    public void DevientInfecte(Virus virus)
    {
        personne.DevientInfecte(gameObject, virus);
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Infection");
    }

    public void DevientGueri()
    {
        personne.DevientGueri();
        GetComponent<MeshRenderer>().material = personne.immunite.getMaterial();
    }


    public void FaireTache()
    {
        tacheEnCours = selecteur.ChoisirTache();
        StartCoroutine(tacheEnCours.FaireTache());
    }

    /// <summary>
    /// Quand la personne re�oit une tache de l'ext�rieur
    /// </summary>
    /// <param name="tacheAFaire">Tache donn� � la personne de l'ext�rieur</param>
    public void FaireTache(Tache tacheAFaire)
    {
        tacheEnCours = tacheAFaire;
        StartCoroutine(tacheAFaire.FaireTache());
    }

    public void Arret()
    {
        agent.enabled = false;
    }
    public void Depart()
    {
        agent.enabled = true;
    }

    public void SetDestination(Vector3 destination)
    {
        if (agent.enabled)
            agent.SetDestination(destination);
    }
    public void UpdatePosition2D()
    {
        position2D = new Vector2(transform.position.x, transform.position.z);
    }
    public void SetNomTache(NomTache nom)
    {
        nomTache = nom;
    }
}
