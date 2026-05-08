using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DataImmunite : Data
{
    public Dictionary<string, string> donnees;
    public DataImmunite()
    {
        donnees = new Dictionary<string, string>();
    }

    public void Add(Immunite immunite)
    {
        donnees.Add("Pourcentage d'immunité", $"{immunite.pourcentageImmunite}");
        if (immunite.immune) donnees.Add("Immunisé au virus", "Oui");
        else donnees.Add("Immunisé au virus", "Non");
    }
}
