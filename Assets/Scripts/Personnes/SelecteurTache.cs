using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SelecteurTache
{
    Dictionary<Tache, float> tachesRoam;
    float poidsTotal;

    /// <summary>
    /// Crée un sélecteur de tâche de base pour être modifié ou directement utilisé
    /// </summary>
    /// <param name="personne"> L'IA de la personne </param>
    public SelecteurTache(IAPersonne personne) {
        tachesRoam = new Dictionary<Tache, float>
        {
            { new AllerAuBureau(personne), 1 },
            { new Roam(personne), 1 }, 
            { new AllerADistributrice(personne), 1 },
            { new AllerAPickUp(personne), 1}
        };
    }



    public Tache ChoisirTache()
    {
        return ChoisirRoam();
    }


    /// <summary>
    /// Choisis une tâche aléatoire parmi les tâche de roaming.
    /// </summary>
    /// <returns> La Tâche sélectionnée </returns>
    public Tache ChoisirRoam()
    {
        CalculerPoidsTotal();
        Tache tâcheFinale = null;
        for (int i = 0; i < 15; i++) {
            float valeurMax = Random.Range(0f, poidsTotal);
            float valeur = 0f;
            foreach (Tache t in tachesRoam.Keys) {
                valeur += tachesRoam.GetValueOrDefault(t);
                if (valeur >= valeurMax) {
                    tâcheFinale = t;
                    break;
                }
            }
            if (tâcheFinale != null && tâcheFinale.VerifierSiFaisable()) {
                return tâcheFinale;
            }
        }
        return null; // Ne devrait pas arriver, le poids total sera tjr plus grand ou égal que le poids mesuré
    }


    public void CalculerPoidsTotal()
    {
        poidsTotal = 0f;
        foreach (Tache t in tachesRoam.Keys)
        {
            poidsTotal += tachesRoam.GetValueOrDefault(t);
        }
    }
}
