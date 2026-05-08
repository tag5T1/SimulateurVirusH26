using System.Collections;
using UnityEngine;

public abstract class Tache
{
    public StatusTache status { get; protected set; }
    public Vector3 destination { get; protected set; }
    public Vector2 destination2D { get; protected set; }
    public IAPersonne personne { get; protected set; }

    public Tache(IAPersonne personne)
    {
        this.personne = personne;
    }


    /// <summary>
    /// La tâche que l'IA peut faire
    /// </summary>
    public abstract IEnumerator FaireTache();
    public abstract bool VerifierSiFaisable();

    public void UpdateDestination(Vector3 destination)
    {
        this.destination = destination;
        destination2D = new Vector2(destination.x, destination.z);
        personne.SetDestination(destination);
        personne.UpdatePosition2D();
    }
}
