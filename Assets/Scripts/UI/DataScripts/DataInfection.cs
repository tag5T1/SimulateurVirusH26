using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataInfection : Data
{
    public Dictionary<string, string> donnees;

    public DataInfection()
    {
        donnees = new Dictionary<string, string>();
    }
    public void Add(bool infecté)
    {
        if (infecté)
        {
            donnees.Add("Infecté", "Oui");

        }
        else
            donnees.Add("Infecté", "Non");
    }
}
