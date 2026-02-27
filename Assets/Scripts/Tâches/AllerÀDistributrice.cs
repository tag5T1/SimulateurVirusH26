using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AllerÀDistributrice : Tâche
{
    public AllerÀDistributrice(IAPersonne personne) : base(personne) { }

    public override IEnumerator FaireTâche()
    {
        status = StatusTâche.EN_COURS;
        personne.SetNomTâche(NomTâche.DÉPLACEMENT);
        Distributrice distributrice = personne.manager.GetDistributrice();
        // Calcule la direction face à la distributrice pour se positionner en file
        var dir = (distributrice.positionInteraction - distributrice.transform.position).normalized;
        // La destination est à la desrnière position de la file
        UpdateDestination(distributrice.positionInteraction + dir * 2 * (distributrice.fileDattente.Count + 1));

        // Se rend en file
        yield return new WaitUntil(() => Vector2.Distance(destination2D, personne.position2D) <= 4); // Distance pour empêcher qlqun de loin d'occuper une place avant d'être rendu
        distributrice.AttendreEnFile(personne);

        // Avance dans la file quand une place se libère
        while (distributrice.fileDattente.IndexOf(personne) != 0)
        {
            UpdateDestination(distributrice.positionInteraction + dir * 2 * (distributrice.fileDattente.IndexOf(personne)));
            yield return new WaitForEndOfFrame();
        }
        // Va à la distributrice quand premier dans la file
        UpdateDestination(distributrice.positionInteraction);
        // Temps d'utilisation
        yield return new WaitForSeconds(2);
        Virus virus = distributrice.Utiliser(personne.personne.virus);
        if (virus != null)
            personne.Infecter(virus);
        distributrice.QuitterFile();
        status = StatusTâche.TERMINÉ;
        personne.SetNomTâche(NomTâche.IDLE);
    }
}
