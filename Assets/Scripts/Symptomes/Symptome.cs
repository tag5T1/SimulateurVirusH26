using UnityEngine;

public abstract class Symptome
{
    protected Virus virus;

    /// <summary>
    /// Valeur qui dicte la force du symptome
    /// </summary>
    protected float intensitéSymptome;
    protected float cooldownMaximum;
    protected float cooldownActuel;

    public Symptome() { }

    public Symptome(Virus virus)
    {
        Initialiser(virus);
    }

    public abstract Symptome Dupliquer();

    public abstract void EffectuerSymptome();
    public abstract void Initialiser(Virus virus);
    protected void RandomiserCooldownActuel()
    {
        cooldownActuel = Random.Range(cooldownMaximum / 2, cooldownMaximum);
    }
}
