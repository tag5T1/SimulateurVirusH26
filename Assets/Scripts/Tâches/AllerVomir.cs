using System.Collections;
using UnityEngine;

public class AllerVomir : Tache
{
    Vomissements vomissements;

    public AllerVomir(IAPersonne personne, Vomissements vomissements) : base(personne) 
    { 
        this.vomissements = vomissements;
    }

    public override IEnumerator FaireTache()
    {
        status = StatusTache.EN_COURS;
        personne.SetNomTache(NomTache.DEPLACEMENT);
        GameObject poubelle = Manager.Instance.GetPoubelleLaPlusProche(personne.transform.position);
        var posInteraction = poubelle.transform.position;

        // Initialise le déplacement
        UpdateDestination(posInteraction);

        // Se rend à la poubelle
        yield return new WaitUntil(() => Vector2.Distance(personne.position2D, destination2D) < 1.5f);

        // Vomit
        vomissements.estEnVomissement = true;
        personne.Arret();
        float temps = vomissements.GetDurée();
        while (temps > 0)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                personne.transform.LookAt(posInteraction);
                vomissements.Vomir();
            }

            yield return new WaitForSeconds(0.25f);
            temps -= 0.25f;
        }

        vomissements.estEnVomissement = false;
        personne.Depart();

        yield return new WaitForSeconds(2f);

        status = StatusTache.TERMINE;
        personne.SetNomTache(NomTache.IDLE);
    }

    public override bool VerifierSiFaisable() {
        return personne.manager.VerifierSiPoubelleAccessible();
    }
}
