using System.Collections;
using UnityEngine;

public class AllerAuBureau : Tache
{
    public AllerAuBureau(IAPersonne personne) : base(personne) { }

    public override IEnumerator FaireTache()
    {
        status = StatusTache.EN_COURS;
        personne.SetNomTache(NomTache.DEPLACEMENT);
        UpdateDestination(personne.personne.GetPositionBureau());
        yield return new WaitUntil(() => Vector2.Distance(destination2D, personne.position2D) <= 2);
        personne.SetNomTache(NomTache.AU_BUREAU);

        yield return new WaitForSeconds(3);
        status = StatusTache.TERMINE;
    }

    public override bool VerifierSiFaisable() {
        return true;
    }
}
