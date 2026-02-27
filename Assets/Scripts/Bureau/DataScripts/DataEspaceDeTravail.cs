using System.Collections.Generic;
using UnityEngine;

public class DataEspaceDeTravail : Data
{
    public Dictionary<string, string> données;

    public DataEspaceDeTravail() 
    { 
        données = new Dictionary<string, string>();
    }
    public void Add(EspaceDeTravail espaceDeTravail)
    {
        données.Add("Emplacement du bureau", $"{espaceDeTravail.bureau.transform.position.x}, {espaceDeTravail.bureau.transform.position.z}");
    }
}
