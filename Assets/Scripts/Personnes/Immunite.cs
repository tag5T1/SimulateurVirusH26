using System.Collections.Generic;
using UnityEngine;

public class Immunite
{
    public GameObject personne { get; private set; }
    public float pourcentageImmunite;
    public Material materialGueri;
    public Material materialImmune;
    public bool immune;

    /// <summary>
    /// Constructeur à paramètres aléatoires aléatoire
    /// </summary>
    /// <param name="personne"> Le Transform de la personne qui possède le Virus </param>
    public Immunite(GameObject personne)
    {
        this.personne = personne;
        pourcentageImmunite = 0;
        materialGueri = Resources.Load<Material>("Materials/Cured");
        materialImmune = Resources.Load<Material>("Materials/Immune");
        immune = false;
    }

    public void GainImmunite()
    {
        pourcentageImmunite += Random.Range(0, 1 - (int)pourcentageImmunite)/100;
        if(pourcentageImmunite == 1)
        {
            immune = true;
        }
    }

    public Material getMaterial()
    {
        if(immune)
        {
            return materialImmune;
        }
        else { return materialGueri; }
    }

}
