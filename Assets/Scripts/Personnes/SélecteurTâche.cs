using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SélecteurTâche
{
    IAPersonne personne;
    Dictionary<Tâche, float> tâchesRoam;
    float poidsTotal;

    /// <summary>
    /// Crée un sélecteur de tâche de base pour être modifié ou directement utilisé
    /// </summary>
    /// <param name="personne"> L'IA de la personne </param>
    public SélecteurTâche(IAPersonne personne) {
        this.personne = personne;
        tâchesRoam = new Dictionary<Tâche, float>
        {
            { new AllerAuBureau(personne), 1 },
            { new Roam(personne), 1 }, 
            { new AllerÀDistributrice(personne), 1 },
            { new AllerÀPickUp(personne), 1}
        };
    }



    public Tâche ChoisirTâche()
    {
        return ChoisirRoam();
    }


    /// <summary>
    /// Choisis une tâche aléatoire parmi les tâche de roaming.
    /// </summary>
    /// <returns> La Tâche sélectionnée </returns>
    public Tâche ChoisirRoam()
    {
        CalculerPoidsTotal();
        Tâche tâcheFinale = null;
        for (int i = 0; i < 15; i++) {
            float valeurMax = Random.Range(0f, poidsTotal);
            float valeur = 0f;
            foreach (Tâche t in tâchesRoam.Keys) {
                valeur += tâchesRoam.GetValueOrDefault(t);
                if (valeur >= valeurMax) {
                    tâcheFinale = t;
                    break;
                }
            }
            if (tâcheFinale != null && tâcheFinale.VérifierSiFaisable()) {
                return tâcheFinale;
            }
        }
        return null; // Ne devrait pas arriver, le poids total sera tjr plus grand ou égal que le poids mesuré
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
