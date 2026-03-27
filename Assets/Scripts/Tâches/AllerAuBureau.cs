using System.Collections;
using UnityEngine;

public class AllerAuBureau : Tâche
{
    public AllerAuBureau(IAPersonne personne) : base(personne) { }

    public override IEnumerator FaireTâche()
    {
        status = StatusTâche.EN_COURS;
        personne.SetNomTâche(NomTâche.DÉPLACEMENT);
        UpdateDestination(personne.personne.GetPositionBureau());
        yield return new WaitUntil(() => Vector2.Distance(destination2D, personne.position2D) <= 2);
        personne.SetNomTâche(NomTâche.AU_BUREAU);

        yield return new WaitForSeconds(3);
        status = StatusTâche.TERMINÉ;
    }
}
