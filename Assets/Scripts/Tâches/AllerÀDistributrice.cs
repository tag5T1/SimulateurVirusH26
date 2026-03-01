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
        Distributrice distributrice = GameObject.Find("Manager").GetComponent<Manager>().GetDistributrice();

        // La destination est à la dernière position de la file
        UpdateDestination(distributrice.positionInteraction + distributrice.distanceEnFile * (distributrice.fileDattente.Count + 1));

        // Se rend en file
        yield return new WaitUntil(() => Vector2.Distance(destination2D, personne.position2D) <= 4); // Distance pour empêcher qlqun de loin d'occuper une place avant d'être rendu
        distributrice.AttendreEnFile(personne);

        // Avance dans la file à la place la plus avancée
        while (distributrice.fileDattente.IndexOf(personne) != 0)
        {
            UpdateDestination(distributrice.positionInteraction + distributrice.distanceEnFile * (distributrice.fileDattente.IndexOf(personne)));
            yield return new WaitForEndOfFrame();
        }

        // Va à la distributrice quand premier dans la file
        UpdateDestination(distributrice.positionInteraction);

        // Temps d'utilisation
        yield return new WaitForSeconds(3);
        Virus virus = distributrice.Utiliser(personne.personne.virus);
        if (virus != null)
            personne.DevientInfecté(virus);


        distributrice.QuitterFile(personne);
        status = StatusTâche.TERMINÉ;
        personne.SetNomTâche(NomTâche.IDLE);
    }
}
