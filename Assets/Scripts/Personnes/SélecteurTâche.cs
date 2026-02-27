using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SélecteurTâche
{
    IAPersonne personne;
    Dictionary<Tâche, float> tâchesRoam;
    float poidsTotal;

    public SélecteurTâche(IAPersonne personne) {
        this.personne = personne;
        tâchesRoam = new Dictionary<Tâche, float>
        {
            { new Roam(personne), 1 }, 
            { new AllerÀDistributrice(personne), 20 }
        };
    }



    public Tâche ChoisirTâche()
    {
        if (personne.nomTâche == NomTâche.AU_BUREAU)
        {
            return ChoisirRoam();
        }
        else
        {
            return new AllerAuBureau(personne);
        }
    }


    /// <summary>
    /// Choisis une tâche aléatoire parmi les tâche de roaming.
    /// </summary>
    /// <returns> La Tâche sélectionnée </returns>
    public Tâche ChoisirRoam()
    {
        CalculerPoidsTotal();
        float valeurMax = Random.Range(0f, poidsTotal);
        float valeur = 0f;
        foreach (Tâche t in tâchesRoam.Keys)
        {
            valeur += tâchesRoam.GetValueOrDefault(t);
            if (valeur > valeurMax)
                return t;
        }
        return null; // Ne devrait pas arriver, le poids total sera tjr plus grand que le poids mesurer
    }


    public void CalculerPoidsTotal()
    {
        poidsTotal = 0f;
        foreach (Tâche t in tâchesRoam.Keys)
        {
            poidsTotal += tâchesRoam.GetValueOrDefault(t);
        }
    }
}
