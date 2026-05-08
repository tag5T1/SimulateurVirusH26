using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Roam : Tache
{
    public Roam(IAPersonne personne) : base(personne) { }

    public override IEnumerator FaireTache()
    {
        status = StatusTache.EN_COURS;
        personne.SetNomTache(NomTache.ROAMING);
        UpdateDestination(new Vector3(Random.Range(-20, 20), -5, Random.Range(-20, 20)));
        yield return new WaitUntil(() => Vector2.Distance(destination2D, personne.position2D) <= 2);
        status = StatusTache.TERMINE;
        personne.SetNomTache(NomTache.IDLE);
    }

    public override bool VerifierSiFaisable() {
        return true;
    }
}
