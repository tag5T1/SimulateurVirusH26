using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class AllerÀPickUp : Tâche
{
    public AllerÀPickUp(IAPersonne personne) : base(personne) { }



    public override IEnumerator FaireTâche()
    {
        status = StatusTâche.EN_COURS;
        personne.SetNomTâche(NomTâche.DÉPLACEMENT);
        PickUpObjet puo = personne.manager.GetPickUpObjet();
        if (puo == null)
            status = StatusTâche.TERMINÉ;

        if (puo.utilisé == true)
        {
            status = StatusTâche.TERMINÉ;
            personne.SetNomTâche(NomTâche.IDLE);
        }
        else
        {
            personne.SetNomTâche(NomTâche.PICKUP);
            puo.utilisé = true;
            // Calcule la direction face à la distributrice pour se positionner en file
            var dir = (puo.positionInteraction - puo.transform.position).normalized;
            Debug.Log("1");

            // La destination 
            UpdateDestination(puo.positionInteraction);
            yield return new WaitUntil(() => Vector2.Distance(personne.gameObject.transform.position, puo.positionInteraction) <= 2);
            Debug.Log("2");

            // PickUp
            puo.Utiliser(personne);
            Virus virus = puo.Infecter(personne.personne.virus);
            if (virus != null)
                personne.DevientInfecté(virus);
            Debug.Log("3");

            // Retour au bureau
            UpdateDestination(personne.personne.espaceDeTravail.bureau.transform.position);
            Debug.Log("4");
            yield return new WaitUntil(() => Vector2.Distance(personne.gameObject.transform.position, personne.personne.espaceDeTravail.bureau.transform.position) <= 0.5);
            Debug.Log("5");
            personne.SetNomTâche(NomTâche.DÉPLACEMENT);

            // Lacher l'objet
            puo.Lacher(personne);
            status = StatusTâche.TERMINÉ;
            personne.SetNomTâche(NomTâche.IDLE);
        }
    }

    public override bool VérifierSiFaisable() {
            return personne.manager.PickupObjetAccessible();
    }
}
