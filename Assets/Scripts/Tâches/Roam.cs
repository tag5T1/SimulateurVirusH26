using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Roam : Tâche
{
    public Roam(IAPersonne personne) : base(personne) { }

    public override IEnumerator FaireTâche()
    {
        status = StatusTâche.EN_COURS;
        personne.SetNomTâche(NomTâche.ROAMING);
        UpdateDestination(new Vector3(Random.Range(-20, 20), -5, Random.Range(-20, 20)));
        yield return new WaitUntil(() => Vector2.Distance(destination2D, personne.position2D) <= 2);
        status = StatusTâche.TERMINÉ;
        personne.SetNomTâche(NomTâche.IDLE);
    }
}
