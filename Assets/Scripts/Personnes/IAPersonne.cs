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
    public float vitesseDeD�placementDeBase { get; private set; }
    public Personne personne { get; private set; }
    S�lecteurT�che s�lecteur;
    public NomT�che nomT�che;
    public T�che t�cheEnCours;
    public Vector2 position2D { get; private set; }
    private float tempsInfecte;



    public void Cr�ation(EspaceDeTravail espace)
    {
        personne = new Personne(espace, gameObject);
        s�lecteur = new S�lecteurT�che(this);
        agent = GetComponent<NavMeshAgent>();
        manager = Manager.Instance;
        vitesseDeD�placementDeBase = agent.speed;
        tempsInfecte = 0;
        FaireT�che();
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

        if (t�cheEnCours != null && t�cheEnCours.status == StatusT�che.TERMIN�)
        {
            FaireT�che();
        }

        if (personne.estInfect�)
        {
            personne.virus.EffectuerSymptomes();

            if (tempsInfecte >= personne.virus.dur�eVie)
            {
                Debug.Log("Gueri");
                DevientGueri();
                tempsInfecte = 0;
            }
        }
        
        transform.LookAt(transform.position + agent.velocity);
    }



    public void DevientInfect�(Virus virus)
    {
        personne.DevientInfect�(gameObject, virus);
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/Infection");
    }

    public void DevientGueri()
    {
        personne.DevientGueri();
        GetComponent<MeshRenderer>().material = personne.immunite.getMaterial();
    }


    public void FaireT�che()
    {
        t�cheEnCours = s�lecteur.ChoisirT�che();
        StartCoroutine(t�cheEnCours.FaireT�che());
    }
    public void FaireT�che(T�che t�che�Faire)
    {
        t�cheEnCours = t�che�Faire;
        StartCoroutine(t�che�Faire.FaireT�che());
    }

    public void Arr�t()
    {
        agent.enabled = false;
    }
    public void D�part()
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
    public void SetNomT�che(NomT�che nom)
    {
        nomT�che = nom;
    }
}
