using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataInfection : Data
{
    public Dictionary<string, string> données;

    public DataInfection()
    {
        données = new Dictionary<string, string>();
    }
    public void Add(bool infecté)
    {
        if (infecté)
        {
            données.Add("Infecté", "Oui");

        }
        else
            données.Add("Infecté", "Non");
    }
}
