using UnityEngine;

public class Virus
{
    Transform personne;

    public float niveauMin {  get; private set; }
    public float force {  get; private set; }
    public  float decceleration {  get; private set; }
    public float gravité { get; private set; }
    public float duréeVie { get; private set; }

    public Virus(Transform personne, float force, float duréeVie, float niveauMin, float decceleration, float gravité)
    {
        this.personne = personne;
        this.force = force;
        this.duréeVie = duréeVie;
        this.niveauMin = niveauMin;
        this.decceleration = decceleration;
        this.gravité = gravité;
    }
}
