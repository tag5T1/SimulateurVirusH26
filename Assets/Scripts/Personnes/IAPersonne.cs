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
    public float vitesseDeDéplacementDeBase { get; private set; }
    public Personne personne { get; private set; }
    SélecteurTâche sélecteur;
    public NomTâche nomTâche;
    public Tâche tâcheEnCours;
    public Vector2 position2D { get; private set; }



    public void Création(EspaceDeTravail espace)
    {
        personne = new Personne(espace);
        sélecteur = new SélecteurTâche(this);
        agent = GetComponent<NavMeshAgent>();
        vitesseDeDéplacementDeBase = agent.speed;
        FaireTâche();
    }

    private void Update()
    {
        UpdatePosition2D();

        if (tâcheEnCours.status == StatusTâche.TERMINÉ)
        {
            FaireTâche();
        }

        if (personne.estInfecté)
        {
            personne.virus.EffectuerSymptomes();
        }
        
        transform.LookAt(transform.position + agent.velocity);
    }



    public void DevientInfecté(Virus virus)
    {
        personne.DevientInfecté(gameObject, virus);
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Infection");
    }


    public void FaireTâche()
    {
        tâcheEnCours = sélecteur.ChoisirTâche();
        StartCoroutine(tâcheEnCours.FaireTâche());
    }
    public void FaireTâche(Tâche tâcheÀFaire)
    {
        tâcheEnCours = tâcheÀFaire;
        StartCoroutine(tâcheÀFaire.FaireTâche());
    }

    public void Arrêt()
    {
        agent.enabled = false;
    }
    public void Départ()
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
    public void SetNomTâche(NomTâche nom)
    {
        nomTâche = nom;
    }
}
