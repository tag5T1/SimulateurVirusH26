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
    NavMeshAgent agent;
    public Personne personne { get; private set; }
    [SerializeField] Material materialInfecté;
    SélecteurTâche sélecteur;
    public NomTâche nomTâche;
    public Tâche tâcheEnCours;
    public Vector2 position2D { get; private set; }



    public void Création(EspaceDeTravail espace)
    {
        personne = new Personne(espace);
        sélecteur = new SélecteurTâche(this);
        agent = GetComponent<NavMeshAgent>();
        FaireTâche();
    }

    private void Update()
    {
        position2D = new Vector2(transform.position.x, transform.position.z);

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
        personne.DevientInfecté(virus);
        GetComponent<MeshRenderer>().material = materialInfecté;
    }


    public void FaireTâche()
    {
        tâcheEnCours = sélecteur.ChoisirTâche();
        StartCoroutine(tâcheEnCours.FaireTâche());
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
    public void SetNomTâche(NomTâche nom)
    {
        nomTâche = nom;
    }
}
