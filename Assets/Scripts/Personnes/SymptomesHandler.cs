using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SymptomesHandler
{
    Virus virus;
    List<Symptome> symptomes;

    public SymptomesHandler(Virus virus, List<Symptome> symptomes)
    {
        this.virus = virus;
        this.symptomes = symptomes;
    }

    public void EffectuerSymptomes()
    {
        foreach (Symptome s in symptomes)
        {
            s.EffectuerSymptome(virus);
        }
    }
}
