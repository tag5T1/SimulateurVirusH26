using System.Collections.Generic;
using UnityEngine;

public class DataVirus : Data
{
    public Dictionary<string, string> donnees;

    public DataVirus()
    {
        donnees = new Dictionary<string, string>();
    }
    public void Add(Virus virus)
    {
        donnees.Add("Type de virus", virus.nom);
        donnees.Add("Force de propulsion des particules", $"{virus.force}");
        donnees.Add("Déccelération de la particule", $"{virus.decceleration}");
        donnees.Add("Durée de vie du virus", $"{virus.dureeVie}");
        donnees.Add("Durée de vie d'une particule", $"{virus.dureeVie/2}");
        donnees.Add("Gravité appliqué sur la particule", $"{virus.gravite}");
        donnees.Add("Puissance de mutation", $"{virus.puissanceMutation}");
        donnees.Add("Rayon de dispersion de particules", $"{virus.maxSpread}");
    }
}
