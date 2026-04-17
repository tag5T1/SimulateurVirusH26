using System.Collections;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class AllerÀPickUp : Tâche
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="personne"></param>
    public AllerÀPickUp(IAPersonne personne) : base(personne) { }
    public override IEnumerator FaireTâche()
    {

        status = StatusTâche.EN_COURS;
        personne.SetNomTâche(NomTâche.DÉPLACEMENT);
        PickUpObjet puo = GameObject.Find("Manager").GetComponent<Manager>().GetPickUpObjet();

        if (puo.utilisé == true)
        {
            status = StatusTâche.TERMINÉ;
            personne.SetNomTâche(NomTâche.IDLE);
        }
        else
        {
            puo.utilisé = true;
            // Calcule la direction face à la distributrice pour se positionner en file
            var dir = (puo.positionInteraction - puo.transform.position).normalized;

            // La destination 
            UpdateDestination(puo.positionInteraction);
            personne.SetNomTâche(NomTâche.PICKUP);
            yield return new WaitUntil(() => Vector2.Distance(personne.gameObject.transform.position, puo.positionInteraction) <= 1);

            // PickUp
            puo.Utiliser(personne);
            Virus virus = puo.Infecter(personne.personne.virus);

            // Retour au bureau
            UpdateDestination(personne.personne.espaceDeTravail.bureau.transform.position);
            yield return new WaitUntil(() => Vector2.Distance(personne.gameObject.transform.position, personne.personne.espaceDeTravail.bureau.transform.position) <= 0.5);
            personne.SetNomTâche(NomTâche.DÉPLACEMENT);

            // Lacher l'objet
            puo.Lacher(personne);
            status = StatusTâche.TERMINÉ;
            personne.SetNomTâche(NomTâche.IDLE);
        }
    }
}
