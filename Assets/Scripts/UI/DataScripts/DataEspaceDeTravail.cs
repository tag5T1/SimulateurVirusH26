using System.Collections.Generic;
using UnityEngine;

public class DataEspaceDeTravail : Data
{
    public Dictionary<string, string> donnees;

    public DataEspaceDeTravail() 
    { 
        donnees = new Dictionary<string, string>();
    }



    public void Add(EspaceDeTravail espaceDeTravail)
    {
        donnees.Add("Emplacement du bureau", $"{espaceDeTravail.bureau.transform.position.x}, {espaceDeTravail.bureau.transform.position.z}");
    }
}
