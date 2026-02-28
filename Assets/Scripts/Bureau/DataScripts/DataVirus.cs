using System.Collections.Generic;
using UnityEngine;

public class DataVirus : Data
{
    public Dictionary<string, string> données;

    public DataVirus()
    {
        données = new Dictionary<string, string>();
    }
    public void Add(Virus virus)
    {
        données.Add("Type de virus", virus.nom);
        données.Add("Niveau minimum d'infection", $"{virus.niveauMin}");
        données.Add("Force de propulsion des particules", $"{virus.force}");
        données.Add("Déccelération de la particule", $"{virus.décceleration}");
        données.Add("Durée de vie d'une particule", $"{virus.duréeVie}");
        données.Add("Gravité appliqué sur la particule", $"{virus.gravité}");
        données.Add("Puissance de mutation", $"{virus.puissanceMutation}");
        données.Add("Rayon de dispersion de particules", $"{virus.maxSpread}");
    }
}
