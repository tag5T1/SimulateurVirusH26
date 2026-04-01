using System.Collections;
using UnityEngine;

public class AllerÀDistributrice : Tâche
{
    public AllerÀDistributrice(IAPersonne personne) : base(personne) { }

    public override IEnumerator FaireTâche()
    {
        status = StatusTâche.EN_COURS;
        personne.SetNomTâche(NomTâche.DÉPLACEMENT);
        Distributrice distributrice = GameObject.Find("Manager").GetComponent<Manager>().GetDistributrice();
        var posInteraction = distributrice.positionInteraction;

        // Initialise le déplacement
        UpdateDestination(posInteraction + distributrice.distanceEntrePersonnesEnFile * distributrice.fileDattente.Count);

        // Se rend en file
        while (Vector2.Distance(destination2D, personne.position2D) >= 5)
        {
            UpdateDestination(posInteraction + distributrice.distanceEntrePersonnesEnFile * distributrice.fileDattente.Count);
            yield return new WaitForFixedUpdate();
        }
        distributrice.AttendreEnFile(personne);

        // Avance dans la file à la place la plus avancée
        while (distributrice.fileDattente.IndexOf(personne) != 0)
        {
            UpdateDestination(posInteraction + distributrice.distanceEntrePersonnesEnFile * distributrice.fileDattente.IndexOf(personne));
            yield return new WaitForFixedUpdate();
        }

        // Va à la distributrice quand premier dans la file
        UpdateDestination(posInteraction);

        // Temps d'utilisation
        yield return new WaitForSeconds(3);
        Virus virus = distributrice.Infecter(personne.personne.virus);
        if (virus != null)
            personne.DevientInfecté(virus);


        distributrice.QuitterFile(personne);
        status = StatusTâche.TERMINÉ;
        personne.SetNomTâche(NomTâche.IDLE);
    }

    public override bool VérifierSiFaisable() {
        return personne.manager.distributrices.Length > 0;
    }
}
