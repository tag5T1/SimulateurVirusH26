using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Android;
using UnityEngine.Rendering;

public class Fièvre : Symptome
{
    NavMeshAgent agent;
    IAPersonne pers;
    float vitesseDeBase;

    public Fièvre() { }
    public Fièvre(Virus virus) : base(virus) { }

    public override void Initialiser(Virus virus)
    {
        this.virus = virus;
        pers = virus.personne.GetComponent<IAPersonne>();
        agent = pers.agent;
        vitesseDeBase = pers.vitesseDeDéplacementDeBase;
        intensitéSymptome = 1f - virus.force * 0.07f;
    }



    public override void EffectuerSymptome()
    {
        agent.speed = vitesseDeBase * intensitéSymptome;
    }

    public override Symptome Dupliquer()
    {
        return new Fièvre();
    }
}
