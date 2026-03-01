using UnityEngine;

public abstract class Symptome
{
    protected float intensitéSymptome;
    protected float cooldownMaximum;
    protected float cooldownActuel;

    public abstract void EffectuerSymptome(Virus virus);
}
