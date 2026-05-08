using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DataImmunite : Data
{
    public Dictionary<string, string> données;
    public DataImmunite()
    {
        données = new Dictionary<string, string>();
    }

    public void Add(Immunite immunite)
    {
        données.Add("Pourcentage d'immunité", $"{immunite.pourcentageImmunite}");
        if (immunite.immune) données.Add("Immunisé au virus", "Oui");
        else données.Add("Immunisé au virus", "Non");
    }
}
