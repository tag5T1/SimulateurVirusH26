using UnityEngine;

public class Vomissements : Symptome
{
    IAPersonne pers;
    Tâche tâcheVomir;
    public bool estEnVomissement;

    public Vomissements() { }
    public Vomissements(Virus virus) : base(virus) { }

    public override void Initialiser(Virus virus)
    {
        this.virus = virus;
        pers = virus.personne.GetComponent<IAPersonne>();
        tâcheVomir = new AllerVomir(pers, this);
        intensitéSymptome = (virus.force + 4) / 3; // Temps de vomissement
        cooldownMaximum = 300;
        RandomiserCooldownActuel();
        cooldownActuel = 20;
    }



    public override void EffectuerSymptome()
    {
        var p = virus.personne;
        if (cooldownActuel < 0f)
        {
            pers.FaireTâche(tâcheVomir);
            RandomiserCooldownActuel();
        }
        else if (!estEnVomissement)
            cooldownActuel -= Time.deltaTime * intensitéSymptome;
    }

    public void Vomir()
    {
        Debug.Log("BLEUUUUAHH");
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Particule");
        GameObject instance;

        var pos = pers.transform.position + 0.55f * pers.transform.forward;
        for (int i = 0; i < 30; i++)
        {
            instance = GameObject.Instantiate(prefab, pos, pers.transform.rotation);
            VirusParticule vir = instance.GetComponent<VirusParticule>();
            vir.CréationSolide(pers.gameObject, virus);
        }
    }

    public float GetDurée()
    {
        return intensitéSymptome;
    }

    public override Symptome Dupliquer()
    {
        return new Vomissements();
    }
}
