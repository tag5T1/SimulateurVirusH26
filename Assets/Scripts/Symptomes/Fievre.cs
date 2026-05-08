using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Android;
using UnityEngine.Rendering;

public class Fievre : Symptome
{
    NavMeshAgent agent;
    IAPersonne pers;
    float vitesseDeBase;

    public Fievre() { }
    public Fievre(Virus virus) : base(virus) { }

    public override void Initialiser(Virus virus)
    {
        this.virus = virus;
        pers = virus.personne.GetComponent<IAPersonne>();
        agent = pers.agent;
        vitesseDeBase = pers.vitesseDeDeplacementDeBase;
        intensiteSymptome = 1f - virus.force * 0.07f;
    }



    public override void EffectuerSymptome()
    {
        agent.speed = vitesseDeBase * intensiteSymptome;
    }

    public override Symptome Dupliquer()
    {
        return new Fievre();
    }
}
