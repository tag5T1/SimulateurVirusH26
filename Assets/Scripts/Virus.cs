using UnityEngine;

public class Virus
{
    Transform personne;

    public float niveauMin {  get; private set; }
    public float force {  get; private set; }
    public  float decceleration {  get; private set; }
    public float gravité { get; private set; }
    public float duréeVie { get; private set; }
    public float puissanceMutation { get; private set; } // % possible de changement
    public float maxSpread = 20;

    // Copie
    public Virus(Virus vir)
    {
        this.personne = vir.personne;
        this.force = vir.force;
        this.duréeVie = vir.duréeVie;
        this.niveauMin = vir.niveauMin;
        this.decceleration = vir.decceleration;
        this.gravité = vir.gravité;
    }

    public Virus(Transform personne, float force, float duréeVie, float niveauMin, float decceleration, float gravité, int puissanceMutation)
    {
        this.personne = personne;
        this.force = force;
        this.duréeVie = duréeVie;
        this.niveauMin = niveauMin;
        this.decceleration = decceleration;
        this.gravité = gravité;
        this.puissanceMutation = puissanceMutation;
    }

    public Virus Muter()
    {
        // Copie le virus
        Virus virusMuté = new Virus(this);
        int scale = 1000;
        float min = scale - (puissanceMutation/100 * scale);
        float max = scale + (puissanceMutation/100 * scale);
        // Modifie légèrement les paramètres
        niveauMin *= Random.Range(min, max) / scale;
        force *= Random.Range(min, max) / scale;
        decceleration *= Random.Range(min, max) / scale;
        gravité *= Random.Range(min, max) / scale;
        duréeVie *= Random.Range(min, max) / scale;
        puissanceMutation *= Random.Range(900, 1100) / 1000; // 10% de mutation de base sur la mutation
        return virusMuté;
    }
}
