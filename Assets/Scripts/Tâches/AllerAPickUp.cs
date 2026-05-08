using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class AllerAPickUp : Tache
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="personne"></param>
    public AllerAPickUp(IAPersonne personne) : base(personne) { }



    public override IEnumerator FaireTache()
    {
        status = StatusTache.EN_COURS;
        personne.SetNomTache(NomTache.DEPLACEMENT);
        PickUpObjet objet = personne.manager.GetPickUpObjet();
        if (objet == null)
            status = StatusTache.TERMINE;

        if (objet.utilisé == true)
        {
            status = StatusTache.TERMINE;
            personne.SetNomTache(NomTache.IDLE);
        }
        else
        {
            personne.SetNomTache(NomTache.PICKUP);
            objet.utilisé = true;
            // Calcule la direction face à la distributrice pour se positionner en file
            var dir = (objet.positionInteraction - objet.transform.position).normalized;

            // La destination 
            UpdateDestination(objet.positionInteraction);
            yield return new WaitUntil(() => Vector3.Distance(personne.gameObject.transform.position, objet.positionInteraction) <= 2);
            

            // PickUp
            objet.Utiliser(personne);
            Virus virus = objet.Infecter(personne.personne.virus);
            if (virus != null)
                personne.DevientInfecte(virus);

            // Retour au bureau
            UpdateDestination(personne.personne.espaceDeTravail.bureau.transform.position);
            yield return new WaitUntil(() => Vector2.Distance(personne.gameObject.transform.position, personne.personne.espaceDeTravail.bureau.transform.position) <= 0.5);
            personne.SetNomTache(NomTache.DEPLACEMENT);

            // Lacher l'objet
            objet.Lacher();
            status = StatusTache.TERMINE;
            personne.SetNomTache(NomTache.IDLE);
        }
    }

    public override bool VerifierSiFaisable() {
            return personne.manager.VerifierPickupObjetAccessible();
    }
}
