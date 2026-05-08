using UnityEngine;

public class Toux : Symptome
{

    public Toux() { }

    public Toux(Virus virus) : base(virus) { }



    public override void Initialiser(Virus virus)
    {
        this.virus = virus;
        intensiteSymptome = virus.force * 2f;
        cooldownMaximum = 80;
        RandomiserCooldownActuel();
    }

    public override void EffectuerSymptome()
    {
        var p = virus.personne;
        if (cooldownActuel < 0f)
        {
            RandomiserCooldownActuel();
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Particule");
            GameObject instance;

            var pos = p.transform.position + 0.6f * p.transform.forward;
            for (int i = 0; i < (int)intensiteSymptome; i++)
            {
                instance = GameObject.Instantiate(prefab, pos, p.transform.rotation);
                VirusParticule vir = instance.GetComponent<VirusParticule>();
                vir.CréationVolatile(p, virus);
            }
        }
        else
            cooldownActuel -= Time.deltaTime * intensiteSymptome;
    }

    public override Symptome Dupliquer()
    {
        return new Toux();
    }
}
