using UnityEngine;

public class Toux : Symptome
{
    GameObject personne;



    public Toux(GameObject personne)
    {
        this.personne = personne;
        intensitéSymptome = 10;
        cooldownMaximum = 80;
        cooldownActuel = Random.Range(intensitéSymptome, cooldownMaximum);
    }

    public override void EffectuerSymptome(Virus virus)
    {
        if (cooldownActuel < 0f)
        {
            cooldownActuel = Random.Range(intensitéSymptome, cooldownMaximum);
            GameObject prefab = Resources.Load<GameObject>("Prefabs/Particule");
            GameObject instance;

            var pos = personne.transform.position + 0.6f * personne.transform.forward;
            for (int i = 0; i < (int)intensitéSymptome; i++)
            {
                instance = GameObject.Instantiate(prefab, pos, personne.transform.rotation);
                VirusParticule vir = instance.GetComponent<VirusParticule>();
                vir.Création(personne, virus);
            }
        }
        else
            cooldownActuel -= Time.deltaTime * intensitéSymptome;
    }
}
