using System.Collections;
using UnityEngine;

public abstract class Tâche
{
    public StatusTâche status { get; protected set; }
    public Vector3 destination { get; protected set; }
    public Vector2 destination2D { get; protected set; }
    public IAPersonne personne { get; protected set; }

    public Tâche(IAPersonne personne)
    {
        this.personne = personne;
    }


    /// <summary>
    /// La tâche que l'IA peut faire
    /// </summary>
    /// <param name="personne"></param>
    /// <returns></returns>
    public abstract IEnumerator FaireTâche();

    public void UpdateDestination(Vector3 destination)
    {
        this.destination = destination;
        destination2D = new Vector2(destination.x, destination.z);
        personne.SetDestination(destination);
    }
}
