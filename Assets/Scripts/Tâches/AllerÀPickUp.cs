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
        PickUpObjet objet = personne.manager.GetPickUpObjet();
        if (objet == null)
            status = StatusTâche.TERMINÉ;

        if (objet.utilisé == true)
        {
            status = StatusTâche.TERMINÉ;
            personne.SetNomTâche(NomTâche.IDLE);
        }
        else
        {
            personne.SetNomTâche(NomTâche.PICKUP);
            objet.utilisé = true;
            // Calcule la direction face à la distributrice pour se positionner en file
            var dir = (objet.positionInteraction - objet.transform.position).normalized;

            // La destination 
            UpdateDestination(objet.positionInteraction);
            yield return new WaitUntil(() => Vector3.Distance(personne.gameObject.transform.position, objet.positionInteraction) <= 2);
            Debug.Log(objet.positionInteraction);
            Debug.Log(personne.gameObject.transform.position);
            Debug.Log(personne.gameObject.transform.position);

            // PickUp
            objet.Utiliser(personne);
            Virus virus = objet.Infecter(personne.personne.virus);
            if (virus != null)
                personne.DevientInfecté(virus);

            // Retour au bureau
            UpdateDestination(personne.personne.espaceDeTravail.bureau.transform.position);
            yield return new WaitUntil(() => Vector2.Distance(personne.gameObject.transform.position, personne.personne.espaceDeTravail.bureau.transform.position) <= 0.5);
            personne.SetNomTâche(NomTâche.DÉPLACEMENT);

            // Lacher l'objet
            objet.Lacher();
            status = StatusTâche.TERMINÉ;
            personne.SetNomTâche(NomTâche.IDLE);
        }
    }

    public override bool VérifierSiFaisable() {
            return personne.manager.VérifierPickupObjetAccessible();
    }
}
