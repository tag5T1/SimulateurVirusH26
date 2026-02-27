using System.Collections;
using UnityEngine;

public class AllerAuBureau : Tâche
{
    public AllerAuBureau(IAPersonne personne) : base(personne) { }

    public override IEnumerator FaireTâche()
    {
        status = StatusTâche.EN_COURS;
        personne.SetActionEnCours("AllerAuBureau");
        UpdateDestination(personne.personne.GetPositionBureau());
        yield return new WaitUntil(() => Vector2.Distance(destination2D, personne.position2D) <= 1);
        status = StatusTâche.TERMINÉ;
        personne.SetActionEnCours("AuBureau");
    }
}
